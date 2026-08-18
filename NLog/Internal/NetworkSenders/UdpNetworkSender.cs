using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using NLog.Common;

namespace NLog.Internal.NetworkSenders
{
	// Token: 0x020000A2 RID: 162
	internal class UdpNetworkSender : NetworkSender
	{
		// Token: 0x06000524 RID: 1316 RVA: 0x0000B311 File Offset: 0x00009511
		public UdpNetworkSender(string url, AddressFamily addressFamily) : base(url)
		{
			this.AddressFamily = addressFamily;
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x0000B321 File Offset: 0x00009521
		// (set) Token: 0x06000526 RID: 1318 RVA: 0x0000B329 File Offset: 0x00009529
		internal AddressFamily AddressFamily { get; set; }

		// Token: 0x06000527 RID: 1319 RVA: 0x0000B334 File Offset: 0x00009534
		protected internal virtual ISocket CreateSocket(AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType)
		{
			SocketProxy socketProxy = new SocketProxy(addressFamily, socketType, protocolType);
			Uri uri;
			if (Uri.TryCreate(base.Address, UriKind.Absolute, out uri) && uri.Host.Equals(IPAddress.Broadcast.ToString(), StringComparison.InvariantCultureIgnoreCase))
			{
				socketProxy.UnderlyingSocket.EnableBroadcast = true;
			}
			return socketProxy;
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0000B37F File Offset: 0x0000957F
		protected override void DoInitialize()
		{
			this.endpoint = this.ParseEndpointAddress(new Uri(base.Address), this.AddressFamily);
			this.socket = this.CreateSocket(this.endpoint.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x0000B3B8 File Offset: 0x000095B8
		protected override void DoClose(AsyncContinuation continuation)
		{
			lock (this)
			{
				try
				{
					if (this.socket != null)
					{
						this.socket.Close();
					}
				}
				catch (Exception exception)
				{
					if (exception.MustBeRethrown())
					{
						throw;
					}
				}
				this.socket = null;
			}
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x0000B424 File Offset: 0x00009624
		protected override void DoSend(byte[] bytes, int offset, int length, AsyncContinuation asyncContinuation)
		{
			lock (this)
			{
				SocketAsyncEventArgs socketAsyncEventArgs = new SocketAsyncEventArgs();
				socketAsyncEventArgs.SetBuffer(bytes, offset, length);
				socketAsyncEventArgs.UserToken = asyncContinuation;
				socketAsyncEventArgs.Completed += this.SocketOperationCompleted;
				socketAsyncEventArgs.RemoteEndPoint = this.endpoint;
				if (!this.socket.SendToAsync(socketAsyncEventArgs))
				{
					this.SocketOperationCompleted(this.socket, socketAsyncEventArgs);
				}
			}
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x0000B4AC File Offset: 0x000096AC
		private void SocketOperationCompleted(object sender, SocketAsyncEventArgs e)
		{
			AsyncContinuation asyncContinuation = e.UserToken as AsyncContinuation;
			Exception exception = null;
			if (e.SocketError != SocketError.Success)
			{
				exception = new IOException("Error: " + e.SocketError);
			}
			e.Dispose();
			if (asyncContinuation != null)
			{
				asyncContinuation(exception);
			}
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0000B4FA File Offset: 0x000096FA
		public override void CheckSocket()
		{
			if (this.socket == null)
			{
				this.DoInitialize();
			}
		}

		// Token: 0x0400010D RID: 269
		private ISocket socket;

		// Token: 0x0400010E RID: 270
		private EndPoint endpoint;
	}
}
