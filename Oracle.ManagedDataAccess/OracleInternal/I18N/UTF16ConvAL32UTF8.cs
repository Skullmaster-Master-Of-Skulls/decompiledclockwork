using System;
using System.Collections.Generic;
using System.Text;

namespace OracleInternal.I18N
{
	// Token: 0x0200010B RID: 267
	internal class UTF16ConvAL32UTF8 : Conv
	{
		// Token: 0x06000B8C RID: 2956 RVA: 0x00080E30 File Offset: 0x0007F030
		internal UTF16ConvAL32UTF8(int oracleId) : base(oracleId)
		{
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x00080E3C File Offset: 0x0007F03C
		private static int GetCharsLengthImpl(byte[] bytes, int byteOffset, int byteCount, ref int bytesCounted)
		{
			int num = 0;
			int num2 = byteOffset;
			int num3 = byteOffset + byteCount;
			if (num3 > bytes.Length)
			{
				num3 = bytes.Length;
			}
			bool flag = true;
			while (num2 < num3 && flag)
			{
				switch ((bytes[num2] & 240) >> 4)
				{
				case 0:
				case 1:
				case 2:
				case 3:
				case 4:
				case 5:
				case 6:
				case 7:
					num2++;
					num++;
					continue;
				case 12:
				case 13:
					if (num2 + 1 >= num3)
					{
						flag = false;
						bytesCounted = num2 - byteOffset;
						continue;
					}
					num++;
					num2 += 2;
					continue;
				case 14:
					if (num2 + 2 >= num3)
					{
						flag = false;
						bytesCounted = num2 - byteOffset;
						continue;
					}
					num++;
					num2 += 3;
					continue;
				case 15:
					if (num2 + 3 >= num3)
					{
						flag = false;
						bytesCounted = num2 - byteOffset;
						continue;
					}
					num += 2;
					num2 += 4;
					continue;
				}
				num2++;
				num++;
			}
			bytesCounted = num2 - byteOffset;
			return num;
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x00080F28 File Offset: 0x0007F128
		private static int GetBytesOffsetImpl(byte[] bytes, int byteOffset, int byteCount, ref int charCount)
		{
			int num = 0;
			int num2 = byteOffset;
			int num3 = byteOffset + byteCount;
			if (num3 > bytes.Length)
			{
				num3 = bytes.Length;
			}
			bool flag = true;
			while (num2 < num3 && flag && num < charCount)
			{
				switch ((bytes[num2] & 240) >> 4)
				{
				case 0:
				case 1:
				case 2:
				case 3:
				case 4:
				case 5:
				case 6:
				case 7:
					num2++;
					num++;
					continue;
				case 12:
				case 13:
					if (num2 + 1 >= num3)
					{
						flag = false;
						continue;
					}
					num++;
					num2 += 2;
					continue;
				case 14:
					if (num2 + 2 >= num3)
					{
						flag = false;
						continue;
					}
					num++;
					num2 += 3;
					continue;
				case 15:
					if (num2 + 3 >= num3)
					{
						flag = false;
						continue;
					}
					num += 2;
					num2 += 4;
					continue;
				}
				num2++;
				num++;
			}
			charCount = num;
			return num2 - byteOffset;
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x00081008 File Offset: 0x0007F208
		public override int GetBytesOffset(byte[] bytes, int byteOffset, int byteCount, int charCount)
		{
			int num = charCount;
			return UTF16ConvAL32UTF8.GetBytesOffsetImpl(bytes, byteOffset, byteCount, ref num);
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x00081024 File Offset: 0x0007F224
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
			for (int i = num; i < num2; i++)
			{
				int num4 = (int)chars[i];
				if (num4 >= 0 && num4 <= 127)
				{
					num3++;
				}
				else if (UTF16ConvUtility.IsHiSurrogate((char)num4))
				{
					if (i + 1 < num2 && UTF16ConvUtility.IsLoSurrogate(chars[i + 1]))
					{
						num3 += 4;
						i++;
					}
					else
					{
						num3 += 3;
					}
				}
				else if (num4 > 2047)
				{
					num3 += 3;
				}
				else
				{
					num3 += 2;
				}
			}
			return num3;
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x000810AC File Offset: 0x0007F2AC
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
			for (int i = num; i < num2; i++)
			{
				int num4 = (int)chars[i];
				if (num4 >= 0 && num4 <= 127)
				{
					num3++;
				}
				else if (UTF16ConvUtility.IsHiSurrogate((char)num4))
				{
					if (i + 1 < num2 && UTF16ConvUtility.IsLoSurrogate(chars[i + 1]))
					{
						num3 += 4;
						i++;
					}
					else
					{
						num3 += 3;
					}
				}
				else if (num4 > 2047)
				{
					num3 += 3;
				}
				else
				{
					num3 += 2;
				}
			}
			return num3;
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x00081148 File Offset: 0x0007F348
		public override int GetBytesLength(byte[] utf16Bytes, int utf16BytesOffset, int utf16BytesCount)
		{
			int num = utf16BytesOffset;
			int num2 = utf16BytesOffset + utf16BytesCount;
			int num3 = 0;
			if (num < 0)
			{
				num = 0;
			}
			if (num2 > utf16Bytes.Length)
			{
				num2 = utf16Bytes.Length;
			}
			for (int i = num; i < num2 - 1; i += 2)
			{
				int num4 = (int)utf16Bytes[i + 1] << 8 | (int)utf16Bytes[i];
				if (num4 >= 0 && num4 <= 127)
				{
					num3++;
				}
				else if (UTF16ConvUtility.IsHiSurrogate((char)num4))
				{
					if (i + 3 < num2 && UTF16ConvUtility.IsLoSurrogate((char)((int)utf16Bytes[i + 3] << 8 | (int)utf16Bytes[i + 2])))
					{
						num3 += 4;
						i += 2;
					}
					else
					{
						num3 += 3;
					}
				}
				else if (num4 > 2047)
				{
					num3 += 3;
				}
				else
				{
					num3 += 2;
				}
			}
			return num3;
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x000811E8 File Offset: 0x0007F3E8
		public override int ConvertCharsToBytes(char[] chars, int chars_offset, int chars_count, byte[] bytes, int bytes_begin, ref int bytes_count, bool bUseReplacementChar)
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
			int num5 = num;
			while (num5 < num2 && num4 < num3)
			{
				int num6 = (int)chars[num5];
				if (num6 >= 0 && num6 <= 127)
				{
					bytes[num4++] = (byte)num6;
				}
				else if (UTF16ConvUtility.IsHiSurrogate((char)num6))
				{
					int num7;
					if (num5 + 1 < num2 && UTF16ConvUtility.IsLoSurrogate((char)(num7 = (int)chars[num5 + 1])))
					{
						if (num4 + 3 >= num3)
						{
							break;
						}
						int num8 = (num6 >> 6 & 15) + 1;
						bytes[num4++] = (byte)(num8 >> 2 | 240);
						bytes[num4++] = (byte)((num8 & 3) << 4 | (num6 >> 2 & 15) | 128);
						bytes[num4++] = (byte)((num6 & 3) << 4 | (num7 >> 6 & 15) | 128);
						bytes[num4++] = (byte)((num7 & 63) | 128);
						num5++;
					}
					else
					{
						if (num4 + 2 >= num3)
						{
							break;
						}
						bytes[num4++] = Conv.REP_CHAR_UTF8[0];
						bytes[num4++] = Conv.REP_CHAR_UTF8[1];
						bytes[num4++] = Conv.REP_CHAR_UTF8[2];
					}
				}
				else if (num6 > 2047)
				{
					if (num4 + 2 >= num3)
					{
						break;
					}
					bytes[num4++] = (byte)(224 | (num6 >> 12 & 15));
					bytes[num4++] = (byte)(128 | (num6 >> 6 & 63));
					bytes[num4++] = (byte)(128 | (num6 & 63));
				}
				else
				{
					if (num4 + 1 >= num3)
					{
						break;
					}
					bytes[num4++] = (byte)(192 | (num6 >> 6 & 31));
					bytes[num4++] = (byte)(128 | (num6 & 63));
				}
				num5++;
			}
			bytes_count = num4 - bytes_begin;
			return num5 - num;
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x000813F4 File Offset: 0x0007F5F4
		public override int ConvertStringToBytes(string chars, int chars_offset, int chars_count, byte[] bytes, int bytes_begin, ref int bytes_count, bool bUseReplacementChar)
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
			int num5 = num;
			while (num5 < num2 && num4 < num3)
			{
				int num6 = (int)chars[num5];
				if (num6 >= 0 && num6 <= 127)
				{
					bytes[num4++] = (byte)num6;
				}
				else if (UTF16ConvUtility.IsHiSurrogate((char)num6))
				{
					int num7;
					if (num5 + 1 < num2 && UTF16ConvUtility.IsLoSurrogate((char)(num7 = (int)chars[num5 + 1])))
					{
						if (num4 + 3 >= num3)
						{
							break;
						}
						int num8 = (num6 >> 6 & 15) + 1;
						bytes[num4++] = (byte)(num8 >> 2 | 240);
						bytes[num4++] = (byte)((num8 & 3) << 4 | (num6 >> 2 & 15) | 128);
						bytes[num4++] = (byte)((num6 & 3) << 4 | (num7 >> 6 & 15) | 128);
						bytes[num4++] = (byte)((num7 & 63) | 128);
						num5++;
					}
					else
					{
						if (num4 + 2 >= num3)
						{
							break;
						}
						bytes[num4++] = Conv.REP_CHAR_UTF8[0];
						bytes[num4++] = Conv.REP_CHAR_UTF8[1];
						bytes[num4++] = Conv.REP_CHAR_UTF8[2];
					}
				}
				else if (num6 > 2047)
				{
					if (num4 + 2 >= num3)
					{
						break;
					}
					bytes[num4++] = (byte)(224 | (num6 >> 12 & 15));
					bytes[num4++] = (byte)(128 | (num6 >> 6 & 63));
					bytes[num4++] = (byte)(128 | (num6 & 63));
				}
				else
				{
					if (num4 + 1 >= num3)
					{
						break;
					}
					bytes[num4++] = (byte)(192 | (num6 >> 6 & 31));
					bytes[num4++] = (byte)(128 | (num6 & 63));
				}
				num5++;
			}
			bytes_count = num4 - bytes_begin;
			return num5 - num;
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x00081610 File Offset: 0x0007F810
		public override int ConvertUTF16ToBytes(byte[] utf16Bytes, int utf16BytesOffset, int utf16BytesCount, byte[] bytes, int byteOffset, ref int byteCount, bool bUseReplacementChar = true)
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
			int num4 = byteOffset;
			int num5 = num;
			while (num5 < num2 - 1 && num4 < num3)
			{
				int num6 = (int)utf16Bytes[num5 + 1] << 8 | (int)utf16Bytes[num5];
				if (num6 >= 0 && num6 <= 127)
				{
					bytes[num4++] = (byte)num6;
				}
				else if (UTF16ConvUtility.IsHiSurrogate((char)num6))
				{
					int num7;
					if (num5 + 3 < num2 && UTF16ConvUtility.IsLoSurrogate((char)(num7 = ((int)utf16Bytes[num5 + 3] << 8 | (int)utf16Bytes[num5 + 2]))))
					{
						if (num4 + 3 >= num3)
						{
							break;
						}
						int num8 = (num6 >> 6 & 15) + 1;
						bytes[num4++] = (byte)(num8 >> 2 | 240);
						bytes[num4++] = (byte)((num8 & 3) << 4 | (num6 >> 2 & 15) | 128);
						bytes[num4++] = (byte)((num6 & 3) << 4 | (num7 >> 6 & 15) | 128);
						bytes[num4++] = (byte)((num7 & 63) | 128);
						num5 += 2;
					}
					else
					{
						if (num4 + 2 >= num3)
						{
							break;
						}
						bytes[num4++] = Conv.REP_CHAR_UTF8[0];
						bytes[num4++] = Conv.REP_CHAR_UTF8[1];
						bytes[num4++] = Conv.REP_CHAR_UTF8[2];
					}
				}
				else if (num6 > 2047)
				{
					if (num4 + 2 >= num3)
					{
						break;
					}
					bytes[num4++] = (byte)(224 | (num6 >> 12 & 15));
					bytes[num4++] = (byte)(128 | (num6 >> 6 & 63));
					bytes[num4++] = (byte)(128 | (num6 & 63));
				}
				else
				{
					if (num4 + 1 >= num3)
					{
						break;
					}
					bytes[num4++] = (byte)(192 | (num6 >> 6 & 31));
					bytes[num4++] = (byte)(128 | (num6 & 63));
				}
				num5 += 2;
			}
			byteCount = num4 - byteOffset;
			return num5 - num;
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x00081830 File Offset: 0x0007FA30
		private static int ConvertBytesToCharsImpl(byte[] bytes, int offset, int count, char[] chars, int charOffset, ref int charCount, bool ccb)
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
			bool flag = true;
			while (num < num2 && num3 < num4 && flag)
			{
				byte b = bytes[num++];
				int num5 = (int)(b & 240);
				switch (num5 / 16)
				{
				case 0:
				case 1:
				case 2:
				case 3:
				case 4:
				case 5:
				case 6:
				case 7:
					chars[num3++] = (char)(b & byte.MaxValue);
					continue;
				case 12:
				case 13:
					if (num >= num2)
					{
						if (!ccb)
						{
							throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_UNICODE", new string[0]));
						}
						flag = false;
						num--;
						continue;
					}
					else
					{
						char c;
						if (b < 194 || b > 223 || (bytes[num] & 192) != 128)
						{
							c = '�';
						}
						else
						{
							c = (char)((int)(b & 31) << 6 | (int)(bytes[num] & 63));
						}
						num++;
						chars[num3++] = c;
						if (!ccb && c == '�')
						{
							throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_UNICODE", new string[0]));
						}
						continue;
					}
					break;
				case 14:
					if (num + 1 >= num2)
					{
						if (!ccb)
						{
							throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_UNICODE", new string[0]));
						}
						flag = false;
						num--;
						continue;
					}
					else
					{
						char c;
						if ((b != 224 || (bytes[num] & 224) != 160 || (bytes[num + 1] & 192) != 128) && (b < 225 || b > 236 || (bytes[num] & 192) != 128 || (bytes[num + 1] & 192) != 128) && (b != 237 || (bytes[num] & 224) != 128 || (bytes[num + 1] & 192) != 128) && (b < 238 || b > 239 || (bytes[num] & 192) != 128 || (bytes[num + 1] & 192) != 128))
						{
							c = '�';
						}
						else
						{
							c = (char)((int)(b & 15) << 12 | (int)(bytes[num] & 63) << 6 | (int)(bytes[num + 1] & 63));
						}
						num += 2;
						chars[num3++] = c;
						if ((b != Conv.REP_CHAR_UTF8[0] || bytes[num - 2] != Conv.REP_CHAR_UTF8[1] || bytes[num - 1] != Conv.REP_CHAR_UTF8[2]) && !ccb && c == '�')
						{
							throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_UNICODE", new string[0]));
						}
						continue;
					}
					break;
				case 15:
					if (num + 2 >= num2)
					{
						if (!ccb)
						{
							throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_UNICODE", new string[0]));
						}
						flag = false;
						num--;
						continue;
					}
					else
					{
						int num6;
						if ((b != 240 || (bytes[num] & 192) != 128 || (bytes[num] & 48) == 0 || (bytes[num + 1] & 192) != 128 || (bytes[num + 2] & 192) != 128) && (b < 241 || b > 243 || (bytes[num] & 192) != 128 || (bytes[num + 1] & 192) != 128 || (bytes[num + 2] & 192) != 128) && (b != 244 || (bytes[num] & 240) != 128 || (bytes[num + 1] & 192) != 128 || (bytes[num + 2] & 192) != 128))
						{
							chars[num3] = '�';
							num6 = 1;
						}
						else if (num3 + 1 >= chars.Length)
						{
							num6 = 0;
						}
						else
						{
							chars[num3] = (char)((((int)(b & 7) << 2 | (bytes[num] >> 4 & 3)) - 1 & 15) << 6 | (int)(bytes[num] & 15) << 2 | (bytes[num + 1] >> 4 & 3) | 55296);
							chars[num3 + 1] = (char)((int)(bytes[num + 1] & 15) << 6 | (int)(bytes[num + 2] & 63) | 56320);
							num6 = 2;
						}
						num += 3;
						if (num6 == 0)
						{
							flag = false;
							num -= 4;
							continue;
						}
						if (num6 != 1)
						{
							num3 += 2;
							continue;
						}
						if (!ccb)
						{
							throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_UNICODE", new string[0]));
						}
						num3++;
						continue;
					}
					break;
				}
				chars[num3++] = '�';
				if (!ccb)
				{
					throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_UNICODE", new string[0]));
				}
			}
			charCount = num3 - charOffset;
			return num - offset;
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x00081CE4 File Offset: 0x0007FEE4
		public override int ConvertBytesToChars(byte[] bytes, int offset, int count, char[] chars, int charOffset, ref int charCount, bool ccb)
		{
			return UTF16ConvAL32UTF8.ConvertBytesToCharsImpl(bytes, offset, count, chars, charOffset, ref charCount, ccb);
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x00081CF8 File Offset: 0x0007FEF8
		private static int ConvertBytesToUTF16Impl(byte[] bytes, int offset, int count, byte[] utfbytes, int utfOffset, ref int utfCount, bool ccb)
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
			bool flag = true;
			while (num < num2 && num3 + 1 < num4 && flag)
			{
				byte b = bytes[num++];
				int num5 = (int)(b & 240);
				switch (num5 / 16)
				{
				case 0:
				case 1:
				case 2:
				case 3:
				case 4:
				case 5:
				case 6:
				case 7:
					utfbytes[num3++] = b;
					utfbytes[num3++] = 0;
					continue;
				case 12:
				case 13:
					if (num >= num2)
					{
						if (!ccb)
						{
							throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_UNICODE", new string[0]));
						}
						flag = false;
						num--;
						continue;
					}
					else
					{
						char c;
						if (b < 194 || b > 223 || (bytes[num] & 192) != 128)
						{
							c = '�';
						}
						else
						{
							c = (char)((int)(b & 31) << 6 | (int)(bytes[num] & 63));
						}
						num++;
						if (!ccb && c == '�')
						{
							throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_UNICODE", new string[0]));
						}
						utfbytes[num3++] = (byte)(c & 'ÿ');
						utfbytes[num3++] = (byte)(c >> 8);
						continue;
					}
					break;
				case 14:
					if (num + 1 >= num2)
					{
						if (!ccb)
						{
							throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_UNICODE", new string[0]));
						}
						flag = false;
						num--;
						continue;
					}
					else
					{
						char c;
						if ((b != 224 || (bytes[num] & 224) != 160 || (bytes[num + 1] & 192) != 128) && (b < 225 || b > 236 || (bytes[num] & 192) != 128 || (bytes[num + 1] & 192) != 128) && (b != 237 || (bytes[num] & 224) != 128 || (bytes[num + 1] & 192) != 128) && (b < 238 || b > 239 || (bytes[num] & 192) != 128 || (bytes[num + 1] & 192) != 128))
						{
							c = '�';
						}
						else
						{
							c = (char)((int)(b & 15) << 12 | (int)(bytes[num] & 63) << 6 | (int)(bytes[num + 1] & 63));
						}
						num += 2;
						utfbytes[num3++] = (byte)(c & 'ÿ');
						utfbytes[num3++] = (byte)(c >> 8);
						if ((b != Conv.REP_CHAR_UTF8[0] || bytes[num - 2] != Conv.REP_CHAR_UTF8[1] || bytes[num - 1] != Conv.REP_CHAR_UTF8[2]) && !ccb && c == '�')
						{
							throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_UNICODE", new string[0]));
						}
						continue;
					}
					break;
				case 15:
					if (num + 2 >= num2)
					{
						if (!ccb)
						{
							throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_UNICODE", new string[0]));
						}
						flag = false;
						num--;
						continue;
					}
					else
					{
						char[] array = new char[2];
						int num6;
						if ((b != 240 || (bytes[num] & 192) != 128 || (bytes[num] & 48) == 0 || (bytes[num + 1] & 192) != 128 || (bytes[num + 2] & 192) != 128) && (b < 241 || b > 243 || (bytes[num] & 192) != 128 || (bytes[num + 1] & 192) != 128 || (bytes[num + 2] & 192) != 128) && (b != 244 || (bytes[num] & 240) != 128 || (bytes[num + 1] & 192) != 128 || (bytes[num + 2] & 192) != 128))
						{
							array[0] = '�';
							num6 = 1;
						}
						else
						{
							array[0] = (char)((((int)(b & 7) << 2 | (bytes[num] >> 4 & 3)) - 1 & 15) << 6 | (int)(bytes[num] & 15) << 2 | (bytes[num + 1] >> 4 & 3) | 55296);
							array[1] = (char)((int)(bytes[num + 1] & 15) << 6 | (int)(bytes[num + 2] & 63) | 56320);
							num6 = 2;
						}
						num += 3;
						if (num6 == 1)
						{
							utfbytes[num3++] = 253;
							utfbytes[num3++] = byte.MaxValue;
							if (!ccb)
							{
								throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_UNICODE", new string[0]));
							}
							continue;
						}
						else
						{
							if (num3 + 3 >= num4)
							{
								flag = false;
								num -= 4;
								continue;
							}
							utfbytes[num3++] = (byte)(array[0] & 'ÿ');
							utfbytes[num3++] = (byte)(array[0] >> 8);
							utfbytes[num3++] = (byte)(array[1] & 'ÿ');
							utfbytes[num3++] = (byte)(array[1] >> 8);
							continue;
						}
					}
					break;
				}
				utfbytes[num3++] = 253;
				utfbytes[num3++] = byte.MaxValue;
				if (!ccb)
				{
					throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_UNICODE", new string[0]));
				}
			}
			utfCount = num3 - utfOffset;
			return num - offset;
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x0008222C File Offset: 0x0008042C
		public override int ConvertBytesToUTF16(byte[] bytes, int offset, int count, byte[] utfbytes, int utfOffset, ref int utfCount, bool ccb)
		{
			return UTF16ConvAL32UTF8.ConvertBytesToUTF16Impl(bytes, offset, count, utfbytes, utfOffset, ref utfCount, ccb);
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x00082240 File Offset: 0x00080440
		public override int ConvertBytesToChars(IList<ArraySegment<byte>> bytes, int bytesOffset, int bytesCount, char[] chars, int charOffset, ref int charCount, bool bUseReplacementChar)
		{
			if (bytes.Count == 1)
			{
				return this.ConvertBytesToChars(bytes[0].Array, bytes[0].Offset + bytesOffset, bytesCount, chars, charOffset, ref charCount, bUseReplacementChar);
			}
			return UTF16ConvUtility.ConvertArraySegListToCharsImpl<char>(bytes, bytesOffset, bytesCount, chars, charOffset, ref charCount, bUseReplacementChar, UTF16ConvAL32UTF8.ConvertBytesToCharsInstance);
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x0008229C File Offset: 0x0008049C
		public override int GetCharsLength(byte[] bytes, int byteOffset, int byteCount)
		{
			int num = 0;
			return UTF16ConvAL32UTF8.GetCharsLengthImpl(bytes, byteOffset, byteCount, ref num);
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x000822B8 File Offset: 0x000804B8
		public override int GetCharsLength(ArraySegment<byte> bytes, int bytesOffset, int bytesCount)
		{
			int num = 0;
			return UTF16ConvAL32UTF8.GetCharsLengthImpl(bytes.Array, bytes.Offset + bytesOffset, bytesCount, ref num);
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x000822E0 File Offset: 0x000804E0
		public override int GetCharsLength(IList<ArraySegment<byte>> bytes, int bytesOffset, int bytesCount)
		{
			return UTF16ConvUtility.GetCharsLengthListSegs(bytes, bytesOffset, bytesCount, UTF16ConvAL32UTF8.GetCharsLengthInstance);
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x000822F0 File Offset: 0x000804F0
		public override int GetBytesOffset(IList<ArraySegment<byte>> bytes, int charCount)
		{
			return UTF16ConvUtility.GetBytesOffsetListSegs(bytes, charCount, UTF16ConvAL32UTF8.GetBytesOffsetInstance);
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000B9F RID: 2975 RVA: 0x00082300 File Offset: 0x00080500
		public override int MinBytesPerChar
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000BA0 RID: 2976 RVA: 0x00082304 File Offset: 0x00080504
		public override int MaxBytesPerChar
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x04000D17 RID: 3351
		private static UTF16ConvUtility.ConvertToCharsDelegate<char> ConvertBytesToCharsInstance = new UTF16ConvUtility.ConvertToCharsDelegate<char>(UTF16ConvAL32UTF8.ConvertBytesToCharsImpl);

		// Token: 0x04000D18 RID: 3352
		private static UTF16ConvUtility.ConvertToCharsDelegate<byte> ConvertBytesToUTF16Instance = new UTF16ConvUtility.ConvertToCharsDelegate<byte>(UTF16ConvAL32UTF8.ConvertBytesToUTF16Impl);

		// Token: 0x04000D19 RID: 3353
		private static UTF16ConvUtility.GetCharsLengthDelegate GetCharsLengthInstance = new UTF16ConvUtility.GetCharsLengthDelegate(UTF16ConvAL32UTF8.GetCharsLengthImpl);

		// Token: 0x04000D1A RID: 3354
		private static UTF16ConvUtility.GetBytesOffsetDelegate GetBytesOffsetInstance = new UTF16ConvUtility.GetBytesOffsetDelegate(UTF16ConvAL32UTF8.GetBytesOffsetImpl);
	}
}
