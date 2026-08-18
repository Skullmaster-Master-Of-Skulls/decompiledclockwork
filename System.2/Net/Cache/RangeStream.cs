using System;
using System.IO;

namespace System.Net.Cache
{
	// Token: 0x0200031E RID: 798
	internal class RangeStream : BaseWrapperStream, ICloseEx
	{
		// Token: 0x06001CA3 RID: 7331 RVA: 0x00087D20 File Offset: 0x00085F20
		internal RangeStream(Stream parentStream, long offset, long size) : base(parentStream)
		{
			this.m_Offset = offset;
			this.m_Size = size;
			if (base.WrappedStream.CanSeek)
			{
				base.WrappedStream.Position = offset;
				this.m_Position = offset;
				return;
			}
			throw new NotSupportedException(SR.GetString("net_cache_non_seekable_stream_not_supported"));
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x06001CA4 RID: 7332 RVA: 0x00087D72 File Offset: 0x00085F72
		public override bool CanRead
		{
			get
			{
				return base.WrappedStream.CanRead;
			}
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x06001CA5 RID: 7333 RVA: 0x00087D7F File Offset: 0x00085F7F
		public override bool CanSeek
		{
			get
			{
				return base.WrappedStream.CanSeek;
			}
		}

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x06001CA6 RID: 7334 RVA: 0x00087D8C File Offset: 0x00085F8C
		public override bool CanWrite
		{
			get
			{
				return base.WrappedStream.CanWrite;
			}
		}

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x06001CA7 RID: 7335 RVA: 0x00087D9C File Offset: 0x00085F9C
		public override long Length
		{
			get
			{
				long length = base.WrappedStream.Length;
				return this.m_Size;
			}
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x06001CA8 RID: 7336 RVA: 0x00087DBB File Offset: 0x00085FBB
		// (set) Token: 0x06001CA9 RID: 7337 RVA: 0x00087DCF File Offset: 0x00085FCF
		public override long Position
		{
			get
			{
				return base.WrappedStream.Position - this.m_Offset;
			}
			set
			{
				value += this.m_Offset;
				if (value > this.m_Offset + this.m_Size)
				{
					value = this.m_Offset + this.m_Size;
				}
				base.WrappedStream.Position = value;
			}
		}

		// Token: 0x06001CAA RID: 7338 RVA: 0x00087E08 File Offset: 0x00086008
		public override long Seek(long offset, SeekOrigin origin)
		{
			if (origin != SeekOrigin.Begin)
			{
				if (origin != SeekOrigin.End)
				{
					if (this.m_Position + offset > this.m_Offset + this.m_Size)
					{
						offset = this.m_Offset + this.m_Size - this.m_Position;
					}
					if (this.m_Position + offset < this.m_Offset)
					{
						offset = this.m_Offset - this.m_Position;
					}
				}
				else
				{
					offset -= this.m_Offset + this.m_Size;
					if (offset > 0L)
					{
						offset = 0L;
					}
					if (offset < -this.m_Size)
					{
						offset = -this.m_Size;
					}
				}
			}
			else
			{
				offset += this.m_Offset;
				if (offset > this.m_Offset + this.m_Size)
				{
					offset = this.m_Offset + this.m_Size;
				}
				if (offset < this.m_Offset)
				{
					offset = this.m_Offset;
				}
			}
			this.m_Position = base.WrappedStream.Seek(offset, origin);
			return this.m_Position - this.m_Offset;
		}

		// Token: 0x06001CAB RID: 7339 RVA: 0x00087EF8 File Offset: 0x000860F8
		public override void SetLength(long value)
		{
			throw new NotSupportedException(SR.GetString("net_cache_unsupported_partial_stream"));
		}

		// Token: 0x06001CAC RID: 7340 RVA: 0x00087F0C File Offset: 0x0008610C
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this.m_Position + (long)count > this.m_Offset + this.m_Size)
			{
				throw new NotSupportedException(SR.GetString("net_cache_unsupported_partial_stream"));
			}
			base.WrappedStream.Write(buffer, offset, count);
			this.m_Position += (long)count;
		}

		// Token: 0x06001CAD RID: 7341 RVA: 0x00087F5E File Offset: 0x0008615E
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (this.m_Position + (long)offset > this.m_Offset + this.m_Size)
			{
				throw new NotSupportedException(SR.GetString("net_cache_unsupported_partial_stream"));
			}
			return base.WrappedStream.BeginWrite(buffer, offset, count, callback, state);
		}

