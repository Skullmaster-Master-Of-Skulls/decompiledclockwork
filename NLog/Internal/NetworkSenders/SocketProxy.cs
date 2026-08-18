using System;
using System.Net.Sockets;

namespace NLog.Internal.NetworkSenders
{
	// Token: 0x0200009F RID: 159
	internal sealed class SocketProxy : ISocket, IDisposable
	{
		// Token: 0x0600050D RID: 1293 RVA: 0x0000AE9B File Offset: 0x0000909B
		internal SocketProxy(AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType)
		{
			this.socket = new Socket(addressFamily, socketType, protocolType);
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x0000AEB1 File Offset: 0x000090B1
		public Socket UnderlyingSocket
		{
			get
			{
				return this.socket;
			}
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0000AEB9 File Offset: 0x000090B9
		public void Close()
		{
			this.socket.Close();
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0000AEC6 File Offset: 0x000090C6
		public bool ConnectAsync(SocketAsyncEventArgs args)
		{
			return this.socket.ConnectAsync(args);
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0000AED4 File Offset: 0x000090D4
		public bool SendAsync(SocketAsyncEventArgs args)
		{
			return this.socket.SendAsync(args);
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0000AEE2 File Offset: 0x000090E2
		public bool SendToAsync(SocketAsyncEventArgs args)
		{
			return this.socket.SendToAsync(args);
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0000AEF0 File Offset: 0x000090F0
		public void Dispose()
		{
			((IDisposable)this.socket).Dispose();
		}

		// Token: 0x04000104 RID: 260
		private readonly Socket socket;
	}
}
