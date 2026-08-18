using System;
using System.Data.Common;
using System.IO;

namespace System.Data.SqlTypes
{
	// Token: 0x02000345 RID: 837
	internal sealed class StreamOnSqlBytes : Stream
	{
		// Token: 0x06002BFE RID: 11262 RVA: 0x002C6558 File Offset: 0x002C5958
		internal StreamOnSqlBytes(SqlBytes sb)
		{
			this.m_sb = sb;
			this.m_lPosition = 0L;
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x06002BFF RID: 11263 RVA: 0x002C6588 File Offset: 0x002C5988
		public override bool CanRead
		{
			get
			{
				return this.m_sb != null && !this.m_sb.IsNull;
			}
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x06002C00 RID: 11264 RVA: 0x002C65B8 File Offset: 0x002C59B8
		public override bool CanSeek
		{
			get
			{
				return this.m_sb != null;
			}
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06002C01 RID: 11265 RVA: 0x002C65D8 File Offset: 0x002C59D8
		public override bool CanWrite
		{
			get
			{
				return this.m_sb != null && (!this.m_sb.IsNull || this.m_sb.m_rgbBuf != null);
			}
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06002C02 RID: 11266 RVA: 0x002C6618 File Offset: 0x002C5A18
		public override long Length
		{
			get
			{
				this.CheckIfStreamClosed("get_Length");
				return this.m_sb.Length;
			}
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06002C03 RID: 11267 RVA: 0x002C6648 File Offset: 0x002C5A48
		// (set) Token: 0x06002C04 RID: 11268 RVA: 0x002C6668 File Offset: 0x002C5A68
		public override long Position
		{
			get
			{
				this.CheckIfStreamClosed("get_Position");
				return this.m_lPosition;
			}
			set
			{
				this.CheckIfStreamClosed("set_Position");
				if (value < 0L || value > this.m_sb.Length)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.m_lPosition = value;
			}
		}

		// Token: 0x06002C05 RID: 11269 RVA: 0x002C66A8 File Offset: 0x002C5AA8
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.CheckIfStreamClosed("Seek");
			switch (origin)
			{
			case SeekOrigin.Begin:
				if (offset < 0L || offset > this.m_sb.Length)
				{
					throw new ArgumentOutOfRangeException("offset");
				}
				this.m_lPosition = offset;
				break;
			case SeekOrigin.Current:
			{
				long num = this.m_lPosition + offset;
				if (num < 0L || num > this.m_sb.Length)
				{
					throw new ArgumentOutOfRangeException("offset");
				}
				this.m_lPosition = num;
				break;
			}
			case SeekOrigin.End:
			{
				long num = this.m_sb.Length + offset;
				if (num < 0L || num > this.m_sb.Length)
				{
					throw new ArgumentOutOfRangeException("offset");
				}
				this.m_lPosition = num;
				break;
			}
			default:
				throw ADP.InvalidSeekOrigin("offset");
			}
			return this.m_lPosition;
		}

		// Token: 0x06002C06 RID: 11270 RVA: 0x002C6788 File Offset: 0x002C5B88
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.CheckIfStreamClosed("Read");
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || count > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			int num = (int)this.m_sb.Read(this.m_lPosition, buffer, offset, count);
			this.m_lPosition += (long)num;
			return num;
		}

		// Token: 0x06002C07 RID: 11271 RVA: 0x002C6808 File Offset: 0x002C5C08
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.CheckIfStreamClosed("Write");
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || count > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			this.m_sb.Write(this.m_lPosition, buffer, offset, count);
			this.m_lPosition += (long)count;
		}

		// Token: 0x06002C08 RID: 11272 RVA: 0x002C6888 File Offset: 0x002C5C88
		public override int ReadByte()
		{
			this.CheckIfStreamClosed("ReadByte");
			if (this.m_lPosition >= this.m_sb.Length)
			{
				return -1;
			}
			int result = (int)this.m_sb[this.m_lPosition];
			this.m_lPosition += 1L;
			return result;
		}

		// Token: 0x06002C09 RID: 11273 RVA: 0x002C68D8 File Offset: 0x002C5CD8
		public override void WriteByte(byte value)
		{
			this.CheckIfStreamClosed("WriteByte");
			this.m_sb[this.m_lPosition] = value;
			this.m_lPosition += 1L;
		}

		// Token: 0x06002C0A RID: 11274 RVA: 0x002C6918 File Offset: 0x002C5D18
		public override void SetLength(long value)
		{
			this.CheckIfStreamClosed("SetLength");
			this.m_sb.SetLength(value);
			if (this.m_lPosition > value)
			{
				this.m_lPosition = value;
			}
		}

		// Token: 0x06002C0B RID: 11275 RVA: 0x002C6958 File Offset: 0x002C5D58
		public override void Flush()
		{
			if (this.m_sb.FStream())
			{
				this.m_sb.m_stream.Flush();
			}
		}

		// Token: 0x06002C0C RID: 11276 RVA: 0x002C6988 File Offset: 0x002C5D88
		protected override void Dispose(bool disposing)
		{
			try
			{
				this.m_sb = null;
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06002C0D RID: 11277 RVA: 0x002C69C8 File Offset: 0x002C5DC8
		private bool FClosed()
		{
			return this.m_sb == null;
		}

		// Token: 0x06002C0E RID: 11278 RVA: 0x002C69E8 File Offset: 0x002C5DE8
		private void CheckIfStreamClosed(string methodname)
		{
			if (this.FClosed())
			{
				throw ADP.StreamClosed(methodname);
			}
		}

		// Token: 0x04001C7D RID: 7293
		private SqlBytes m_sb;

		// Token: 0x04001C7E RID: 7294
		private long m_lPosition;
	}
}
