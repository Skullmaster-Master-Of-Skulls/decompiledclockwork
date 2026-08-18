using System;
using System.Text;

namespace System.Web.Hosting
{
	// Token: 0x020002A1 RID: 673
	internal class RecyclableByteBuffer
	{
		// Token: 0x0600230B RID: 8971 RVA: 0x00096D3A File Offset: 0x00095D3A
		internal RecyclableByteBuffer()
		{
			this._byteBuffer = (byte[])RecyclableByteBuffer.s_ByteBufferAllocator.GetBuffer();
			this._recyclable = true;
		}

		// Token: 0x0600230C RID: 8972 RVA: 0x00096D60 File Offset: 0x00095D60
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

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x0600230D RID: 8973 RVA: 0x00096DB3 File Offset: 0x00095DB3
		internal byte[] Buffer
		{
			get
			{
				return this._byteBuffer;
			}
		}

		// Token: 0x0600230E RID: 8974 RVA: 0x00096DBB File Offset: 0x00095DBB
		internal void Resize(int newSize)
		{
			this._byteBuffer = new byte[newSize];
			this._recyclable = false;
		}

		// Token: 0x0600230F RID: 8975 RVA: 0x00096DD0 File Offset: 0x00095DD0
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

		// Token: 0x06002310 RID: 8976 RVA: 0x00096E18 File Offset: 0x00095E18
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

		// Token: 0x06002311 RID: 8977 RVA: 0x00096E5C File Offset: 0x00095E5C
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

		// Token: 0x06002312 RID: 8978 RVA: 0x00096EF0 File Offset: 0x00095EF0
		internal string GetDecodedString(Encoding encoding, int len)
		{
			return encoding.GetString(this._byteBuffer, 0, len);
		}

		// Token: 0x06002313 RID: 8979 RVA: 0x00096F00 File Offset: 0x00095F00
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

		// Token: 0x04001B8C RID: 7052
		private const int BUFFER_SIZE = 4096;

		// Token: 0x04001B8D RID: 7053
		private const int MAX_FREE_BUFFERS = 64;

		// Token: 0x04001B8E RID: 7054
		private static UbyteBufferAllocator s_ByteBufferAllocator = new UbyteBufferAllocator(4096, 64);

		// Token: 0x04001B8F RID: 7055
		private static CharBufferAllocator s_CharBufferAllocator = new CharBufferAllocator(4096, 64);

		// Token: 0x04001B90 RID: 7056
		private int _offset;

		// Token: 0x04001B91 RID: 7057
		private byte[] _byteBuffer;

		// Token: 0x04001B92 RID: 7058
		private bool _recyclable;

		// Token: 0x04001B93 RID: 7059
		private char[] _charBuffer;
	}
}
