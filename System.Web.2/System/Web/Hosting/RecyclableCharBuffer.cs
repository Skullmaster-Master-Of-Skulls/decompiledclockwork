using System;
using System.Text;

namespace System.Web.Hosting
{
	// Token: 0x020007C3 RID: 1987
	internal class RecyclableCharBuffer
	{
		// Token: 0x06005F2C RID: 24364 RVA: 0x001487AE File Offset: 0x001469AE
		internal RecyclableCharBuffer()
		{
			this._charBuffer = (char[])RecyclableCharBuffer.s_CharBufferAllocator.GetBuffer();
			this._size = this._charBuffer.Length;
			this._freePos = 0;
			this._recyclable = true;
		}

		// Token: 0x06005F2D RID: 24365 RVA: 0x001487E8 File Offset: 0x001469E8
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

		// Token: 0x06005F2E RID: 24366 RVA: 0x0014883C File Offset: 0x00146A3C
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

		// Token: 0x06005F2F RID: 24367 RVA: 0x001488A0 File Offset: 0x00146AA0
		internal void Append(char ch)
		{
			if (this._freePos >= this._size)
			{
				this.Grow(this._freePos + 1);
			}
			char[] charBuffer = this._charBuffer;
			int freePos = this._freePos;
			this._freePos = freePos + 1;
			charBuffer[freePos] = ch;
		}

		// Token: 0x06005F30 RID: 24368 RVA: 0x001488E4 File Offset: 0x00146AE4
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

		// Token: 0x06005F31 RID: 24369 RVA: 0x0014892C File Offset: 0x00146B2C
		internal byte[] GetEncodedBytesBuffer()
		{
			return this.GetEncodedBytesBuffer(Encoding.UTF8);
		}

		// Token: 0x06005F32 RID: 24370 RVA: 0x0014893C File Offset: 0x00146B3C
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

		// Token: 0x06005F33 RID: 24371 RVA: 0x001489CB File Offset: 0x00146BCB
		public override string ToString()
		{
			if (this._charBuffer == null || this._freePos <= 0)
			{
				return null;
			}
			return new string(this._charBuffer, 0, this._freePos);
		}

		// Token: 0x0400319B RID: 12699
		private const int BUFFER_SIZE = 1024;

		// Token: 0x0400319C RID: 12700
		private const int MAX_FREE_BUFFERS = 64;

		// Token: 0x0400319D RID: 12701
		private static CharBufferAllocator s_CharBufferAllocator = new CharBufferAllocator(1024, 64);

		// Token: 0x0400319E RID: 12702
		private static UbyteBufferAllocator s_ByteBufferAllocator = new UbyteBufferAllocator(Encoding.UTF8.GetMaxByteCount(1024), 64);

		// Token: 0x0400319F RID: 12703
		private char[] _charBuffer;

		// Token: 0x040031A0 RID: 12704
		private int _size;

		// Token: 0x040031A1 RID: 12705
		private int _freePos;

		// Token: 0x040031A2 RID: 12706
		private bool _recyclable;

		// Token: 0x040031A3 RID: 12707
		private byte[] _byteBuffer;
	}
}
