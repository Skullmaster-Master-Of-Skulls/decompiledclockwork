using System;

namespace System.Net.Sockets
{
	// Token: 0x0200038F RID: 911
	internal class DisconnectOverlappedAsyncResult : BaseOverlappedAsyncResult
	{
		// Token: 0x0600223F RID: 8767 RVA: 0x000A3D3C File Offset: 0x000A1F3C
		internal DisconnectOverlappedAsyncResult(Socket socket, object asyncState, AsyncCallback asyncCallback) : base(socket, asyncState, asyncCallback)
		{
		}

		// Token: 0x06002240 RID: 8768 RVA: 0x000A3D48 File Offset: 0x000A1F48
		internal override object PostCompletion(int numBytes)
		{
			if (base.ErrorCode == 0)
			{
				Socket socket = (Socket)base.AsyncObject;
				socket.SetToDisconnected();
				socket.m_RemoteEndPoint = null;
			}
			return base.PostCompletion(numBytes);
		}
	}
}
