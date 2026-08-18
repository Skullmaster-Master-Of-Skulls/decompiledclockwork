using System;
using System.Data.Common;
using System.IO;

namespace System.Data.SqlTypes
{
	// Token: 0x0200018A RID: 394
	internal sealed class SqlXmlStreamWrapper : Stream
	{
		// Token: 0x060017AA RID: 6058 RVA: 0x000A8D4C File Offset: 0x000A814C
		internal SqlXmlStreamWrapper(Stream stream)
		{
			this.m_stream = stream;
			this.m_lPosition = 0L;
			this.m_isClosed = false;
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x060017AB RID: 6059 RVA: 0x000A8D78 File Offset: 0x000A8178
		public override bool CanRead
		{
			get
			{
				return !this.IsStreamClosed() && this.m_stream.CanRead;
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x060017AC RID: 6060 RVA: 0x000A8D9C File Offset: 0x000A819C
		public override bool CanSeek
		{
			get
			{
				return !this.IsStreamClosed() && this.m_stream.CanSeek;
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x060017AD RID: 6061 RVA: 0x000A8DC0 File Offset: 0x000A81C0
		public override bool CanWrite
		{
			get
			{
				return !this.IsStreamClosed() && this.m_stream.CanWrite;
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x060017AE RID: 6062 RVA: 0x000A8DE4 File Offset: 0x000A81E4
		public override long Length
		{
			get
			{
				this.ThrowIfStreamClosed("get_Length");
				this.ThrowIfStreamCannotSeek("get_Length");
				return this.m_stream.Length;
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x060017AF RID: 6063 RVA: 0x000A8E14 File Offset: 0x000A8214
		// (set) Token: 0x060017B0 RID: 6064 RVA: 0x000A8E40 File Offset: 0x000A8240
		public override long Position
		{
			get
			{
				this.ThrowIfStreamClosed("get_Position");
				this.ThrowIfStreamCannotSeek("get_Position");
				return this.m_lPosition;
			}
			set
			{
				this.ThrowIfStreamClosed("set_Position");
				this.ThrowIfStreamCannotSeek("set_Position");
				if (value < 0L || value > this.m_stream.Length)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.m_lPosition = value;
			}
		}

		// Token: 0x060017B1 RID: 6065 RVA: 0x000A8E88 File Offset: 0x000A8288
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.ThrowIfStreamClosed("Seek");
			this.ThrowIfStreamCannotSeek("Seek");
			switch (origin)
			{
			case SeekOrigin.Begin:
				if (offset < 0L || offset > this.m_stream.Length)
				{
					throw new ArgumentOutOfRangeException("offset");
				}
				this.m_lPosition = offset;
				break;
			case SeekOrigin.Current:
			{
				long num = this.m_lPosition + offset;
				if (num < 0L || num > this.m_stream.Length)
				{
					throw new ArgumentOutOfRangeException("offset");
				}
				this.m_lPosition = num;
				break;
			}
			case SeekOrigin.End:
			{
				long num = this.m_stream.Length + offset;
				if (num < 0L || num > this.m_stream.Length)
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

		// Token: 0x060017B2 RID: 6066 RVA: 0x000A8F64 File Offset: 0x000A8364
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.ThrowIfStreamClosed("Read");
			this.ThrowIfStreamCannotRead("Read");
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
			if (this.m_stream.CanSeek && this.m_stream.Position != this.m_lPosition)
			{
				this.m_stream.Seek(this.m_lPosition, SeekOrigin.Begin);
			}
			int num = this.m_stream.Read(buffer, offset, count);
			this.m_lPosition += (long)num;
			return num;
		}

		// Token: 0x060017B3 RID: 6067 RVA: 0x000A9014 File Offset: 0x000A8414
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.ThrowIfStreamClosed("Write");
			this.ThrowIfStreamCannotWrite("Write");
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
			if (this.m_stream.CanSeek && this.m_stream.Position != this.m_lPosition)
			{
				this.m_stream.Seek(this.m_lPosition, SeekOrigin.Begin);
			}
			this.m_stream.Write(buffer, offset, count);
			this.m_lPosition += (long)count;
		}

		// Token: 0x060017B4 RID: 6068 RVA: 0x000A90C4 File Offset: 0x000A84C4
		public override int ReadByte()
		{
			this.ThrowIfStreamClosed("ReadByte");
			this.ThrowIfStreamCannotRead("ReadByte");
			if (this.m_stream.CanSeek && this.m_lPosition >= this.m_stream.Length)
			{
				return -1;
			}
			if (this.m_stream.CanSeek && this.m_stream.Position != this.m_lPosition)
			{
				this.m_stream.Seek(this.m_lPosition, SeekOrigin.Begin);
			}
			int result = this.m_stream.ReadByte();
			this.m_lPosition += 1L;
			return result;
		}

		// Token: 0x060017B5 RID: 6069 RVA: 0x000A9158 File Offset: 0x000A8558
		public override void WriteByte(byte value)
		{
			this.ThrowIfStreamClosed("WriteByte");
			this.ThrowIfStreamCannotWrite("WriteByte");
			if (this.m_stream.CanSeek && this.m_stream.Position != this.m_lPosition)
			{
				this.m_stream.Seek(this.m_lPosition, SeekOrigin.Begin);
			}
			this.m_stream.WriteByte(value);
			this.m_lPosition += 1L;
		}

		// Token: 0x060017B6 RID: 6070 RVA: 0x000A91CC File Offset: 0x000A85CC
		public override void SetLength(long value)
		{
			this.ThrowIfStreamClosed("SetLength");
			this.ThrowIfStreamCannotSeek("SetLength");
			this.m_stream.SetLength(value);
			if (this.m_lPosition > value)
			{
				this.m_lPosition = value;
			}
		}

		// Token: 0x060017B7 RID: 6071 RVA: 0x000A920C File Offset: 0x000A860C
		public override void Flush()
		{
			if (this.m_stream != null)
			{
				this.m_stream.Flush();
			}
		}

		// Token: 0x060017B8 RID: 6072 RVA: 0x000A922C File Offset: 0x000A862C
		protected override void Dispose(bool disposing)
		{
			try
			{
				this.m_isClosed = true;
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x060017B9 RID: 6073 RVA: 0x000A9268 File Offset: 0x000A8668
		private void ThrowIfStreamCannotSeek(string method)
		{
			if (!this.m_stream.CanSeek)
			{
				throw new NotSupportedException(SQLResource.InvalidOpStreamNonSeekable(method));
			}
		}

		// Token: 0x060017BA RID: 6074 RVA: 0x000A9290 File Offset: 0x000A8690
		private void ThrowIfStreamCannotRead(string method)
		{
			if (!this.m_stream.CanRead)
			{
				throw new NotSupportedException(SQLResource.InvalidOpStreamNonReadable(method));
			}
		}

		// Token: 0x060017BB RID: 6075 RVA: 0x000A92B8 File Offset: 0x000A86B8
		private void ThrowIfStreamCannotWrite(string method)
		{
			if (!this.m_stream.CanWrite)
			{
				throw new NotSupportedException(SQLResource.InvalidOpStreamNonWritable(method));
			}
		}

		// Token: 0x060017BC RID: 6076 RVA: 0x000A92E0 File Offset: 0x000A86E0
		private void ThrowIfStreamClosed(string method)
		{
			if (this.IsStreamClosed())
			{
				throw new ObjectDisposedException(SQLResource.InvalidOpStreamClosed(method));
			}
		}

		// Token: 0x060017BD RID: 6077 RVA: 0x000A9304 File Offset: 0x000A8704
		private bool IsStreamClosed()
		{
			return this.m_isClosed || this.m_stream == null || (!this.m_stream.CanRead && !this.m_stream.CanWrite && !this.m_stream.CanSeek);
		}

		// Token: 0x04000E58 RID: 3672
		private Stream m_stream;

		// Token: 0x04000E59 RID: 3673
		private long m_lPosition;

		// Token: 0x04000E5A RID: 3674
		private bool m_isClosed;
	}
}
