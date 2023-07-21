using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Xamarin.Essentials;
using SMSServer.Models;
using SMSExchange;

namespace SMSServer.IP
{
    /// <summary>
    /// Based on example from http://msdn2.microsoft.com/en-us/library/system.net.sockets.socketasynceventargs.aspx
    /// Implements the connection logic for the socket server.  
    /// After accepting a connection, all data read from the client is sent back. 
    /// The read and echo back to the client pattern is continued until the client disconnects.
    /// </summary>
    internal sealed class SocketListener
    {
        /// <summary>
        /// The socket used to listen for incoming connection requests.
        /// </summary>
        private Socket _ListenSocket;

        /// <summary>
        /// The total number of clients connected to the server.
        /// </summary>
        private Int32 numConnectedSockets;

        /// <summary>
        /// the maximum number of connections the sample is designed to handle simultaneously.
        /// </summary>
        private Int32 numConnections;

        /// <summary>
        /// Pool of reusable SocketAsyncEventArgs objects for write, read and accept socket operations.
        /// </summary>
        private SocketAsyncEventArgsPool readWritePool;

        /// <summary>
        /// Create an uninitialized server instance.  
        /// To start the server listening for connection requests
        /// call the Init method followed by Start method.
        /// </summary>
        /// <param name="numConnections">Maximum number of connections to be handled simultaneously.</param>
        internal SocketListener(Int32 numConnections)
        { 
            this.numConnectedSockets = 0;
            this.numConnections = numConnections;

            this.readWritePool = new SocketAsyncEventArgsPool();
        }

        /// <summary>
        /// Starts the server listening for incoming connection requests.
        /// </summary>
        /// <param name="port">Port where the server will listen for connection requests.</param>
        internal void Start(Int32 port)
        {
            IPAddress ipa = new IPAddress(0x0);
            //// Instantiates the endpoint and socket.
            IPEndPoint hostEndPoint = new IPEndPoint(ipa, port);

            // Create the socket which listens for incoming connections.
            this._ListenSocket = new Socket(hostEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            this._ListenSocket.ReceiveBufferSize = SocketAsyncEventArgsPool.BufferSize;
            this._ListenSocket.SendBufferSize = SocketAsyncEventArgsPool.BufferSize;

            if (hostEndPoint.AddressFamily == AddressFamily.InterNetworkV6)
            {
                // Set dual-mode (IPv4 & IPv6) for the socket listener.
                // 27 is equivalent to IPV6_V6ONLY socket option in the winsock snippet below,
                // based on http://blogs.msdn.com/wndp/archive/2006/10/24/creating-ip-agnostic-applications-part-2-dual-mode-sockets.aspx
                this._ListenSocket.SetSocketOption(SocketOptionLevel.IPv6, (SocketOptionName)27, false);
                this._ListenSocket.Bind(new IPEndPoint(IPAddress.IPv6Any, hostEndPoint.Port));
            }
            else
            {
                // Associate the socket with the local endpoint.
                this._ListenSocket.Bind(hostEndPoint);
            }

            // Start the server.
            this._ListenSocket.Listen(this.numConnections);

            // Post accepts on the listening socket.
            this.StartAccept(null);
        }

        /// <summary>
        /// Begins an operation to accept a connection request from the client.
        /// </summary>
        /// <param name="acceptEventArg">The context object to use when issuing 
        /// the accept operation on the server's listening socket.</param>
        private void StartAccept(SocketAsyncEventArgs acceptEventArg)
        {

            if (acceptEventArg == null)
            {
                acceptEventArg = new SocketAsyncEventArgs();
                acceptEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(OnAcceptCompleted);
            }
            else
            {
                // Socket must be cleared since the context object is being reused.
                acceptEventArg.AcceptSocket = null;
            }
            try
            {
                if (!this._ListenSocket.AcceptAsync(acceptEventArg)) this.ProcessAccept(acceptEventArg);
            }
            catch (Exception ex)
            {
                App.Logger.Exception(ex, "At StartAccept.");
            }
        }

        /// <summary>
        /// Callback method associated with Socket.AcceptAsync 
        /// operations and is invoked when an accept operation is complete.
        /// </summary>
        /// <param name="sender">Object who raised the event.</param>
        /// <param name="e">SocketAsyncEventArg associated with the completed accept operation.</param>
        private void OnAcceptCompleted(object sender, SocketAsyncEventArgs e)
        {
            if (e.LastOperation == SocketAsyncOperation.Accept) this.ProcessAccept(e);
        }

        private Dictionary<string, Token> _TokenList = new Dictionary<string, Token>();
        /// <summary>
        /// Process the accept for the socket listener.
        /// </summary>
        /// <param name="e">SocketAsyncEventArg associated with the completed accept operation.</param>
        private void ProcessAccept(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.OperationAborted) return;
            Socket s = e.AcceptSocket;
            if (e.SocketError != SocketError.Success)
            {
                App.Logger.Error(string.Format("Socket error {0} while ProcessAccept.", e.SocketError.ToString()));
                if (s != null)
                {
                    if (s.Connected) s.Disconnect(false);
                    s.Shutdown(SocketShutdown.Both);
                    s.Close();
                }
                this.StartAccept(e);
                return;
            }

            if (s.Connected)
            {
                if (this.numConnectedSockets >= this.numConnections)
                {
                    Response resp = new Response() { Kind = 1, Text = string.Format("ToBeDisconnected: Too much connections {0}.", this.numConnectedSockets), SMSOriginalId = string.Empty };
                    ResponseTransport rT = new ResponseTransport();
                    rT.AddResponse(resp);
                    byte[] bytes = rT.GetJsonBytes();
                    s.Send(bytes);

                    if (s.Connected) s.Disconnect(false);
                    s.Shutdown(SocketShutdown.Both);
                    s.Close();
                    this.StartAccept(e);
                    return;
                }
                Interlocked.Increment(ref this.numConnectedSockets);

                Token tT = new Token(s, this, readWritePool);
                lock (_TokenList)
                {
                    if (!_TokenList.ContainsKey(tT.RemoteEndPoint.ToString())) _TokenList.Add(tT.RemoteEndPoint.ToString(), tT);
                }

                tT.StartReceive();

                RaiseConnectionEvent(tT, true);

                App.Logger.Info(string.Format("SERVER - Client connection from {0} accepted. There are {1} clients connected to the server", tT.RemoteEndPoint, this.numConnectedSockets));

                // Accept the next connection request.
                this.StartAccept(e);
            }
        }

