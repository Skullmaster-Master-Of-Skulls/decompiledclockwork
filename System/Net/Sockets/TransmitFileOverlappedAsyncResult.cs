using System;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Net.Sockets
{
	// Token: 0x020005D3 RID: 1491
	internal class TransmitFileOverlappedAsyncResult : BaseOverlappedAsyncResult
	{
		// Token: 0x06002EDC RID: 11996 RVA: 0x000CE981 File Offset: 0x000CD981
		internal TransmitFileOverlappedAsyncResult(Socket socket, object asyncState, AsyncCallback asyncCallback) : base(socket, asyncState, asyncCallback)
		{
		}

		// Token: 0x06002EDD RID: 11997 RVA: 0x000CE98C File Offset: 0x000CD98C
		internal TransmitFileOverlappedAsyncResult(Socket socket) : base(socket)
		{
		}

		// Token: 0x06002EDE RID: 11998 RVA: 0x000CE998 File Offset: 0x000CD998
		internal void SetUnmanagedStructures(byte[] preBuffer, byte[] postBuffer, FileStream fileStream, TransmitFileOptions flags, bool sync)
		{
			this.m_fileStream = fileStream;
			this.m_flags = flags;
			this.m_buffers = null;
			int num = 0;
			if (preBuffer != null && preBuffer.Length > 0)
			{
				num++;
			}
			if (postBuffer != null && postBuffer.Length > 0)
			{
				num++;
			}
			if (num != 0)
			{
				num++;
				object[] array = new object[num];
				this.m_buffers = new TransmitFileBuffers();
				array[--num] = this.m_buffers;
				if (preBuffer != null && preBuffer.Length > 0)
				{
					this.m_buffers.preBufferLength = preBuffer.Length;
					array[--num] = preBuffer;
				}
				if (postBuffer != null && postBuffer.Length > 0)
				{
					this.m_buffers.postBufferLength = postBuffer.Length;
					array[num - 1] = postBuffer;
				}
				if (sync)
				{
					base.PinUnmanagedObjects(array);
				}
				else
				{
					base.SetUnmanagedStructures(array);
				}
				if (preBuffer != null && preBuffer.Length > 0)
				{
					this.m_buffers.preBuffer = Marshal.UnsafeAddrOfPinnedArrayElement(preBuffer, 0);
				}
				if (postBuffer != null && postBuffer.Length > 0)
				{
					this.m_buffers.postBuffer = Marshal.UnsafeAddrOfPinnedArrayElement(postBuffer, 0);
					return;
				}
			}
			else if (!sync)
			{
				base.SetUnmanagedStructures(null);
			}
		}

		// Token: 0x06002EDF RID: 11999 RVA: 0x000CEA96 File Offset: 0x000CDA96
		internal void SetUnmanagedStructures(byte[] preBuffer, byte[] postBuffer, FileStream fileStream, TransmitFileOptions flags, ref OverlappedCache overlappedCache)
		{
			base.SetupCache(ref overlappedCache);
			this.SetUnmanagedStructures(preBuffer, postBuffer, fileStream, flags, false);
		}

		// Token: 0x06002EE0 RID: 12000 RVA: 0x000CEAAC File Offset: 0x000CDAAC
		protected override void ForceReleaseUnmanagedStructures()
		{
			if (this.m_fileStream != null)
			{
				this.m_fileStream.Close();
				this.m_fileStream = null;
			}
			base.ForceReleaseUnmanagedStructures();
		}

		// Token: 0x06002EE1 RID: 12001 RVA: 0x000CEACE File Offset: 0x000CDACE
		internal void SyncReleaseUnmanagedStructures()
		{
			this.ForceReleaseUnmanagedStructures();
		}

		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x06002EE2 RID: 12002 RVA: 0x000CEAD6 File Offset: 0x000CDAD6
		internal TransmitFileBuffers TransmitFileBuffers
		{
			get
			{
				return this.m_buffers;
			}
		}

		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x06002EE3 RID: 12003 RVA: 0x000CEADE File Offset: 0x000CDADE
		internal TransmitFileOptions Flags
		{
			get
			{
				return this.m_flags;
			}
		}

		// Token: 0x04002C5A RID: 11354
		private FileStream m_fileStream;

		// Token: 0x04002C5B RID: 11355
		private TransmitFileOptions m_flags;

		// Token: 0x04002C5C RID: 11356
		private TransmitFileBuffers m_buffers;
	}
}
