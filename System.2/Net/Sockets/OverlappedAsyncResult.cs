using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Net.Sockets
{
	// Token: 0x0200039C RID: 924
	internal class OverlappedAsyncResult : BaseOverlappedAsyncResult
	{
		// Token: 0x06002283 RID: 8835 RVA: 0x000A495D File Offset: 0x000A2B5D
		internal OverlappedAsyncResult(Socket socket, object asyncState, AsyncCallback asyncCallback) : base(socket, asyncState, asyncCallback)
		{
		}

		// Token: 0x06002284 RID: 8836 RVA: 0x000A4968 File Offset: 0x000A2B68
		internal IntPtr GetSocketAddressPtr()
		{
			return Marshal.UnsafeAddrOfPinnedArrayElement(this.m_SocketAddress.m_Buffer, 0);
		}

		// Token: 0x06002285 RID: 8837 RVA: 0x000A497B File Offset: 0x000A2B7B
		internal IntPtr GetSocketAddressSizePtr()
		{
			return Marshal.UnsafeAddrOfPinnedArrayElement(this.m_SocketAddress.m_Buffer, this.m_SocketAddress.GetAddressSizeOffset());
		}

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x06002286 RID: 8838 RVA: 0x000A4998 File Offset: 0x000A2B98
		internal SocketAddress SocketAddress
		{
			get
			{
				return this.m_SocketAddress;
			}
		}

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x06002287 RID: 8839 RVA: 0x000A49A0 File Offset: 0x000A2BA0
		// (set) Token: 0x06002288 RID: 8840 RVA: 0x000A49A8 File Offset: 0x000A2BA8
		internal SocketAddress SocketAddressOriginal
		{
			get
			{
				return this.m_SocketAddressOriginal;
			}
			set
			{
				this.m_SocketAddressOriginal = value;
			}
		}

		// Token: 0x06002289 RID: 8841 RVA: 0x000A49B4 File Offset: 0x000A2BB4
		internal void SetUnmanagedStructures(byte[] buffer, int offset, int size, SocketAddress socketAddress, bool pinSocketAddress)
		{
			this.m_SocketAddress = socketAddress;
			if (pinSocketAddress && this.m_SocketAddress != null)
			{
				object[] array = new object[2];
				array[0] = buffer;
				this.m_SocketAddress.CopyAddressSizeIntoBuffer();
				array[1] = this.m_SocketAddress.m_Buffer;
				base.SetUnmanagedStructures(array);
			}
			else
			{
				base.SetUnmanagedStructures(buffer);
			}
			this.m_SingleBuffer.Length = size;
			this.m_SingleBuffer.Pointer = Marshal.UnsafeAddrOfPinnedArrayElement(buffer, offset);
		}

		// Token: 0x0600228A RID: 8842 RVA: 0x000A4A29 File Offset: 0x000A2C29
		internal void SetUnmanagedStructures(byte[] buffer, int offset, int size, SocketAddress socketAddress, bool pinSocketAddress, ref OverlappedCache overlappedCache)
		{
			base.SetupCache(ref overlappedCache);
			this.SetUnmanagedStructures(buffer, offset, size, socketAddress, pinSocketAddress);
		}

		// Token: 0x0600228B RID: 8843 RVA: 0x000A4A40 File Offset: 0x000A2C40
		internal void SetUnmanagedStructures(BufferOffsetSize[] buffers)
		{
			this.m_WSABuffers = new WSABuffer[buffers.Length];
			object[] array = new object[buffers.Length];
			for (int i = 0; i < buffers.Length; i++)
			{
				array[i] = buffers[i].Buffer;
			}
			base.SetUnmanagedStructures(array);
			for (int j = 0; j < buffers.Length; j++)
			{
				this.m_WSABuffers[j].Length = buffers[j].Size;
				this.m_WSABuffers[j].Pointer = Marshal.UnsafeAddrOfPinnedArrayElement(buffers[j].Buffer, buffers[j].Offset);
			}
		}

		// Token: 0x0600228C RID: 8844 RVA: 0x000A4AD1 File Offset: 0x000A2CD1
		internal void SetUnmanagedStructures(BufferOffsetSize[] buffers, ref OverlappedCache overlappedCache)
		{
			base.SetupCache(ref overlappedCache);
			this.SetUnmanagedStructures(buffers);
		}

		// Token: 0x0600228D RID: 8845 RVA: 0x000A4AE4 File Offset: 0x000A2CE4
		internal void SetUnmanagedStructures(IList<ArraySegment<byte>> buffers)
		{
			int count = buffers.Count;
			ArraySegment<byte>[] array = new ArraySegment<byte>[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = buffers[i];
				ValidationHelper.ValidateSegment(array[i]);
			}
			this.m_WSABuffers = new WSABuffer[count];
			object[] array2 = new object[count];
			for (int j = 0; j < count; j++)
			{
				array2[j] = array[j].Array;
			}
			base.SetUnmanagedStructures(array2);
			for (int k = 0; k < count; k++)
			{
				this.m_WSABuffers[k].Length = array[k].Count;
				this.m_WSABuffers[k].Pointer = Marshal.UnsafeAddrOfPinnedArrayElement(array[k].Array, array[k].Offset);
			}
		}

		// Token: 0x0600228E RID: 8846 RVA: 0x000A4BC0 File Offset: 0x000A2DC0
		internal void SetUnmanagedStructures(IList<ArraySegment<byte>> buffers, ref OverlappedCache overlappedCache)
		{
			base.SetupCache(ref overlappedCache);
			this.SetUnmanagedStructures(buffers);
		}

		// Token: 0x0600228F RID: 8847 RVA: 0x000A4BD0 File Offset: 0x000A2DD0
		internal override object PostCompletion(int numBytes)
		{
			if (base.ErrorCode == 0 && Logging.On)
			{
				this.LogBuffer(numBytes);
			}
			return numBytes;
		}

		// Token: 0x06002290 RID: 8848 RVA: 0x000A4BF0 File Offset: 0x000A2DF0
		private void LogBuffer(int size)
		{
			if (size > -1)
			{
				if (this.m_WSABuffers != null)
				{
					foreach (WSABuffer wsabuffer in this.m_WSABuffers)
					{
						Logging.Dump(Logging.Sockets, base.AsyncObject, "PostCompletion", wsabuffer.Pointer, Math.Min(wsabuffer.Length, size));
						if ((size -= wsabuffer.Length) <= 0)
						{
							return;
						}
					}
					return;
				}
				Logging.Dump(Logging.Sockets, base.AsyncObject, "PostCompletion", this.m_SingleBuffer.Pointer, Math.Min(this.m_SingleBuffer.Length, size));
			}
		}

		// Token: 0x04001F88 RID: 8072
		private SocketAddress m_SocketAddress;

		// Token: 0x04001F89 RID: 8073
		private SocketAddress m_SocketAddressOriginal;

		// Token: 0x04001F8A RID: 8074
		internal WSABuffer m_SingleBuffer;

		// Token: 0x04001F8B RID: 8075
		internal WSABuffer[] m_WSABuffers;
	}
}