		// Token: 0x06001CAE RID: 7342 RVA: 0x00087F9A File Offset: 0x0008619A
		public override void EndWrite(IAsyncResult asyncResult)
		{
			base.WrappedStream.EndWrite(asyncResult);
			this.m_Position = base.WrappedStream.Position;
		}

		// Token: 0x06001CAF RID: 7343 RVA: 0x00087FB9 File Offset: 0x000861B9
		public override void Flush()
		{
			base.WrappedStream.Flush();
		}

		// Token: 0x06001CB0 RID: 7344 RVA: 0x00087FC8 File Offset: 0x000861C8
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
			int num = base.WrappedStream.Read(buffer, offset, count);
			this.m_Position += (long)num;
			return num;
		}

		// Token: 0x06001CB1 RID: 7345 RVA: 0x0008803C File Offset: 0x0008623C
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
			return base.WrappedStream.BeginRead(buffer, offset, count, callback, state);
		}

		// Token: 0x06001CB2 RID: 7346 RVA: 0x000880A4 File Offset: 0x000862A4
		public override int EndRead(IAsyncResult asyncResult)
		{
			int num = base.WrappedStream.EndRead(asyncResult);
			this.m_Position += (long)num;
			return num;
		}

		// Token: 0x06001CB3 RID: 7347 RVA: 0x000880CE File Offset: 0x000862CE
		protected sealed override void Dispose(bool disposing)
		{
			this.Dispose(disposing, CloseExState.Normal);
		}

		// Token: 0x06001CB4 RID: 7348 RVA: 0x000880D8 File Offset: 0x000862D8
		void ICloseEx.CloseEx(CloseExState closeState)
		{
			this.Dispose(true, closeState);
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06001CB5 RID: 7349 RVA: 0x000880E2 File Offset: 0x000862E2
		public override bool CanTimeout
		{
			get
			{
				return base.WrappedStream.CanTimeout;
			}
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06001CB6 RID: 7350 RVA: 0x000880EF File Offset: 0x000862EF
		// (set) Token: 0x06001CB7 RID: 7351 RVA: 0x000880FC File Offset: 0x000862FC
		public override int ReadTimeout
		{
			get
			{
				return base.WrappedStream.ReadTimeout;
			}
			set
			{
				base.WrappedStream.ReadTimeout = value;
			}
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x06001CB8 RID: 7352 RVA: 0x0008810A File Offset: 0x0008630A
		// (set) Token: 0x06001CB9 RID: 7353 RVA: 0x00088117 File Offset: 0x00086317
		public override int WriteTimeout
		{
			get
			{
				return base.WrappedStream.WriteTimeout;
			}
			set
			{
				base.WrappedStream.WriteTimeout = value;
			}
		}

		// Token: 0x06001CBA RID: 7354 RVA: 0x00088128 File Offset: 0x00086328
		protected virtual void Dispose(bool disposing, CloseExState closeState)
		{
			try
			{
				if (disposing)
				{
					ICloseEx closeEx = base.WrappedStream as ICloseEx;
					if (closeEx != null)
					{
						closeEx.CloseEx(closeState);
					}
					else
					{
						base.WrappedStream.Close();
					}
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x04001BB0 RID: 7088
		private long m_Offset;

		// Token: 0x04001BB1 RID: 7089
		private long m_Size;

		// Token: 0x04001BB2 RID: 7090
		private long m_Position;
	}
}
