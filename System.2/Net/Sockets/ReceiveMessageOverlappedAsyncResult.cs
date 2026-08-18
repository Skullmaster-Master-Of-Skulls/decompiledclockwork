using System;
using System.Runtime.InteropServices;

namespace System.Net.Sockets
{
	// Token: 0x0200039E RID: 926
	internal class ReceiveMessageOverlappedAsyncResult : BaseOverlappedAsyncResult
	{
		// Token: 0x06002299 RID: 8857 RVA: 0x000A4DE6 File Offset: 0x000A2FE6
		internal ReceiveMessageOverlappedAsyncResult(Socket socket, object asyncState, AsyncCallback asyncCallback) : base(socket, asyncState, asyncCallback)
		{
		}

		// Token: 0x0600229A RID: 8858 RVA: 0x000A4DF1 File Offset: 0x000A2FF1
		internal IntPtr GetSocketAddressSizePtr()
		{
			return Marshal.UnsafeAddrOfPinnedArrayElement(this.m_SocketAddress.m_Buffer, this.m_SocketAddress.GetAddressSizeOffset());
		}

		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x0600229B RID: 8859 RVA: 0x000A4E0E File Offset: 0x000A300E
		internal SocketAddress SocketAddress
		{
			get
			{
				return this.m_SocketAddress;
			}
		}

		// Token: 0x0600229C RID: 8860 RVA: 0x000A4E18 File Offset: 0x000A3018
		internal unsafe void SetUnmanagedStructures(byte[] buffer, int offset, int size, SocketAddress socketAddress, SocketFlags socketFlags)
		{
			this.m_MessageBuffer = new byte[ReceiveMessageOverlappedAsyncResult.s_WSAMsgSize];
			this.m_WSABufferArray = new byte[ReceiveMessageOverlappedAsyncResult.s_WSABufferSize];
			IPAddress ipaddress = (socketAddress.Family == AddressFamily.InterNetworkV6) ? socketAddress.GetIPAddress() : null;
			bool flag = ((Socket)base.AsyncObject).AddressFamily == AddressFamily.InterNetwork || (ipaddress != null && ipaddress.IsIPv4MappedToIPv6);
			bool flag2 = ((Socket)base.AsyncObject).AddressFamily == AddressFamily.InterNetworkV6;
			if (flag)
			{
				this.m_ControlBuffer = new byte[ReceiveMessageOverlappedAsyncResult.s_ControlDataSize];
			}
			else if (flag2)
			{
				this.m_ControlBuffer = new byte[ReceiveMessageOverlappedAsyncResult.s_ControlDataIPv6Size];
			}
			object[] array = new object[(this.m_ControlBuffer != null) ? 5 : 4];
			array[0] = buffer;
			array[1] = this.m_MessageBuffer;
			array[2] = this.m_WSABufferArray;
			this.m_SocketAddress = socketAddress;
			this.m_SocketAddress.CopyAddressSizeIntoBuffer();
			array[3] = this.m_SocketAddress.m_Buffer;
			if (this.m_ControlBuffer != null)
			{
				array[4] = this.m_ControlBuffer;
			}
			base.SetUnmanagedStructures(array);
			this.m_WSABuffer = (WSABuffer*)((void*)Marshal.UnsafeAddrOfPinnedArrayElement(this.m_WSABufferArray, 0));
			this.m_WSABuffer->Length = size;
			this.m_WSABuffer->Pointer = Marshal.UnsafeAddrOfPinnedArrayElement(buffer, offset);
			this.m_Message = (UnsafeNclNativeMethods.OSSOCK.WSAMsg*)((void*)Marshal.UnsafeAddrOfPinnedArrayElement(this.m_MessageBuffer, 0));
			this.m_Message->socketAddress = Marshal.UnsafeAddrOfPinnedArrayElement(this.m_SocketAddress.m_Buffer, 0);
			this.m_Message->addressLength = (uint)this.m_SocketAddress.Size;
			this.m_Message->buffers = Marshal.UnsafeAddrOfPinnedArrayElement(this.m_WSABufferArray, 0);
			this.m_Message->count = 1U;
			if (this.m_ControlBuffer != null)
			{
				this.m_Message->controlBuffer.Pointer = Marshal.UnsafeAddrOfPinnedArrayElement(this.m_ControlBuffer, 0);
				this.m_Message->controlBuffer.Length = this.m_ControlBuffer.Length;
			}
			this.m_Message->flags = socketFlags;
		}

		// Token: 0x0600229D RID: 8861 RVA: 0x000A5006 File Offset: 0x000A3206
		internal void SetUnmanagedStructures(byte[] buffer, int offset, int size, SocketAddress socketAddress, SocketFlags socketFlags, ref OverlappedCache overlappedCache)
		{
			base.SetupCache(ref overlappedCache);
			this.SetUnmanagedStructures(buffer, offset, size, socketAddress, socketFlags);
		}

