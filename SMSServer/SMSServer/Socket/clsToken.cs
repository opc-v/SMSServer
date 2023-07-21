using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Globalization;
using System.Buffers.Binary;
using Xamarin.Essentials;
using System.Net;

namespace SMSServer.IP
{
    delegate void ProcessData(SocketAsyncEventArgs args);

    /// <summary>
    /// Token for use with SocketAsyncEventArgs.
    /// </summary>
    internal sealed class Token : IDisposable
    {
        private System.Net.Sockets.Socket _Connection;

        private SocketListener _Listener;
        private IPEndPoint _RemoteEndPoint;
        private SocketAsyncEventArgsPool _ReadWritePool;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="connection">Socket to accept incoming data.</param>
        internal Token(Socket connection, SocketListener listener, SocketAsyncEventArgsPool readWritePool)
        {
            this._Connection = connection;
            _Listener = listener;
            _RemoteEndPoint = (IPEndPoint)connection.RemoteEndPoint;
            _ReadWritePool = readWritePool;
        }

        /// <summary>
        /// Accept socket.
        /// </summary>
        internal Socket Connection
        {
            get { return this._Connection; }
        }
        internal IPEndPoint RemoteEndPoint { get { return this._RemoteEndPoint; } }

        #region "Receive"
        internal void StartReceive()
        {
            SocketAsyncEventArgs readEventArgs = _ReadWritePool.Pop();
            // Get the socket for the accepted client connection and put it into the 
            // ReadEventArg object user token.
            readEventArgs.UserToken = this;

            readEventArgs.Completed += new EventHandler<SocketAsyncEventArgs>(OnReceiveCompleted);

            try
            {
                if (!_Connection.ReceiveAsync(readEventArgs)) this.ProcessReceive(readEventArgs);
            }
            catch (Exception exe)
            {
                App.Logger.Exception(exe, string.Format("Exception at StartReceive from { 0}", _RemoteEndPoint));
            }
        }

        /// <summary>
        /// Callback called whenever a receive or send operation is completed on a socket.
        /// </summary>
        /// <param name="sender">Object who raised the event.</param>
        /// <param name="e">SocketAsyncEventArg associated with the completed send/receive operation.</param>
        private void OnReceiveCompleted(object sender, SocketAsyncEventArgs e)
        {
            this.ProcessReceive(e);
        }

        /// <summary>
        /// This method is invoked when an asynchronous receive operation completes. 
        /// If the remote host closed the connection, then the socket is closed.  
        /// If data was received then the data is echoed back to the client.
        /// </summary>
        /// <param name="e">SocketAsyncEventArg associated with the completed receive operation.</param>
        private void ProcessReceive(SocketAsyncEventArgs e)
        {
            string strT;
            if (e.SocketError != SocketError.Success)
            {
                _Listener.ProcessError(e);
                return;
            }
            if (e.LastOperation != SocketAsyncOperation.Receive)
            {
                strT = string.Format("Unexpected last operation - {0} from {1} while receive.", e.LastOperation, _RemoteEndPoint);
                App.Logger.Error(strT);
            }
            // Check if the remote host closed the connection.
            if (e.BytesTransferred > 0)
            {
                this.ProcessSocketData(e);

                if (_Connection != null && _Connection.Connected)
                {
                    try
                    {
                        if (!_Connection.ReceiveAsync(e)) this.ProcessReceive(e);
                    }
                    catch (Exception exe)
                    {
                        App.Logger.Exception(exe, string.Format("Exception at ProcessReceive from { 0}", _RemoteEndPoint));
                    }
                }
                else
                {
                    _Listener.CloseClientSocket(e);
                }
            }
            else
            {
                _Listener.CloseClientSocket(e);
            }
        }

