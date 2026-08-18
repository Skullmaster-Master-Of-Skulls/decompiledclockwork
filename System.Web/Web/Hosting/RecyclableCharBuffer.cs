using System;
using System.Text;

namespace System.Web.Hosting
{
	// Token: 0x020002A0 RID: 672
	internal class RecyclableCharBuffer
	{
		// Token: 0x06002302 RID: 8962 RVA: 0x00096AD2 File Offset: 0x00095AD2
		internal RecyclableCharBuffer()
		{
			this._charBuffer = (char[])RecyclableCharBuffer.s_CharBufferAllocator.GetBuffer();
			this._size = this._charBuffer.Length;
			this._freePos = 0;
			this._recyclable = true;
		}

		// Token: 0x06002303 RID: 8963 RVA: 0x00096B0C File Offset: 0x00095B0C
		internal void Dispose()
		{
			if (this._recyclable)
			{
				if (this._charBuffer != null)
				{
					RecyclableCharBuffer.s_CharBufferAllocator.ReuseBuffer(this._charBuffer);
				}
				if (this._byteBuffer != null)
				{
					RecyclableCharBuffer.s_ByteBufferAllocator.ReuseBuffer(this._byteBuffer);
				}
			}
			this._charBuffer = null;
			this._byteBuffer = null;
		}

		// Token: 0x06002304 RID: 8964 RVA: 0x00096B60 File Offset: 0x00095B60
		private void Grow(int newSize)
		{
			if (newSize <= this._size)
			{
				return;
			}
			if (newSize < this._size * 2)
			{
				newSize = this._size * 2;
			}
			char[] array = new char[newSize];
			if (this._freePos > 0)
			{
				Array.Copy(this._charBuffer, array, this._freePos);
			}
			this._charBuffer = array;
			this._size = newSize;
			this._recyclable = false;
		}

		// Token: 0x06002305 RID: 8965 RVA: 0x00096BC4 File Offset: 0x00095BC4
		internal void Append(char ch)
		{
			if (this._freePos >= this._size)
			{
				this.Grow(this._freePos + 1);
			}
			this._charBuffer[this._freePos++] = ch;
		}

		// Token: 0x06002306 RID: 8966 RVA: 0x00096C08 File Offset: 0x00095C08
		internal void Append(string s)
		{
			int length = s.Length;
			int num = this._freePos + length;
			if (num > this._size)
			{
				this.Grow(num);
			}
			s.CopyTo(0, this._charBuffer, this._freePos, length);
			this._freePos = num;
		}

		// Token: 0x06002307 RID: 8967 RVA: 0x00096C50 File Offset: 0x00095C50
		internal byte[] GetEncodedBytesBuffer()
		{
			return this.GetEncodedBytesBuffer(Encoding.UTF8);
		}

		// Token: 0x06002308 RID: 8968 RVA: 0x00096C60 File Offset: 0x00095C60
		internal byte[] GetEncodedBytesBuffer(Encoding encoding)
		{
			if (this._byteBuffer != null)
			{
				return this._byteBuffer;
			}
			if (encoding == null)
			{
				encoding = Encoding.UTF8;
			}
			this.Append('\0');
			if (this._recyclable)
			{
				this._byteBuffer = (byte[])RecyclableCharBuffer.s_ByteBufferAllocator.GetBuffer();
				if (this._freePos > 0)
				{
					encoding.GetBytes(this._charBuffer, 0, this._freePos, this._byteBuffer, 0);
				}
			}
			else
			{
				this._byteBuffer = encoding.GetBytes(this._charBuffer, 0, this._freePos);
			}
			return this._byteBuffer;
		}

		// Token: 0x06002309 RID: 8969 RVA: 0x00096CEF File Offset: 0x00095CEF
		public override string ToString()
		{
			if (this._charBuffer == null || this._freePos <= 0)
			{
				return null;
			}
			return new string(this._charBuffer, 0, this._freePos);
		}

		// Token: 0x04001B83 RID: 7043
		private const int BUFFER_SIZE = 1024;

		// Token: 0x04001B84 RID: 7044
		private const int MAX_FREE_BUFFERS = 64;

		// Token: 0x04001B85 RID: 7045
		private static CharBufferAllocator s_CharBufferAllocator = new CharBufferAllocator(1024, 64);

		// Token: 0x04001B86 RID: 7046
		private static UbyteBufferAllocator s_ByteBufferAllocator = new UbyteBufferAllocator(Encoding.UTF8.GetMaxByteCount(1024), 64);

		// Token: 0x04001B87 RID: 7047
		private char[] _charBuffer;

		// Token: 0x04001B88 RID: 7048
		private int _size;

		// Token: 0x04001B89 RID: 7049
		private int _freePos;

		// Token: 0x04001B8A RID: 7050
		private bool _recyclable;

		// Token: 0x04001B8B RID: 7051
		private byte[] _byteBuffer;
	}
}
