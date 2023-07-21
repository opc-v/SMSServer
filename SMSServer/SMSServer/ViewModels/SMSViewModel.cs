using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCO = System.Threading.Tasks.TaskCreationOptions;
using SMSExchange;
using System.IO;
using System.Net.Sockets;
using System.Globalization;
using System.Threading;
using SMSServer.IP;
using SMSServer.Models;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using SMSServer.Interfaces;
using System.Data.Common;

namespace SMSServer.ViewModels
{
    public class SMSViewModel : BaseViewModel
    {
        private bool _IsSMSShouldBeProcessed = true;
        public ObservableCollection<OutgoingSMS> Items { get; }
        public ObservableCollection<ClientConnection> Connections { get; }

        private DateTime? _SessionStartedDT;
        public DateTime? SessionStartedDT {
            get => _SessionStartedDT;
            private set => SetProperty(ref _SessionStartedDT, value);
        }
        private DateTime? _SessionStopedDT;
        public DateTime? SessionStopedDT {
            get => _SessionStopedDT;
            set => SetProperty(ref _SessionStopedDT, value);
        }
        private Int32 _SMSSentCount;
        public Int32 SMSSentCount {
            get => _SMSSentCount;
            set => SetProperty(ref _SMSSentCount, value);
        }
        private CancellationTokenSource _TaskCancellation;

        public SMSViewModel()
        {
            Title = "SMS server logic";
            Items = new ObservableCollection<OutgoingSMS>();
            Connections = new ObservableCollection<ClientConnection>();
        }
        public void AddOutgoingSMS(OutgoingSMS sT)
        {
            if (sT.OriginalDT==DateTime.MinValue) sT.OriginalDT = DateTime.Now;
            lock (Items)
            {
                sT.Id = Items.Count + 1;
                Items.Add(sT);
            }
            SMSSentCount = sT.Id;

        }

        internal void ProcessSocketMessageData(ReadOnlySpan<byte> rosT, Token token)
        {
            if (!_IsSMSShouldBeProcessed) return;
            TransportUnit tuT = TransportUnit.DeSerialize(rosT);
            tuT.IpEndpoint = token.RemoteEndPoint.ToString();
            App.Logger.Debug("Server ProcessSMSJsonData");

            CancellationToken cToken = _TaskCancellation.Token;
            var tcT = new IncomingTaskContainer() { CnclltnTk = cToken, TU = tuT };
            TaskFactory factory = new TaskFactory(cToken);

            factory.StartNew(ProcessSMSPacketAsync, tcT);
        }

        private async Task ProcessSMSPacketAsync(object oT)
        {
            App.Logger.Debug("Server ProcessSMSPacketAsync");
            if (!_IsSMSShouldBeProcessed) return;
            IncomingTaskContainer taskC = (IncomingTaskContainer)oT;
            if (taskC.CnclltnTk.IsCancellationRequested) return;

            try
            {
                if (taskC.TU.SMSList != null)
                {
                    if (taskC.TU.SMSList.Count > 0)
                    {
                        SMS4Json smsT = taskC.TU.SMSList[0];
                        lock (Connections)
                        {
                            ClientConnection ccT = Connections.FirstOrDefault<ClientConnection>(x => x.IpEndpoint == taskC.TU.IpEndpoint);
                            if (ccT == null)
                            {
                                ccT = new ClientConnection() { IpEndpoint = taskC.TU.IpEndpoint, ClientCode = smsT.ClientCode};
                                Connections.Add(ccT);
                            }
                            else
                            {
                                if ( string.IsNullOrEmpty( ccT.ClientCode)) ccT.ClientCode = smsT.ClientCode;
                            }
                        }
                    }
                    foreach (SMS4Json smsSrc in taskC.TU.SMSList)
                    {
                        if (!_IsSMSShouldBeProcessed) return ;
                        if (taskC.CnclltnTk.IsCancellationRequested) return;
                        OutgoingSMS newSMS = new OutgoingSMS(smsSrc);
                        newSMS.IpEndpoint = taskC.TU.IpEndpoint;
                        AddOutgoingSMS(newSMS);
                        App.SMSManager.SendSMS(newSMS.Id, newSMS.PhoneNumber, newSMS.Text);
                    }
                }
            }
            catch (Exception exe)
            {
                App.Logger.Exception(exe, "Exception at ProcessSMSPacketAsync");
            }
            //!!!!!!!! if (taskC.RT.ResponseList.Count==0) return;
        }

