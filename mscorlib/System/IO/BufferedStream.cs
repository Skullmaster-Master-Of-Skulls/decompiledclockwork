using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.IO
{
	// Token: 0x020005A8 RID: 1448
	[ComVisible(true)]
	public sealed class BufferedStream : Stream
	{
		// Token: 0x06003521 RID: 13601 RVA: 0x000AFE3F File Offset: 0x000AEE3F
		private BufferedStream()
		{
		}

		// Token: 0x06003522 RID: 13602 RVA: 0x000AFE47 File Offset: 0x000AEE47
		public BufferedStream(Stream stream) : this(stream, 4096)
		{
		}

		// Token: 0x06003523 RID: 13603 RVA: 0x000AFE58 File Offset: 0x000AEE58
		public BufferedStream(Stream stream, int bufferSize)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (bufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_MustBePositive"), new object[]
				{
					"bufferSize"
				}));
			}
			this._s = stream;
			this._bufferSize = bufferSize;
			if (!this._s.CanRead && !this._s.CanWrite)
			{
				__Error.StreamIsClosed();
			}
		}

		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x06003524 RID: 13604 RVA: 0x000AFED9 File Offset: 0x000AEED9
		public override bool CanRead
		{
			get
			{
				return this._s != null && this._s.CanRead;
			}
		}

		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x06003525 RID: 13605 RVA: 0x000AFEF0 File Offset: 0x000AEEF0
		public override bool CanWrite
		{
			get
			{
				return this._s != null && this._s.CanWrite;
			}
		}

		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x06003526 RID: 13606 RVA: 0x000AFF07 File Offset: 0x000AEF07
		public override bool CanSeek
		{
			get
			{
				return this._s != null && this._s.CanSeek;
			}
		}

		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x06003527 RID: 13607 RVA: 0x000AFF1E File Offset: 0x000AEF1E
		public override long Length
		{
			get
			{
				if (this._s == null)
				{
					__Error.StreamIsClosed();
				}
				if (this._writePos > 0)
				{
					this.FlushWrite();
				}
				return this._s.Length;
			}
		}

		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x06003528 RID: 13608 RVA: 0x000AFF48 File Offset: 0x000AEF48
		// (set) Token: 0x06003529 RID: 13609 RVA: 0x000AFF98 File Offset: 0x000AEF98
		public override long Position
		{
			get
			{
				if (this._s == null)
				{
					__Error.StreamIsClosed();
				}
				if (!this._s.CanSeek)
				{
					__Error.SeekNotSupported();
				}
				return this._s.Position + (long)(this._readPos - this._readLen + this._writePos);
			}
			set
			{
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
				if (this._s == null)
				{
					__Error.StreamIsClosed();
				}
				if (!this._s.CanSeek)
				{
					__Error.SeekNotSupported();
				}
				if (this._writePos > 0)
				{
					this.FlushWrite();
				}
				this._readPos = 0;
				this._readLen = 0;
				this._s.Seek(value, SeekOrigin.Begin);
			}
		}

		// Token: 0x0600352A RID: 13610 RVA: 0x000B000C File Offset: 0x000AF00C
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && this._s != null)
				{
					try
					{
						this.Flush();
					}
					finally
					{
						this._s.Close();
					}
				}
			}
			finally
			{
				this._s = null;
				this._buffer = null;
				base.Dispose(disposing);
			}
		}

		// Token: 0x0600352B RID: 13611 RVA: 0x000B006C File Offset: 0x000AF06C
		public override void Flush()
		{
			if (this._s == null)
			{
				__Error.StreamIsClosed();
			}
			if (this._writePos > 0)
			{
				this.FlushWrite();
			}
			else if (this._readPos < this._readLen && this._s.CanSeek)
			{
				this.FlushRead();
			}
			this._readPos = 0;
			this._readLen = 0;
		}

		// Token: 0x0600352C RID: 13612 RVA: 0x000B00C6 File Offset: 0x000AF0C6
		private void FlushRead()
		{
			if (this._readPos - this._readLen != 0)
			{
				this._s.Seek((long)(this._readPos - this._readLen), SeekOrigin.Current);
			}
			this._readPos = 0;
			this._readLen = 0;
		}

		// Token: 0x0600352D RID: 13613 RVA: 0x000B0100 File Offset: 0x000AF100
		private void FlushWrite()
		{
			this._s.Write(this._buffer, 0, this._writePos);
			this._writePos = 0;
			this._s.Flush();
		}

		// Token: 0x0600352E RID: 13614 RVA: 0x000B012C File Offset: 0x000AF12C
		public override int Read([In] [Out] byte[] array, int offset, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array", Environment.GetResourceString("ArgumentNull_Buffer"));
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (array.Length - offset < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			if (this._s == null)
			{
				__Error.StreamIsClosed();
			}
			int num = this._readLen - this._readPos;
			if (num == 0)
			{
				if (!this._s.CanRead)
				{
					__Error.ReadNotSupported();
				}
				if (this._writePos > 0)
				{
					this.FlushWrite();
				}
				if (count >= this._bufferSize)
				{
					num = this._s.Read(array, offset, count);
					this._readPos = 0;
					this._readLen = 0;
					return num;
				}
				if (this._buffer == null)
				{
					this._buffer = new byte[this._bufferSize];
				}
				num = this._s.Read(this._buffer, 0, this._bufferSize);
				if (num == 0)
				{
					return 0;
				}
				this._readPos = 0;
				this._readLen = num;
			}
			if (num > count)
			{
				num = count;
			}
			Buffer.InternalBlockCopy(this._buffer, this._readPos, array, offset, num);
			this._readPos += num;
			if (num < count)
			{
				int num2 = this._s.Read(array, offset + num, count - num);
				num += num2;
				this._readPos = 0;
				this._readLen = 0;
			}
			return num;
		}

		// Token: 0x0600352F RID: 13615 RVA: 0x000B029C File Offset: 0x000AF29C
		public override int ReadByte()
		{
			if (this._s == null)
			{
				__Error.StreamIsClosed();
			}
			if (this._readLen == 0 && !this._s.CanRead)
			{
				__Error.ReadNotSupported();
			}
			if (this._readPos == this._readLen)
			{
				if (this._writePos > 0)
				{
					this.FlushWrite();
				}
				if (this._buffer == null)
				{
					this._buffer = new byte[this._bufferSize];
				}
				this._readLen = this._s.Read(this._buffer, 0, this._bufferSize);
				this._readPos = 0;
			}
			if (this._readPos == this._readLen)
			{
				return -1;
			}
			return (int)this._buffer[this._readPos++];
		}

		// Token: 0x06003530 RID: 13616 RVA: 0x000B0354 File Offset: 0x000AF354
		public override void Write(byte[] array, int offset, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array", Environment.GetResourceString("ArgumentNull_Buffer"));
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (array.Length - offset < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			if (this._s == null)
			{
				__Error.StreamIsClosed();
			}
			if (this._writePos == 0)
			{
				if (!this._s.CanWrite)
				{
					__Error.WriteNotSupported();
				}
				if (this._readPos < this._readLen)
				{
					this.FlushRead();
				}
				else
				{
					this._readPos = 0;
					this._readLen = 0;
				}
			}
			if (this._writePos > 0)
			{
				int num = this._bufferSize - this._writePos;
				if (num > 0)
				{
					if (num > count)
					{
						num = count;
					}
					Buffer.InternalBlockCopy(array, offset, this._buffer, this._writePos, num);
					this._writePos += num;
					if (count == num)
					{
						return;
					}
					offset += num;
					count -= num;
				}
				this._s.Write(this._buffer, 0, this._writePos);
				this._writePos = 0;
			}
			if (count >= this._bufferSize)
			{
				this._s.Write(array, offset, count);
				return;
			}
			if (count == 0)
			{
				return;
			}
			if (this._buffer == null)
			{
				this._buffer = new byte[this._bufferSize];
			}
			Buffer.InternalBlockCopy(array, offset, this._buffer, 0, count);
			this._writePos = count;
		}

		// Token: 0x06003531 RID: 13617 RVA: 0x000B04CC File Offset: 0x000AF4CC
		public override void WriteByte(byte value)
		{
			if (this._s == null)
			{
				__Error.StreamIsClosed();
			}
			if (this._writePos == 0)
			{
				if (!this._s.CanWrite)
				{
					__Error.WriteNotSupported();
				}
				if (this._readPos < this._readLen)
				{
					this.FlushRead();
				}
				else
				{
					this._readPos = 0;
					this._readLen = 0;
				}
				if (this._buffer == null)
				{
					this._buffer = new byte[this._bufferSize];
				}
			}
			if (this._writePos == this._bufferSize)
			{
				this.FlushWrite();
			}
			this._buffer[this._writePos++] = value;
		}

		// Token: 0x06003532 RID: 13618 RVA: 0x000B056C File Offset: 0x000AF56C
		public override long Seek(long offset, SeekOrigin origin)
		{
			if (this._s == null)
			{
				__Error.StreamIsClosed();
			}
			if (!this._s.CanSeek)
			{
				__Error.SeekNotSupported();
			}
			if (this._writePos > 0)
			{
				this.FlushWrite();
			}
			else if (origin == SeekOrigin.Current)
			{
				offset -= (long)(this._readLen - this._readPos);
			}
			long num = this._s.Position + (long)(this._readPos - this._readLen);
			long num2 = this._s.Seek(offset, origin);
			if (this._readLen > 0)
			{
				if (num == num2)
				{
					if (this._readPos > 0)
					{
						Buffer.InternalBlockCopy(this._buffer, this._readPos, this._buffer, 0, this._readLen - this._readPos);
						this._readLen -= this._readPos;
						this._readPos = 0;
					}
					if (this._readLen > 0)
					{
						this._s.Seek((long)this._readLen, SeekOrigin.Current);
					}
				}
				else if (num - (long)this._readPos < num2 && num2 < num + (long)this._readLen - (long)this._readPos)
				{
					int num3 = (int)(num2 - num);
					Buffer.InternalBlockCopy(this._buffer, this._readPos + num3, this._buffer, 0, this._readLen - (this._readPos + num3));
					this._readLen -= this._readPos + num3;
					this._readPos = 0;
					if (this._readLen > 0)
					{
						this._s.Seek((long)this._readLen, SeekOrigin.Current);
					}
				}
				else
				{
					this._readPos = 0;
					this._readLen = 0;
				}
			}
			return num2;
		}

		// Token: 0x06003533 RID: 13619 RVA: 0x000B0700 File Offset: 0x000AF700
		public override void SetLength(long value)
		{
			if (value < 0L)
			{
				throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_NegFileSize"));
			}
			if (this._s == null)
			{
				__Error.StreamIsClosed();
			}
			if (!this._s.CanSeek)
			{
				__Error.SeekNotSupported();
			}
			if (!this._s.CanWrite)
			{
				__Error.WriteNotSupported();
			}
			if (this._writePos > 0)
			{
				this.FlushWrite();
			}
			else if (this._readPos < this._readLen)
			{
				this.FlushRead();
			}
			this._readPos = 0;
			this._readLen = 0;
			this._s.SetLength(value);
		}

		// Token: 0x04001C0A RID: 7178
		private const int _DefaultBufferSize = 4096;

		// Token: 0x04001C0B RID: 7179
		private Stream _s;

		// Token: 0x04001C0C RID: 7180
		private byte[] _buffer;

		// Token: 0x04001C0D RID: 7181
		private int _readPos;

		// Token: 0x04001C0E RID: 7182
		private int _readLen;

		// Token: 0x04001C0F RID: 7183
		private int _writePos;

		// Token: 0x04001C10 RID: 7184
		private int _bufferSize;
	}
}
