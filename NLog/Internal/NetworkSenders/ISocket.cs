using System;
using System.Net.Sockets;

namespace NLog.Internal.NetworkSenders
{
	// Token: 0x0200009D RID: 157
	internal interface ISocket
	{
		// Token: 0x06000506 RID: 1286
		bool ConnectAsync(SocketAsyncEventArgs args);

		// Token: 0x06000507 RID: 1287
		void Close();

		// Token: 0x06000508 RID: 1288
		bool SendAsync(SocketAsyncEventArgs args);

		// Token: 0x06000509 RID: 1289
		bool SendToAsync(SocketAsyncEventArgs args);
	}
}
