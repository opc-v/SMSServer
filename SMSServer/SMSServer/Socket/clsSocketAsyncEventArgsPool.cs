using SMSServer.Models;
using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace SMSServer.IP
{
    /// <summary>
    /// Based on example from http://msdn2.microsoft.com/en-us/library/system.net.sockets.socketasynceventargs.socketasynceventargs.aspx
    /// Represents a collection of reusable SocketAsyncEventArgs objects.  
    /// </summary>
    internal sealed class SocketAsyncEventArgsPool
    {
        /// <summary>
        /// Buffer size to use for each socket I/O operation.
        /// </summary>
        internal const int BufferSize = 8192;

        /// <summary>
        /// Pool of SocketAsyncEventArgs.
        /// </summary>
        Stack<SocketAsyncEventArgs> pool;
          
        /// <summary>
        /// Initializes the object pool to the specified size.
        /// </summary>
        /// <param name="capacity">Maximum number of SocketAsyncEventArgs objects the pool can hold.</param>
        internal SocketAsyncEventArgsPool()
        {
            this.pool = new Stack<SocketAsyncEventArgs>();
            // Preallocate pool of SocketAsyncEventArgs objects.
            SocketAsyncEventArgs argT = new SocketAsyncEventArgs();
            argT.SetBuffer(new byte[BufferSize], 0, BufferSize);
            this.pool.Push(argT);
            argT = new SocketAsyncEventArgs();
            argT.SetBuffer(new byte[BufferSize], 0, BufferSize );
            this.pool.Push(argT);
        }

        /// <summary>
        /// Removes a SocketAsyncEventArgs instance from the pool.
        /// </summary>
        /// <returns>SocketAsyncEventArgs removed from the pool.</returns>
        internal SocketAsyncEventArgs Pop()
        {
            lock (this.pool)
            {
                if (this.pool.Count > 0)
                {
                    return this.pool.Pop();
                }
                else
                {
                    SocketAsyncEventArgs argT = new SocketAsyncEventArgs();
                    argT.SetBuffer(new byte[BufferSize], 0, BufferSize );
                    return argT;
                }
            }
        }

        /// <summary>
        /// Add a SocketAsyncEventArg instance to the pool. 
        /// </summary>
        /// <param name="item">SocketAsyncEventArgs instance to add to the pool.</param>
        internal void Push(SocketAsyncEventArgs item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item", "Item added to a SocketAsyncEventArgsPool cannot be null");
            }
            item.UserToken = null;
            lock (this.pool)
            {
                this.pool.Push(item);
            }
        }
    }
}
