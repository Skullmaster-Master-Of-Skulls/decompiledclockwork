using System;
using System.Runtime.InteropServices;

namespace System.Net.Sockets
{
	// Token: 0x020005D4 RID: 1492
	internal class ReceiveMessageOverlappedAsyncResult : BaseOverlappedAsyncResult
	{
		// Token: 0x06002EE4 RID: 12004 RVA: 0x000CEAE6 File Offset: 0x000CDAE6
		internal ReceiveMessageOverlappedAsyncResult(Socket socket, object asyncState, AsyncCallback asyncCallback) : base(socket, asyncState, asyncCallback)
		{
		}

		// Token: 0x06002EE5 RID: 12005 RVA: 0x000CEAF1 File Offset: 0x000CDAF1
		internal IntPtr GetSocketAddressSizePtr()
		{
			return Marshal.UnsafeAddrOfPinnedArrayElement(this.m_SocketAddress.m_Buffer, this.m_SocketAddress.GetAddressSizeOffset());
		}

		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x06002EE6 RID: 12006 RVA: 0x000CEB0E File Offset: 0x000CDB0E
		internal SocketAddress SocketAddress
		{
			get
			{
				return this.m_SocketAddress;
			}
		}

		// Token: 0x06002EE7 RID: 12007 RVA: 0x000CEB18 File Offset: 0x000CDB18
		internal unsafe void SetUnmanagedStructures(byte[] buffer, int offset, int size, SocketAddress socketAddress, SocketFlags socketFlags)
		{
			bool flag = ((Socket)base.AsyncObject).AddressFamily == AddressFamily.InterNetwork;
			bool flag2 = ((Socket)base.AsyncObject).AddressFamily == AddressFamily.InterNetworkV6;
			this.m_MessageBuffer = new byte[ReceiveMessageOverlappedAsyncResult.s_WSAMsgSize];
			this.m_WSABufferArray = new byte[ReceiveMessageOverlappedAsyncResult.s_WSABufferSize];
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

		// Token: 0x06002EE8 RID: 12008 RVA: 0x000CECE1 File Offset: 0x000CDCE1
		internal void SetUnmanagedStructures(byte[] buffer, int offset, int size, SocketAddress socketAddress, SocketFlags socketFlags, ref OverlappedCache overlappedCache)
		{
			base.SetupCache(ref overlappedCache);
			this.SetUnmanagedStructures(buffer, offset, size, socketAddress, socketFlags);
		}

		// Token: 0x06002EE9 RID: 12009 RVA: 0x000CECF8 File Offset: 0x000CDCF8
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

		// Token: 0x06002EEA RID: 12010 RVA: 0x000CEDF8 File Offset: 0x000CDDF8
		internal void SyncReleaseUnmanagedStructures()
		{
			this.InitIPPacketInformation();
			this.ForceReleaseUnmanagedStructures();
		}

		// Token: 0x06002EEB RID: 12011 RVA: 0x000CEE06 File Offset: 0x000CDE06
		protected unsafe override void ForceReleaseUnmanagedStructures()
		{
			this.m_flags = this.m_Message->flags;
			base.ForceReleaseUnmanagedStructures();
		}

		// Token: 0x06002EEC RID: 12012 RVA: 0x000CEE1F File Offset: 0x000CDE1F
		internal override object PostCompletion(int numBytes)
		{
			this.InitIPPacketInformation();
			if (base.ErrorCode == 0 && Logging.On)
			{
				this.LogBuffer(numBytes);
			}
			return numBytes;
		}

		// Token: 0x06002EED RID: 12013 RVA: 0x000CEE43 File Offset: 0x000CDE43
		private unsafe void LogBuffer(int size)
		{
			Logging.Dump(Logging.Sockets, base.AsyncObject, "PostCompletion", this.m_WSABuffer->Pointer, Math.Min(this.m_WSABuffer->Length, size));
		}

		// Token: 0x04002C5D RID: 11357
		private unsafe UnsafeNclNativeMethods.OSSOCK.WSAMsg* m_Message;

		// Token: 0x04002C5E RID: 11358
		internal SocketAddress SocketAddressOriginal;

		// Token: 0x04002C5F RID: 11359
		internal SocketAddress m_SocketAddress;

		// Token: 0x04002C60 RID: 11360
		private unsafe WSABuffer* m_WSABuffer;

		// Token: 0x04002C61 RID: 11361
		private byte[] m_WSABufferArray;

		// Token: 0x04002C62 RID: 11362
		private byte[] m_ControlBuffer;

		// Token: 0x04002C63 RID: 11363
		internal byte[] m_MessageBuffer;

		// Token: 0x04002C64 RID: 11364
		internal SocketFlags m_flags;

		// Token: 0x04002C65 RID: 11365
		private static readonly int s_ControlDataSize = Marshal.SizeOf(typeof(UnsafeNclNativeMethods.OSSOCK.ControlData));

		// Token: 0x04002C66 RID: 11366
		private static readonly int s_ControlDataIPv6Size = Marshal.SizeOf(typeof(UnsafeNclNativeMethods.OSSOCK.ControlDataIPv6));

		// Token: 0x04002C67 RID: 11367
		private static readonly int s_WSABufferSize = Marshal.SizeOf(typeof(WSABuffer));

		// Token: 0x04002C68 RID: 11368
		private static readonly int s_WSAMsgSize = Marshal.SizeOf(typeof(UnsafeNclNativeMethods.OSSOCK.WSAMsg));

		// Token: 0x04002C69 RID: 11369
		internal IPPacketInformation m_IPPacketInformation;
	}
}
