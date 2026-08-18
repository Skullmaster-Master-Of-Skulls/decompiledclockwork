using System;
using System.Collections.Generic;

namespace OracleInternal.I18N
{
	// Token: 0x0200010F RID: 271
	internal class UTF16ConvUtility
	{
		// Token: 0x06000BD5 RID: 3029 RVA: 0x00084EF8 File Offset: 0x000830F8
		private UTF16ConvUtility()
		{
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x00084F00 File Offset: 0x00083100
		public static bool IsHiSurrogate(char c)
		{
			return (ushort)(c & 'ﰀ') == 55296;
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x00084F14 File Offset: 0x00083114
		public static bool IsLoSurrogate(char c)
		{
			return (ushort)(c & 'ﰀ') == 56320;
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x00084F28 File Offset: 0x00083128
		public static bool Check80toBF(byte b)
		{
			return (b & 192) == 128;
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x00084F38 File Offset: 0x00083138
		public static bool Check80to8F(byte b)
		{
			return (b & 240) == 128;
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x00084F48 File Offset: 0x00083148
		public static bool Check80to9F(byte b)
		{
			return (b & 224) == 128;
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x00084F58 File Offset: 0x00083158
		public static bool CheckA0toBF(byte b)
		{
			return (b & 224) == 160;
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x00084F68 File Offset: 0x00083168
		public static bool Check90toBF(byte b)
		{
			return (b & 192) == 128 && (b & 48) != 0;
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x00084F84 File Offset: 0x00083184
		public static char Conv3ByteUTFtoUTF16(byte c, byte c2, byte c3)
		{
			if ((c != 224 || !UTF16ConvUtility.CheckA0toBF(c2) || !UTF16ConvUtility.Check80toBF(c3)) && (c < 225 || c > 239 || !UTF16ConvUtility.Check80toBF(c2) || !UTF16ConvUtility.Check80toBF(c3)))
			{
				return '�';
			}
			return (char)((int)(c & 15) << 12 | (int)(c2 & 63) << 6 | (int)(c3 & 63));
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x00084FE4 File Offset: 0x000831E4
		public static bool isDefined(string str)
		{
			int length = str.Length;
			for (int i = 0; i < length; i++)
			{
				char c = str[i];
				if (c >= '\udc00' && c <= '\udfff')
				{
					return false;
				}
				if (c >= '\ud800' && c <= '\udbff')
				{
					if (i + 1 >= length)
					{
						return false;
					}
					char c2 = str[i + 1];
					if (c2 < '\udc00' || c2 > '\udfff')
					{
						return false;
					}
					i++;
				}
				else if (c == '￾' || c == '￿')
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x00085070 File Offset: 0x00083270
		public static void GetRemainingBytes(int numBytesToRead, byte[] buffer1, int offset1, int buffer1Bytes, IList<ArraySegment<byte>> bytes, ref int currSegIndex, ref int continuationOffset, byte[] dstBuffer)
		{
			int num = 0;
			for (int i = offset1; i < offset1 + buffer1Bytes; i++)
			{
				dstBuffer[num++] = buffer1[i];
				numBytesToRead--;
			}
			while (numBytesToRead > 0)
			{
				byte[] array = bytes[currSegIndex + 1].Array;
				int count = bytes[currSegIndex + 1].Count;
				int offset2 = bytes[currSegIndex + 1].Offset;
				continuationOffset = 0;
				for (int j = offset2; j < offset2 + count; j++)
				{
					dstBuffer[num++] = array[j];
					numBytesToRead--;
					continuationOffset++;
					if (numBytesToRead <= 0)
					{
						break;
					}
				}
				if (numBytesToRead > 0)
				{
					currSegIndex++;
				}
			}
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x0008512C File Offset: 0x0008332C
		public static int ConvertArraySegListToCharsImpl<T>(IList<ArraySegment<byte>> bytes, int bytesOffset, int bytesCount, T[] chars, int charOffset, ref int charCount, bool bUseReplacementChar, UTF16ConvUtility.ConvertToCharsDelegate<T> t)
		{
			int num = charOffset;
			int num2 = charCount;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			byte[] array = null;
			int byteCounts = 0;
			int num6 = 0;
			int offset = bytes[0].Offset;
			bool flag = false;
			if (bytesOffset > 0)
			{
				UTF16ConvUtility.GetSegementIndices(bytes, bytesOffset, ref num6, ref offset);
			}
			int num7 = num6;
			while (num7 < bytes.Count && !flag && num2 > 0)
			{
				int num8 = bytes[num7].Offset + num5;
				int num9 = bytes[num7].Count - num5;
				if (num7 == num6)
				{
					num8 = offset + num5;
					num9 = bytes[num7].Count - (offset - bytes[num7].Offset) - num5;
				}
				if (bytesCount - num4 <= num9)
				{
					num9 = bytesCount - num4;
					flag = true;
				}
				int num10 = t(bytes[num7].Array, num8, num9, chars, num, ref num2, bUseReplacementChar);
				num4 += num10;
				num += num2;
				num3 += num2;
				num2 = charCount - num3;
				if (num2 > 0 && num10 < num9 && !flag && num7 < bytes.Count - 1)
				{
					if (array == null)
					{
						array = new byte[4];
					}
					byte[] array2 = bytes[num7 + 1].Array;
					int num11 = num8 + num10;
					byte b = bytes[num7].Array[num11];
					int num12 = (int)((b & 240) / 16);
					int buffer1Bytes = num9 - num10;
					if (num12 == 12 || num12 == 13)
					{
						array[0] = b;
						array[1] = array2[bytes[num7 + 1].Offset];
						num5 = 1;
						byteCounts = 2;
					}
					else if (num12 == 14)
					{
						UTF16ConvUtility.GetRemainingBytes(3, bytes[num7].Array, num11, buffer1Bytes, bytes, ref num7, ref num5, array);
						byteCounts = 3;
					}
					else if (num12 == 15)
					{
						UTF16ConvUtility.GetRemainingBytes(4, bytes[num7].Array, num11, buffer1Bytes, bytes, ref num7, ref num5, array);
						byteCounts = 4;
					}
					num10 = t(array, 0, byteCounts, chars, num, ref num2, bUseReplacementChar);
					if (num10 == 0)
					{
						break;
					}
					num4 += num10;
					num += num2;
					num3 += num2;
					num2 = charCount - num3;
				}
				else
				{
					num5 = 0;
				}
				num7++;
			}
			charCount = num3;
			return num4;
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x0008537C File Offset: 0x0008357C
		public static int GetBytesOffsetListSegs(IList<ArraySegment<byte>> bytes, int charCount, UTF16ConvUtility.GetBytesOffsetDelegate t)
		{
			int num = charCount;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			byte[] array = null;
			int byteCount = 0;
			int num5 = 0;
			while (num5 < bytes.Count && num2 < charCount)
			{
				int num6 = bytes[num5].Offset + num4;
				int num7 = bytes[num5].Count - num4;
				byte[] array2 = bytes[num5].Array;
				int num8 = t(array2, num6, num7, ref num);
				num3 += num8;
				num2 += num;
				num = charCount - num2;
				if (num > 0 && num8 < num7 && num5 < bytes.Count - 1)
				{
					if (array == null)
					{
						array = new byte[4];
					}
					byte[] array3 = bytes[num5 + 1].Array;
					int num9 = num6 + num8;
					byte b = array2[num9];
					int num10 = (int)((b & 240) / 16);
					int buffer1Bytes = num7 - num8;
					if (num10 == 12 || num10 == 13)
					{
						array[0] = b;
						array[1] = array3[bytes[num5 + 1].Offset];
						num4 = 1;
						byteCount = 2;
					}
					else if (num10 == 14)
					{
						UTF16ConvUtility.GetRemainingBytes(3, array2, num9, buffer1Bytes, bytes, ref num5, ref num4, array);
						byteCount = 3;
					}
					else if (num10 == 15)
					{
						UTF16ConvUtility.GetRemainingBytes(4, array2, num9, buffer1Bytes, bytes, ref num5, ref num4, array);
						byteCount = 4;
					}
					num8 = t(array, 0, byteCount, ref num);
					num3 += num8;
					num2 += num;
					num = charCount - num2;
				}
				else
				{
					num4 = 0;
				}
				num5++;
			}
			return num3;
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x00085500 File Offset: 0x00083700
		public static int GetCharsLengthListSegs(IList<ArraySegment<byte>> bytes, int bytesOffset, int bytesCount, UTF16ConvUtility.GetCharsLengthDelegate t)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			byte[] array = null;
			int byteCount = 0;
			int num5 = 0;
			int offset = bytes[0].Offset;
			bool flag = false;
			if (bytesOffset > 0)
			{
				UTF16ConvUtility.GetSegementIndices(bytes, bytesOffset, ref num5, ref offset);
			}
			int num6 = num5;
			while (num6 < bytes.Count && !flag)
			{
				int num7 = bytes[num6].Offset + num4;
				int num8 = bytes[num6].Count - num4;
				if (num6 == num5)
				{
					num7 = offset + num4;
					num8 = bytes[num6].Count - (offset - bytes[num6].Offset) - num4;
				}
				if (bytesCount - num3 <= num8)
				{
					num8 = bytesCount - num3;
					flag = true;
				}
				int num9 = t(bytes[num6].Array, num7, num8, ref num2);
				num += num9;
				num3 += num2;
				if (num2 < num8 && !flag && num6 < bytes.Count - 1)
				{
					if (array == null)
					{
						array = new byte[4];
					}
					byte[] array2 = bytes[num6 + 1].Array;
					int num10 = num7 + num2;
					byte b = bytes[num6].Array[num10];
					int num11 = (int)((b & 240) / 16);
					int buffer1Bytes = num8 - num2;
					if (num11 == 12 || num11 == 13)
					{
						array[0] = b;
						array[1] = array2[bytes[num6 + 1].Offset];
						num4 = 1;
						byteCount = 2;
					}
					else if (num11 == 14)
					{
						UTF16ConvUtility.GetRemainingBytes(3, bytes[num6].Array, num10, buffer1Bytes, bytes, ref num6, ref num4, array);
						byteCount = 3;
					}
					else if (num11 == 15)
					{
						UTF16ConvUtility.GetRemainingBytes(4, bytes[num6].Array, num10, buffer1Bytes, bytes, ref num6, ref num4, array);
						byteCount = 4;
					}
					num9 = t(array, 0, byteCount, ref num2);
					num += num9;
					num3 += num2;
				}
				else
				{
					num4 = 0;
				}
				num6++;
			}
			return num;
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x00085710 File Offset: 0x00083910
		public static void GetSegementIndices(IList<ArraySegment<byte>> segs, int offset, ref int idx1, ref int offSet1)
		{
			int num = 0;
			int i = 0;
			idx1 = 0;
			offSet1 = segs[0].Offset;
			if (offset < 0)
			{
				return;
			}
			while (i < segs.Count)
			{
				num += segs[i].Count;
				if (offset < num)
				{
					int num2 = num - offset;
					idx1 = i;
					offSet1 = segs[i].Offset + segs[i].Count - num2;
					return;
				}
				i++;
			}
			idx1 = segs.Count;
		}

		// Token: 0x02000110 RID: 272
		// (Invoke) Token: 0x06000BE5 RID: 3045
		public delegate int ConvertToCharsDelegate<T>(byte[] bytes, int byteOffsets, int byteCounts, T[] chars, int charOffset, ref int charCount, bool bUseReplacementChar);

		// Token: 0x02000111 RID: 273
		// (Invoke) Token: 0x06000BE9 RID: 3049
		public delegate int GetBytesOffsetDelegate(byte[] bytes, int byteOffset, int byteCount, ref int charCount);

		// Token: 0x02000112 RID: 274
		// (Invoke) Token: 0x06000BED RID: 3053
		public delegate int GetCharsLengthDelegate(byte[] bytes, int byteOffset, int byteCount, ref int bytesCounted);
	}
}
