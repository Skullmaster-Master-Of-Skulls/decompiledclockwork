using System;
using System.Text;

namespace System.Web.Hosting
{
	// Token: 0x020007C4 RID: 1988
	internal class RecyclableByteBuffer
	{
		// Token: 0x06005F35 RID: 24373 RVA: 0x00148A16 File Offset: 0x00146C16
		internal RecyclableByteBuffer()
		{
			this._byteBuffer = (byte[])RecyclableByteBuffer.s_ByteBufferAllocator.GetBuffer();
			this._recyclable = true;
		}

		// Token: 0x06005F36 RID: 24374 RVA: 0x00148A3C File Offset: 0x00146C3C
		internal void Dispose()
		{
			if (this._recyclable)
			{
				if (this._byteBuffer != null)
				{
					RecyclableByteBuffer.s_ByteBufferAllocator.ReuseBuffer(this._byteBuffer);
				}
				if (this._charBuffer != null)
				{
					RecyclableByteBuffer.s_CharBufferAllocator.ReuseBuffer(this._charBuffer);
				}
			}
			this._byteBuffer = null;
			this._charBuffer = null;
		}

		// Token: 0x17001B68 RID: 7016
		// (get) Token: 0x06005F37 RID: 24375 RVA: 0x00148A8F File Offset: 0x00146C8F
		internal byte[] Buffer
		{
			get
			{
				return this._byteBuffer;
			}
		}

		// Token: 0x06005F38 RID: 24376 RVA: 0x00148A97 File Offset: 0x00146C97
		internal void Resize(int newSize)
		{
			this._byteBuffer = new byte[newSize];
			this._recyclable = false;
		}

		// Token: 0x06005F39 RID: 24377 RVA: 0x00148AAC File Offset: 0x00146CAC
		private void Skip(int count)
		{
			if (count <= 0)
			{
				return;
			}
			int num = this._byteBuffer.Length;
			int num2 = 0;
			for (int i = 0; i < num; i++)
			{
				if (this._byteBuffer[i] == 9 && ++num2 == count)
				{
					this._offset = i + 1;
					return;
				}
			}
		}

		// Token: 0x06005F3A RID: 24378 RVA: 0x00148AF4 File Offset: 0x00146CF4
		private int CalcLength()
		{
			if (this._byteBuffer != null)
			{
				int num = this._byteBuffer.Length;
				for (int i = this._offset; i < num; i++)
				{
					if (this._byteBuffer[i] == 0)
					{
						return i - this._offset;
					}
				}
			}
			return 0;
		}

		// Token: 0x06005F3B RID: 24379 RVA: 0x00148B38 File Offset: 0x00146D38
		private char[] GetDecodedCharBuffer(Encoding encoding, ref int len)
		{
			if (this._charBuffer != null)
			{
				return this._charBuffer;
			}
			if (len == 0)
			{
				this._charBuffer = new char[0];
			}
			else if (this._recyclable)
			{
				this._charBuffer = (char[])RecyclableByteBuffer.s_CharBufferAllocator.GetBuffer();
				len = encoding.GetChars(this._byteBuffer, this._offset, len, this._charBuffer, 0);
			}
			else
			{
				this._charBuffer = encoding.GetChars(this._byteBuffer, this._offset, len);
				len = this._charBuffer.Length;
			}
			return this._charBuffer;
		}

		// Token: 0x06005F3C RID: 24380 RVA: 0x00148BCC File Offset: 0x00146DCC
		internal string GetDecodedString(Encoding encoding, int len)
		{
			return encoding.GetString(this._byteBuffer, 0, len);
		}

		// Token: 0x06005F3D RID: 24381 RVA: 0x00148BDC File Offset: 0x00146DDC
		internal string[] GetDecodedTabSeparatedStrings(Encoding encoding, int numStrings, int numSkipStrings)
		{
			if (numSkipStrings > 0)
			{
				this.Skip(numSkipStrings);
			}
			int num = this.CalcLength();
			char[] decodedCharBuffer = this.GetDecodedCharBuffer(encoding, ref num);
			string[] array = new string[numStrings];
			int num2 = 0;
			int num3 = 0;
			for (int i = 0; i < numStrings; i++)
			{
				int num4 = num;
				for (int j = num2; j < num; j++)
				{
					if (decodedCharBuffer[j] == '\t')
					{
						num4 = j;
						break;
					}
				}
				if (num4 > num2)
				{
					array[i] = new string(decodedCharBuffer, num2, num4 - num2);
				}
				else
				{
					array[i] = string.Empty;
				}
				num3++;
				if (num4 == num)
				{
					break;
				}
				num2 = num4 + 1;
			}
			if (num3 < numStrings)
			{
				num = this.CalcLength();
				num2 = this._offset;
				for (int k = 0; k < numStrings; k++)
				{
					int num4 = num;
					for (int l = num2; l < num; l++)
					{
						if (this._byteBuffer[l] == 9)
						{
							num4 = l;
							break;
						}
					}
					if (num4 > num2)
					{
						array[k] = encoding.GetString(this._byteBuffer, num2, num4 - num2);
					}
					else
					{
						array[k] = string.Empty;
					}
					if (num4 == num)
					{
						break;
					}
					num2 = num4 + 1;
				}
			}
			return array;
		}

		// Token: 0x040031A4 RID: 12708
		private const int BUFFER_SIZE = 4096;

		// Token: 0x040031A5 RID: 12709
		private const int MAX_FREE_BUFFERS = 64;

		// Token: 0x040031A6 RID: 12710
		private static UbyteBufferAllocator s_ByteBufferAllocator = new UbyteBufferAllocator(4096, 64);

		// Token: 0x040031A7 RID: 12711
		private static CharBufferAllocator s_CharBufferAllocator = new CharBufferAllocator(4096, 64);

		// Token: 0x040031A8 RID: 12712
		private int _offset;

		// Token: 0x040031A9 RID: 12713
		private byte[] _byteBuffer;

		// Token: 0x040031AA RID: 12714
		private bool _recyclable;

		// Token: 0x040031AB RID: 12715
		private char[] _charBuffer;
	}
}
