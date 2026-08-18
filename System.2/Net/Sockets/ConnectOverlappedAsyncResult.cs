using System;
using System.Runtime.InteropServices;

namespace System.Net.Sockets
{
	// Token: 0x0200038E RID: 910
	internal class ConnectOverlappedAsyncResult : BaseOverlappedAsyncResult
	{
		// Token: 0x0600223C RID: 8764 RVA: 0x000A3CAC File Offset: 0x000A1EAC
		internal ConnectOverlappedAsyncResult(Socket socket, EndPoint endPoint, object asyncState, AsyncCallback asyncCallback) : base(socket, asyncState, asyncCallback)
		{
			this.m_EndPoint = endPoint;
		}

		// Token: 0x0600223D RID: 8765 RVA: 0x000A3CC0 File Offset: 0x000A1EC0
		internal override object PostCompletion(int numBytes)
		{
			SocketError socketError = (SocketError)base.ErrorCode;
			Socket socket = (Socket)base.AsyncObject;
			if (socketError == SocketError.Success)
			{
				try
				{
					socketError = UnsafeNclNativeMethods.OSSOCK.setsockopt(socket.SafeHandle, SocketOptionLevel.Socket, SocketOptionName.UpdateConnectContext, null, 0);
					if (socketError == SocketError.SocketError)
					{
						socketError = (SocketError)Marshal.GetLastWin32Error();
					}
				}
				catch (ObjectDisposedException)
				{
					socketError = SocketError.OperationAborted;
				}
				base.ErrorCode = (int)socketError;
			}
			if (socketError == SocketError.Success)
			{
				socket.SetToConnected();
				return socket;
			}
			return null;
		}

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x0600223E RID: 8766 RVA: 0x000A3D34 File Offset: 0x000A1F34
		internal EndPoint RemoteEndPoint
		{
			get
			{
				return this.m_EndPoint;
			}
		}

		// Token: 0x04001F6F RID: 8047
		private EndPoint m_EndPoint;
	}
}
