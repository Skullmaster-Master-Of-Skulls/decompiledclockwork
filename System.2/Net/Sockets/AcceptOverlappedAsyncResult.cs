using System;
using System.Runtime.InteropServices;

namespace System.Net.Sockets
{
	// Token: 0x0200038B RID: 907
	internal class AcceptOverlappedAsyncResult : BaseOverlappedAsyncResult
	{
		// Token: 0x0600221A RID: 8730 RVA: 0x000A330E File Offset: 0x000A150E
		internal AcceptOverlappedAsyncResult(Socket listenSocket, object asyncState, AsyncCallback asyncCallback) : base(listenSocket, asyncState, asyncCallback)
		{
			this.m_ListenSocket = listenSocket;
		}

		// Token: 0x0600221B RID: 8731 RVA: 0x000A3320 File Offset: 0x000A1520
		internal override object PostCompletion(int numBytes)
		{
			SocketError socketError = (SocketError)base.ErrorCode;
			SocketAddress socketAddress = null;
			if (socketError == SocketError.Success)
			{
				this.m_LocalBytesTransferred = numBytes;
				if (Logging.On)
				{
					this.LogBuffer((long)numBytes);
				}
				socketAddress = this.m_ListenSocket.m_RightEndPoint.Serialize();
				try
				{
					IntPtr intPtr;
					int num;
					IntPtr source;
					this.m_ListenSocket.GetAcceptExSockaddrs(Marshal.UnsafeAddrOfPinnedArrayElement(this.m_Buffer, 0), this.m_Buffer.Length - this.m_AddressBufferLength * 2, this.m_AddressBufferLength, this.m_AddressBufferLength, out intPtr, out num, out source, out socketAddress.m_Size);
					Marshal.Copy(source, socketAddress.m_Buffer, 0, socketAddress.m_Size);
					IntPtr intPtr2 = this.m_ListenSocket.SafeHandle.DangerousGetHandle();
					socketError = UnsafeNclNativeMethods.OSSOCK.setsockopt(this.m_AcceptSocket.SafeHandle, SocketOptionLevel.Socket, SocketOptionName.UpdateAcceptContext, ref intPtr2, Marshal.SizeOf(intPtr2));
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
				return this.m_ListenSocket.UpdateAcceptSocket(this.m_AcceptSocket, this.m_ListenSocket.m_RightEndPoint.Create(socketAddress), false);
			}
			return null;
		}

		// Token: 0x0600221C RID: 8732 RVA: 0x000A3448 File Offset: 0x000A1648
		internal void SetUnmanagedStructures(byte[] buffer, int addressBufferLength)
		{
			base.SetUnmanagedStructures(buffer);
			this.m_AddressBufferLength = addressBufferLength;
			this.m_Buffer = buffer;
		}

		// Token: 0x0600221D RID: 8733 RVA: 0x000A3460 File Offset: 0x000A1660
		private void LogBuffer(long size)
		{
			IntPtr intPtr = Marshal.UnsafeAddrOfPinnedArrayElement(this.m_Buffer, 0);
			if (intPtr != IntPtr.Zero)
			{
				if (size > -1L)
				{
					Logging.Dump(Logging.Sockets, this.m_ListenSocket, "PostCompletion", intPtr, (int)Math.Min(size, (long)this.m_Buffer.Length));
					return;
				}
				Logging.Dump(Logging.Sockets, this.m_ListenSocket, "PostCompletion", intPtr, this.m_Buffer.Length);
			}
		}

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x0600221E RID: 8734 RVA: 0x000A34D1 File Offset: 0x000A16D1
		internal byte[] Buffer
		{
			get
			{
				return this.m_Buffer;
			}
		}

		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x0600221F RID: 8735 RVA: 0x000A34D9 File Offset: 0x000A16D9
		internal int BytesTransferred
		{
			get
			{
				return this.m_LocalBytesTransferred;
			}
		}

		// Token: 0x170008C1 RID: 2241
		// (set) Token: 0x06002220 RID: 8736 RVA: 0x000A34E1 File Offset: 0x000A16E1
		internal Socket AcceptSocket
		{
			set
			{
				this.m_AcceptSocket = value;
			}
		}

		// Token: 0x04001F5E RID: 8030
		private int m_LocalBytesTransferred;

		// Token: 0x04001F5F RID: 8031
		private Socket m_ListenSocket;

		// Token: 0x04001F60 RID: 8032
		private Socket m_AcceptSocket;

		// Token: 0x04001F61 RID: 8033
		private int m_AddressBufferLength;

		// Token: 0x04001F62 RID: 8034
		private byte[] m_Buffer;
	}
}
