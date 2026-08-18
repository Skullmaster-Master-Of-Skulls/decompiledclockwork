using System;
using System.Runtime.CompilerServices;

namespace System.Xml
{
	// Token: 0x02000062 RID: 98
	internal class Base64Decoder : IncrementalReadDecoder
	{
		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000373 RID: 883 RVA: 0x0000DB85 File Offset: 0x0000BD85
		internal override int DecodedCount
		{
			get
			{
				return this.curIndex - this.startIndex;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000374 RID: 884 RVA: 0x0000DB94 File Offset: 0x0000BD94
		internal override bool IsFull
		{
			get
			{
				return this.curIndex == this.endIndex;
			}
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0000DBA4 File Offset: 0x0000BDA4
		internal unsafe override int Decode(char[] chars, int startPos, int len)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars");
			}
			if (len < 0)
			{
				throw new ArgumentOutOfRangeException("len");
			}
			if (startPos < 0)
			{
				throw new ArgumentOutOfRangeException("startPos");
			}
			if (chars.Length - startPos < len)
			{
				throw new ArgumentOutOfRangeException("len");
			}
			if (len == 0)
			{
				return 0;
			}
			int result;
			int num;
			fixed (char* ptr = &chars[startPos])
			{
				char* ptr2 = ptr;
				fixed (byte* ptr3 = &this.buffer[this.curIndex])
				{
					byte* ptr4 = ptr3;
					this.Decode(ptr2, ptr2 + len, ptr4, ptr4 + (this.endIndex - this.curIndex), out result, out num);
				}
			}
			this.curIndex += num;
			return result;
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000DC54 File Offset: 0x0000BE54
		internal unsafe override int Decode(string str, int startPos, int len)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			if (len < 0)
			{
				throw new ArgumentOutOfRangeException("len");
			}
			if (startPos < 0)
			{
				throw new ArgumentOutOfRangeException("startPos");
			}
			if (str.Length - startPos < len)
			{
				throw new ArgumentOutOfRangeException("len");
			}
			if (len == 0)
			{
				return 0;
			}
			int result;
			int num;
			fixed (string text = str)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				fixed (byte* ptr2 = &this.buffer[this.curIndex])
				{
					byte* ptr3 = ptr2;
					this.Decode(ptr + startPos, ptr + startPos + len, ptr3, ptr3 + (this.endIndex - this.curIndex), out result, out num);
				}
			}
			this.curIndex += num;
			return result;
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000DD12 File Offset: 0x0000BF12
		internal override void Reset()
		{
			this.bitsFilled = 0;
			this.bits = 0;
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000DD22 File Offset: 0x0000BF22
		internal override void SetNextOutputBuffer(Array buffer, int index, int count)
		{
			this.buffer = (byte[])buffer;
			this.startIndex = index;
			this.curIndex = index;
			this.endIndex = index + count;
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000DD48 File Offset: 0x0000BF48
		private static byte[] ConstructMapBase64()
		{
			byte[] array = new byte[123];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = byte.MaxValue;
			}
			for (int j = 0; j < Base64Decoder.CharsBase64.Length; j++)
			{
				array[(int)Base64Decoder.CharsBase64[j]] = (byte)j;
			}
			return array;
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0000DD98 File Offset: 0x0000BF98
		private unsafe void Decode(char* pChars, char* pCharsEndPos, byte* pBytes, byte* pBytesEndPos, out int charsDecoded, out int bytesDecoded)
		{
			byte* ptr = pBytes;
			char* ptr2 = pChars;
			int num = this.bits;
			int num2 = this.bitsFilled;
			XmlCharType instance = XmlCharType.Instance;
			while (ptr2 < pCharsEndPos && ptr < pBytesEndPos)
			{
				char c = *ptr2;
				if (c == '=')
				{
					break;
				}
				ptr2++;
				if ((instance.charProperties[c] & 1) == 0)
				{
					int num3;
					if (c > 'z' || (num3 = (int)Base64Decoder.MapBase64[(int)c]) == 255)
					{
						throw new XmlException("Xml_InvalidBase64Value", new string(pChars, 0, (int)((long)(pCharsEndPos - pChars))));
					}
					num = (num << 6 | num3);
					num2 += 6;
					if (num2 >= 8)
					{
						*(ptr++) = (byte)(num >> num2 - 8 & 255);
						num2 -= 8;
						if (ptr == pBytesEndPos)
						{
							IL_F0:
							this.bits = num;
							this.bitsFilled = num2;
							bytesDecoded = (int)((long)(ptr - pBytes));
							charsDecoded = (int)((long)(ptr2 - pChars));
							return;
						}
					}
				}
			}
			if (ptr2 >= pCharsEndPos || *ptr2 != '=')
			{
				goto IL_F0;
			}
			num2 = 0;
			do
			{
				ptr2++;
			}
			while (ptr2 < pCharsEndPos && *ptr2 == '=');
			if (ptr2 < pCharsEndPos)
			{
				while ((instance.charProperties[*(ptr2++)] & 1) != 0)
				{
					if (ptr2 >= pCharsEndPos)
					{
						goto IL_F0;
					}
				}
				throw new XmlException("Xml_InvalidBase64Value", new string(pChars, 0, (int)((long)(pCharsEndPos - pChars))));
			}
			goto IL_F0;
		}

		// Token: 0x04000190 RID: 400
		private byte[] buffer;

		// Token: 0x04000191 RID: 401
		private int startIndex;

		// Token: 0x04000192 RID: 402
		private int curIndex;

		// Token: 0x04000193 RID: 403
		private int endIndex;

		// Token: 0x04000194 RID: 404
		private int bits;

		// Token: 0x04000195 RID: 405
		private int bitsFilled;

		// Token: 0x04000196 RID: 406
		private static readonly string CharsBase64 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";

		// Token: 0x04000197 RID: 407
		private static readonly byte[] MapBase64 = Base64Decoder.ConstructMapBase64();

		// Token: 0x04000198 RID: 408
		private const int MaxValidChar = 122;

		// Token: 0x04000199 RID: 409
		private const byte Invalid = 255;
	}
}
