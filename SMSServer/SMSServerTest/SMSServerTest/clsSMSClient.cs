using SMSExchange;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using System.ComponentModel;
using System.Diagnostics;

namespace SMSServerTest
{
    public class SMSClient
    {
        private string _ClientCode = null;
        public SMSClient(string clientCode)
        {
            _ClientCode = clientCode;
        }

        public BindingList<SMS4Json> OutgoingSMS = new BindingList<SMS4Json>();
        public BindingList<Response> ResponseList = new BindingList<Response>();

        private SocketClient _SocketClient;

        internal EndPoint LocalEndpoint
        {
            get { return  _SocketClient.LocalEndpoint ; }
        }
        public bool Connect(String hostAddress, Int32 port)
        {
            string[] charsAdr = hostAddress.Split('.');
            byte[] bytAdr = new byte[4];
            bytAdr[0] = System.Convert.ToByte( charsAdr[0]);
            bytAdr[1] = System.Convert.ToByte(charsAdr[1]);
            bytAdr[2] = System.Convert.ToByte(charsAdr[2]);
            bytAdr[3] = System.Convert.ToByte(charsAdr[3]);
            // Addres of the host.
            IPAddress ipServer = new IPAddress(bytAdr);

            //// Get host related information.
            //IPHostEntry host = Dns.GetHostEntry(hostName);
            //IPAddress[] addressList = host.AddressList;
            //IPAddress address = addressList[addressList.Length - 1];

            

            // Instantiates the endpoint and socket.
            IPEndPoint serverEndPoint = new IPEndPoint(ipServer, port);

            _SocketClient = new SocketClient(serverEndPoint);
            _SocketClient.ProcessResponseJsonData = ProcessResponseJsonData;
            return _SocketClient.Connect();
        }

        public bool SendSMS(SMS4Json sMS4Json)
        {
            TransportUnit tu = new TransportUnit();

            try
            {
                sMS4Json.ClientCode = _ClientCode;
                tu.AddSMS(sMS4Json);

                byte[] bytes = tu.GetJsonBytes();
                _SocketClient.SendBytes(ref bytes);
                OutgoingSMS.Insert(0, sMS4Json);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error sending SMS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }
        public bool SendSMS(List<SMS4Json> listSMS4Json)
        {
            TransportUnit tu = new TransportUnit();

            try
            {
                foreach (SMS4Json sT in listSMS4Json)
                {
                    sT.ClientCode = _ClientCode;
                    tu.AddSMS(sT);
                }

                byte[] bytes = tu.GetJsonBytes();
                _SocketClient.SendBytes(ref bytes);
                foreach (SMS4Json sT in listSMS4Json)
                {
                    OutgoingSMS.Insert(0, sT);
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error sending SMS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        public bool SendSMS_Test(List<SMS4Json> listSMS4Json)
        {
            TransportUnit tu = new TransportUnit();

            try
            {
                foreach (SMS4Json sT in listSMS4Json)
                {
                    sT.ClientCode = _ClientCode;
                    tu.AddSMS(sT);
                }

                byte[] bytes = tu.GetJsonBytes();
                UInt16 length = BitConverter.ToUInt16(bytes, 0);

                _SocketClient.SendBytes(ref bytes);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error sending SMS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }
        internal void ProcessResponseJsonData(ReadOnlySpan<byte> rspT)
        {
            //System.Diagnostics.Debug.WriteLine("Client ProcessResponseJsonData Task={0}, Thread={1}", Task.CurrentId, Thread.CurrentThread.ManagedThreadId);
            List<Response> newResponseList = new List<Response>();

            ResponseTransport trT = ResponseTransport.DeSerialize(rspT);

            if (trT.ResponseList != null)
            {
                foreach (var r in trT.ResponseList)
                {
                    newResponseList.Add(r);
                    string strMessage = String.Format("CLIENT - Response with Id {0} recieved. Thread={1} \r\n {2}", r.SMSOriginalId, Thread.CurrentThread.ManagedThreadId, r.Text);

                    System.Diagnostics.Debug.WriteLine(strMessage);
                }
            }
            var context = SynchronizationContext.Current;
            context.Post(AddNewResponses, newResponseList);
        }

        private void AddNewResponses(object listOfResponses)
        {
            List<Response> newResponseList = listOfResponses as List<Response>;
            for (int i = newResponseList.Count - 1; i >= 0; i--)
            {
                ResponseList.Insert(0, newResponseList[i]);
            }

        }
    }
}