        internal void ProcessError(SocketAsyncEventArgs e)
        {
            if (!this._ListenSocket.Connected) return;
            Token token = e.UserToken as Token;

            App.Logger.Error(string.Format("Socket error {0} on endpoint {1} during {2}.", e.SocketError.ToString(), token?.RemoteEndPoint, e.LastOperation));

            this.CloseClientSocket(e);
        }

        /// <summary>
        /// Close the socket associated with the client.
        /// </summary>
        /// <param name="e">SocketAsyncEventArg associated with the completed send/receive operation.</param>
        internal void CloseClientSocket(SocketAsyncEventArgs e)
        {
            Interlocked.Decrement(ref this.numConnectedSockets);

            Token token = e.UserToken as Token;

            if (token == null) return;
            lock (_TokenList)
            {
                if (_TokenList.ContainsKey(token.RemoteEndPoint.ToString())) _TokenList.Remove(token.RemoteEndPoint.ToString());
            }
            RaiseConnectionEvent(token, false);
            App.Logger.Info(string.Format("Client {0} disconnectining. There are {1} clients connected to the server.", token.RemoteEndPoint, this.numConnectedSockets));
        }

        /// <summary>
        /// Stop the server.
        /// </summary>
        internal void Stop()
        {
            if (this._ListenSocket != null && this._ListenSocket.Connected)
            {
                this._ListenSocket.Disconnect(false);
                this._ListenSocket.Shutdown(SocketShutdown.Both);
            }
            this._ListenSocket.Close();
        }

        public delegate void ConnectionEventHandler(object sender, ConnectionEventArgs e);
        public event ConnectionEventHandler ConnectionEvent;
        private void RaiseConnectionEvent(Token connectedToken, Boolean isConnected)
        {
            // Raise the event in a thread-safe manner using the ?. operator.
            ConnectionEvent?.Invoke(this, new ConnectionEventArgs(connectedToken, isConnected));
        }
    }

    internal class ConnectionEventArgs
    {
        public ConnectionEventArgs(Token connectedToken, Boolean isConnected) { ConnectedToken = connectedToken; IsConnected = isConnected; }
        public Token ConnectedToken { get; } // readonly
        public Boolean IsConnected { get; }
    }
}