		// Token: 0x0600229E RID: 8862 RVA: 0x000A5020 File Offset: 0x000A3220
		private unsafe void InitIPPacketInformation()
		{
			IPAddress ipaddress = null;
			if (this.m_ControlBuffer.Length == ReceiveMessageOverlappedAsyncResult.s_ControlDataSize)
			{
				UnsafeNclNativeMethods.OSSOCK.ControlData controlData = (UnsafeNclNativeMethods.OSSOCK.ControlData)Marshal.PtrToStructure(this.m_Message->controlBuffer.Pointer, typeof(UnsafeNclNativeMethods.OSSOCK.ControlData));
				if (controlData.length != UIntPtr.Zero)
				{
					ipaddress = new IPAddress((long)((ulong)controlData.address));
				}
				this.m_IPPacketInformation = new IPPacketInformation((ipaddress != null) ? ipaddress : IPAddress.None, (int)controlData.index);
				return;
			}
			if (this.m_ControlBuffer.Length == ReceiveMessageOverlappedAsyncResult.s_ControlDataIPv6Size)
			{
				UnsafeNclNativeMethods.OSSOCK.ControlDataIPv6 controlDataIPv = (UnsafeNclNativeMethods.OSSOCK.ControlDataIPv6)Marshal.PtrToStructure(this.m_Message->controlBuffer.Pointer, typeof(UnsafeNclNativeMethods.OSSOCK.ControlDataIPv6));
				if (controlDataIPv.length != UIntPtr.Zero)
				{
					ipaddress = new IPAddress(controlDataIPv.address);
				}
				this.m_IPPacketInformation = new IPPacketInformation((ipaddress != null) ? ipaddress : IPAddress.IPv6None, (int)controlDataIPv.index);
				return;
			}
			this.m_IPPacketInformation = default(IPPacketInformation);
		}

		// Token: 0x0600229F RID: 8863 RVA: 0x000A511A File Offset: 0x000A331A
		internal void SyncReleaseUnmanagedStructures()
		{
			this.InitIPPacketInformation();
			this.ForceReleaseUnmanagedStructures();
		}

		// Token: 0x060022A0 RID: 8864 RVA: 0x000A5128 File Offset: 0x000A3328
		protected unsafe override void ForceReleaseUnmanagedStructures()
		{
			this.m_flags = this.m_Message->flags;
			base.ForceReleaseUnmanagedStructures();
		}

		// Token: 0x060022A1 RID: 8865 RVA: 0x000A5141 File Offset: 0x000A3341
		internal override object PostCompletion(int numBytes)
		{
			this.InitIPPacketInformation();
			if (base.ErrorCode == 0 && Logging.On)
			{
				this.LogBuffer(numBytes);
			}
			return numBytes;
		}

		// Token: 0x060022A2 RID: 8866 RVA: 0x000A5165 File Offset: 0x000A3365
		private unsafe void LogBuffer(int size)
		{
			Logging.Dump(Logging.Sockets, base.AsyncObject, "PostCompletion", this.m_WSABuffer->Pointer, Math.Min(this.m_WSABuffer->Length, size));
		}

		// Token: 0x04001F8F RID: 8079
		private unsafe UnsafeNclNativeMethods.OSSOCK.WSAMsg* m_Message;

		// Token: 0x04001F90 RID: 8080
		internal SocketAddress SocketAddressOriginal;

		// Token: 0x04001F91 RID: 8081
		internal SocketAddress m_SocketAddress;

		// Token: 0x04001F92 RID: 8082
		private unsafe WSABuffer* m_WSABuffer;

		// Token: 0x04001F93 RID: 8083
		private byte[] m_WSABufferArray;

		// Token: 0x04001F94 RID: 8084
		private byte[] m_ControlBuffer;

		// Token: 0x04001F95 RID: 8085
		internal byte[] m_MessageBuffer;

		// Token: 0x04001F96 RID: 8086
		internal SocketFlags m_flags;

		// Token: 0x04001F97 RID: 8087
		private static readonly int s_ControlDataSize = Marshal.SizeOf(typeof(UnsafeNclNativeMethods.OSSOCK.ControlData));

		// Token: 0x04001F98 RID: 8088
		private static readonly int s_ControlDataIPv6Size = Marshal.SizeOf(typeof(UnsafeNclNativeMethods.OSSOCK.ControlDataIPv6));

		// Token: 0x04001F99 RID: 8089
		private static readonly int s_WSABufferSize = Marshal.SizeOf(typeof(WSABuffer));

		// Token: 0x04001F9A RID: 8090
		private static readonly int s_WSAMsgSize = Marshal.SizeOf(typeof(UnsafeNclNativeMethods.OSSOCK.WSAMsg));

		// Token: 0x04001F9B RID: 8091
		internal IPPacketInformation m_IPPacketInformation;
	}
}
