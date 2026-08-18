using System;

namespace System.Net.Sockets
{
	// Token: 0x020005D1 RID: 1489
	internal class DisconnectOverlappedAsyncResult : BaseOverlappedAsyncResult
	{
		// Token: 0x06002ECC RID: 11980 RVA: 0x000CE5FC File Offset: 0x000CD5FC
		internal DisconnectOverlappedAsyncResult(Socket socket, object asyncState, AsyncCallback asyncCallback) : base(socket, asyncState, asyncCallback)
		{
		}

		// Token: 0x06002ECD RID: 11981 RVA: 0x000CE608 File Offset: 0x000CD608
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
