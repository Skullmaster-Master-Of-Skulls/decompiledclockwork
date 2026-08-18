using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Net.Sockets
{
	// Token: 0x020005D2 RID: 1490
	internal class OverlappedAsyncResult : BaseOverlappedAsyncResult
	{
		// Token: 0x06002ECE RID: 11982 RVA: 0x000CE63D File Offset: 0x000CD63D
		internal OverlappedAsyncResult(Socket socket, object asyncState, AsyncCallback asyncCallback) : base(socket, asyncState, asyncCallback)
		{
		}

		// Token: 0x06002ECF RID: 11983 RVA: 0x000CE648 File Offset: 0x000CD648
		internal IntPtr GetSocketAddressPtr()
		{
			return Marshal.UnsafeAddrOfPinnedArrayElement(this.m_SocketAddress.m_Buffer, 0);
		}

		// Token: 0x06002ED0 RID: 11984 RVA: 0x000CE65B File Offset: 0x000CD65B
		internal IntPtr GetSocketAddressSizePtr()
		{
			return Marshal.UnsafeAddrOfPinnedArrayElement(this.m_SocketAddress.m_Buffer, this.m_SocketAddress.GetAddressSizeOffset());
		}

		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x06002ED1 RID: 11985 RVA: 0x000CE678 File Offset: 0x000CD678
		internal SocketAddress SocketAddress
		{
			get
			{
				return this.m_SocketAddress;
			}
		}

		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x06002ED2 RID: 11986 RVA: 0x000CE680 File Offset: 0x000CD680
		// (set) Token: 0x06002ED3 RID: 11987 RVA: 0x000CE688 File Offset: 0x000CD688
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

		// Token: 0x06002ED4 RID: 11988 RVA: 0x000CE694 File Offset: 0x000CD694
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

		// Token: 0x06002ED5 RID: 11989 RVA: 0x000CE709 File Offset: 0x000CD709
		internal void SetUnmanagedStructures(byte[] buffer, int offset, int size, SocketAddress socketAddress, bool pinSocketAddress, ref OverlappedCache overlappedCache)
		{
			base.SetupCache(ref overlappedCache);
			this.SetUnmanagedStructures(buffer, offset, size, socketAddress, pinSocketAddress);
		}

		// Token: 0x06002ED6 RID: 11990 RVA: 0x000CE720 File Offset: 0x000CD720
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

		// Token: 0x06002ED7 RID: 11991 RVA: 0x000CE7B1 File Offset: 0x000CD7B1
		internal void SetUnmanagedStructures(BufferOffsetSize[] buffers, ref OverlappedCache overlappedCache)
		{
			base.SetupCache(ref overlappedCache);
			this.SetUnmanagedStructures(buffers);
		}

		// Token: 0x06002ED8 RID: 11992 RVA: 0x000CE7C4 File Offset: 0x000CD7C4
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

		// Token: 0x06002ED9 RID: 11993 RVA: 0x000CE8AA File Offset: 0x000CD8AA
		internal void SetUnmanagedStructures(IList<ArraySegment<byte>> buffers, ref OverlappedCache overlappedCache)
		{
			base.SetupCache(ref overlappedCache);
			this.SetUnmanagedStructures(buffers);
		}

		// Token: 0x06002EDA RID: 11994 RVA: 0x000CE8BA File Offset: 0x000CD8BA
		internal override object PostCompletion(int numBytes)
		{
			if (base.ErrorCode == 0 && Logging.On)
			{
				this.LogBuffer(numBytes);
			}
			return numBytes;
		}

		// Token: 0x06002EDB RID: 11995 RVA: 0x000CE8D8 File Offset: 0x000CD8D8
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

		// Token: 0x04002C56 RID: 11350
		private SocketAddress m_SocketAddress;

		// Token: 0x04002C57 RID: 11351
		private SocketAddress m_SocketAddressOriginal;

		// Token: 0x04002C58 RID: 11352
		internal WSABuffer m_SingleBuffer;

		// Token: 0x04002C59 RID: 11353
		internal WSABuffer[] m_WSABuffers;
	}
}