        private UInt16 _NextDataIndex = 0, _DataLength;
        private byte[] _DataBuffer = new byte[UInt16.MaxValue/4];
        private static long _ProcessedCount = 0;
        /// <summary>
        /// Process data received from the client.
        /// </summary>
        /// <param name="args">SocketAsyncEventArgs used in the operation.</param>
        internal void ProcessSocketData(SocketAsyncEventArgs args)
        {
            UInt16 curIndex = 0, take = 0;
            do
            {
                if (_NextDataIndex == 0)
                {
                    _DataLength = BitConverter.ToUInt16(args.Buffer, curIndex);
                    curIndex += 2;
                }

                if ((_NextDataIndex + args.BytesTransferred - curIndex) > _DataLength) take = (UInt16)(_DataLength - _NextDataIndex);
                else take = (UInt16)(args.BytesTransferred - curIndex);

                Array.Copy(args.Buffer, curIndex, _DataBuffer, _NextDataIndex, take );
                if ((_NextDataIndex + take) >= _DataLength)
                {
                    ReadOnlySpan<byte> rosT = new System.ReadOnlySpan<byte>(_DataBuffer, 0, _DataLength);
                    App.SMSVM.ProcessSocketMessageData(rosT, this);
                    _DataLength = 0;
                    _NextDataIndex = 0;
                }
                else _NextDataIndex += take;

                curIndex += take;
            } while (curIndex < args.BytesTransferred);
            _ProcessedCount++;
            App.Logger.Info(string.Format("Server Process Data Buffer. ProcessedCount={0}.", _ProcessedCount));
            // Get the message received from the client.
        }

        #endregion

        #region "SEND"
        internal void SendResponse(ref byte[] bytes)
        {

            if (bytes.Length > SocketAsyncEventArgsPool.BufferSize)
            {
                string strT = string.Format("Exceeding {0} the size of the sending buffer {1}.", bytes.Length, SocketAsyncEventArgsPool.BufferSize);
                throw new ArgumentException(strT);
            }
            if (_Connection == null) return;
            if (!_Connection.Connected)
            {
                //throw new System.Net.Sockets.SocketException((int)SocketError.NotConnected);
                return;
            }

            SocketAsyncEventArgs writeEventArgs = _ReadWritePool.Pop();
            writeEventArgs.Completed += new EventHandler<SocketAsyncEventArgs>(OnSendCompleted);

            writeEventArgs.UserToken = this;
            bytes.CopyTo(writeEventArgs.Buffer, 0);
            try
            {
                if (!_Connection.SendAsync(writeEventArgs)) this.ProcessSend(writeEventArgs);
            }
            catch (Exception ex)
            {
                App.Logger.Exception(ex, string.Format("While sending response to client {0}.", this._RemoteEndPoint));
            }
        }

        /// <summary>
        /// Callback called whenever a send operation is completed on a socket.
        /// </summary>
        /// <param name="sender">Object who raised the event.</param>
        /// <param name="e">SocketAsyncEventArg associated with the completed send operation.</param>
        private void OnSendCompleted(object sender, SocketAsyncEventArgs e)
        {
            // Determine which type of operation just completed and call the associated handler.
            if (e.LastOperation == SocketAsyncOperation.Send) ProcessSend(e);
        }
        private void ProcessSend(SocketAsyncEventArgs e)
        {
            if (e.SocketError != SocketError.Success)
            {
                string strT = string.Format(" Socket error {0} at ProcessSend for {1}.", e.SocketError, this._RemoteEndPoint);
                App.Logger.Error(strT);
            }
            e.UserToken = null;
            e.Completed -= OnSendCompleted;
            _ReadWritePool.Push(e);
        }
        #endregion

        internal void CloseClientConnection(SocketAsyncEventArgs e)
        {
            if (e != null)
            {
                e.UserToken = null;
                e.Completed -= OnReceiveCompleted;
                _ReadWritePool.Push(e);
            }

            if (_Connection == null) return;

            if (_Connection.Connected) _Connection.Disconnect(false);
            _Connection.Shutdown(SocketShutdown.Both);
            _Connection.Close();
            _Connection = null;
        }

        #region IDisposable Members
        /// <summary>
        /// Release instance.
        /// </summary>
        public void Dispose()
        {
            try
            {
                this._Connection.Shutdown(SocketShutdown.Both);
            }
            catch (Exception)
            {
                // Throw if client has closed, so it is not necessary to catch.
            }
            finally
            {
                this._Connection.Close();
            }
        }

        #endregion
    }
}
