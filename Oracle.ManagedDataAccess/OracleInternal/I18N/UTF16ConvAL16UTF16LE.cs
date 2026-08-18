using System;
using System.Collections.Generic;
using System.Text;

namespace OracleInternal.I18N
{
	// Token: 0x0200010A RID: 266
	internal class UTF16ConvAL16UTF16LE : Conv
	{
		// Token: 0x06000B7B RID: 2939 RVA: 0x000808BC File Offset: 0x0007EABC
		internal UTF16ConvAL16UTF16LE(int oracleId) : base(oracleId)
		{
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x000808C8 File Offset: 0x0007EAC8
		public override int ConvertBytesToChars(byte[] bytes, int offset, int count, char[] chars, int charOffset, ref int charCount, bool ccb)
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
			int num5 = num3;
			while (num5 < num4 && num + 1 < num2)
			{
				if (num + 1 >= num2)
				{
					if (!ccb)
					{
						throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_UNICODE", new string[0]));
					}
					break;
				}
				else
				{
					char c = (char)(((int)bytes[num + 1] << 8) + (int)bytes[num]);
					if (UTF16ConvUtility.IsHiSurrogate(c))
					{
						if (num + 3 >= num2)
						{
							if (!ccb)
							{
								throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_UNICODE", new string[0]));
							}
							break;
						}
						else
						{
							if (num5 + 1 >= num4)
							{
								break;
							}
							num += 2;
							char c2 = (char)(((int)bytes[num + 1] << 8) + (int)bytes[num]);
							if (UTF16ConvUtility.IsLoSurrogate(c2))
							{
								chars[num5++] = c;
							}
							else
							{
								if (!ccb)
								{
									throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_UNICODE", new string[0]));
								}
								chars[num5++] = '�';
							}
							chars[num5++] = c2;
						}
					}
					else
					{
						chars[num5++] = c;
					}
					num += 2;
				}
			}
			charCount = num5 - charOffset;
			return num - offset;
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x00080A00 File Offset: 0x0007EC00
		public override int ConvertCharsToBytes(char[] chars, int charsOffset, int nchars, byte[] bytes, int byteOffset, ref int byteCount, bool bUseReplacementChar)
		{
			int num = charsOffset;
			if (num < 0)
			{
				num = 0;
			}
			int num2 = byteOffset;
			if (num2 < 0)
			{
				num2 = 0;
			}
			int num3 = charsOffset + nchars;
			if (num3 > chars.Length)
			{
				num3 = chars.Length;
			}
			int num4 = byteOffset + byteCount;
			if (num4 > bytes.Length)
			{
				num4 = bytes.Length;
			}
			while (num < num3 && num2 + 1 < num4)
			{
				bytes[num2 + 1] = (byte)(chars[num] >> 8);
				bytes[num2] = (byte)(chars[num] & 'ÿ');
				num2 += 2;
				num++;
			}
			byteCount = num2 - byteOffset;
			return num - charsOffset;
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x00080A78 File Offset: 0x0007EC78
		public override int ConvertStringToBytes(string chars, int charsOffset, int nchars, byte[] bytes, int byteOffset, ref int byteCount, bool bUseReplacementChar)
		{
			int num = charsOffset;
			if (num < 0)
			{
				num = 0;
			}
			int num2 = byteOffset;
			if (num2 < 0)
			{
				num2 = 0;
			}
			int num3 = num + nchars;
			if (num3 > chars.Length)
			{
				num3 = chars.Length;
			}
			int num4 = num2 + byteCount;
			if (num4 > bytes.Length)
			{
				num4 = bytes.Length;
			}
			while (num < num3 && num2 + 1 < num4)
			{
				bytes[num2 + 1] = (byte)(chars[num] >> 8);
				bytes[num2] = (byte)(chars[num] & 'ÿ');
				num2 += 2;
				num++;
			}
			byteCount = num2 - byteOffset;
			return num - charsOffset;
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x00080AFC File Offset: 0x0007ECFC
		public override int ConvertBytesToChars(IList<ArraySegment<byte>> bytes, int bytesOffset, int bytesCount, char[] chars, int charOffset, ref int charCount, bool bUseReplacementChar)
		{
			int num = charOffset;
			int num2 = charCount;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			byte[] array = null;
			int byteCount = 0;
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
				int num10 = this.ConvertBytesToChars(bytes[num7].Array, num8, num9, chars, num, ref num2, bUseReplacementChar);
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
					char c = (char)(array2[0] << 8);
					bool flag2 = UTF16ConvUtility.IsHiSurrogate(c);
					int buffer1Bytes = num9 - num10;
					if (!flag2)
					{
						array[0] = b;
						array[1] = array2[bytes[num7 + 1].Offset];
						num5 = 1;
						byteCount = 2;
					}
					else if (flag2)
					{
						UTF16ConvUtility.GetRemainingBytes(4, bytes[num7].Array, num11, buffer1Bytes, bytes, ref num7, ref num5, array);
						byteCount = 4;
					}
					num10 = this.ConvertBytesToChars(array, 0, byteCount, chars, num, ref num2, bUseReplacementChar);
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

		// Token: 0x06000B80 RID: 2944 RVA: 0x00080D1C File Offset: 0x0007EF1C
		public override int ConvertBytesToUTF16(byte[] bytes, int byteOffset, int byteCount, byte[] utf16Bytes, int utf16BytesOffset, ref int utf16BytesCount, bool bUseReplacementChar)
		{
			int num = byteOffset;
			if (num < 0)
			{
				num = 0;
			}
			int num2 = num + byteCount;
			if (num2 > bytes.Length)
			{
				num2 = bytes.Length;
			}
			int num3 = utf16BytesOffset;
			if (num3 < 0)
			{
				num3 = 0;
			}
			int num4 = num3 + utf16BytesCount;
			if (num4 > utf16Bytes.Length)
			{
				num4 = utf16Bytes.Length;
			}
			while (num < num2 && num3 < num4)
			{
				utf16Bytes[num3++] = bytes[num++];
			}
			utf16BytesCount = num3 - utf16BytesOffset;
			return num - byteOffset;
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x00080D80 File Offset: 0x0007EF80
		public override int ConvertUTF16ToBytes(byte[] utf16Bytes, int utf16BytesOffset, int utf16BytesCount, byte[] bytes, int byteOffset, ref int byteCount, bool bUseReplacementChar = true)
		{
			return this.ConvertBytesToUTF16(utf16Bytes, utf16BytesOffset, utf16BytesCount, bytes, byteOffset, ref byteCount, bUseReplacementChar);
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x00080D94 File Offset: 0x0007EF94
		public override int GetCharsLength(byte[] bytes, int byteOffset, int byteCount)
		{
			if (byteOffset + byteCount > bytes.Length)
			{
				return (bytes.Length - byteOffset) / 2;
			}
			return byteCount / 2;
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x00080DAC File Offset: 0x0007EFAC
		public override int GetCharsLength(ArraySegment<byte> bytes, int bytesOffset, int bytesCount)
		{
			if (bytesCount > bytes.Count)
			{
				return (bytes.Count - bytesOffset) / 2;
			}
			return bytesCount / 2;
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x00080DC8 File Offset: 0x0007EFC8
		public override int GetCharsLength(IList<ArraySegment<byte>> bytes, int bytesOffset, int bytesCount)
		{
			return bytesCount / 2;
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x00080DD0 File Offset: 0x0007EFD0
		public override int GetBytesLength(char[] chars, int charOffset, int charCount)
		{
			if (charOffset + charCount > chars.Length)
			{
				return (chars.Length - charOffset) * 2;
			}
			return 2 * charCount;
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x00080DE8 File Offset: 0x0007EFE8
		public override int GetBytesLength(string str, int strOffset, int strCount)
		{
			if (strOffset + strCount > str.Length)
			{
				return (str.Length - strOffset) * 2;
			}
			return 2 * strCount;
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x00080E04 File Offset: 0x0007F004
		public override int GetBytesLength(byte[] utf16Bytes, int utf16BytesOffset, int utf16BytesCount)
		{
			if (utf16BytesOffset + utf16BytesCount > utf16Bytes.Length)
			{
				return utf16Bytes.Length - utf16BytesOffset;
			}
			return utf16BytesCount;
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x00080E18 File Offset: 0x0007F018
		public override int GetBytesOffset(byte[] bytes, int byteOffset, int byteCount, int charCount)
		{
			return charCount * 2;
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x00080E20 File Offset: 0x0007F020
		public override int GetBytesOffset(IList<ArraySegment<byte>> bytes, int charCount)
		{
			return charCount * 2;
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000B8A RID: 2954 RVA: 0x00080E28 File Offset: 0x0007F028
		public override int MinBytesPerChar
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000B8B RID: 2955 RVA: 0x00080E2C File Offset: 0x0007F02C
		public override int MaxBytesPerChar
		{
			get
			{
				return 2;
			}
		}
	}
}
