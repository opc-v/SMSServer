using SMSExchange;
using System;
using System.Buffers.Binary;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SMSServerTest
{
    /// <summary>
    /// Implements the connection logic for the socket client.
    /// </summary>
    internal sealed class SocketClient : IDisposable
    {
        /// <summary>
        /// The socket used to send/receive messages.
        /// </summary>
        private Socket _ClientSocket;

        /// <summary>
        /// Flag for connected socket.
        /// </summary>
        private Boolean connected = false;

        /// <summary>
        /// Listener endpoint.
        /// </summary>
        private IPEndPoint hostEndPoint;

        internal EndPoint LocalEndpoint
        {
            get { return (_ClientSocket.Connected ? _ClientSocket.LocalEndPoint : null); }
        }
        public delegate void ProcessResponseJsonDataDelegate(ReadOnlySpan<byte> rspT);
        internal ProcessResponseJsonDataDelegate ProcessResponseJsonData;
        /// <summary>
        /// Create an uninitialized client instance.  
        /// To start the send/receive processing
        /// call the Connect method followed by SendReceive method.
        /// </summary>
        /// <param name="serverEndPoint">IPEndPoint of the host where the listener is running.</param>
        internal SocketClient(IPEndPoint serverEndPoint)
        {
            this.hostEndPoint = serverEndPoint;
            this._ClientSocket = new Socket(this.hostEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            this._ClientSocket = new Socket(this.hostEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        }

        /// <summary>
        /// Connect to the host.
        /// </summary>
        /// <returns>True if connection has succeded, else false.</returns>
        internal bool Connect()
        {
            this._ClientSocket.Connect(this.hostEndPoint);
            if (!this._ClientSocket.Connected) return false;

            SocketAsyncEventArgs readEventArg = new SocketAsyncEventArgs();
            readEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(OnReceive);
            readEventArg.SetBuffer(new Byte[8192], 0, 8192);

            if (!this._ClientSocket.ReceiveAsync(readEventArg)) OnReceive(this._ClientSocket, readEventArg);

            return true;
            //SocketAsyncEventArgs connectArgs = new SocketAsyncEventArgs();

            //connectArgs.UserToken = this.clientSocket;
            //connectArgs.RemoteEndPoint = this.hostEndPoint;
            //connectArgs.Completed += new EventHandler<SocketAsyncEventArgs>(OnConnect);

            //if (!clientSocket.ConnectAsync(connectArgs)) { 
            //    OnConnect(clientSocket, connectArgs);
            //    return;
            //}
        }

        private void OnConnect(object sender, SocketAsyncEventArgs e)
        {
            this.connected = (e.SocketError == SocketError.Success);

            if (e.SocketError != SocketError.Success)
            {
                ProcessError(e);
                return;
            }

            SocketAsyncEventArgs readEventArg = new SocketAsyncEventArgs();
            readEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(OnReceive);
            readEventArg.SetBuffer(new Byte[8192], 0, 8192);
            Socket s = e.UserToken as Socket;
            if (!s.ReceiveAsync(readEventArg)) OnReceive(s, readEventArg);
        }

        /// <summary>
        /// Disconnect from the host.
        /// </summary>
        internal void Disconnect()
        {
            if(_ClientSocket != null &&_ClientSocket.Connected) _ClientSocket.Disconnect(false);
        }


        private void OnReceive(object sender, SocketAsyncEventArgs e)
        {
            ProcessSocketData(e);

            SocketAsyncEventArgs readEventArg = new SocketAsyncEventArgs();
            readEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(OnReceive);
            readEventArg.SetBuffer(new Byte[8192], 0, 8192);
            Socket s = sender as Socket;
            if (!s.ReceiveAsync(readEventArg)) OnReceive(s, readEventArg);
        }

        private static long _ProcessedCount = 0;
        /// <summary>
        /// Process data received from the server.
        /// </summary>
        /// <param name="args">SocketAsyncEventArgs used in the operation.</param>
        internal void ProcessSocketData(SocketAsyncEventArgs args)
        {
            StringBuilder sbT = new StringBuilder();
            sbT.Append("Client ProcessSocketData ManagedThreadId=");
            sbT.Append(Thread.CurrentThread.ManagedThreadId.ToString());
            sbT.Append(" ProcessedCount=");
            sbT.Append(_ProcessedCount.ToString());
            string strT = string.Format(sbT.ToString());
            System.Diagnostics.Debug.WriteLine(strT);

            UInt16 length = BitConverter.ToUInt16(args.Buffer, 0);
            if (length==0)
            { 
                Response resp = new Response() { Kind = 0, Text = "Empty message from server", SMSOriginalId = "0"};
                ResponseTransport rT = new ResponseTransport();
                rT.AddResponse(resp);
                byte[] bytes = rT.GetJsonBytes();
                length = BitConverter.ToUInt16(bytes, 0);
                ReadOnlySpan<byte> rosT0 = new System.ReadOnlySpan<byte>(bytes, 2, length);

                if (ProcessResponseJsonData != null) ProcessResponseJsonData(rosT0);
                return;
            }

            ReadOnlySpan<byte> rosT = new System.ReadOnlySpan<byte>(args.Buffer, 2, length);

            if (ProcessResponseJsonData != null) ProcessResponseJsonData(rosT);
            _ProcessedCount++;
        }

        private void OnSend(object sender, SocketAsyncEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Client OnSend ManagedThreadId=" + System.Threading.Thread.CurrentThread.ManagedThreadId.ToString());
            if (e.SocketError != SocketError.Success) this.ProcessError(e);
        }

        /// <summary>
        /// Close socket in case of failure and throws a SockeException according to the SocketError.
        /// </summary>
        /// <param name="e">SocketAsyncEventArg associated with the failed operation.</param>
        private void ProcessError(SocketAsyncEventArgs e)
        {
            //e.AcceptSocket
            Socket s = e.UserToken as Socket;
            if (s.Connected)
            {
                // close the socket associated with the client
                try
                {
                    s.Shutdown(SocketShutdown.Both);
                }
                catch (Exception)
                {
                    // throws if client process has already closed
                }
                finally
                {
                    if (s.Connected)
                    {
                        s.Close();
                    }
                }
            }

            // Throw the SocketException
            throw new SocketException((Int32)e.SocketError);
        }

        internal void SendBytes(ref byte[] bytesToSend)
        {
            System.Diagnostics.Debug.WriteLine("Client SendBytes ManagedThreadId=" + System.Threading.Thread.CurrentThread.ManagedThreadId.ToString());
            if (_ClientSocket.Connected)
            {
                // Prepare arguments for send/receive operation.
                SocketAsyncEventArgs completeArgs = new SocketAsyncEventArgs();
                completeArgs.SetBuffer(bytesToSend, 0, bytesToSend.Length);
                completeArgs.UserToken = this._ClientSocket;
                completeArgs.RemoteEndPoint = this.hostEndPoint;
                completeArgs.Completed += new EventHandler<SocketAsyncEventArgs>(OnSend);

                // Start sending asyncronally.
                if (!_ClientSocket.SendAsync(completeArgs)) OnSend(this._ClientSocket, completeArgs);
            }
            else
            {
                throw new SocketException((Int32)SocketError.NotConnected);
            }
        }
        #region IDisposable Members

        /// <summary>
        /// Disposes the instance of SocketClient.
        /// </summary>
        public void Dispose()
        {
            if (this._ClientSocket.Connected)
            {
                this._ClientSocket.Close();
            }
        }

        #endregion
    }
}
