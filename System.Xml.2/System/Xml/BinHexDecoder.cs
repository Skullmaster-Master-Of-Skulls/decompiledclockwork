using System;
using System.Runtime.CompilerServices;

namespace System.Xml
{
	// Token: 0x02000066 RID: 102
	internal class BinHexDecoder : IncrementalReadDecoder
	{
		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600038A RID: 906 RVA: 0x0000E16C File Offset: 0x0000C36C
		internal override int DecodedCount
		{
			get
			{
				return this.curIndex - this.startIndex;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600038B RID: 907 RVA: 0x0000E17B File Offset: 0x0000C37B
		internal override bool IsFull
		{
			get
			{
				return this.curIndex == this.endIndex;
			}
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000E18C File Offset: 0x0000C38C
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
					BinHexDecoder.Decode(ptr2, ptr2 + len, ptr4, ptr4 + (this.endIndex - this.curIndex), ref this.hasHalfByteCached, ref this.cachedHalfByte, out result, out num);
				}
			}
			this.curIndex += num;
			return result;
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000E244 File Offset: 0x0000C444
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
					BinHexDecoder.Decode(ptr + startPos, ptr + startPos + len, ptr3, ptr3 + (this.endIndex - this.curIndex), ref this.hasHalfByteCached, ref this.cachedHalfByte, out result, out num);
				}
			}
			this.curIndex += num;
			return result;
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000E30D File Offset: 0x0000C50D
		internal override void Reset()
		{
			this.hasHalfByteCached = false;
			this.cachedHalfByte = 0;
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000E31D File Offset: 0x0000C51D
		internal override void SetNextOutputBuffer(Array buffer, int index, int count)
		{
			this.buffer = (byte[])buffer;
			this.startIndex = index;
			this.curIndex = index;
			this.endIndex = index + count;
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000E344 File Offset: 0x0000C544
		public unsafe static byte[] Decode(char[] chars, bool allowOddChars)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars");
			}
			int num = chars.Length;
			if (num == 0)
			{
				return new byte[0];
			}
			byte[] array = new byte[(num + 1) / 2];
			bool flag = false;
			byte b = 0;
			int num3;
			fixed (char* ptr = &chars[0])
			{
				char* ptr2 = ptr;
				fixed (byte* ptr3 = &array[0])
				{
					byte* ptr4 = ptr3;
					int num2;
					BinHexDecoder.Decode(ptr2, ptr2 + num, ptr4, ptr4 + array.Length, ref flag, ref b, out num2, out num3);
				}
			}
			if (flag && !allowOddChars)
			{
				throw new XmlException("Xml_InvalidBinHexValueOddCount", new string(chars));
			}
			if (num3 < array.Length)
			{
				byte[] array2 = new byte[num3];
				Array.Copy(array, 0, array2, 0, num3);
				array = array2;
			}
			return array;
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000E3F8 File Offset: 0x0000C5F8
		private unsafe static void Decode(char* pChars, char* pCharsEndPos, byte* pBytes, byte* pBytesEndPos, ref bool hasHalfByteCached, ref byte cachedHalfByte, out int charsDecoded, out int bytesDecoded)
		{
			char* ptr = pChars;
			byte* ptr2 = pBytes;
			XmlCharType instance = XmlCharType.Instance;
			while (ptr < pCharsEndPos && ptr2 < pBytesEndPos)
			{
				char c = *(ptr++);
				byte b;
				if (c >= 'a' && c <= 'f')
				{
					b = (byte)(c - 'a' + '\n');
				}
				else if (c >= 'A' && c <= 'F')
				{
					b = (byte)(c - 'A' + '\n');
				}
				else if (c >= '0' && c <= '9')
				{
					b = (byte)(c - '0');
				}
				else
				{
					if ((instance.charProperties[c] & 1) == 0)
					{
						throw new XmlException("Xml_InvalidBinHexValue", new string(pChars, 0, (int)((long)(pCharsEndPos - pChars))));
					}
					continue;
				}
				if (hasHalfByteCached)
				{
					*(ptr2++) = (byte)(((int)cachedHalfByte << 4) + (int)b);
					hasHalfByteCached = false;
				}
				else
				{
					cachedHalfByte = b;
					hasHalfByteCached = true;
				}
			}
			bytesDecoded = (int)((long)(ptr2 - pBytes));
			charsDecoded = (int)((long)(ptr - pChars));
		}

		// Token: 0x040001A1 RID: 417
		private byte[] buffer;

		// Token: 0x040001A2 RID: 418
		private int startIndex;

		// Token: 0x040001A3 RID: 419
		private int curIndex;

		// Token: 0x040001A4 RID: 420
		private int endIndex;

		// Token: 0x040001A5 RID: 421
		private bool hasHalfByteCached;

		// Token: 0x040001A6 RID: 422
		private byte cachedHalfByte;
	}
}
