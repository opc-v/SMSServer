using SMSServer.ViewModels;
using SMSServer.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Windows.Input;
using SMSServer.IP;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using SMSServer.Models;

namespace SMSServer
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public Dictionary<string, Type> Routes { get; private set; } = new Dictionary<string, Type>();
        public ICommand StartStopCommand => new Command(StartStopAsync);

        private async void StartStopAsync()
        {
            Xamarin.Forms.MenuItem menu = (Xamarin.Forms.MenuItem)this.FindByName("StartStopMenu");
            menu.IsEnabled = false;

            if (menu.Text.StartsWith("Start"))
            {
                if(await StartSMSServerAsync())
                {
                    menu.Text = "Stop SMS Server";
                    menu.IconImageSource = "StopServer.png";
                }
            }
            else
            {
                if (await StopSMSServerAsync())
                {
                    menu.Text = "Start SMS Server";
                    menu.IconImageSource = "StartServer.png";
                }
            }

            await System.Threading.Tasks.Task.Delay(100);
            menu.IsEnabled = true;
        }

        public AppShell()
        {
            InitializeComponent();
            RegisterRoutes();
            BindingContext = this;
        }

        void RegisterRoutes()
        {
            this.FindByName("");

            Routes.Add("outgoingsmsdetails", typeof(OutgoingSMSDetailPage));

            foreach (var item in Routes)
            {
                Routing.RegisterRoute(item.Key, item.Value);
            }
        }

        SocketListener _SMSServerSocket;
        private async Task<bool> StartSMSServerAsync()
        {
            if (_SMSServerSocket != null) await StopSMSServerAsync();
            try
            {
                await Task.Run(async () => {
                    _SMSServerSocket = new SocketListener(10);
                    _SMSServerSocket.ConnectionEvent += App.SMSVM.ManageClientConnections;
                    _SMSServerSocket.Start(9111);
                    App.SMSManager.RegisterReceiver();
                    App.SMSVM.StartSMSProcessing();
                    App.Logger.Info("SMS server started.");
                });
                return true;
            }
            catch (Exception ex)
            {
                _SMSServerSocket = null;
                App.Logger.Exception(ex, "SMSServer not started");
                return false;
            }
        }
        private async Task<bool> StopSMSServerAsync()
        {
            if (_SMSServerSocket == null) return true;
            try
            {
                await Task.Run(() => {
                    App.SMSVM.StopSMSProcessing();
                    App.SMSManager.UnregisterReceiver();
                    _SMSServerSocket.ConnectionEvent -= App.SMSVM.ManageClientConnections;
                    _SMSServerSocket.Stop();
                    _SMSServerSocket = null;
                    App.Logger.Info("SMS server stopped.");
                });
                return true;
            }
            catch (Exception ex)
            {
                _SMSServerSocket = null;
                App.Logger.Exception(ex, "Exception while SMSServer stopping");
                return false;
            }
        }
    }
}
