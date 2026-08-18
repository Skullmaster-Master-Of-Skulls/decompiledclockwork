using System;
using System.IO;

namespace System.Net.Cache
{
	// Token: 0x0200057F RID: 1407
	internal class RangeStream : Stream, ICloseEx
	{
		// Token: 0x06002B0A RID: 11018 RVA: 0x000B70C8 File Offset: 0x000B60C8
		internal RangeStream(Stream parentStream, long offset, long size)
		{
			this.m_ParentStream = parentStream;
			this.m_Offset = offset;
			this.m_Size = size;
			if (this.m_ParentStream.CanSeek)
			{
				this.m_ParentStream.Position = offset;
				this.m_Position = offset;
				return;
			}
			throw new NotSupportedException(SR.GetString("net_cache_non_seekable_stream_not_supported"));
		}

		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x06002B0B RID: 11019 RVA: 0x000B7120 File Offset: 0x000B6120
		public override bool CanRead
		{
			get
			{
				return this.m_ParentStream.CanRead;
			}
		}

		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x06002B0C RID: 11020 RVA: 0x000B712D File Offset: 0x000B612D
		public override bool CanSeek
		{
			get
			{
				return this.m_ParentStream.CanSeek;
			}
		}

		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x06002B0D RID: 11021 RVA: 0x000B713A File Offset: 0x000B613A
		public override bool CanWrite
		{
			get
			{
				return this.m_ParentStream.CanWrite;
			}
		}

		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x06002B0E RID: 11022 RVA: 0x000B7147 File Offset: 0x000B6147
		public override long Length
		{
			get
			{
				long length = this.m_ParentStream.Length;
				return this.m_Size;
			}
		}

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x06002B0F RID: 11023 RVA: 0x000B715B File Offset: 0x000B615B
		// (set) Token: 0x06002B10 RID: 11024 RVA: 0x000B716F File Offset: 0x000B616F
		public override long Position
		{
			get
			{
				return this.m_ParentStream.Position - this.m_Offset;
			}
			set
			{
				value += this.m_Offset;
				if (value > this.m_Offset + this.m_Size)
				{
					value = this.m_Offset + this.m_Size;
				}
				this.m_ParentStream.Position = value;
			}
		}

		// Token: 0x06002B11 RID: 11025 RVA: 0x000B71A8 File Offset: 0x000B61A8
		public override long Seek(long offset, SeekOrigin origin)
		{
			switch (origin)
			{
			case SeekOrigin.Begin:
				offset += this.m_Offset;
				if (offset > this.m_Offset + this.m_Size)
				{
					offset = this.m_Offset + this.m_Size;
				}
				if (offset < this.m_Offset)
				{
					offset = this.m_Offset;
					goto IL_D0;
				}
				goto IL_D0;
			case SeekOrigin.End:
				offset -= this.m_Offset + this.m_Size;
				if (offset > 0L)
				{
					offset = 0L;
				}
				if (offset < -this.m_Size)
				{
					offset = -this.m_Size;
					goto IL_D0;
				}
				goto IL_D0;
			}
			if (this.m_Position + offset > this.m_Offset + this.m_Size)
			{
				offset = this.m_Offset + this.m_Size - this.m_Position;
			}
			if (this.m_Position + offset < this.m_Offset)
			{
				offset = this.m_Offset - this.m_Position;
			}
			IL_D0:
			this.m_Position = this.m_ParentStream.Seek(offset, origin);
			return this.m_Position - this.m_Offset;
		}

		// Token: 0x06002B12 RID: 11026 RVA: 0x000B72A5 File Offset: 0x000B62A5
		public override void SetLength(long value)
		{
			throw new NotSupportedException(SR.GetString("net_cache_unsupported_partial_stream"));
		}

