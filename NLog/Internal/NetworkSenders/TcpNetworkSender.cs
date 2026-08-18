using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using NLog.Common;

namespace NLog.Internal.NetworkSenders
{
	// Token: 0x020000A0 RID: 160
	internal class TcpNetworkSender : NetworkSender
	{
		// Token: 0x06000514 RID: 1300 RVA: 0x0000AEFD File Offset: 0x000090FD
		public TcpNetworkSender(string url, AddressFamily addressFamily) : base(url)
		{
			this.AddressFamily = addressFamily;
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x0000AF18 File Offset: 0x00009118
		// (set) Token: 0x06000516 RID: 1302 RVA: 0x0000AF20 File Offset: 0x00009120
		internal AddressFamily AddressFamily { get; set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x0000AF29 File Offset: 0x00009129
		// (set) Token: 0x06000518 RID: 1304 RVA: 0x0000AF31 File Offset: 0x00009131
		internal int MaxQueueSize { get; set; }

		// Token: 0x06000519 RID: 1305 RVA: 0x0000AF3A File Offset: 0x0000913A
		protected internal virtual ISocket CreateSocket(AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType)
		{
			return new SocketProxy(addressFamily, socketType, protocolType);
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0000AF44 File Offset: 0x00009144
		protected override void DoInitialize()
		{
			TcpNetworkSender.MySocketAsyncEventArgs mySocketAsyncEventArgs = new TcpNetworkSender.MySocketAsyncEventArgs();
			mySocketAsyncEventArgs.RemoteEndPoint = this.ParseEndpointAddress(new Uri(base.Address), this.AddressFamily);
			mySocketAsyncEventArgs.Completed += this.SocketOperationCompleted;
			mySocketAsyncEventArgs.UserToken = null;
			this.socket = this.CreateSocket(mySocketAsyncEventArgs.RemoteEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			this.asyncOperationInProgress = true;
			if (!this.socket.ConnectAsync(mySocketAsyncEventArgs))
			{
				this.SocketOperationCompleted(this.socket, mySocketAsyncEventArgs);
			}
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0000AFC8 File Offset: 0x000091C8
		protected override void DoClose(AsyncContinuation continuation)
		{
			lock (this)
			{
				if (this.asyncOperationInProgress)
				{
					this.closeContinuation = continuation;
				}
				else
				{
					this.CloseSocket(continuation);
				}
			}
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0000B018 File Offset: 0x00009218
		protected override void DoFlush(AsyncContinuation continuation)
		{
			lock (this)
			{
				if (!this.asyncOperationInProgress && this.pendingRequests.Count == 0)
				{
					continuation(null);
				}
				else
				{
					this.flushContinuation = continuation;
				}
			}
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0000B074 File Offset: 0x00009274
		protected override void DoSend(byte[] bytes, int offset, int length, AsyncContinuation asyncContinuation)
		{
			TcpNetworkSender.MySocketAsyncEventArgs mySocketAsyncEventArgs = new TcpNetworkSender.MySocketAsyncEventArgs();
			mySocketAsyncEventArgs.SetBuffer(bytes, offset, length);
			mySocketAsyncEventArgs.UserToken = asyncContinuation;
			mySocketAsyncEventArgs.Completed += this.SocketOperationCompleted;
			lock (this)
			{
				if (this.MaxQueueSize != 0 && this.pendingRequests.Count >= this.MaxQueueSize)
				{
					SocketAsyncEventArgs socketAsyncEventArgs = this.pendingRequests.Dequeue();
					if (socketAsyncEventArgs != null)
					{
						socketAsyncEventArgs.Dispose();
					}
				}
				this.pendingRequests.Enqueue(mySocketAsyncEventArgs);
			}
			this.ProcessNextQueuedItem();
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0000B114 File Offset: 0x00009314
		private void CloseSocket(AsyncContinuation continuation)
		{
			try
			{
				ISocket socket = this.socket;
				this.socket = null;
				if (socket != null)
				{
					socket.Close();
				}
				continuation(null);
			}
			catch (Exception exception)
			{
				if (exception.MustBeRethrown())
				{
					throw;
				}
				continuation(exception);
			}
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0000B168 File Offset: 0x00009368
		private void SocketOperationCompleted(object sender, SocketAsyncEventArgs e)
		{
			lock (this)
			{
				this.asyncOperationInProgress = false;
				AsyncContinuation asyncContinuation = e.UserToken as AsyncContinuation;
				if (e.SocketError != SocketError.Success)
				{
					this.pendingError = new IOException("Error: " + e.SocketError);
				}
				e.Dispose();
				if (asyncContinuation != null)
				{
					asyncContinuation(this.pendingError);
				}
			}
			this.ProcessNextQueuedItem();
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0000B1F4 File Offset: 0x000093F4
		private void ProcessNextQueuedItem()
		{
			lock (this)
			{
				if (!this.asyncOperationInProgress)
				{
					if (this.pendingError != null)
					{
						while (this.pendingRequests.Count != 0)
						{
							SocketAsyncEventArgs socketAsyncEventArgs = this.pendingRequests.Dequeue();
							AsyncContinuation asyncContinuation = (AsyncContinuation)socketAsyncEventArgs.UserToken;
							socketAsyncEventArgs.Dispose();
							asyncContinuation(this.pendingError);
						}
					}
					if (this.pendingRequests.Count == 0)
					{
						AsyncContinuation asyncContinuation2 = this.flushContinuation;
						if (asyncContinuation2 != null)
						{
							this.flushContinuation = null;
							asyncContinuation2(this.pendingError);
						}
						AsyncContinuation asyncContinuation3 = this.closeContinuation;
						if (asyncContinuation3 != null)
						{
							this.closeContinuation = null;
							this.CloseSocket(asyncContinuation3);
						}
					}
					else
					{
						SocketAsyncEventArgs socketAsyncEventArgs = this.pendingRequests.Dequeue();
						this.asyncOperationInProgress = true;
						if (!this.socket.SendAsync(socketAsyncEventArgs))
						{
							this.SocketOperationCompleted(this.socket, socketAsyncEventArgs);
						}
					}
				}
			}
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x0000B2F0 File Offset: 0x000094F0
		public override void CheckSocket()
		{
			if (this.socket == null)
			{
				this.DoInitialize();
			}
		}

		// Token: 0x04000105 RID: 261
		private readonly Queue<SocketAsyncEventArgs> pendingRequests = new Queue<SocketAsyncEventArgs>();

		// Token: 0x04000106 RID: 262
		private ISocket socket;

		// Token: 0x04000107 RID: 263
		private Exception pendingError;

		// Token: 0x04000108 RID: 264
		private bool asyncOperationInProgress;

		// Token: 0x04000109 RID: 265
		private AsyncContinuation closeContinuation;

		// Token: 0x0400010A RID: 266
		private AsyncContinuation flushContinuation;

		// Token: 0x020000A1 RID: 161
		internal class MySocketAsyncEventArgs : SocketAsyncEventArgs
		{
			// Token: 0x06000522 RID: 1314 RVA: 0x0000B300 File Offset: 0x00009500
			public void RaiseCompleted()
			{
				this.OnCompleted(this);
			}
		}
	}
}
