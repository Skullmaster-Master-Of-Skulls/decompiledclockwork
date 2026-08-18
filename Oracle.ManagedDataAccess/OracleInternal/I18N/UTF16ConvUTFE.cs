using System;
using System.Collections.Generic;
using System.Text;

namespace OracleInternal.I18N
{
	// Token: 0x0200010E RID: 270
	internal class UTF16ConvUTFE : Conv
	{
		// Token: 0x06000BBC RID: 3004 RVA: 0x000837E4 File Offset: 0x000819E4
		internal UTF16ConvUTFE(int oracleId) : base(oracleId)
		{
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x000837F0 File Offset: 0x000819F0
		private static int GetCharsLengthImpl(byte[] bytes, int byteOffset, int byteCount, ref int bytesCounted)
		{
			int num = byteOffset;
			int num2 = byteOffset + byteCount;
			int num3 = 0;
			if (num2 > bytes.Length)
			{
				num2 = bytes.Length;
			}
			bool flag = true;
			while (num < num2 && flag)
			{
				byte b = UTF16ConvUTFE.utfe2utf8m[UTF16ConvUTFE.high((int)bytes[num])][UTF16ConvUTFE.low((int)bytes[num])];
				switch (b >> 4 & 15)
				{
				case 0:
				case 1:
				case 2:
				case 3:
				case 4:
				case 5:
				case 6:
				case 7:
				case 8:
				case 9:
					num3++;
					num++;
					continue;
				case 12:
				case 13:
					if (num + 1 >= num2)
					{
						flag = false;
						bytesCounted = num - byteOffset;
						continue;
					}
					num3++;
					num += 2;
					continue;
				case 14:
					if (num + 2 >= num2)
					{
						flag = false;
						bytesCounted = num - byteOffset;
						continue;
					}
					num3++;
					num += 3;
					continue;
				case 15:
					if (num + 3 >= num2)
					{
						flag = false;
						bytesCounted = num - byteOffset;
						continue;
					}
					num3++;
					num += 4;
					continue;
				}
				num3++;
				num++;
			}
			return num3;
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x000838EC File Offset: 0x00081AEC
		private static int GetBytesOffsetImpl(byte[] bytes, int byteOffset, int byteCount, ref int charCount)
		{
			int num = byteOffset;
			int num2 = byteOffset + byteCount;
			int num3 = 0;
			if (num2 > bytes.Length)
			{
				num2 = bytes.Length;
			}
			bool flag = true;
			while (num < num2 && flag && num3 < charCount)
			{
				byte b = UTF16ConvUTFE.utfe2utf8m[UTF16ConvUTFE.high((int)bytes[num])][UTF16ConvUTFE.low((int)bytes[num])];
				switch (b >> 4 & 15)
				{
				case 0:
				case 1:
				case 2:
				case 3:
				case 4:
				case 5:
				case 6:
				case 7:
				case 8:
				case 9:
					num3++;
					num++;
					continue;
				case 12:
				case 13:
					if (num + 1 >= num2)
					{
						flag = false;
						continue;
					}
					num3++;
					num += 2;
					continue;
				case 14:
					if (num + 2 >= num2)
					{
						flag = false;
						continue;
					}
					num3++;
					num += 3;
					continue;
				case 15:
					if (num + 3 >= num2)
					{
						flag = false;
						continue;
					}
					num3++;
					num += 4;
					continue;
				}
				num3++;
				num++;
			}
			charCount = num3;
			return num - byteOffset;
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x000839E4 File Offset: 0x00081BE4
		public override int GetBytesOffset(byte[] bytes, int byteOffset, int byteCount, int charCount)
		{
			int num = charCount;
			return UTF16ConvUTFE.GetBytesOffsetImpl(bytes, byteOffset, byteCount, ref num);
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x00083A00 File Offset: 0x00081C00
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
				byte b = UTF16ConvUTFE.utfe2utf8m[UTF16ConvUTFE.high((int)bytes[num])][UTF16ConvUTFE.low((int)bytes[num++])];
				switch (b >> 4 & 15)
				{
				case 0:
				case 1:
				case 2:
				case 3:
				case 4:
				case 5:
				case 6:
				case 7:
					chars[num3++] = (char)(b & 127);
					continue;
				case 8:
				case 9:
					chars[num3++] = (char)(b & 31);
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
						b &= 31;
						byte b2 = UTF16ConvUTFE.utfe2utf8m[UTF16ConvUTFE.high((int)bytes[num])][UTF16ConvUTFE.low((int)bytes[num++])];
						if (UTF16ConvUTFE.is101xxxxx(b2))
						{
							chars[num3++] = (char)((int)b << 5 | (int)(b2 & 31));
							continue;
						}
						if (!ccb)
						{
							throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_UNICODE", new string[0]));
						}
						chars[num3++] = '�';
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
						b &= 15;
						byte b2 = UTF16ConvUTFE.utfe2utf8m[UTF16ConvUTFE.high((int)bytes[num])][UTF16ConvUTFE.low((int)bytes[num++])];
						byte b3 = UTF16ConvUTFE.utfe2utf8m[UTF16ConvUTFE.high((int)bytes[num])][UTF16ConvUTFE.low((int)bytes[num++])];
						if (UTF16ConvUTFE.is101xxxxx(b2) && UTF16ConvUTFE.is101xxxxx(b3))
						{
							chars[num3++] = (char)((int)b << 10 | (int)(b2 & 31) << 5 | (int)(b3 & 31));
							continue;
						}
						if (!ccb)
						{
							throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_UNICODE", new string[0]));
						}
						chars[num3++] = '�';
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
						b &= 1;
						byte b2 = UTF16ConvUTFE.utfe2utf8m[UTF16ConvUTFE.high((int)bytes[num])][UTF16ConvUTFE.low((int)bytes[num++])];
						byte b3 = UTF16ConvUTFE.utfe2utf8m[UTF16ConvUTFE.high((int)bytes[num])][UTF16ConvUTFE.low((int)bytes[num++])];
						byte b4 = UTF16ConvUTFE.utfe2utf8m[UTF16ConvUTFE.high((int)bytes[num])][UTF16ConvUTFE.low((int)bytes[num++])];
						if (UTF16ConvUTFE.is101xxxxx(b2) && UTF16ConvUTFE.is101xxxxx(b3) && UTF16ConvUTFE.is101xxxxx(b4))
						{
							chars[num3++] = (char)((int)b << 15 | (int)(b2 & 31) << 10 | (int)(b3 & 31) << 5 | (int)(b4 & 31));
							continue;
						}
						if (!ccb)
						{
							throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_UNICODE", new string[0]));
						}
						chars[num3++] = '�';
						continue;
					}
					break;
				}
				if (!ccb)
				{
					throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_UNICODE", new string[0]));
				}
				chars[num3++] = '�';
			}
			charCount = num3 - charOffset;
			return num - offset;
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x00083D78 File Offset: 0x00081F78
		public override int ConvertBytesToChars(byte[] bytes, int offset, int count, char[] chars, int charOffset, ref int charCount, bool ccb)
		{
			return UTF16ConvUTFE.ConvertBytesToCharsImpl(bytes, offset, count, chars, charOffset, ref charCount, ccb);
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x00083D8C File Offset: 0x00081F8C
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
				byte b = UTF16ConvUTFE.utfe2utf8m[UTF16ConvUTFE.high((int)bytes[num])][UTF16ConvUTFE.low((int)bytes[num++])];
				switch (b >> 4 & 15)
				{
				case 0:
				case 1:
				case 2:
				case 3:
				case 4:
				case 5:
				case 6:
				case 7:
					utfbytes[num3++] = (b & 127);
					utfbytes[num3++] = 0;
					continue;
				case 8:
				case 9:
					utfbytes[num3++] = (b & 31);
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
						b &= 31;
						byte b2 = UTF16ConvUTFE.utfe2utf8m[UTF16ConvUTFE.high((int)bytes[num])][UTF16ConvUTFE.low((int)bytes[num++])];
						if (UTF16ConvUTFE.is101xxxxx(b2))
						{
							char c = (char)((int)b << 5 | (int)(b2 & 31));
							utfbytes[num3++] = (byte)(c & 'ÿ');
							utfbytes[num3++] = (byte)(c >> 8);
							continue;
						}
						if (!ccb)
						{
							throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_UNICODE", new string[0]));
						}
						utfbytes[num3++] = 253;
						utfbytes[num3++] = byte.MaxValue;
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
						b &= 15;
						byte b2 = UTF16ConvUTFE.utfe2utf8m[UTF16ConvUTFE.high((int)bytes[num])][UTF16ConvUTFE.low((int)bytes[num++])];
						byte b3 = UTF16ConvUTFE.utfe2utf8m[UTF16ConvUTFE.high((int)bytes[num])][UTF16ConvUTFE.low((int)bytes[num++])];
						if (UTF16ConvUTFE.is101xxxxx(b2) && UTF16ConvUTFE.is101xxxxx(b3))
						{
							char c = (char)((int)b << 10 | (int)(b2 & 31) << 5 | (int)(b3 & 31));
							utfbytes[num3++] = (byte)(c & 'ÿ');
							utfbytes[num3++] = (byte)(c >> 8);
							continue;
						}
						if (!ccb)
						{
							throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_UNICODE", new string[0]));
						}
						utfbytes[num3++] = 253;
						utfbytes[num3++] = byte.MaxValue;
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
						b &= 1;
						byte b2 = UTF16ConvUTFE.utfe2utf8m[UTF16ConvUTFE.high((int)bytes[num])][UTF16ConvUTFE.low((int)bytes[num++])];
						byte b3 = UTF16ConvUTFE.utfe2utf8m[UTF16ConvUTFE.high((int)bytes[num])][UTF16ConvUTFE.low((int)bytes[num++])];
						byte b4 = UTF16ConvUTFE.utfe2utf8m[UTF16ConvUTFE.high((int)bytes[num])][UTF16ConvUTFE.low((int)bytes[num++])];
						if (UTF16ConvUTFE.is101xxxxx(b2) && UTF16ConvUTFE.is101xxxxx(b3) && UTF16ConvUTFE.is101xxxxx(b4))
						{
							char c = (char)((int)b << 15 | (int)(b2 & 31) << 10 | (int)(b3 & 31) << 5 | (int)(b4 & 31));
							utfbytes[num3++] = (byte)(c & 'ÿ');
							utfbytes[num3++] = (byte)(c >> 8);
							continue;
						}
						if (!ccb)
						{
							throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_UNICODE", new string[0]));
						}
						utfbytes[num3++] = 253;
						utfbytes[num3++] = byte.MaxValue;
						continue;
					}
					break;
				}
				if (!ccb)
				{
					throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_UNICODE", new string[0]));
				}
				if (num3 + 1 < num4)
				{
					utfbytes[num3++] = 253;
					utfbytes[num3++] = byte.MaxValue;
				}
			}
			utfCount = num3 - utfOffset;
			return num - offset;
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x0008418C File Offset: 0x0008238C
		public override int ConvertBytesToUTF16(byte[] bytes, int offset, int count, byte[] utfbytes, int utfOffset, ref int utfCount, bool ccb)
		{
			return UTF16ConvUTFE.ConvertBytesToUTF16Impl(bytes, offset, count, utfbytes, utfOffset, ref utfCount, ccb);
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x000841A0 File Offset: 0x000823A0
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
				if (num4 <= 31)
				{
					num3++;
				}
				else if (num4 <= 127)
				{
					num3++;
				}
				else if (num4 <= 1023)
				{
					num3 += 2;
				}
				else if (num4 <= 16383)
				{
					num3 += 3;
				}
				else
				{
					num3 += 4;
				}
			}
			return num3;
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x00084214 File Offset: 0x00082414
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
				if (num4 <= 31)
				{
					num3++;
				}
				else if (num4 <= 127)
				{
					num3++;
				}
				else if (num4 <= 1023)
				{
					num3 += 2;
				}
				else if (num4 <= 16383)
				{
					num3 += 3;
				}
				else
				{
					num3 += 4;
				}
			}
			return num3;
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x00084290 File Offset: 0x00082490
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
				if (num4 <= 31)
				{
					num3++;
				}
				else if (num4 <= 127)
				{
					num3++;
				}
				else if (num4 <= 1023)
				{
					num3 += 2;
				}
				else if (num4 <= 16383)
				{
					num3 += 3;
				}
				else
				{
					num3 += 4;
				}
			}
			return num3;
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x0008430C File Offset: 0x0008250C
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
				if (num6 <= 31)
				{
					int b = num6 | 128;
					bytes[num4++] = UTF16ConvUTFE.utf8m2utfe[UTF16ConvUTFE.high(b)][UTF16ConvUTFE.low(b)];
				}
				else if (num6 <= 127)
				{
					bytes[num4++] = UTF16ConvUTFE.utf8m2utfe[UTF16ConvUTFE.high(num6)][UTF16ConvUTFE.low(num6)];
				}
				else if (num6 <= 1023)
				{
					if (num4 + 1 >= num3)
					{
						break;
					}
					int b = (num6 & 992) >> 5 | 192;
					bytes[num4++] = UTF16ConvUTFE.utf8m2utfe[UTF16ConvUTFE.high(b)][UTF16ConvUTFE.low(b)];
					b = ((num6 & 31) | 160);
					bytes[num4++] = UTF16ConvUTFE.utf8m2utfe[UTF16ConvUTFE.high(b)][UTF16ConvUTFE.low(b)];
				}
				else if (num6 <= 16383)
				{
					if (num4 + 2 >= num3)
					{
						break;
					}
					int b = (num6 & 15360) >> 10 | 224;
					bytes[num4++] = UTF16ConvUTFE.utf8m2utfe[UTF16ConvUTFE.high(b)][UTF16ConvUTFE.low(b)];
					b = ((num6 & 992) >> 5 | 160);
					bytes[num4++] = UTF16ConvUTFE.utf8m2utfe[UTF16ConvUTFE.high(b)][UTF16ConvUTFE.low(b)];
					b = ((num6 & 31) | 160);
					bytes[num4++] = UTF16ConvUTFE.utf8m2utfe[UTF16ConvUTFE.high(b)][UTF16ConvUTFE.low(b)];
				}
				else
				{
					if (num4 + 3 >= num3)
					{
						break;
					}
					int b = (num6 & 32768) >> 15 | 240;
					bytes[num4++] = UTF16ConvUTFE.utf8m2utfe[UTF16ConvUTFE.high(b)][UTF16ConvUTFE.low(b)];
					b = ((num6 & 31744) >> 10 | 160);
					bytes[num4++] = UTF16ConvUTFE.utf8m2utfe[UTF16ConvUTFE.high(b)][UTF16ConvUTFE.low(b)];
					b = ((num6 & 992) >> 5 | 160);
					bytes[num4++] = UTF16ConvUTFE.utf8m2utfe[UTF16ConvUTFE.high(b)][UTF16ConvUTFE.low(b)];
					b = ((num6 & 31) | 160);
					bytes[num4++] = UTF16ConvUTFE.utf8m2utfe[UTF16ConvUTFE.high(b)][UTF16ConvUTFE.low(b)];
				}
				num5++;
			}
			bytes_count = num4 - bytes_begin;
			return num5 - num;
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x000845C0 File Offset: 0x000827C0
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
				if (num6 <= 31)
				{
					int b = num6 | 128;
					bytes[num4++] = UTF16ConvUTFE.utf8m2utfe[UTF16ConvUTFE.high(b)][UTF16ConvUTFE.low(b)];
				}
				else if (num6 <= 127)
				{
					bytes[num4++] = UTF16ConvUTFE.utf8m2utfe[UTF16ConvUTFE.high(num6)][UTF16ConvUTFE.low(num6)];
				}
				else if (num6 <= 1023)
				{
					if (num4 + 1 >= num3)
					{
						break;
					}
					int b = (num6 & 992) >> 5 | 192;
					bytes[num4++] = UTF16ConvUTFE.utf8m2utfe[UTF16ConvUTFE.high(b)][UTF16ConvUTFE.low(b)];
					b = ((num6 & 31) | 160);
					bytes[num4++] = UTF16ConvUTFE.utf8m2utfe[UTF16ConvUTFE.high(b)][UTF16ConvUTFE.low(b)];
				}
				else if (num6 <= 16383)
				{
					if (num4 + 2 >= num3)
					{
						break;
					}
					int b = (num6 & 15360) >> 10 | 224;
					bytes[num4++] = UTF16ConvUTFE.utf8m2utfe[UTF16ConvUTFE.high(b)][UTF16ConvUTFE.low(b)];
					b = ((num6 & 992) >> 5 | 160);
					bytes[num4++] = UTF16ConvUTFE.utf8m2utfe[UTF16ConvUTFE.high(b)][UTF16ConvUTFE.low(b)];
					b = ((num6 & 31) | 160);
					bytes[num4++] = UTF16ConvUTFE.utf8m2utfe[UTF16ConvUTFE.high(b)][UTF16ConvUTFE.low(b)];
				}
				else
				{
					if (num4 + 3 >= num3)
					{
						break;
					}
					int b = (num6 & 32768) >> 15 | 240;
					bytes[num4++] = UTF16ConvUTFE.utf8m2utfe[UTF16ConvUTFE.high(b)][UTF16ConvUTFE.low(b)];
					b = ((num6 & 31744) >> 10 | 160);
					bytes[num4++] = UTF16ConvUTFE.utf8m2utfe[UTF16ConvUTFE.high(b)][UTF16ConvUTFE.low(b)];
					b = ((num6 & 992) >> 5 | 160);
					bytes[num4++] = UTF16ConvUTFE.utf8m2utfe[UTF16ConvUTFE.high(b)][UTF16ConvUTFE.low(b)];
					b = ((num6 & 31) | 160);
					bytes[num4++] = UTF16ConvUTFE.utf8m2utfe[UTF16ConvUTFE.high(b)][UTF16ConvUTFE.low(b)];
				}
				num5++;
			}
			bytes_count = num4 - bytes_begin;
			return num5 - num;
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x0008487C File Offset: 0x00082A7C
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
				if (num6 <= 31)
				{
					int b = num6 | 128;
					bytes[num4++] = UTF16ConvUTFE.utf8m2utfe[UTF16ConvUTFE.high(b)][UTF16ConvUTFE.low(b)];
				}
				else if (num6 <= 127)
				{
					bytes[num4++] = UTF16ConvUTFE.utf8m2utfe[UTF16ConvUTFE.high(num6)][UTF16ConvUTFE.low(num6)];
				}
				else if (num6 <= 1023)
				{
					if (num4 + 1 >= num3)
					{
						break;
					}
					int b = (num6 & 992) >> 5 | 192;
					bytes[num4++] = UTF16ConvUTFE.utf8m2utfe[UTF16ConvUTFE.high(b)][UTF16ConvUTFE.low(b)];
					b = ((num6 & 31) | 160);
					bytes[num4++] = UTF16ConvUTFE.utf8m2utfe[UTF16ConvUTFE.high(b)][UTF16ConvUTFE.low(b)];
				}
				else if (num6 <= 16383)
				{
					if (num4 + 2 >= num3)
					{
						break;
					}
					int b = (num6 & 15360) >> 10 | 224;
					bytes[num4++] = UTF16ConvUTFE.utf8m2utfe[UTF16ConvUTFE.high(b)][UTF16ConvUTFE.low(b)];
					b = ((num6 & 992) >> 5 | 160);
					bytes[num4++] = UTF16ConvUTFE.utf8m2utfe[UTF16ConvUTFE.high(b)][UTF16ConvUTFE.low(b)];
					b = ((num6 & 31) | 160);
					bytes[num4++] = UTF16ConvUTFE.utf8m2utfe[UTF16ConvUTFE.high(b)][UTF16ConvUTFE.low(b)];
				}
				else
				{
					if (num4 + 3 >= num3)
					{
						break;
					}
					int b = (num6 & 32768) >> 15 | 240;
					bytes[num4++] = UTF16ConvUTFE.utf8m2utfe[UTF16ConvUTFE.high(b)][UTF16ConvUTFE.low(b)];
					b = ((num6 & 31744) >> 10 | 160);
					bytes[num4++] = UTF16ConvUTFE.utf8m2utfe[UTF16ConvUTFE.high(b)][UTF16ConvUTFE.low(b)];
					b = ((num6 & 992) >> 5 | 160);
					bytes[num4++] = UTF16ConvUTFE.utf8m2utfe[UTF16ConvUTFE.high(b)][UTF16ConvUTFE.low(b)];
					b = ((num6 & 31) | 160);
					bytes[num4++] = UTF16ConvUTFE.utf8m2utfe[UTF16ConvUTFE.high(b)][UTF16ConvUTFE.low(b)];
				}
				num5 += 2;
			}
			byteCount = num4 - byteOffset;
			return num5 - num;
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x00084B38 File Offset: 0x00082D38
		private static int high(int b)
		{
			return b >> 4 & 15;
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x00084B40 File Offset: 0x00082D40
		private static int low(int b)
		{
			return b & 15;
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x00084B48 File Offset: 0x00082D48
		private static bool is101xxxxx(byte c)
		{
			return (c & 224) == 160;
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x00084B58 File Offset: 0x00082D58
		public override int ConvertBytesToChars(IList<ArraySegment<byte>> bytes, int bytesOffset, int bytesCount, char[] chars, int charOffset, ref int charCount, bool bUseReplacementChar)
		{
			return UTF16ConvUtility.ConvertArraySegListToCharsImpl<char>(bytes, bytesOffset, bytesCount, chars, charOffset, ref charCount, bUseReplacementChar, UTF16ConvUTFE.ConvertBytesToCharsInstance);
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x00084B70 File Offset: 0x00082D70
		public override int GetCharsLength(byte[] bytes, int byteOffset, int byteCount)
		{
			int num = 0;
			return UTF16ConvUTFE.GetCharsLengthImpl(bytes, byteOffset, byteCount, ref num);
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x00084B8C File Offset: 0x00082D8C
		public override int GetCharsLength(ArraySegment<byte> bytes, int bytesOffset, int bytesCount)
		{
			int num = 0;
			return UTF16ConvUTFE.GetCharsLengthImpl(bytes.Array, bytes.Offset + bytesOffset, bytesCount, ref num);
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x00084BB4 File Offset: 0x00082DB4
		public override int GetCharsLength(IList<ArraySegment<byte>> bytes, int bytesOffset, int bytesCount)
		{
			return UTF16ConvUtility.GetCharsLengthListSegs(bytes, bytesOffset, bytesCount, UTF16ConvUTFE.GetCharsLengthInstance);
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x00084BC4 File Offset: 0x00082DC4
		public override int GetBytesOffset(IList<ArraySegment<byte>> bytes, int charCount)
		{
			return UTF16ConvUtility.GetBytesOffsetListSegs(bytes, charCount, UTF16ConvUTFE.GetBytesOffsetInstance);
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000BD2 RID: 3026 RVA: 0x00084BD4 File Offset: 0x00082DD4
		public override int MinBytesPerChar
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000BD3 RID: 3027 RVA: 0x00084BD8 File Offset: 0x00082DD8
		public override int MaxBytesPerChar
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x04000D1B RID: 3355
		private static readonly byte[][] utf8m2utfe = new byte[][]
		{
			new byte[]
			{
				0,
				1,
				2,
				3,
				55,
				45,
				46,
				47,
				22,
				5,
				21,
				11,
				12,
				13,
				14,
				15
			},
			new byte[]
			{
				16,
				17,
				18,
				19,
				60,
				61,
				50,
				38,
				24,
				25,
				63,
				39,
				28,
				29,
				30,
				31
			},
			new byte[]
			{
				64,
				90,
				127,
				123,
				91,
				108,
				80,
				125,
				77,
				93,
				92,
				78,
				107,
				96,
				75,
				97
			},
			new byte[]
			{
				240,
				241,
				242,
				243,
				244,
				245,
				246,
				247,
				248,
				249,
				122,
				94,
				76,
				126,
				110,
				111
			},
			new byte[]
			{
				124,
				193,
				194,
				195,
				196,
				197,
				198,
				199,
				200,
				201,
				209,
				210,
				211,
				212,
				213,
				214
			},
			new byte[]
			{
				215,
				216,
				217,
				226,
				227,
				228,
				229,
				230,
				231,
				232,
				233,
				173,
				224,
				189,
				95,
				109
			},
			new byte[]
			{
				121,
				129,
				130,
				131,
				132,
				133,
				134,
				135,
				136,
				137,
				145,
				146,
				147,
				148,
				149,
				150
			},
			new byte[]
			{
				151,
				152,
				153,
				162,
				163,
				164,
				165,
				166,
				167,
				168,
				169,
				192,
				79,
				208,
				161,
				7
			},
			new byte[]
			{
				32,
				33,
				34,
				35,
				36,
				37,
				6,
				23,
				40,
				41,
				42,
				43,
				44,
				9,
				10,
				27
			},
			new byte[]
			{
				48,
				49,
				26,
				51,
				52,
				53,
				54,
				8,
				56,
				57,
				58,
				59,
				4,
				20,
				62,
				byte.MaxValue
			},
			new byte[]
			{
				65,
				66,
				67,
				68,
				69,
				70,
				71,
				72,
				73,
				74,
				81,
				82,
				83,
				84,
				85,
				86
			},
			new byte[]
			{
				87,
				88,
				89,
				98,
				99,
				100,
				101,
				102,
				103,
				104,
				105,
				106,
				112,
				113,
				114,
				115
			},
			new byte[]
			{
				116,
				117,
				118,
				119,
				120,
				128,
				138,
				139,
				140,
				141,
				142,
				143,
				144,
				154,
				155,
				156
			},
			new byte[]
			{
				157,
				158,
				159,
				160,
				170,
				171,
				172,
				174,
				175,
				176,
				177,
				178,
				179,
				180,
				181,
				182
			},
			new byte[]
			{
				183,
				184,
				185,
				186,
				187,
				188,
				190,
				191,
				202,
				203,
				204,
				205,
				206,
				207,
				218,
				219
			},
			new byte[]
			{
				220,
				221,
				222,
				223,
				225,
				234,
				235,
				236,
				237,
				238,
				239,
				250,
				251,
				252,
				253,
				254
			}
		};

		// Token: 0x04000D1C RID: 3356
		private static readonly byte[][] utfe2utf8m = new byte[][]
		{
			new byte[]
			{
				0,
				1,
				2,
				3,
				156,
				9,
				134,
				127,
				151,
				141,
				142,
				11,
				12,
				13,
				14,
				15
			},
			new byte[]
			{
				16,
				17,
				18,
				19,
				157,
				10,
				8,
				135,
				24,
				25,
				146,
				143,
				28,
				29,
				30,
				31
			},
			new byte[]
			{
				128,
				129,
				130,
				131,
				132,
				133,
				23,
				27,
				136,
				137,
				138,
				139,
				140,
				5,
				6,
				7
			},
			new byte[]
			{
				144,
				145,
				22,
				147,
				148,
				149,
				150,
				4,
				152,
				153,
				154,
				155,
				20,
				21,
				158,
				26
			},
			new byte[]
			{
				32,
				160,
				161,
				162,
				163,
				164,
				165,
				166,
				167,
				168,
				169,
				46,
				60,
				40,
				43,
				124
			},
			new byte[]
			{
				38,
				170,
				171,
				172,
				173,
				174,
				175,
				176,
				177,
				178,
				33,
				36,
				42,
				41,
				59,
				94
			},
			new byte[]
			{
				45,
				47,
				179,
				180,
				181,
				182,
				183,
				184,
				185,
				186,
				187,
				44,
				37,
				95,
				62,
				63
			},
			new byte[]
			{
				188,
				189,
				190,
				191,
				192,
				193,
				194,
				195,
				196,
				96,
				58,
				35,
				64,
				39,
				61,
				34
			},
			new byte[]
			{
				197,
				97,
				98,
				99,
				100,
				101,
				102,
				103,
				104,
				105,
				198,
				199,
				200,
				201,
				202,
				203
			},
			new byte[]
			{
				204,
				106,
				107,
				108,
				109,
				110,
				111,
				112,
				113,
				114,
				205,
				206,
				207,
				208,
				209,
				210
			},
			new byte[]
			{
				211,
				126,
				115,
				116,
				117,
				118,
				119,
				120,
				121,
				122,
				212,
				213,
				214,
				88,
				215,
				216
			},
			new byte[]
			{
				217,
				218,
				219,
				220,
				221,
				222,
				223,
				224,
				225,
				226,
				227,
				228,
				229,
				93,
				230,
				231
			},
			new byte[]
			{
				123,
				65,
				66,
				67,
				68,
				69,
				70,
				71,
				72,
				73,
				232,
				233,
				234,
				235,
				236,
				237
			},
			new byte[]
			{
				13,
				74,
				75,
				76,
				77,
				78,
				79,
				80,
				81,
				82,
				238,
				239,
				240,
				241,
				242,
				243
			},
			new byte[]
			{
				92,
				244,
				83,
				84,
				85,
				86,
				87,
				88,
				89,
				90,
				245,
				246,
				247,
				248,
				249,
				250
			},
			new byte[]
			{
				48,
				49,
				50,
				51,
				52,
				53,
				54,
				55,
				56,
				57,
				251,
				252,
				253,
				254,
				byte.MaxValue,
				159
			}
		};

		// Token: 0x04000D1D RID: 3357
		private static UTF16ConvUtility.ConvertToCharsDelegate<char> ConvertBytesToCharsInstance = new UTF16ConvUtility.ConvertToCharsDelegate<char>(UTF16ConvUTFE.ConvertBytesToCharsImpl);

		// Token: 0x04000D1E RID: 3358
		private static UTF16ConvUtility.ConvertToCharsDelegate<byte> ConvertBytesToUTF16Instance = new UTF16ConvUtility.ConvertToCharsDelegate<byte>(UTF16ConvUTFE.ConvertBytesToUTF16Impl);

		// Token: 0x04000D1F RID: 3359
		private static UTF16ConvUtility.GetCharsLengthDelegate GetCharsLengthInstance = new UTF16ConvUtility.GetCharsLengthDelegate(UTF16ConvUTFE.GetCharsLengthImpl);

		// Token: 0x04000D20 RID: 3360
		private static UTF16ConvUtility.GetBytesOffsetDelegate GetBytesOffsetInstance = new UTF16ConvUtility.GetBytesOffsetDelegate(UTF16ConvUTFE.GetBytesOffsetImpl);
	}
}
