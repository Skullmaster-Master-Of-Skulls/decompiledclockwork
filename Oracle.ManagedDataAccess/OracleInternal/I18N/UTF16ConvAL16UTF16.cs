using System;
using System.Collections.Generic;
using System.Text;

namespace OracleInternal.I18N
{
	// Token: 0x02000109 RID: 265
	internal class UTF16ConvAL16UTF16 : Conv
	{
		// Token: 0x06000B6A RID: 2922 RVA: 0x00080340 File Offset: 0x0007E540
		internal UTF16ConvAL16UTF16(int oracleId) : base(oracleId)
		{
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x0008034C File Offset: 0x0007E54C
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
			while (num5 < num4 && num < num2)
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
					char c = (char)(((int)bytes[num] << 8) + (int)bytes[num + 1]);
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
							char c2 = (char)(((int)bytes[num] << 8) + (int)bytes[num + 1]);
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

		// Token: 0x06000B6C RID: 2924 RVA: 0x00080480 File Offset: 0x0007E680
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
				bytes[num2++] = (byte)(chars[num] >> 8);
				bytes[num2++] = (byte)(chars[num] & 'ÿ');
				num++;
			}
			byteCount = num2 - byteOffset;
			return num - charsOffset;
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x000804FC File Offset: 0x0007E6FC
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
				bytes[num2++] = (byte)(chars[num] >> 8);
				bytes[num2++] = (byte)(chars[num] & 'ÿ');
				num++;
			}
			byteCount = num2 - byteOffset;
			return num - charsOffset;
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x00080584 File Offset: 0x0007E784
		public override int ConvertBytesToChars(IList<ArraySegment<byte>> bytes, int bytesOffset, int bytesCount, char[] chars, int charOffset, ref int charCount, bool bUseReplacementChar)
		{
			int num = charOffset;
			int num2 = charCount;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			byte[] array = null;
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
					char c = (char)(b << 8);
					bool flag2 = UTF16ConvUtility.IsHiSurrogate(c);
					int buffer1Bytes = num9 - num10;
					int byteCount;
					if (!flag2)
					{
						array[0] = b;
						array[1] = array2[bytes[num7 + 1].Offset];
						num5 = 1;
						byteCount = 2;
					}
					else
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

		// Token: 0x06000B6F RID: 2927 RVA: 0x0008079C File Offset: 0x0007E99C
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
			while (num + 1 < num2 && num3 + 1 < num4)
			{
				utf16Bytes[num3 + 1] = bytes[num];
				utf16Bytes[num3] = bytes[num + 1];
				num3 += 2;
				num += 2;
			}
			utf16BytesCount = num3 - utf16BytesOffset;
			return num - byteOffset;
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x0008080C File Offset: 0x0007EA0C
		public override int ConvertUTF16ToBytes(byte[] utf16Bytes, int utf16BytesOffset, int utf16BytesCount, byte[] bytes, int byteOffset, ref int byteCount, bool bUseReplacementChar = true)
		{
			return this.ConvertBytesToUTF16(utf16Bytes, utf16BytesOffset, utf16BytesCount, bytes, byteOffset, ref byteCount, bUseReplacementChar);
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x00080820 File Offset: 0x0007EA20
		public override int GetCharsLength(byte[] bytes, int byteOffset, int byteCount)
		{
			if (byteOffset + byteCount > bytes.Length)
			{
				return (bytes.Length - byteOffset) / 2;
			}
			return byteCount / 2;
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x00080838 File Offset: 0x0007EA38
		public override int GetCharsLength(ArraySegment<byte> bytes, int bytesOffset, int bytesCount)
		{
			if (bytesCount > bytes.Count)
			{
				return (bytes.Count - bytesOffset) / 2;
			}
			return bytesCount / 2;
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x00080854 File Offset: 0x0007EA54
		public override int GetCharsLength(IList<ArraySegment<byte>> bytes, int bytesOffset, int bytesCount)
		{
			return bytesCount / 2;
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x0008085C File Offset: 0x0007EA5C
		public override int GetBytesLength(char[] chars, int charOffset, int charCount)
		{
			if (charOffset + charCount > chars.Length)
			{
				return (chars.Length - charOffset) * 2;
			}
			return 2 * charCount;
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x00080874 File Offset: 0x0007EA74
		public override int GetBytesLength(string str, int strOffset, int strCount)
		{
			if (strOffset + strCount > str.Length)
			{
				return (str.Length - strOffset) * 2;
			}
			return 2 * strCount;
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x00080890 File Offset: 0x0007EA90
		public override int GetBytesLength(byte[] utf16Bytes, int utf16BytesOffset, int utf16BytesCount)
		{
			if (utf16BytesOffset + utf16BytesCount > utf16Bytes.Length)
			{
				return utf16Bytes.Length - utf16BytesOffset;
			}
			return utf16BytesCount;
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x000808A4 File Offset: 0x0007EAA4
		public override int GetBytesOffset(byte[] bytes, int byteOffset, int byteCount, int charCount)
		{
			return charCount * 2;
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x000808AC File Offset: 0x0007EAAC
		public override int GetBytesOffset(IList<ArraySegment<byte>> bytes, int charCount)
		{
			return charCount * 2;
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000B79 RID: 2937 RVA: 0x000808B4 File Offset: 0x0007EAB4
		public override int MinBytesPerChar
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000B7A RID: 2938 RVA: 0x000808B8 File Offset: 0x0007EAB8
		public override int MaxBytesPerChar
		{
			get
			{
				return 2;
			}
		}
	}
}
