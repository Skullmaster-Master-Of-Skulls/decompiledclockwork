using System;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Net.Sockets
{
	// Token: 0x0200039D RID: 925
	internal class TransmitFileOverlappedAsyncResult : BaseOverlappedAsyncResult
	{
		// Token: 0x06002291 RID: 8849 RVA: 0x000A4C90 File Offset: 0x000A2E90
		internal TransmitFileOverlappedAsyncResult(Socket socket, object asyncState, AsyncCallback asyncCallback) : base(socket, asyncState, asyncCallback)
		{
		}

		// Token: 0x06002292 RID: 8850 RVA: 0x000A4C9B File Offset: 0x000A2E9B
		internal TransmitFileOverlappedAsyncResult(Socket socket) : base(socket)
		{
		}

		// Token: 0x06002293 RID: 8851 RVA: 0x000A4CA4 File Offset: 0x000A2EA4
		internal void SetUnmanagedStructures(byte[] preBuffer, byte[] postBuffer, FileStream fileStream, TransmitFileOptions flags, bool sync)
		{
			this.m_fileStream = fileStream;
			this.m_flags = flags;
			this.m_buffers = null;
			int num = 0;
			if (preBuffer != null && preBuffer.Length != 0)
			{
				num++;
			}
			if (postBuffer != null && postBuffer.Length != 0)
			{
				num++;
			}
			if (num != 0)
			{
				num++;
				object[] array = new object[num];
				this.m_buffers = new TransmitFileBuffers();
				array[--num] = this.m_buffers;
				if (preBuffer != null && preBuffer.Length != 0)
				{
					this.m_buffers.preBufferLength = preBuffer.Length;
					array[--num] = preBuffer;
				}
				if (postBuffer != null && postBuffer.Length != 0)
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
				if (preBuffer != null && preBuffer.Length != 0)
				{
					this.m_buffers.preBuffer = Marshal.UnsafeAddrOfPinnedArrayElement(preBuffer, 0);
				}
				if (postBuffer != null && postBuffer.Length != 0)
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

		// Token: 0x06002294 RID: 8852 RVA: 0x000A4D96 File Offset: 0x000A2F96
		internal void SetUnmanagedStructures(byte[] preBuffer, byte[] postBuffer, FileStream fileStream, TransmitFileOptions flags, ref OverlappedCache overlappedCache)
		{
			base.SetupCache(ref overlappedCache);
			this.SetUnmanagedStructures(preBuffer, postBuffer, fileStream, flags, false);
		}

		// Token: 0x06002295 RID: 8853 RVA: 0x000A4DAC File Offset: 0x000A2FAC
		protected override void ForceReleaseUnmanagedStructures()
		{
			if (this.m_fileStream != null)
			{
				this.m_fileStream.Close();
				this.m_fileStream = null;
			}
			base.ForceReleaseUnmanagedStructures();
		}

		// Token: 0x06002296 RID: 8854 RVA: 0x000A4DCE File Offset: 0x000A2FCE
		internal void SyncReleaseUnmanagedStructures()
		{
			this.ForceReleaseUnmanagedStructures();
		}

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x06002297 RID: 8855 RVA: 0x000A4DD6 File Offset: 0x000A2FD6
		internal TransmitFileBuffers TransmitFileBuffers
		{
			get
			{
				return this.m_buffers;
			}
		}

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x06002298 RID: 8856 RVA: 0x000A4DDE File Offset: 0x000A2FDE
		internal TransmitFileOptions Flags
		{
			get
			{
				return this.m_flags;
			}
		}

		// Token: 0x04001F8C RID: 8076
		private FileStream m_fileStream;

		// Token: 0x04001F8D RID: 8077
		private TransmitFileOptions m_flags;

		// Token: 0x04001F8E RID: 8078
		private TransmitFileBuffers m_buffers;
	}
}
