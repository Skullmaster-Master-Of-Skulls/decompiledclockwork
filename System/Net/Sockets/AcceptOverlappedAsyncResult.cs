using System;
using System.Runtime.InteropServices;

namespace System.Net.Sockets
{
	// Token: 0x020005CE RID: 1486
	internal class AcceptOverlappedAsyncResult : BaseOverlappedAsyncResult
	{
		// Token: 0x06002EB8 RID: 11960 RVA: 0x000CE22A File Offset: 0x000CD22A
		internal AcceptOverlappedAsyncResult(Socket listenSocket, object asyncState, AsyncCallback asyncCallback) : base(listenSocket, asyncState, asyncCallback)
		{
			this.m_ListenSocket = listenSocket;
		}

		// Token: 0x06002EB9 RID: 11961 RVA: 0x000CE23C File Offset: 0x000CD23C
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
				IntPtr intPtr;
				int num;
				IntPtr source;
				UnsafeNclNativeMethods.OSSOCK.GetAcceptExSockaddrs(Marshal.UnsafeAddrOfPinnedArrayElement(this.m_Buffer, 0), this.m_Buffer.Length - this.m_AddressBufferLength * 2, this.m_AddressBufferLength, this.m_AddressBufferLength, out intPtr, out num, out source, out socketAddress.m_Size);
				Marshal.Copy(source, socketAddress.m_Buffer, 0, socketAddress.m_Size);
				try
				{
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

		// Token: 0x06002EBA RID: 11962 RVA: 0x000CE35C File Offset: 0x000CD35C
		internal void SetUnmanagedStructures(byte[] buffer, int addressBufferLength)
		{
			base.SetUnmanagedStructures(buffer);
			this.m_AddressBufferLength = addressBufferLength;
			this.m_Buffer = buffer;
		}

		// Token: 0x06002EBB RID: 11963 RVA: 0x000CE374 File Offset: 0x000CD374
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

		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x06002EBC RID: 11964 RVA: 0x000CE3E5 File Offset: 0x000CD3E5
		internal byte[] Buffer
		{
			get
			{
				return this.m_Buffer;
			}
		}

		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x06002EBD RID: 11965 RVA: 0x000CE3ED File Offset: 0x000CD3ED
		internal int BytesTransferred
		{
			get
			{
				return this.m_LocalBytesTransferred;
			}
		}

		// Token: 0x170009CE RID: 2510
		// (set) Token: 0x06002EBE RID: 11966 RVA: 0x000CE3F5 File Offset: 0x000CD3F5
		internal Socket AcceptSocket
		{
			set
			{
				this.m_AcceptSocket = value;
			}
		}

		// Token: 0x04002C4C RID: 11340
		private int m_LocalBytesTransferred;

		// Token: 0x04002C4D RID: 11341
		private Socket m_ListenSocket;

		// Token: 0x04002C4E RID: 11342
		private Socket m_AcceptSocket;

		// Token: 0x04002C4F RID: 11343
		private int m_AddressBufferLength;

		// Token: 0x04002C50 RID: 11344
		private byte[] m_Buffer;
	}
}