        internal class IncomingTaskContainer
        {
            internal TransportUnit TU { get; set; }
            internal CancellationToken CnclltnTk { get; set; }
            internal ResponseTransport RT { get; set; }
            internal IncomingTaskContainer()
            {
                RT = new ResponseTransport();
            }
        }

        internal void StopSMSProcessing()
        {
            _IsSMSShouldBeProcessed = false;
            _TaskCancellation.Cancel();
            _TaskCancellation.Dispose();
            _TaskCancellation=null;
            lock (Connections) { Connections.Clear();  }
            SessionStopedDT = DateTime.Now;
        }
  
        internal void StartSMSProcessing()
        {
            this.Items.Clear();
            _TaskCancellation=new CancellationTokenSource();
            SessionStopedDT = null;
            SessionStartedDT = DateTime.Now;
            SMSSentCount = 0;
            _IsSMSShouldBeProcessed = true;
        }
        internal void ManageClientConnections(object sender, IP.ConnectionEventArgs e)
        {
            if (!_IsSMSShouldBeProcessed) return;
            lock (Connections)
            {
                ClientConnection ccT = Connections.FirstOrDefault<ClientConnection>(x => x.IpEndpoint == e.ConnectedToken.RemoteEndPoint.ToString());
                if (e.IsConnected)
                {
                    if (ccT != null) return;
                    ccT = new ClientConnection() { IpEndpoint = e.ConnectedToken.RemoteEndPoint.ToString(), SMSToken = e.ConnectedToken };
                    Connections.Add(ccT);
                }
                else
                {
                    if (ccT != null) Connections.Remove(ccT);
                }
            }
            return;
        }

        internal void SendMessageToClient(OutgoingSMS source, ResponseKind kind, string strResult )
        {
            ClientConnection ccT = null;
            Token token = null;
            lock (Connections)
            {
                ccT = Connections.FirstOrDefault<ClientConnection>(x => x.IpEndpoint == source.IpEndpoint);
                if (ccT == null)
                {
                    string strT = string.Format("Token not found for {0}, client {1} .", source.IpEndpoint, source.ClientCode);
                    App.Logger.Error(strT);
                    return;
                }
                token = ccT.SMSToken;
            }
            if (token == null) { return; }

            string strMessage = null;
            switch (kind)
            {
                case ResponseKind.Sent:
                    ccT.SMSSentQntty++;
                    strMessage = String.Format(CultureInfo.CurrentCulture, "SMS with Id {0} is sent at {1} with result {2}.", source.OriginalId, source.SentDT, strResult);
                    break;
                case ResponseKind.Delivered:
                    strMessage = String.Format(CultureInfo.CurrentCulture, "SMS with Id {0} is delivered at {1} with result {2}.", source.OriginalId, source.DeliveredDT, strResult);
                    break;
                default:
                    return;
            }
            Response resp = new Response() { Kind = 1, Text = strMessage, SMSOriginalId = source.OriginalId };
            ResponseTransport rT = new ResponseTransport();
            rT.AddResponse(resp);
            byte[] bytes = rT.GetJsonBytes();
            token.SendResponse(ref bytes);
        }
        public  void SMSResponseProcessor(int intId, SMSServer.Models.ResponseKind responseKind, string strError)
        {
            if (!_IsSMSShouldBeProcessed) return;

            try
            {
                OutgoingSMS proccesed = Items.FirstOrDefault(x => x.Id == intId);
                if (proccesed == null) return;

                if (responseKind == ResponseKind.Sent) proccesed.SentDT = DateTime.Now;
                else if (responseKind == ResponseKind.Delivered) proccesed.DeliveredDT = DateTime.Now;

                SendMessageToClient(proccesed, responseKind, (string.IsNullOrEmpty(strError) ? "OK" : strError));
            }
            catch (Exception exe)
            {
                App.Logger.Exception(exe, "Exception at SMSResponseProcessor");
            }
        }
    }
}
