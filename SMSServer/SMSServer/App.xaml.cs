using SMSServer.Services;
using SMSServer.ViewModels;
using SMSServer.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SMSServer
{
    public partial class App : Application
    {
        public static SMSViewModel SMSVM;

        public App()
        {
            InitializeComponent();

            Logger = DependencyService.Get<Interfaces.ILogService>();
            Logger.LogTag = "Tag4SMSServer";
            SMSVM = new SMSViewModel();
            SMSManager = DependencyService.Get<Interfaces.ISMSService>();
            MainPage = new AppShell();
        }
        public static Interfaces.ILogService Logger;
        public static Interfaces.ISMSService SMSManager;

        protected override void OnStart()
        {
            base.OnStart();
            Logger.Info(string.Format("App OnStart at {0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            base.OnSleep();
            Logger.Info(string.Format("App OnSleep at {0}", DateTime.Now.ToString("dd HH:mm:ss.fff")));
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            base.OnResume();
            Logger.Info(string.Format("App OnResume at {0}", DateTime.Now.ToString("dd HH:mm:ss.fff")));
            // Handle when your app resumes
        }

    }
}
