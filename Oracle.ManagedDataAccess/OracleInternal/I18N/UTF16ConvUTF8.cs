using System;
using System.Collections.Generic;
using System.Text;

namespace OracleInternal.I18N
{
	// Token: 0x0200010C RID: 268
	internal class UTF16ConvUTF8 : Conv
	{
		// Token: 0x06000BA2 RID: 2978 RVA: 0x0008235C File Offset: 0x0008055C
		internal UTF16ConvUTF8(int oracleId) : base(oracleId)
		{
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x00082368 File Offset: 0x00080568
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
				byte b = bytes[num2];
				int num4 = (int)(b & 240);
				switch (num4 / 16)
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
				{
					if (num2 + 2 >= num3)
					{
						flag = false;
						continue;
					}
					char c = (char)((int)(bytes[num2] & 15) << 12 | (int)(bytes[num2 + 1] & 63) << 6 | (int)(bytes[num2 + 2] & 63));
					num2 += 3;
					num++;
					if (!UTF16ConvUtility.IsHiSurrogate(c))
					{
						continue;
					}
					if (num2 + 2 >= num3)
					{
						flag = false;
						continue;
					}
					num2 += 3;
					num++;
					continue;
				}
				}
				num2++;
				num++;
			}
			bytesCounted = num2 - byteOffset;
			return num;
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x00082470 File Offset: 0x00080670
		private int GetBytesOffsetImpl(byte[] bytes, int byteOffset, int byteCount, ref int charCount)
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
				byte b = bytes[num2];
				int num4 = (int)(b & 240);
				switch (num4 / 16)
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
				{
					if (num2 + 2 >= num3)
					{
						flag = false;
						continue;
					}
					char c = (char)((int)(bytes[num2] & 15) << 12 | (int)(bytes[num2 + 1] & 63) << 6 | (int)(bytes[num2 + 2] & 63));
					num2 += 3;
					num++;
					if (!UTF16ConvUtility.IsHiSurrogate(c))
					{
						continue;
					}
					if (num2 + 2 >= num3)
					{
						flag = false;
						continue;
					}
					num2 += 3;
					num++;
					continue;
				}
				}
				num2++;
				num++;
			}
			charCount = num;
			return num2 - byteOffset;
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x00082580 File Offset: 0x00080780
		public override int GetBytesOffset(byte[] bytes, int byteOffset, int byteCount, int charCount)
		{
			int num = charCount;
			return this.GetBytesOffsetImpl(bytes, byteOffset, byteCount, ref num);
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x0008259C File Offset: 0x0008079C
		private static int UTFToChars(byte[] bytes, int offset, int count, char[] chars, int charOffset, ref int charCount, ref int remainingBytes, bool ccb)
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
					chars[num3++] = (char)b;
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
						remainingBytes = 3;
						flag = false;
						num--;
						continue;
					}
					else
					{
						char c2;
						if ((b != 224 || (bytes[num] & 224) != 160 || (bytes[num + 1] & 192) != 128) && (b < 225 || b > 239 || (bytes[num] & 192) != 128 || (bytes[num + 1] & 192) != 128))
						{
							c2 = '�';
						}
						else
						{
							c2 = (char)((int)(b & 15) << 12 | (int)(bytes[num] & 63) << 6 | (int)(bytes[num + 1] & 63));
						}
						num += 2;
						if ((b != Conv.REP_CHAR_UTF8[0] || bytes[num - 2] != Conv.REP_CHAR_UTF8[1] || bytes[num - 1] != Conv.REP_CHAR_UTF8[2]) && !ccb && c2 == '�')
						{
							throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_UNICODE", new string[0]));
						}
						if (!UTF16ConvUtility.IsHiSurrogate(c2))
						{
							chars[num3++] = c2;
							continue;
						}
						if (num >= num2)
						{
							remainingBytes = 6;
							flag = false;
							num -= 3;
							continue;
						}
						b = bytes[num];
						if ((b & 240) != 224)
						{
							chars[num3++] = '�';
							if (!ccb)
							{
								throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_UNICODE", new string[0]));
							}
							continue;
						}
						else if (num + 2 >= num2)
						{
							if (!ccb)
							{
								throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_UNICODE", new string[0]));
							}
							remainingBytes = 6;
							flag = false;
							num -= 3;
							continue;
						}
						else
						{
							if (num3 + 1 >= num4)
							{
								flag = false;
								num -= 3;
								continue;
							}
							num++;
							char c;
							if ((b != 224 || (bytes[num] & 224) != 160 || (bytes[num + 1] & 192) != 128) && (b < 225 || b > 239 || (bytes[num] & 192) != 128 || (bytes[num + 1] & 192) != 128))
							{
								c = '�';
							}
							else
							{
								c = (char)((int)(b & 15) << 12 | (int)(bytes[num] & 63) << 6 | (int)(bytes[num + 1] & 63));
							}
							num += 2;
							if (UTF16ConvUtility.IsLoSurrogate(c))
							{
								chars[num3++] = c2;
							}
							else
							{
								chars[num3++] = '�';
								if (!ccb)
								{
									throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_UNICODE", new string[0]));
								}
							}
							chars[num3++] = c;
							continue;
						}
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

		// Token: 0x06000BA7 RID: 2983 RVA: 0x000829AC File Offset: 0x00080BAC
		private static int UTFToUTF16Bytes(byte[] bytes, int offset, int count, byte[] utfbytes, int utfOffset, ref int utfCount, ref int remainingBytes, bool ccb)
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
						utfbytes[num3++] = (byte)(c & 'ÿ');
						utfbytes[num3++] = (byte)(c >> 8);
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
						remainingBytes = 3;
						flag = false;
						num--;
						continue;
					}
					else
					{
						char c2;
						if ((b != 224 || (bytes[num] & 224) != 160 || (bytes[num + 1] & 192) != 128) && (b < 225 || b > 239 || (bytes[num] & 192) != 128 || (bytes[num + 1] & 192) != 128))
						{
							c2 = '�';
						}
						else
						{
							c2 = (char)((int)(b & 15) << 12 | (int)(bytes[num] & 63) << 6 | (int)(bytes[num + 1] & 63));
						}
						num += 2;
						if ((b != Conv.REP_CHAR_UTF8[0] || bytes[num - 2] != Conv.REP_CHAR_UTF8[1] || bytes[num - 1] != Conv.REP_CHAR_UTF8[2]) && !ccb && c2 == '�')
						{
							throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_UNICODE", new string[0]));
						}
						if (!UTF16ConvUtility.IsHiSurrogate(c2))
						{
							utfbytes[num3++] = (byte)(c2 & 'ÿ');
							utfbytes[num3++] = (byte)(c2 >> 8);
							continue;
						}
						if (num >= num2)
						{
							remainingBytes = 6;
							flag = false;
							num -= 3;
							continue;
						}
						b = bytes[num];
						if ((b & 240) != 224)
						{
							utfbytes[num3++] = 253;
							utfbytes[num3++] = byte.MaxValue;
							if (!ccb)
							{
								throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_UNICODE", new string[0]));
							}
							continue;
						}
						else if (num + 2 >= num2)
						{
							if (!ccb)
							{
								throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_UNICODE", new string[0]));
							}
							remainingBytes = 6;
							flag = false;
							num -= 3;
							continue;
						}
						else
						{
							if (num3 + 3 >= num4)
							{
								num -= 3;
								continue;
							}
							num++;
							char c;
							if ((b != 224 || (bytes[num] & 224) != 160 || (bytes[num + 1] & 192) != 128) && (b < 225 || b > 239 || (bytes[num] & 192) != 128 || (bytes[num + 1] & 192) != 128))
							{
								c = '�';
							}
							else
							{
								c = (char)((int)(b & 15) << 12 | (int)(bytes[num] & 63) << 6 | (int)(bytes[num + 1] & 63));
							}
							num += 2;
							if (UTF16ConvUtility.IsLoSurrogate(c))
							{
								utfbytes[num3++] = (byte)(c2 & 'ÿ');
								utfbytes[num3++] = (byte)(c2 >> 8);
							}
							else
							{
								utfbytes[num3++] = 253;
								utfbytes[num3++] = byte.MaxValue;
								if (!ccb)
								{
									throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_UNICODE", new string[0]));
								}
							}
							utfbytes[num3++] = (byte)(c & 'ÿ');
							utfbytes[num3++] = (byte)(c >> 8);
							continue;
						}
					}
					break;
				}
				if (num3 + 1 < num4)
				{
					utfbytes[num3++] = 253;
					utfbytes[num3++] = byte.MaxValue;
				}
				if (!ccb)
				{
					throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_UNICODE", new string[0]));
				}
			}
			utfCount = num3 - utfOffset;
			return num - offset;
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x00082E38 File Offset: 0x00081038
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

		// Token: 0x06000BA9 RID: 2985 RVA: 0x00082E94 File Offset: 0x00081094
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

		// Token: 0x06000BAA RID: 2986 RVA: 0x00082EFC File Offset: 0x000810FC
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

		// Token: 0x06000BAB RID: 2987 RVA: 0x00082F64 File Offset: 0x00081164
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

		// Token: 0x06000BAC RID: 2988 RVA: 0x0008307C File Offset: 0x0008127C
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

		// Token: 0x06000BAD RID: 2989 RVA: 0x000831A0 File Offset: 0x000813A0
		public override int ConvertUTF16ToBytes(byte[] utf16Bytes, int utf16BytesOffset, int utf16BytesCount, byte[] bytes, int byteOffset, ref int byteCount, bool bUseReplacementChar = true)
		{
			int num = utf16BytesOffset;
			int num2 = num + utf16BytesCount;
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

		// Token: 0x06000BAE RID: 2990 RVA: 0x000832C4 File Offset: 0x000814C4
		public override int ConvertBytesToChars(byte[] bytes, int byteOffset, int byteCount, char[] chars, int charOffset, ref int charCount, bool bUseReplacementChar)
		{
			int num = 0;
			return UTF16ConvUTF8.UTFToChars(bytes, byteOffset, byteCount, chars, charOffset, ref charCount, ref num, bUseReplacementChar);
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x000832E8 File Offset: 0x000814E8
		public override int ConvertBytesToUTF16(byte[] bytes, int byteOffset, int byteCount, byte[] utf16Bytes, int utf16BytesOffset, ref int utf16BytesCount, bool bUseReplacementChar)
		{
			int num = 0;
			return UTF16ConvUTF8.UTFToUTF16Bytes(bytes, byteOffset, byteCount, utf16Bytes, utf16BytesOffset, ref utf16BytesCount, ref num, bUseReplacementChar);
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x0008330C File Offset: 0x0008150C
		private int ConvertUtf8ArraySegListToCharsImpl<T>(IList<ArraySegment<byte>> bytes, int bytesOffset, int bytesCount, T[] chars, int charOffset, ref int charCount, bool bUseReplacementChar, UTF16ConvUTF8.ConvertToCharsDelegate<T> t)
		{
			int num = charOffset;
			int num2 = charCount;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			byte[] array = null;
			int byteCounts = 0;
			int num7 = 0;
			int offset = bytes[0].Offset;
			bool flag = false;
			if (bytesOffset > 0)
			{
				UTF16ConvUtility.GetSegementIndices(bytes, bytesOffset, ref num7, ref offset);
			}
			int num8 = num7;
			while (num8 < bytes.Count && !flag && num2 > 0)
			{
				int num9 = bytes[num8].Offset + num6;
				int num10 = bytes[num8].Count - num6;
				if (num8 == num7)
				{
					num9 = offset + num6;
					num10 = bytes[num8].Count - (offset - bytes[num8].Offset) - num6;
				}
				if (bytesCount - num4 <= num10)
				{
					num10 = bytesCount - num4;
					flag = true;
				}
				int num11 = t(bytes[num8].Array, num9, num10, chars, num, ref num2, ref num5, bUseReplacementChar);
				num4 += num11;
				num += num2;
				num3 += num2;
				num2 = charCount - num3;
				if (num2 > 0 && num11 < num10 && !flag && num8 < bytes.Count - 1)
				{
					if (array == null)
					{
						array = new byte[6];
					}
					byte[] array2 = bytes[num8 + 1].Array;
					int num12 = num9 + num11;
					byte b = bytes[num8].Array[num12];
					int num13 = (int)((b & 240) / 16);
					int buffer1Bytes = num10 - num11;
					if (num13 == 12 || num13 == 13)
					{
						array[0] = b;
						array[1] = array2[bytes[num8 + 1].Offset];
						num6 = 1;
						byteCounts = 2;
					}
					else if (num13 == 14)
					{
						UTF16ConvUtility.GetRemainingBytes(num5, bytes[num8].Array, num12, buffer1Bytes, bytes, ref num8, ref num6, array);
						byteCounts = num5;
						if (num5 == 3)
						{
							char c = UTF16ConvUtility.Conv3ByteUTFtoUTF16(array[0], array[1], array[2]);
							if (UTF16ConvUtility.IsHiSurrogate(c))
							{
								UTF16ConvUtility.GetRemainingBytes(6, bytes[num8].Array, num12, buffer1Bytes, bytes, ref num8, ref num6, array);
								num5 = 6;
								byteCounts = 6;
							}
						}
					}
					num11 = t(array, 0, byteCounts, chars, num, ref num2, ref num5, bUseReplacementChar);
					if (num11 == 0)
					{
						break;
					}
					num4 += num11;
					num += num2;
					num3 += num2;
					num2 = charCount - num3;
				}
				else
				{
					num6 = 0;
				}
				num8++;
			}
			charCount = num3;
			return num4;
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x00083584 File Offset: 0x00081784
		public override int GetBytesOffset(IList<ArraySegment<byte>> bytes, int charCount)
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
				byte[] array2 = bytes[num5].Array;
				int num6 = bytes[num5].Offset + num4;
				int num7 = bytes[num5].Count - num4;
				int bytesOffsetImpl = this.GetBytesOffsetImpl(array2, num6, num7, ref num);
				num3 += bytesOffsetImpl;
				num2 += num;
				num = charCount - num2;
				if (num > 0 && bytesOffsetImpl < num7 && num5 < bytes.Count - 1)
				{
					if (array == null)
					{
						array = new byte[6];
					}
					byte[] array3 = bytes[num5 + 1].Array;
					int num8 = num6 + bytesOffsetImpl;
					byte b = array2[num8];
					int num9 = (int)((b & 240) / 16);
					int buffer1Bytes = num7 - bytesOffsetImpl;
					if (num9 == 12 || num9 == 13)
					{
						array[0] = b;
						array[1] = array3[bytes[num5 + 1].Offset];
						num4 = 1;
						byteCount = 2;
					}
					else if (num9 == 14)
					{
						UTF16ConvUtility.GetRemainingBytes(3, array2, num8, buffer1Bytes, bytes, ref num5, ref num4, array);
						byteCount = 3;
						char c = UTF16ConvUtility.Conv3ByteUTFtoUTF16(array[0], array[1], array[2]);
						if (UTF16ConvUtility.IsHiSurrogate(c))
						{
							UTF16ConvUtility.GetRemainingBytes(6, array2, num8, buffer1Bytes, bytes, ref num5, ref num4, array);
							byteCount = 6;
						}
					}
					bytesOffsetImpl = this.GetBytesOffsetImpl(array, 0, byteCount, ref num);
					num3 += bytesOffsetImpl;
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

		// Token: 0x06000BB2 RID: 2994 RVA: 0x0008371C File Offset: 0x0008191C
		public override int ConvertBytesToChars(IList<ArraySegment<byte>> bytes, int bytesOffset, int bytesCount, char[] chars, int charOffset, ref int charCount, bool bUseReplacementChar)
		{
			if (bytes.Count == 1)
			{
				return this.ConvertBytesToChars(bytes[0].Array, bytes[0].Offset + bytesOffset, bytesCount, chars, charOffset, ref charCount, bUseReplacementChar);
			}
			return this.ConvertUtf8ArraySegListToCharsImpl<char>(bytes, bytesOffset, bytesCount, chars, charOffset, ref charCount, bUseReplacementChar, new UTF16ConvUTF8.ConvertToCharsDelegate<char>(UTF16ConvUTF8.UTFToChars));
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x00083780 File Offset: 0x00081980
		public override int GetCharsLength(byte[] bytes, int byteOffset, int byteCount)
		{
			int num = 0;
			return UTF16ConvUTF8.GetCharsLengthImpl(bytes, byteOffset, byteCount, ref num);
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x0008379C File Offset: 0x0008199C
		public override int GetCharsLength(ArraySegment<byte> bytes, int bytesOffset, int bytesCount)
		{
			int num = 0;
			return UTF16ConvUTF8.GetCharsLengthImpl(bytes.Array, bytes.Offset + bytesOffset, bytesCount, ref num);
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x000837C4 File Offset: 0x000819C4
		public override int GetCharsLength(IList<ArraySegment<byte>> bytes, int bytesOffset, int bytesCount)
		{
			return UTF16ConvUtility.GetCharsLengthListSegs(bytes, bytesOffset, bytesCount, new UTF16ConvUtility.GetCharsLengthDelegate(UTF16ConvUTF8.GetCharsLengthImpl));
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000BB6 RID: 2998 RVA: 0x000837DC File Offset: 0x000819DC
		public override int MinBytesPerChar
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000BB7 RID: 2999 RVA: 0x000837E0 File Offset: 0x000819E0
		public override int MaxBytesPerChar
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x0200010D RID: 269
		// (Invoke) Token: 0x06000BB9 RID: 3001
		private delegate int ConvertToCharsDelegate<T>(byte[] bytes, int byteOffsets, int byteCounts, T[] chars, int charOffset, ref int charCount, ref int remainingBytes, bool bUseReplacementChar);
	}
}
