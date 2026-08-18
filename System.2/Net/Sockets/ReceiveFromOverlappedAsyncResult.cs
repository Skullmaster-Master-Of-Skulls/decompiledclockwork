using System;

namespace System.Net.Sockets
{
	// Token: 0x0200039F RID: 927
	internal class ReceiveFromOverlappedAsyncResult : OverlappedAsyncResult
	{
		// Token: 0x060022A4 RID: 8868 RVA: 0x000A51F5 File Offset: 0x000A33F5
		internal ReceiveFromOverlappedAsyncResult(Socket socket, object asyncState, AsyncCallback asyncCallback) : base(socket, asyncState, asyncCallback)
		{
		}

		// Token: 0x060022A5 RID: 8869 RVA: 0x000A5200 File Offset: 0x000A3400
		internal override object PostCompletion(int numBytes)
		{
			base.SocketAddress.SetSize(base.GetSocketAddressSizePtr());
			return base.PostCompletion(numBytes);
		}
	}
}
