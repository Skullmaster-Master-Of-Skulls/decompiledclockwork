using System;
using System.Collections.Generic;
using System.Text;

namespace OracleInternal.I18N
{
	// Token: 0x02000105 RID: 261
	[Serializable]
	internal class TLBConvShift : TLBConv12Byte
	{
		// Token: 0x06000B39 RID: 2873 RVA: 0x0007DCA4 File Offset: 0x0007BEA4
		internal override bool IsShitCharset()
		{
			return true;
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x0007DCA8 File Offset: 0x0007BEA8
		public TLBConvShift()
		{
			this.m_groupId = 7;
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x0007DCB8 File Offset: 0x0007BEB8
		private int GetCharsLengthShiftImpl(byte[] bytes, int offset, int count, ref bool shiftIn, ref int bytesCounted)
		{
			byte b = 15;
			int i = offset;
			int num = offset + count;
			if (num > bytes.Length)
			{
				num = bytes.Length;
			}
			int num2 = 0;
			while (i < num)
			{
				if (bytes[i] == 15)
				{
					b = 15;
					shiftIn = true;
					i++;
				}
				else if (bytes[i] == 14)
				{
					b = 14;
					shiftIn = false;
					i++;
				}
				else if (b == 15 && shiftIn)
				{
					int srcChar = (int)(bytes[i] & byte.MaxValue);
					int num3 = base.ToUnicode(srcChar, true);
					if (((long)num3 & (long)((ulong)-1)) > 65535L)
					{
						num2 += 2;
					}
					else
					{
						num2++;
					}
					i++;
				}
				else
				{
					if (i >= num - 1)
					{
						bytesCounted = i - offset;
						break;
					}
					int srcChar = ((int)bytes[i] << 8 & 65280) | (int)(bytes[i + 1] & byte.MaxValue);
					int num4 = base.ToUnicode(srcChar, true);
					if (((long)num4 & (long)((ulong)-1)) > 65535L)
					{
						num2 += 2;
					}
					else
					{
						num2++;
					}
					i += 2;
				}
			}
			bytesCounted = i - offset;
			return num2;
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x0007DDB0 File Offset: 0x0007BFB0
		public override int GetCharsLength(byte[] bytes, int byteOffset, int byteCount)
		{
			int num = 0;
			bool flag = true;
			return this.GetCharsLengthShiftImpl(bytes, byteOffset, byteCount, ref flag, ref num);
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x0007DDD0 File Offset: 0x0007BFD0
		private int GetBytesOffsetShiftImpl(byte[] bytes, int offset, int count, ref int charCount, ref bool shiftIn)
		{
			byte b = 15;
			int num = offset;
			int num2 = offset + count;
			if (num2 > bytes.Length)
			{
				num2 = bytes.Length;
			}
			int num3 = 0;
			while (num < num2 && num3 < charCount)
			{
				if (bytes[num] == 15)
				{
					b = 15;
					shiftIn = true;
					num++;
				}
				else if (bytes[num] == 14)
				{
					b = 14;
					shiftIn = false;
					num++;
				}
				else if (b == 15 && shiftIn)
				{
					int srcChar = (int)(bytes[num] & byte.MaxValue);
					int num4 = base.ToUnicode(srcChar, true);
					if (((long)num4 & (long)((ulong)-1)) > 65535L)
					{
						num3 += 2;
					}
					else
					{
						num3++;
					}
					num++;
				}
				else
				{
					if (num >= num2 - 1)
					{
						break;
					}
					int srcChar = ((int)bytes[num] << 8 & 65280) | (int)(bytes[num + 1] & byte.MaxValue);
					int num5 = base.ToUnicode(srcChar, true);
					if (((long)num5 & (long)((ulong)-1)) > 65535L)
					{
						num3 += 2;
					}
					else
					{
						num3++;
					}
					num += 2;
				}
			}
			charCount = num3;
			return num - offset;
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x0007DEC4 File Offset: 0x0007C0C4
		public override int GetBytesOffset(byte[] bytes, int byteOffset, int byteCount, int charCount)
		{
			int num = charCount;
			bool flag = true;
			return this.GetBytesOffsetShiftImpl(bytes, byteOffset, byteCount, ref num, ref flag);
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x0007DEE4 File Offset: 0x0007C0E4
		public override int GetCharsLength(ArraySegment<byte> bytes, int bytesOffset, int bytesCount)
		{
			int num = 0;
			bool flag = true;
			return this.GetCharsLengthShiftImpl(bytes.Array, bytes.Offset + bytesOffset, bytesCount, ref flag, ref num);
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x0007DF10 File Offset: 0x0007C110
		public override int GetCharsLength(IList<ArraySegment<byte>> bytes, int bytesOffset, int bytesCount)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			byte[] array = null;
			bool flag = true;
			int num5 = 0;
			int offset = bytes[0].Offset;
			bool flag2 = false;
			if (bytesOffset > 0)
			{
				UTF16ConvUtility.GetSegementIndices(bytes, bytesOffset, ref num5, ref offset);
			}
			int num6 = num5;
			while (num6 < bytes.Count && !flag2)
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
					flag2 = true;
				}
				int charsLengthShiftImpl = this.GetCharsLengthShiftImpl(bytes[num6].Array, num7, num8, ref flag, ref num2);
				num += charsLengthShiftImpl;
				num3 += num2;
				if (num2 < num8 && !flag2 && num6 < bytes.Count - 1)
				{
					if (array == null)
					{
						array = new byte[2];
					}
					byte[] array2 = bytes[num6 + 1].Array;
					int num9 = num7 + num2;
					byte b = bytes[num6].Array[num9];
					UTF16ConvUtility.GetRemainingBytes(2, bytes[num6].Array, num9, 1, bytes, ref num6, ref num4, array);
					int count = 2;
					charsLengthShiftImpl = this.GetCharsLengthShiftImpl(array, 0, count, ref flag, ref num2);
					num += charsLengthShiftImpl;
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

		// Token: 0x06000B41 RID: 2881 RVA: 0x0007E0A8 File Offset: 0x0007C2A8
		private int ConvertBytesToCharsImpl(byte[] bytes, int offset, int count, char[] chars, int charOffset, ref int charCount, ref bool shiftIn, bool ccb)
		{
			byte b = 15;
			int num = offset;
			if (num < 0)
			{
				num = 0;
			}
			int num2 = num + count;
			if (num2 > bytes.Length)
			{
				num2 = bytes.Length;
			}
			int num3 = charOffset;
			if (num3 < 0)
			{
				num3 = 0;
			}
			int num4 = num3 + charCount;
			if (num4 > chars.Length)
			{
				num4 = chars.Length;
			}
			while (num < num2 && num3 < num4)
			{
				if (bytes[num] == 15)
				{
					b = 15;
					shiftIn = true;
					num++;
				}
				else if (bytes[num] == 14)
				{
					b = 14;
					shiftIn = false;
					num++;
				}
				else if (b == 15 && shiftIn)
				{
					int srcChar = (int)(bytes[num] & byte.MaxValue);
					int num5 = base.ToUnicode(srcChar, ccb);
					if (((long)num5 & (long)((ulong)-1)) > 65535L)
					{
						if (num3 >= num4)
						{
							break;
						}
						chars[num3++] = (char)(num5 >> 16);
						chars[num3++] = (char)(num5 & 65535);
					}
					else
					{
						chars[num3++] = (char)num5;
					}
					num++;
				}
				else if (num < num2 - 1)
				{
					int srcChar = ((int)bytes[num] << 8 & 65280) | (int)(bytes[num + 1] & byte.MaxValue);
					int num6 = base.ToUnicode(srcChar, ccb);
					if (((long)num6 & (long)((ulong)-1)) > 65535L)
					{
						if (num3 >= num4)
						{
							break;
						}
						chars[num3++] = (char)(num6 >> 16);
						chars[num3++] = (char)(num6 & 65535);
					}
					else
					{
						chars[num3++] = (char)num6;
					}
					num += 2;
				}
				else
				{
					if (!ccb)
					{
						throw new DecoderFallbackException("Failed to convert bytes to Unicode");
					}
					break;
				}
			}
			charCount = num3 - charOffset;
			return num - offset;
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x0007E234 File Offset: 0x0007C434
		private int ConvertBytesToUTF16Impl(byte[] bytes, int offset, int count, byte[] utfbytes, int utfOffset, ref int utfCount, ref bool shiftIn, bool ccb)
		{
			int num = offset;
			if (num < 0)
			{
				num = 0;
			}
			int num2 = num + count;
			if (num2 > bytes.Length)
			{
				num2 = bytes.Length;
			}
			int num3 = utfOffset;
			if (num3 < 0)
			{
				num3 = 0;
			}
			int num4 = num3 + utfCount;
			if (num4 > utfbytes.Length)
			{
				num4 = utfbytes.Length;
			}
			byte b = 15;
			while (num < num2 && num3 + 1 < num4)
			{
				if (bytes[num] == 15)
				{
					b = 15;
					shiftIn = true;
					num++;
				}
				else if (bytes[num] == 14)
				{
					b = 14;
					shiftIn = false;
					num++;
				}
				else if (b == 15 && shiftIn)
				{
					int srcChar = (int)(bytes[num] & byte.MaxValue);
					int num5 = base.ToUnicode(srcChar, ccb);
					if (((long)num5 & (long)((ulong)-1)) > 65535L)
					{
						if (num3 + 3 >= num4)
						{
							break;
						}
						char[] array = new char[]
						{
							(char)(num5 >> 16),
							(char)(num5 & 65535)
						};
						utfbytes[num3++] = (byte)(array[0] & 'ÿ');
						utfbytes[num3++] = (byte)(array[0] >> 8);
						utfbytes[num3++] = (byte)(array[1] & 'ÿ');
						utfbytes[num3++] = (byte)(array[1] >> 8);
					}
					else
					{
						utfbytes[num3++] = (byte)((ushort)num5 & 255);
						utfbytes[num3++] = (byte)((ushort)num5 >> 8);
					}
					num++;
				}
				else if (num < num2 - 1)
				{
					int srcChar = ((int)bytes[num] << 8 & 65280) | (int)(bytes[num + 1] & byte.MaxValue);
					int num6 = base.ToUnicode(srcChar, ccb);
					if (((long)num6 & (long)((ulong)-1)) > 65535L)
					{
						if (num3 + 3 >= num4)
						{
							break;
						}
						char[] array2 = new char[]
						{
							(char)(num6 >> 16),
							(char)(num6 & 65535)
						};
						utfbytes[num3++] = (byte)(array2[0] & 'ÿ');
						utfbytes[num3++] = (byte)(array2[0] >> 8);
						utfbytes[num3++] = (byte)(array2[1] & 'ÿ');
						utfbytes[num3++] = (byte)(array2[1] >> 8);
					}
					else
					{
						utfbytes[num3++] = (byte)((ushort)num6 & 255);
						utfbytes[num3++] = (byte)((ushort)num6 >> 8);
					}
					num += 2;
				}
				else
				{
					if (!ccb)
					{
						throw new DecoderFallbackException("Failed to convert bytes to Unicode");
					}
					break;
				}
			}
			utfCount = num3 - utfOffset;
			return num - offset;
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x0007E470 File Offset: 0x0007C670
		public override int ConvertBytesToUTF16(byte[] bytes, int offset, int count, byte[] utfbytes, int utfOffset, ref int utfCount, bool ccb)
		{
			bool flag = true;
			return this.ConvertBytesToUTF16Impl(bytes, offset, count, utfbytes, utfOffset, ref utfCount, ref flag, ccb);
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x0007E494 File Offset: 0x0007C694
		public override int ConvertBytesToChars(byte[] bytes, int offset, int count, char[] chars, int charOffset, ref int charCount, bool ccb)
		{
			bool flag = true;
			return this.ConvertBytesToCharsImpl(bytes, offset, count, chars, charOffset, ref charCount, ref flag, ccb);
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x0007E4B8 File Offset: 0x0007C6B8
		internal override int ConvertBytesToChars(byte[] bytes, int offset, int count, char[] chars, int charOffset, ref int charCount, ref bool shiftIn, bool ccb)
		{
			return this.ConvertBytesToCharsImpl(bytes, offset, count, chars, charOffset, ref charCount, ref shiftIn, ccb);
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x0007E4D8 File Offset: 0x0007C6D8
		private int ConvertByteArraySegListToCharsShiftImpl<T>(IList<ArraySegment<byte>> bytes, int bytesOffset, int bytesCount, T[] chars, int charOffset, ref int charCount, ref bool shiftState, bool bUseReplacementChar, TLBConvShift.ConvertByteToCharsShiftDelegate<T> t)
		{
			int num = charOffset;
			int num2 = charCount;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			byte[] array = null;
			bool flag = shiftState;
			int num6 = 0;
			int offset = bytes[0].Offset;
			bool flag2 = false;
			if (bytesOffset > 0)
			{
				UTF16ConvUtility.GetSegementIndices(bytes, bytesOffset, ref num6, ref offset);
			}
			int num7 = num6;
			while (num7 < bytes.Count && !flag2 && num2 > 0)
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
					flag2 = true;
				}
				int num10 = t(bytes[num7].Array, num8, num9, chars, num, ref num2, ref flag, bUseReplacementChar);
				num4 += num10;
				num += num2;
				num3 += num2;
				num2 = charCount - num3;
				if (num2 > 0 && num10 < num9 && !flag2 && num7 < bytes.Count - 1)
				{
					if (array == null)
					{
						array = new byte[2];
					}
					byte[] array2 = bytes[num7 + 1].Array;
					int num11 = num8 + num10;
					byte b = bytes[num7].Array[num11];
					UTF16ConvUtility.GetRemainingBytes(2, bytes[num7].Array, num11, 1, bytes, ref num7, ref num5, array);
					int byteCounts = 2;
					num10 = t(array, 0, byteCounts, chars, num, ref num2, ref flag, bUseReplacementChar);
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
			shiftState = flag;
			charCount = num3;
			return num4;
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x0007E6BC File Offset: 0x0007C8BC
		public override int GetBytesOffset(IList<ArraySegment<byte>> bytes, int charCount)
		{
			int num = charCount;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			byte[] array = null;
			bool flag = true;
			int num5 = 0;
			while (num5 < bytes.Count && num2 < charCount)
			{
				int num6 = bytes[num5].Offset + num4;
				int num7 = bytes[num5].Count - num4;
				byte[] array2 = bytes[num5].Array;
				int bytesOffsetShiftImpl = this.GetBytesOffsetShiftImpl(array2, num6, num7, ref num, ref flag);
				num3 += bytesOffsetShiftImpl;
				num2 += num;
				num = charCount - num2;
				if (num > 0 && bytesOffsetShiftImpl < num7 && num5 < bytes.Count - 1)
				{
					if (array == null)
					{
						array = new byte[2];
					}
					byte[] array3 = bytes[num5 + 1].Array;
					int num8 = num6 + bytesOffsetShiftImpl;
					byte b = array2[num8];
					UTF16ConvUtility.GetRemainingBytes(2, array2, num8, 1, bytes, ref num5, ref num4, array);
					int count = 2;
					bytesOffsetShiftImpl = this.GetBytesOffsetShiftImpl(array, 0, count, ref num, ref flag);
					num3 += bytesOffsetShiftImpl;
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

		// Token: 0x06000B48 RID: 2888 RVA: 0x0007E7D0 File Offset: 0x0007C9D0
		public override int ConvertBytesToChars(IList<ArraySegment<byte>> bytes, int bytesOffset, int bytesCount, char[] chars, int charOffset, ref int charCount, bool bUseReplacementChar)
		{
			bool flag = true;
			return this.ConvertByteArraySegListToCharsShiftImpl<char>(bytes, bytesOffset, bytesCount, chars, charOffset, ref charCount, ref flag, bUseReplacementChar, new TLBConvShift.ConvertByteToCharsShiftDelegate<char>(this.ConvertBytesToCharsImpl));
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x0007E800 File Offset: 0x0007CA00
		internal override int ConvertBytesToChars(IList<ArraySegment<byte>> bytes, int bytesOffset, int bytesCount, char[] chars, int charOffset, ref int charCount, ref bool shiftIn, bool bUseReplacementChar)
		{
			return this.ConvertByteArraySegListToCharsShiftImpl<char>(bytes, bytesOffset, bytesCount, chars, charOffset, ref charCount, ref shiftIn, bUseReplacementChar, new TLBConvShift.ConvertByteToCharsShiftDelegate<char>(this.ConvertBytesToCharsImpl));
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000B4A RID: 2890 RVA: 0x0007E82C File Offset: 0x0007CA2C
		public override int MinBytesPerChar
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000B4B RID: 2891 RVA: 0x0007E830 File Offset: 0x0007CA30
		public override int MaxBytesPerChar
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x0007E834 File Offset: 0x0007CA34
		public override int GetBytesLength(char[] chars, int charOffset, int charCount)
		{
			int num = charOffset;
			int num2 = charOffset + charCount;
			int num3 = 0;
			if (num < 0)
			{
				num = 0;
			}
			if (num2 > chars.Length)
			{
				num2 = chars.Length;
			}
			byte b = 15;
			int i = num;
			while (i < num2)
			{
				char c;
				if (chars[i] < '\ud800' || chars[i] >= '\udc00')
				{
					c = base.ToOracleCharacter(chars[i], '\0', true);
					goto IL_94;
				}
				if (i + 1 < num2 && chars[i + 1] >= '\udc00' && chars[i + 1] <= '\udfff')
				{
					c = base.ToOracleCharacter(chars[i], chars[i + 1], true);
					i++;
					goto IL_94;
				}
				if (b == 15)
				{
					b = 14;
					num3++;
				}
				num3 += 2;
				IL_C4:
				i++;
				continue;
				IL_94:
				if ((c & '＀') != '\0')
				{
					if (b == 15)
					{
						b = 14;
						num3++;
					}
					num3 += 2;
					goto IL_C4;
				}
				if (b == 14)
				{
					b = 15;
					num3++;
				}
				num3++;
				goto IL_C4;
			}
			if (b == 14)
			{
				num3++;
			}
			return num3;
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x0007E920 File Offset: 0x0007CB20
		public override int GetBytesLength(string chars, int charOffset, int charCount)
		{
			int num = charOffset;
			int num2 = charOffset + charCount;
			int num3 = 0;
			if (num < 0)
			{
				num = 0;
			}
			if (num2 > chars.Length)
			{
				num2 = chars.Length;
			}
			byte b = 15;
			int i = num;
			while (i < num2)
			{
				char c;
				if (chars[i] < '\ud800' || chars[i] >= '\udc00')
				{
					c = base.ToOracleCharacter(chars[i], '\0', true);
					goto IL_B6;
				}
				if (i + 1 < num2 && chars[i + 1] >= '\udc00' && chars[i + 1] <= '\udfff')
				{
					c = base.ToOracleCharacter(chars[i], chars[i + 1], true);
					i++;
					goto IL_B6;
				}
				if (b == 15)
				{
					b = 14;
					num3++;
				}
				num3 += 2;
				IL_E6:
				i++;
				continue;
				IL_B6:
				if ((c & '＀') != '\0')
				{
					if (b == 15)
					{
						b = 14;
						num3++;
					}
					num3 += 2;
					goto IL_E6;
				}
				if (b == 14)
				{
					b = 15;
					num3++;
				}
				num3++;
				goto IL_E6;
			}
			if (b == 14)
			{
				num3++;
			}
			return num3;
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x0007EA30 File Offset: 0x0007CC30
		public override int GetBytesLength(byte[] utf16Bytes, int utf16BytesOffset, int utf16BytesCount)
		{
			int num = utf16BytesOffset;
			int num2 = utf16BytesOffset + utf16BytesCount;
			if (num < 0)
			{
				num = 0;
			}
			if (num2 > utf16Bytes.Length)
			{
				num2 = utf16Bytes.Length;
			}
			char c = char.MaxValue;
			byte b = 15;
			int num3 = 0;
			int i = num;
			while (i < num2 - 1)
			{
				int num4 = (int)utf16Bytes[i + 1] << 8 | (int)utf16Bytes[i];
				if (num4 < 55296 || num4 >= 56320)
				{
					c = base.ToOracleCharacter((char)num4, '\0', true);
					goto IL_A8;
				}
				if (i + 3 < num2)
				{
					int num5 = (int)utf16Bytes[i + 3] << 8 | (int)utf16Bytes[i + 2];
					if (num5 >= 56320 && num5 <= 57343)
					{
						c = base.ToOracleCharacter((char)num4, (char)num5, true);
						i++;
						goto IL_A8;
					}
					goto IL_A8;
				}
				else
				{
					if (b == 15)
					{
						b = 14;
						num3++;
					}
					num3 += 2;
				}
				IL_DF:
				i += 2;
				continue;
				IL_A8:
				if ((c & '＀') != '\0')
				{
					if (b == 15)
					{
						b = 14;
						num3++;
					}
					num3 += 2;
					goto IL_DF;
				}
				if (b == 14)
				{
					b = 15;
					num3++;
				}
				num3++;
				goto IL_DF;
			}
			if (b == 14)
			{
				num3++;
			}
			return num3;
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x0007EB3C File Offset: 0x0007CD3C
		public override int ConvertCharsToBytes(char[] chars, int chars_offset, int chars_count, byte[] bytes, int bytes_begin, ref int bytes_count, bool ccb)
		{
			int num = chars_offset;
			int num2 = chars_offset + chars_count;
			if (num < 0)
			{
				num = 0;
			}
			if (num2 > chars.Length)
			{
				num2 = chars.Length;
			}
			if (bytes_begin < 0)
			{
				bytes_begin = 0;
			}
			int num3 = bytes_begin + bytes_count;
			if (num3 > bytes.Length)
			{
				num3 = bytes.Length;
			}
			int num4 = bytes_begin;
			byte b = 15;
			int num5 = num;
			while (num5 < num2 && num4 < num3)
			{
				bool flag = false;
				char c;
				if (chars[num5] < '\ud800' || chars[num5] >= '\udc00')
				{
					c = base.ToOracleCharacter(chars[num5], '\0', ccb);
					goto IL_11C;
				}
				if (num5 + 1 < num2 && chars[num5 + 1] >= '\udc00' && chars[num5 + 1] <= '\udfff')
				{
					c = base.ToOracleCharacter(chars[num5], chars[num5 + 1], ccb);
					flag = true;
					num5++;
					goto IL_11C;
				}
				if (!ccb)
				{
					throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_BYTES", new string[0]));
				}
				if (num4 + 1 >= num3)
				{
					break;
				}
				if (b == 15)
				{
					if (num4 + 2 >= num3)
					{
						break;
					}
					b = 14;
					bytes[num4++] = 14;
				}
				bytes[num4++] = (byte)(this.m_2ByteOraCharReplacement[0] >> 8);
				bytes[num4++] = (byte)this.m_2ByteOraCharReplacement[0];
				IL_1B9:
				num5++;
				continue;
				IL_11C:
				int num6;
				if ((num6 = (int)(c & '＀')) == 0)
				{
					if (b == 14)
					{
						if (num4 + 1 >= num3)
						{
							if (flag)
							{
								num5--;
								break;
							}
							break;
						}
						else
						{
							b = 15;
							bytes[num4++] = 15;
						}
					}
					bytes[num4++] = (byte)c;
					goto IL_1B9;
				}
				if (num4 + 1 < num3)
				{
					if (b == 15)
					{
						if (num4 + 2 >= num3)
						{
							if (flag)
							{
								num5--;
								break;
							}
							break;
						}
						else
						{
							b = 14;
							bytes[num4++] = 14;
						}
					}
					bytes[num4++] = (byte)(num6 >> 8);
					bytes[num4++] = (byte)c;
					goto IL_1B9;
				}
				if (flag)
				{
					num5--;
					break;
				}
				break;
			}
			if (b == 14 && num4 < num3)
			{
				bytes[num4++] = 15;
			}
			bytes_count = num4 - bytes_begin;
			return num5 - num;
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x0007ED38 File Offset: 0x0007CF38
		public override int ConvertStringToBytes(string chars, int chars_offset, int chars_count, byte[] bytes, int bytes_begin, ref int bytes_count, bool ccb)
		{
			int num = chars_offset;
			int num2 = chars_offset + chars_count;
			if (num < 0)
			{
				num = 0;
			}
			if (num2 > chars.Length)
			{
				num2 = chars.Length;
			}
			if (bytes_begin < 0)
			{
				bytes_begin = 0;
			}
			int num3 = bytes_begin + bytes_count;
			if (num3 > bytes.Length)
			{
				num3 = bytes.Length;
			}
			int num4 = bytes_begin;
			byte b = 15;
			int num5 = num;
			while (num5 < num2 && num4 < num3)
			{
				bool flag = false;
				char c;
				if (chars[num5] < '\ud800' || chars[num5] >= '\udc00')
				{
					c = base.ToOracleCharacter(chars[num5], '\0', ccb);
					goto IL_141;
				}
				if (num5 + 1 < num2 && chars[num5 + 1] >= '\udc00' && chars[num5 + 1] <= '\udfff')
				{
					c = base.ToOracleCharacter(chars[num5], chars[num5 + 1], ccb);
					flag = true;
					num5++;
					goto IL_141;
				}
				if (!ccb)
				{
					throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_BYTES", new string[0]));
				}
				if (num4 + 1 >= num3)
				{
					break;
				}
				if (b == 15)
				{
					if (num4 + 2 >= num3)
					{
						break;
					}
					b = 14;
					bytes[num4++] = 14;
				}
				bytes[num4++] = (byte)(this.m_2ByteOraCharReplacement[0] >> 8);
				bytes[num4++] = (byte)this.m_2ByteOraCharReplacement[0];
				IL_1DE:
				num5++;
				continue;
				IL_141:
				int num6;
				if ((num6 = (int)(c & '＀')) == 0)
				{
					if (b == 14)
					{
						if (num4 + 1 >= num3)
						{
							if (flag)
							{
								num5--;
								break;
							}
							break;
						}
						else
						{
							b = 15;
							bytes[num4++] = 15;
						}
					}
					bytes[num4++] = (byte)c;
					goto IL_1DE;
				}
				if (num4 + 1 < num3)
				{
					if (b == 15)
					{
						if (num4 + 2 >= num3)
						{
							if (flag)
							{
								num5--;
								break;
							}
							break;
						}
						else
						{
							b = 14;
							bytes[num4++] = 14;
						}
					}
					bytes[num4++] = (byte)(num6 >> 8);
					bytes[num4++] = (byte)c;
					goto IL_1DE;
				}
				if (flag)
				{
					num5--;
					break;
				}
				break;
			}
			if (b == 14 && num4 < num3)
			{
				bytes[num4++] = 15;
			}
			bytes_count = num4 - bytes_begin;
			return num5 - num;
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x0007EF5C File Offset: 0x0007D15C
		public override int ConvertUTF16ToBytes(byte[] utf16Bytes, int utf16BytesOffset, int utf16BytesCount, byte[] bytes, int byteOffset, ref int byteCount, bool ccb = true)
		{
			int num = utf16BytesOffset;
			int num2 = utf16BytesOffset + utf16BytesCount;
			if (num < 0)
			{
				num = 0;
			}
			if (num2 > utf16Bytes.Length)
			{
				num2 = utf16Bytes.Length;
			}
			if (byteOffset < 0)
			{
				byteOffset = 0;
			}
			int num3 = byteOffset + byteCount;
			if (num3 > bytes.Length)
			{
				num3 = bytes.Length;
			}
			char c = char.MaxValue;
			byte b = 15;
			int num4 = byteOffset;
			int num5 = num;
			while (num5 < num2 - 1 && num4 < num3)
			{
				bool flag = false;
				int num6 = (int)utf16Bytes[num5 + 1] << 8 | (int)utf16Bytes[num5];
				if (num6 < 55296 || num6 >= 56320)
				{
					c = base.ToOracleCharacter((char)num6, '\0', ccb);
					goto IL_134;
				}
				if (num5 + 3 < num2)
				{
					int num7 = (int)utf16Bytes[num5 + 3] << 8 | (int)utf16Bytes[num5 + 2];
					if (num7 >= 56320 && num7 <= 57343)
					{
						c = base.ToOracleCharacter((char)num6, (char)num7, ccb);
						flag = true;
						num5 += 2;
						goto IL_134;
					}
					goto IL_134;
				}
				else
				{
					if (!ccb)
					{
						throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_BYTES", new string[0]));
					}
					if (num4 + 1 >= num3)
					{
						break;
					}
					if (b == 15)
					{
						if (num4 + 2 >= num3)
						{
							break;
						}
						b = 14;
						bytes[num4++] = 14;
					}
					bytes[num4++] = (byte)(this.m_2ByteOraCharReplacement[0] >> 8);
					bytes[num4++] = (byte)this.m_2ByteOraCharReplacement[0];
				}
				IL_1D1:
				num5 += 2;
				continue;
				IL_134:
				int num8;
				if ((num8 = (int)(c & '＀')) == 0)
				{
					if (b == 14)
					{
						if (num4 + 1 >= num3)
						{
							if (flag)
							{
								num5 -= 2;
								break;
							}
							break;
						}
						else
						{
							b = 15;
							bytes[num4++] = 15;
						}
					}
					bytes[num4++] = (byte)c;
					goto IL_1D1;
				}
				if (num4 + 1 < num3)
				{
					if (b == 15)
					{
						if (num4 + 2 >= num3)
						{
							if (flag)
							{
								num5 -= 2;
								break;
							}
							break;
						}
						else
						{
							b = 14;
							bytes[num4++] = 14;
						}
					}
					bytes[num4++] = (byte)(num8 >> 8);
					bytes[num4++] = (byte)c;
					goto IL_1D1;
				}
				if (flag)
				{
					num5 -= 2;
					break;
				}
				break;
			}
			if (b == 14 && num4 < num3)
			{
				bytes[num4++] = 15;
			}
			byteCount = num4 - byteOffset;
			return num5 - num;
		}

		// Token: 0x04000D13 RID: 3347
		private const byte SHIFT_OUT = 14;

		// Token: 0x04000D14 RID: 3348
		private const byte SHIFT_IN = 15;

		// Token: 0x02000106 RID: 262
		// (Invoke) Token: 0x06000B53 RID: 2899
		private delegate int ConvertByteToCharsShiftDelegate<T>(byte[] bytes, int byteOffsets, int byteCounts, T[] chars, int charOffset, ref int charCount, ref bool shiftIn, bool bUseReplacementChar);
	}
}
