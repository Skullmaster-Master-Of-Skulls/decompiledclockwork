using System;
using System.Runtime.InteropServices;

namespace System.IO
{
	// Token: 0x020005BF RID: 1471
	[ComVisible(true)]
	[Serializable]
	public class MemoryStream : Stream
	{
		// Token: 0x0600366D RID: 13933 RVA: 0x000B6C26 File Offset: 0x000B5C26
		public MemoryStream() : this(0)
		{
		}

		// Token: 0x0600366E RID: 13934 RVA: 0x000B6C30 File Offset: 0x000B5C30
		public MemoryStream(int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", Environment.GetResourceString("ArgumentOutOfRange_NegativeCapacity"));
			}
			this._buffer = new byte[capacity];
			this._capacity = capacity;
			this._expandable = true;
			this._writable = true;
			this._exposable = true;
			this._origin = 0;
			this._isOpen = true;
		}

		// Token: 0x0600366F RID: 13935 RVA: 0x000B6C92 File Offset: 0x000B5C92
		public MemoryStream(byte[] buffer) : this(buffer, true)
		{
		}

		// Token: 0x06003670 RID: 13936 RVA: 0x000B6C9C File Offset: 0x000B5C9C
		public MemoryStream(byte[] buffer, bool writable)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", Environment.GetResourceString("ArgumentNull_Buffer"));
			}
			this._buffer = buffer;
			this._length = (this._capacity = buffer.Length);
			this._writable = writable;
			this._exposable = false;
			this._origin = 0;
			this._isOpen = true;
		}

		// Token: 0x06003671 RID: 13937 RVA: 0x000B6CFC File Offset: 0x000B5CFC
		public MemoryStream(byte[] buffer, int index, int count) : this(buffer, index, count, true, false)
		{
		}

		// Token: 0x06003672 RID: 13938 RVA: 0x000B6D09 File Offset: 0x000B5D09
		public MemoryStream(byte[] buffer, int index, int count, bool writable) : this(buffer, index, count, writable, false)
		{
		}

		// Token: 0x06003673 RID: 13939 RVA: 0x000B6D18 File Offset: 0x000B5D18
		public MemoryStream(byte[] buffer, int index, int count, bool writable, bool publiclyVisible)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", Environment.GetResourceString("ArgumentNull_Buffer"));
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			this._buffer = buffer;
			this._position = index;
			this._origin = index;
			this._length = (this._capacity = index + count);
			this._writable = writable;
			this._exposable = publiclyVisible;
			this._expandable = false;
			this._isOpen = true;
		}

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x06003674 RID: 13940 RVA: 0x000B6DD4 File Offset: 0x000B5DD4
		public override bool CanRead
		{
			get
			{
				return this._isOpen;
			}
		}

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x06003675 RID: 13941 RVA: 0x000B6DDC File Offset: 0x000B5DDC
		public override bool CanSeek
		{
			get
			{
				return this._isOpen;
			}
		}

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x06003676 RID: 13942 RVA: 0x000B6DE4 File Offset: 0x000B5DE4
		public override bool CanWrite
		{
			get
			{
				return this._writable;
			}
		}

		// Token: 0x06003677 RID: 13943 RVA: 0x000B6DEC File Offset: 0x000B5DEC
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this._isOpen = false;
					this._writable = false;
					this._expandable = false;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06003678 RID: 13944 RVA: 0x000B6E2C File Offset: 0x000B5E2C
		private bool EnsureCapacity(int value)
		{
			if (value < 0)
			{
				throw new IOException(Environment.GetResourceString("IO.IO_StreamTooLong"));
			}
			if (value > this._capacity)
			{
				int num = value;
				if (num < 256)
				{
					num = 256;
				}
				if (num < this._capacity * 2)
				{
					num = this._capacity * 2;
				}
				this.Capacity = num;
				return true;
			}
			return false;
		}

		// Token: 0x06003679 RID: 13945 RVA: 0x000B6E84 File Offset: 0x000B5E84
		public override void Flush()
		{
		}

		// Token: 0x0600367A RID: 13946 RVA: 0x000B6E86 File Offset: 0x000B5E86
		public virtual byte[] GetBuffer()
		{
			if (!this._exposable)
			{
				throw new UnauthorizedAccessException(Environment.GetResourceString("UnauthorizedAccess_MemStreamBuffer"));
			}
			return this._buffer;
		}

		// Token: 0x0600367B RID: 13947 RVA: 0x000B6EA6 File Offset: 0x000B5EA6
		internal byte[] InternalGetBuffer()
		{
			return this._buffer;
		}

		// Token: 0x0600367C RID: 13948 RVA: 0x000B6EAE File Offset: 0x000B5EAE
		internal void InternalGetOriginAndLength(out int origin, out int length)
		{
			if (!this._isOpen)
			{
				__Error.StreamIsClosed();
			}
			origin = this._origin;
			length = this._length;
		}

		// Token: 0x0600367D RID: 13949 RVA: 0x000B6ECD File Offset: 0x000B5ECD
		internal int InternalGetPosition()
		{
			if (!this._isOpen)
			{
				__Error.StreamIsClosed();
			}
			return this._position;
		}

		// Token: 0x0600367E RID: 13950 RVA: 0x000B6EE4 File Offset: 0x000B5EE4
		internal int InternalReadInt32()
		{
			if (!this._isOpen)
			{
				__Error.StreamIsClosed();
			}
			int num = this._position += 4;
			if (num > this._length)
			{
				this._position = this._length;
				__Error.EndOfFile();
			}
			return (int)this._buffer[num - 4] | (int)this._buffer[num - 3] << 8 | (int)this._buffer[num - 2] << 16 | (int)this._buffer[num - 1] << 24;
		}

		// Token: 0x0600367F RID: 13951 RVA: 0x000B6F60 File Offset: 0x000B5F60
		internal int InternalEmulateRead(int count)
		{
			if (!this._isOpen)
			{
				__Error.StreamIsClosed();
			}
			int num = this._length - this._position;
			if (num > count)
			{
				num = count;
			}
			if (num < 0)
			{
				num = 0;
			}
			this._position += num;
			return num;
		}

		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x06003680 RID: 13952 RVA: 0x000B6FA3 File Offset: 0x000B5FA3
		// (set) Token: 0x06003681 RID: 13953 RVA: 0x000B6FC0 File Offset: 0x000B5FC0
		public virtual int Capacity
		{
			get
			{
				if (!this._isOpen)
				{
					__Error.StreamIsClosed();
				}
				return this._capacity - this._origin;
			}
			set
			{
				if (!this._isOpen)
				{
					__Error.StreamIsClosed();
				}
				if (value != this._capacity)
				{
					if (!this._expandable)
					{
						__Error.MemoryStreamNotExpandable();
					}
					if (value < this._length)
					{
						throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_SmallCapacity"));
					}
					if (value > 0)
					{
						byte[] array = new byte[value];
						if (this._length > 0)
						{
							Buffer.InternalBlockCopy(this._buffer, 0, array, 0, this._length);
						}
						this._buffer = array;
					}
					else
					{
						this._buffer = null;
					}
					this._capacity = value;
				}
			}
		}

		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x06003682 RID: 13954 RVA: 0x000B704D File Offset: 0x000B604D
		public override long Length
		{
			get
			{
				if (!this._isOpen)
				{
					__Error.StreamIsClosed();
				}
				return (long)(this._length - this._origin);
			}
		}

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x06003683 RID: 13955 RVA: 0x000B706A File Offset: 0x000B606A
		// (set) Token: 0x06003684 RID: 13956 RVA: 0x000B7088 File Offset: 0x000B6088
		public override long Position
		{
			get
			{
				if (!this._isOpen)
				{
					__Error.StreamIsClosed();
				}
				return (long)(this._position - this._origin);
			}
			set
			{
				if (!this._isOpen)
				{
					__Error.StreamIsClosed();
				}
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
				if (value > 2147483647L)
				{
					throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_MemStreamLength"));
				}
				this._position = this._origin + (int)value;
			}
		}

		// Token: 0x06003685 RID: 13957 RVA: 0x000B70EC File Offset: 0x000B60EC
		public override int Read([In] [Out] byte[] buffer, int offset, int count)
		{
			if (!this._isOpen)
			{
				__Error.StreamIsClosed();
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", Environment.GetResourceString("ArgumentNull_Buffer"));
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			int num = this._length - this._position;
			if (num > count)
			{
				num = count;
			}
			if (num <= 0)
			{
				return 0;
			}
			if (num <= 8)
			{
				int num2 = num;
				while (--num2 >= 0)
				{
					buffer[offset + num2] = this._buffer[this._position + num2];
				}
			}
			else
			{
				Buffer.InternalBlockCopy(this._buffer, this._position, buffer, offset, num);
			}
			this._position += num;
			return num;
		}

		// Token: 0x06003686 RID: 13958 RVA: 0x000B71CC File Offset: 0x000B61CC
		public override int ReadByte()
		{
			if (!this._isOpen)
			{
				__Error.StreamIsClosed();
			}
			if (this._position >= this._length)
			{
				return -1;
			}
			return (int)this._buffer[this._position++];
		}

		// Token: 0x06003687 RID: 13959 RVA: 0x000B7210 File Offset: 0x000B6210
		public override long Seek(long offset, SeekOrigin loc)
		{
			if (!this._isOpen)
			{
				__Error.StreamIsClosed();
			}
			if (offset > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_MemStreamLength"));
			}
			switch (loc)
			{
			case SeekOrigin.Begin:
				if (offset < 0L)
				{
					throw new IOException(Environment.GetResourceString("IO.IO_SeekBeforeBegin"));
				}
				this._position = this._origin + (int)offset;
				break;
			case SeekOrigin.Current:
				if (offset + (long)this._position < (long)this._origin)
				{
					throw new IOException(Environment.GetResourceString("IO.IO_SeekBeforeBegin"));
				}
				this._position += (int)offset;
				break;
			case SeekOrigin.End:
				if ((long)this._length + offset < (long)this._origin)
				{
					throw new IOException(Environment.GetResourceString("IO.IO_SeekBeforeBegin"));
				}
				this._position = this._length + (int)offset;
				break;
			default:
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidSeekOrigin"));
			}
			return (long)this._position;
		}

		// Token: 0x06003688 RID: 13960 RVA: 0x000B7304 File Offset: 0x000B6304
		public override void SetLength(long value)
		{
			if (!this._writable)
			{
				__Error.WriteNotSupported();
			}
			if (value > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_MemStreamLength"));
			}
			if (value < 0L || value > (long)(2147483647 - this._origin))
			{
				throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_MemStreamLength"));
			}
			int num = this._origin + (int)value;
			if (!this.EnsureCapacity(num) && num > this._length)
			{
				Array.Clear(this._buffer, this._length, num - this._length);
			}
			this._length = num;
			if (this._position > num)
			{
				this._position = num;
			}
		}

		// Token: 0x06003689 RID: 13961 RVA: 0x000B73B4 File Offset: 0x000B63B4
		public virtual byte[] ToArray()
		{
			byte[] array = new byte[this._length - this._origin];
			Buffer.InternalBlockCopy(this._buffer, this._origin, array, 0, this._length - this._origin);
			return array;
		}

		// Token: 0x0600368A RID: 13962 RVA: 0x000B73F8 File Offset: 0x000B63F8
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (!this._isOpen)
			{
				__Error.StreamIsClosed();
			}
			if (!this._writable)
			{
				__Error.WriteNotSupported();
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", Environment.GetResourceString("ArgumentNull_Buffer"));
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			int num = this._position + count;
			if (num < 0)
			{
				throw new IOException(Environment.GetResourceString("IO.IO_StreamTooLong"));
			}
			if (num > this._length)
			{
				bool flag = this._position > this._length;
				if (num > this._capacity)
				{
					bool flag2 = this.EnsureCapacity(num);
					if (flag2)
					{
						flag = false;
					}
				}
				if (flag)
				{
					Array.Clear(this._buffer, this._length, num - this._length);
				}
				this._length = num;
			}
			if (count <= 8)
			{
				int num2 = count;
				while (--num2 >= 0)
				{
					this._buffer[this._position + num2] = buffer[offset + num2];
				}
			}
			else
			{
				Buffer.InternalBlockCopy(buffer, offset, this._buffer, this._position, count);
			}
			this._position = num;
		}

		// Token: 0x0600368B RID: 13963 RVA: 0x000B7530 File Offset: 0x000B6530
		public override void WriteByte(byte value)
		{
			if (!this._isOpen)
			{
				__Error.StreamIsClosed();
			}
			if (!this._writable)
			{
				__Error.WriteNotSupported();
			}
			if (this._position >= this._length)
			{
				int num = this._position + 1;
				bool flag = this._position > this._length;
				if (num >= this._capacity)
				{
					bool flag2 = this.EnsureCapacity(num);
					if (flag2)
					{
						flag = false;
					}
				}
				if (flag)
				{
					Array.Clear(this._buffer, this._length, this._position - this._length);
				}
				this._length = num;
			}
			this._buffer[this._position++] = value;
		}

		// Token: 0x0600368C RID: 13964 RVA: 0x000B75D4 File Offset: 0x000B65D4
		public virtual void WriteTo(Stream stream)
		{
			if (!this._isOpen)
			{
				__Error.StreamIsClosed();
			}
			if (stream == null)
			{
				throw new ArgumentNullException("stream", Environment.GetResourceString("ArgumentNull_Stream"));
			}
			stream.Write(this._buffer, this._origin, this._length - this._origin);
		}

		// Token: 0x04001C8B RID: 7307
		private const int MemStreamMaxLength = 2147483647;

		// Token: 0x04001C8C RID: 7308
		private byte[] _buffer;

		// Token: 0x04001C8D RID: 7309
		private int _origin;

		// Token: 0x04001C8E RID: 7310
		private int _position;

		// Token: 0x04001C8F RID: 7311
		private int _length;

		// Token: 0x04001C90 RID: 7312
		private int _capacity;

		// Token: 0x04001C91 RID: 7313
		private bool _expandable;

		// Token: 0x04001C92 RID: 7314
		private bool _writable;

		// Token: 0x04001C93 RID: 7315
		private bool _exposable;

		// Token: 0x04001C94 RID: 7316
		private bool _isOpen;
	}
}
