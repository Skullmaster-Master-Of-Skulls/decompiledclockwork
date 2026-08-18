using System;
using System.Runtime.InteropServices;

namespace System.Net.Sockets
{
	// Token: 0x020005D0 RID: 1488
	internal class ConnectOverlappedAsyncResult : BaseOverlappedAsyncResult
	{
		// Token: 0x06002EC9 RID: 11977 RVA: 0x000CE56C File Offset: 0x000CD56C
		internal ConnectOverlappedAsyncResult(Socket socket, EndPoint endPoint, object asyncState, AsyncCallback asyncCallback) : base(socket, asyncState, asyncCallback)
		{
			this.m_EndPoint = endPoint;
		}

		// Token: 0x06002ECA RID: 11978 RVA: 0x000CE580 File Offset: 0x000CD580
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

		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x06002ECB RID: 11979 RVA: 0x000CE5F4 File Offset: 0x000CD5F4
		internal EndPoint RemoteEndPoint
		{
			get
			{
				return this.m_EndPoint;
			}
		}

		// Token: 0x04002C55 RID: 11349
		private EndPoint m_EndPoint;
	}
}