		// Token: 0x06002B13 RID: 11027 RVA: 0x000B72B8 File Offset: 0x000B62B8
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this.m_Position + (long)count > this.m_Offset + this.m_Size)
			{
				throw new NotSupportedException(SR.GetString("net_cache_unsupported_partial_stream"));
			}
			this.m_ParentStream.Write(buffer, offset, count);
			this.m_Position += (long)count;
		}

		// Token: 0x06002B14 RID: 11028 RVA: 0x000B730A File Offset: 0x000B630A
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (this.m_Position + (long)offset > this.m_Offset + this.m_Size)
			{
				throw new NotSupportedException(SR.GetString("net_cache_unsupported_partial_stream"));
			}
			return this.m_ParentStream.BeginWrite(buffer, offset, count, callback, state);
		}

		// Token: 0x06002B15 RID: 11029 RVA: 0x000B7346 File Offset: 0x000B6346
		public override void EndWrite(IAsyncResult asyncResult)
		{
			this.m_ParentStream.EndWrite(asyncResult);
			this.m_Position = this.m_ParentStream.Position;
		}

		// Token: 0x06002B16 RID: 11030 RVA: 0x000B7365 File Offset: 0x000B6365
		public override void Flush()
		{
			this.m_ParentStream.Flush();
		}

		// Token: 0x06002B17 RID: 11031 RVA: 0x000B7374 File Offset: 0x000B6374
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this.m_Position >= this.m_Offset + this.m_Size)
			{
				return 0;
			}
			if (this.m_Position + (long)count > this.m_Offset + this.m_Size)
			{
				count = (int)(this.m_Offset + this.m_Size - this.m_Position);
			}
			int num = this.m_ParentStream.Read(buffer, offset, count);
			this.m_Position += (long)num;
			return num;
		}

		// Token: 0x06002B18 RID: 11032 RVA: 0x000B73E8 File Offset: 0x000B63E8
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (this.m_Position >= this.m_Offset + this.m_Size)
			{
				count = 0;
			}
			else if (this.m_Position + (long)count > this.m_Offset + this.m_Size)
			{
				count = (int)(this.m_Offset + this.m_Size - this.m_Position);
			}
			return this.m_ParentStream.BeginRead(buffer, offset, count, callback, state);
		}

		// Token: 0x06002B19 RID: 11033 RVA: 0x000B7450 File Offset: 0x000B6450
		public override int EndRead(IAsyncResult asyncResult)
		{
			int num = this.m_ParentStream.EndRead(asyncResult);
			this.m_Position += (long)num;
			return num;
		}

		// Token: 0x06002B1A RID: 11034 RVA: 0x000B747A File Offset: 0x000B647A
		protected sealed override void Dispose(bool disposing)
		{
			this.Dispose(disposing, CloseExState.Normal);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002B1B RID: 11035 RVA: 0x000B748A File Offset: 0x000B648A
		void ICloseEx.CloseEx(CloseExState closeState)
		{
			this.Dispose(true, closeState);
			GC.SuppressFinalize(this);
		}

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x06002B1C RID: 11036 RVA: 0x000B749A File Offset: 0x000B649A
		public override bool CanTimeout
		{
			get
			{
				return this.m_ParentStream.CanTimeout;
			}
		}

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x06002B1D RID: 11037 RVA: 0x000B74A7 File Offset: 0x000B64A7
		// (set) Token: 0x06002B1E RID: 11038 RVA: 0x000B74B4 File Offset: 0x000B64B4
		public override int ReadTimeout
		{
			get
			{
				return this.m_ParentStream.ReadTimeout;
			}
			set
			{
				this.m_ParentStream.ReadTimeout = value;
			}
		}

		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x06002B1F RID: 11039 RVA: 0x000B74C2 File Offset: 0x000B64C2
		// (set) Token: 0x06002B20 RID: 11040 RVA: 0x000B74CF File Offset: 0x000B64CF
		public override int WriteTimeout
		{
			get
			{
				return this.m_ParentStream.WriteTimeout;
			}
			set
			{
				this.m_ParentStream.WriteTimeout = value;
			}
		}

		// Token: 0x06002B21 RID: 11041 RVA: 0x000B74E0 File Offset: 0x000B64E0
		protected virtual void Dispose(bool disposing, CloseExState closeState)
		{
			ICloseEx closeEx = this.m_ParentStream as ICloseEx;
			if (closeEx != null)
			{
				closeEx.CloseEx(closeState);
			}
			else
			{
				this.m_ParentStream.Close();
			}
			base.Dispose(disposing);
		}

		// Token: 0x040029AB RID: 10667
		private Stream m_ParentStream;

		// Token: 0x040029AC RID: 10668
		private long m_Offset;

		// Token: 0x040029AD RID: 10669
		private long m_Size;

		// Token: 0x040029AE RID: 10670
		private long m_Position;
	}
}
