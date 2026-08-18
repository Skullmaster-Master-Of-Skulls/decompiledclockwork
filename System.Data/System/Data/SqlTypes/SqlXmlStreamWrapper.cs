using System;
using System.Data.Common;
using System.IO;

namespace System.Data.SqlTypes
{
	// Token: 0x02000379 RID: 889
	internal sealed class SqlXmlStreamWrapper : Stream
	{
		// Token: 0x06002F50 RID: 12112 RVA: 0x002D4178 File Offset: 0x002D3578
		internal SqlXmlStreamWrapper(Stream stream)
		{
			this.m_stream = stream;
			this.m_lPosition = 0L;
			this.m_isClosed = false;
		}

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x06002F51 RID: 12113 RVA: 0x002D41A8 File Offset: 0x002D35A8
		public override bool CanRead
		{
			get
			{
				return !this.IsStreamClosed() && this.m_stream.CanRead;
			}
		}

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x06002F52 RID: 12114 RVA: 0x002D41D8 File Offset: 0x002D35D8
		public override bool CanSeek
		{
			get
			{
				return !this.IsStreamClosed() && this.m_stream.CanSeek;
			}
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x06002F53 RID: 12115 RVA: 0x002D4208 File Offset: 0x002D3608
		public override bool CanWrite
		{
			get
			{
				return !this.IsStreamClosed() && this.m_stream.CanWrite;
			}
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x06002F54 RID: 12116 RVA: 0x002D4238 File Offset: 0x002D3638
		public override long Length
		{
			get
			{
				this.ThrowIfStreamClosed("get_Length");
				this.ThrowIfStreamCannotSeek("get_Length");
				return this.m_stream.Length;
			}
		}

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x06002F55 RID: 12117 RVA: 0x002D4268 File Offset: 0x002D3668
		// (set) Token: 0x06002F56 RID: 12118 RVA: 0x002D4298 File Offset: 0x002D3698
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

		// Token: 0x06002F57 RID: 12119 RVA: 0x002D42E8 File Offset: 0x002D36E8
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

		// Token: 0x06002F58 RID: 12120 RVA: 0x002D43C8 File Offset: 0x002D37C8
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

		// Token: 0x06002F59 RID: 12121 RVA: 0x002D4478 File Offset: 0x002D3878
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

		// Token: 0x06002F5A RID: 12122 RVA: 0x002D4528 File Offset: 0x002D3928
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

		// Token: 0x06002F5B RID: 12123 RVA: 0x002D45C8 File Offset: 0x002D39C8
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

		// Token: 0x06002F5C RID: 12124 RVA: 0x002D4648 File Offset: 0x002D3A48
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

		// Token: 0x06002F5D RID: 12125 RVA: 0x002D4688 File Offset: 0x002D3A88
		public override void Flush()
		{
			if (this.m_stream != null)
			{
				this.m_stream.Flush();
			}
		}

		// Token: 0x06002F5E RID: 12126 RVA: 0x002D46A8 File Offset: 0x002D3AA8
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

		// Token: 0x06002F5F RID: 12127 RVA: 0x002D46E8 File Offset: 0x002D3AE8
		private void ThrowIfStreamCannotSeek(string method)
		{
			if (!this.m_stream.CanSeek)
			{
				throw new NotSupportedException(SQLResource.InvalidOpStreamNonSeekable(method));
			}
		}

		// Token: 0x06002F60 RID: 12128 RVA: 0x002D4718 File Offset: 0x002D3B18
		private void ThrowIfStreamCannotRead(string method)
		{
			if (!this.m_stream.CanRead)
			{
				throw new NotSupportedException(SQLResource.InvalidOpStreamNonReadable(method));
			}
		}

		// Token: 0x06002F61 RID: 12129 RVA: 0x002D4748 File Offset: 0x002D3B48
		private void ThrowIfStreamCannotWrite(string method)
		{
			if (!this.m_stream.CanWrite)
			{
				throw new NotSupportedException(SQLResource.InvalidOpStreamNonWritable(method));
			}
		}

		// Token: 0x06002F62 RID: 12130 RVA: 0x002D4778 File Offset: 0x002D3B78
		private void ThrowIfStreamClosed(string method)
		{
			if (this.IsStreamClosed())
			{
				throw new ObjectDisposedException(SQLResource.InvalidOpStreamClosed(method));
			}
		}

		// Token: 0x06002F63 RID: 12131 RVA: 0x002D47A8 File Offset: 0x002D3BA8
		private bool IsStreamClosed()
		{
			return this.m_isClosed || this.m_stream == null || (!this.m_stream.CanRead && !this.m_stream.CanWrite && !this.m_stream.CanSeek);
		}

		// Token: 0x04001D6B RID: 7531
		private Stream m_stream;

		// Token: 0x04001D6C RID: 7532
		private long m_lPosition;

		// Token: 0x04001D6D RID: 7533
		private bool m_isClosed;
	}
}
