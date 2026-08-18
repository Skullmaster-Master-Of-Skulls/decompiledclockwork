using System;
using System.Collections.Generic;
using System.Text;

namespace OracleInternal.I18N
{
	// Token: 0x02000104 RID: 260
	[Serializable]
	internal class TLBConvJAEUC : TLBConvLC
	{
		// Token: 0x06000B29 RID: 2857 RVA: 0x0007CF2C File Offset: 0x0007B12C
		public TLBConvJAEUC()
		{
			this.m_groupId = 2;
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x0007CF3C File Offset: 0x0007B13C
		private int ToUnicodeJAEUC(int srcChar, bool ccb)
		{
			int num = 0;
			if ((srcChar >> 16 & 65535) == 143)
			{
				num = 256;
			}
			int num2 = srcChar >> 8 & 255 + num;
			int num3 = srcChar & 255;
			int result;
			if (this.m_ucsCharLevel1[num2] != '￿' && this.m_ucsCharLevel2[(int)this.m_ucsCharLevel1[num2] + num3] != 65535)
			{
				result = this.m_ucsCharLevel2[(int)this.m_ucsCharLevel1[num2] + num3];
			}
			else
			{
				if (!ccb)
				{
					throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_UNICODE", new string[0]));
				}
				result = this.m_ucsCharReplacement;
			}
			return result;
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x0007CFD4 File Offset: 0x0007B1D4
		private int GetCharsLengthJAEUCImpl(byte[] bytes, int offset, int count, ref int bytesCounted)
		{
			int i = offset;
			int num = offset + count;
			if (num > bytes.Length)
			{
				num = bytes.Length;
			}
			int num2 = 0;
			while (i < num)
			{
				int num3 = (int)(bytes[i] & byte.MaxValue);
				int num4 = 0;
				int num5;
				if (num3 == 143)
				{
					if (i + 2 >= num)
					{
						bytesCounted = i - offset;
						break;
					}
					num3 = (((int)bytes[i + 1] << 8 & 65280) | (int)(bytes[i + 2] & byte.MaxValue));
					num5 = 3;
					num4 = 256;
				}
				else if (num3 > 127)
				{
					if (i + 1 >= num)
					{
						bytesCounted = i - offset;
						break;
					}
					num3 = (((int)bytes[i] << 8 & 65280) | (int)(bytes[i + 1] & byte.MaxValue));
					num5 = 2;
				}
				else
				{
					num5 = 1;
				}
				int num6 = (num3 >> 8 & 255) + num4;
				int num7 = num3 & 255;
				int num8;
				if (this.m_ucsCharLevel1[num6] != '￿' && this.m_ucsCharLevel2[(int)this.m_ucsCharLevel1[num6] + num7] != 65535)
				{
					num8 = this.m_ucsCharLevel2[(int)this.m_ucsCharLevel1[num6] + num7];
				}
				else
				{
					num8 = this.m_ucsCharReplacement;
				}
				if (((long)num8 & (long)((ulong)-1)) > 65535L)
				{
					num2 += 2;
				}
				else
				{
					num2++;
				}
				i += num5;
			}
			bytesCounted = i - offset;
			return num2;
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x0007D118 File Offset: 0x0007B318
		public override int GetCharsLength(byte[] bytes, int byteOffset, int byteCount)
		{
			int num = 0;
			return this.GetCharsLengthJAEUCImpl(bytes, byteOffset, byteCount, ref num);
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x0007D134 File Offset: 0x0007B334
		private int GetBytesOffsetJAEUCImpl(byte[] bytes, int offset, int count, ref int charCount)
		{
			int num = offset;
			int num2 = offset + count;
			if (num2 > bytes.Length)
			{
				num2 = bytes.Length;
			}
			int num3 = 0;
			while (num < num2 && num3 < charCount)
			{
				int num4 = (int)(bytes[num] & byte.MaxValue);
				int num5 = 0;
				int num6;
				if (num4 == 143)
				{
					if (num + 2 >= num2)
					{
						break;
					}
					num4 = (((int)bytes[num + 1] << 8 & 65280) | (int)(bytes[num + 2] & byte.MaxValue));
					num6 = 3;
					num5 = 256;
				}
				else if (num4 > 127)
				{
					if (num + 1 >= num2)
					{
						break;
					}
					num4 = (((int)bytes[num] << 8 & 65280) | (int)(bytes[num + 1] & byte.MaxValue));
					num6 = 2;
				}
				else
				{
					num6 = 1;
				}
				int num7 = (num4 >> 8 & 255) + num5;
				int num8 = num4 & 255;
				int num9;
				if (this.m_ucsCharLevel1[num7] != '￿' && this.m_ucsCharLevel2[(int)this.m_ucsCharLevel1[num7] + num8] != 65535)
				{
					num9 = this.m_ucsCharLevel2[(int)this.m_ucsCharLevel1[num7] + num8];
				}
				else
				{
					num9 = this.m_ucsCharReplacement;
				}
				if (((long)num9 & (long)((ulong)-1)) > 65535L)
				{
					num3 += 2;
				}
				else
				{
					num3++;
				}
				num += num6;
			}
			charCount = num3;
			return num - offset;
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x0007D26C File Offset: 0x0007B46C
		public override int GetBytesOffset(byte[] bytes, int byteOffset, int byteCount, int charCount)
		{
			int num = charCount;
			return this.GetBytesOffsetJAEUCImpl(bytes, byteOffset, byteCount, ref num);
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x0007D288 File Offset: 0x0007B488
		public override int GetCharsLength(ArraySegment<byte> bytes, int bytesOffset, int bytesCount)
		{
			int num = 0;
			return this.GetCharsLengthJAEUCImpl(bytes.Array, bytes.Offset + bytesOffset, bytesCount, ref num);
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x0007D2B0 File Offset: 0x0007B4B0
		public override int GetCharsLength(IList<ArraySegment<byte>> bytes, int bytesOffset, int bytesCount)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			byte[] array = null;
			int count = 0;
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
				int charsLengthJAEUCImpl = this.GetCharsLengthJAEUCImpl(bytes[num6].Array, num7, num8, ref num2);
				num += charsLengthJAEUCImpl;
				num3 += num2;
				if (num2 < num8 && !flag && num6 < bytes.Count - 1)
				{
					if (array == null)
					{
						array = new byte[4];
					}
					byte[] array2 = bytes[num6 + 1].Array;
					int num9 = num7 + num2;
					int num10 = (int)(bytes[num6].Array[num9] & byte.MaxValue);
					int buffer1Bytes = num8 - num2;
					if (num10 == 143)
					{
						UTF16ConvUtility.GetRemainingBytes(3, bytes[num6].Array, num9, buffer1Bytes, bytes, ref num6, ref num4, array);
						count = 3;
					}
					else if (num10 > 127)
					{
						UTF16ConvUtility.GetRemainingBytes(2, bytes[num6].Array, num9, buffer1Bytes, bytes, ref num6, ref num4, array);
						count = 2;
					}
					charsLengthJAEUCImpl = this.GetCharsLengthJAEUCImpl(array, 0, count, ref num2);
					num += charsLengthJAEUCImpl;
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

		// Token: 0x06000B31 RID: 2865 RVA: 0x0007D488 File Offset: 0x0007B688
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
			while (num < num2 && num3 < num4)
			{
				int num5 = (int)(bytes[num] & byte.MaxValue);
				int num6 = 0;
				int num7;
				if (num5 == 143)
				{
					if (num + 2 >= num2)
					{
						if (!ccb)
						{
							throw new DecoderFallbackException("Fail to convert bytes to Unicode");
						}
						break;
					}
					else
					{
						num5 = (((int)bytes[num + 1] << 8 & 65280) | (int)(bytes[num + 2] & byte.MaxValue));
						num7 = 3;
						num6 = 256;
					}
				}
				else if (num5 > 127)
				{
					if (num + 1 >= num2)
					{
						if (!ccb)
						{
							throw new DecoderFallbackException("Fail to convert bytes to Unicode");
						}
						break;
					}
					else
					{
						num5 = (((int)bytes[num] << 8 & 65280) | (int)(bytes[num + 1] & byte.MaxValue));
						num7 = 2;
					}
				}
				else
				{
					num7 = 1;
				}
				int num8 = (num5 >> 8 & 255) + num6;
				int num9 = num5 & 255;
				int num10;
				if (this.m_ucsCharLevel1[num8] != '￿' && this.m_ucsCharLevel2[(int)this.m_ucsCharLevel1[num8] + num9] != 65535)
				{
					num10 = this.m_ucsCharLevel2[(int)this.m_ucsCharLevel1[num8] + num9];
				}
				else
				{
					num10 = this.m_ucsCharReplacement;
				}
				if (((long)num10 & (long)((ulong)-1)) > 65535L)
				{
					if (num3 >= num4)
					{
						break;
					}
					chars[num3++] = (char)(num10 >> 16);
					chars[num3++] = (char)(num10 & 65535);
				}
				else
				{
					chars[num3++] = (char)num10;
				}
				num += num7;
			}
			charCount = num3 - charOffset;
			return num - offset;
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x0007D630 File Offset: 0x0007B830
		public override int ConvertBytesToUTF16(byte[] bytes, int offset, int count, byte[] utfbytes, int utfOffset, ref int utfCount, bool ccb)
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
			while (num < num2 && num3 + 1 < num4)
			{
				int num5 = (int)(bytes[num] & byte.MaxValue);
				int num6 = 0;
				int num7;
				if (num5 == 143)
				{
					if (num + 2 >= num2)
					{
						if (!ccb)
						{
							throw new DecoderFallbackException("Fail to convert bytes to Unicode");
						}
						break;
					}
					else
					{
						num5 = (((int)bytes[num + 1] << 8 & 65280) | (int)(bytes[num + 2] & byte.MaxValue));
						num7 = 3;
						num6 = 256;
					}
				}
				else if (num5 > 127)
				{
					if (num + 1 >= num2)
					{
						if (!ccb)
						{
							throw new DecoderFallbackException("Fail to convert bytes to Unicode");
						}
						break;
					}
					else
					{
						num5 = (((int)bytes[num] << 8 & 65280) | (int)(bytes[num + 1] & byte.MaxValue));
						num7 = 2;
					}
				}
				else
				{
					num7 = 1;
				}
				int num8 = (num5 >> 8 & 255) + num6;
				int num9 = num5 & 255;
				int num10;
				if (this.m_ucsCharLevel1[num8] != '￿' && this.m_ucsCharLevel2[(int)this.m_ucsCharLevel1[num8] + num9] != 65535)
				{
					num10 = this.m_ucsCharLevel2[(int)this.m_ucsCharLevel1[num8] + num9];
				}
				else
				{
					num10 = this.m_ucsCharReplacement;
				}
				if (((long)num10 & (long)((ulong)-1)) > 65535L)
				{
					if (num3 + 3 >= num4)
					{
						break;
					}
					char[] array = new char[]
					{
						(char)(num10 >> 16),
						(char)(num10 & 65535)
					};
					utfbytes[num3++] = (byte)(array[0] & 'ÿ');
					utfbytes[num3++] = (byte)(array[0] >> 8);
					utfbytes[num3++] = (byte)(array[1] & 'ÿ');
					utfbytes[num3++] = (byte)(array[1] >> 8);
				}
				else
				{
					utfbytes[num3++] = (byte)(num10 & 255);
					utfbytes[num3++] = (byte)(num10 >> 8);
				}
				num += num7;
			}
			utfCount = num3 - utfOffset;
			return num - offset;
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x0007D830 File Offset: 0x0007BA30
		private int ConvertByteArraySegListToCharsImpl<T>(IList<ArraySegment<byte>> bytes, int bytesOffset, int bytesCount, T[] chars, int charOffset, ref int charCount, bool bUseReplacementChar, TLBConvLC.ConvertByteToCharsDelegate<T> t)
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
						array = new byte[3];
					}
					byte[] array2 = bytes[num7 + 1].Array;
					int num11 = num8 + num10;
					int num12 = (int)(bytes[num7].Array[num11] & byte.MaxValue);
					int buffer1Bytes = num9 - num10;
					if (num12 == 143)
					{
						UTF16ConvUtility.GetRemainingBytes(3, bytes[num7].Array, num11, buffer1Bytes, bytes, ref num7, ref num5, array);
						byteCounts = 3;
					}
					else if (num12 > 127)
					{
						UTF16ConvUtility.GetRemainingBytes(2, bytes[num7].Array, num11, buffer1Bytes, bytes, ref num7, ref num5, array);
						byteCounts = 2;
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

		// Token: 0x06000B34 RID: 2868 RVA: 0x0007DA48 File Offset: 0x0007BC48
		public override int GetBytesOffset(IList<ArraySegment<byte>> bytes, int charCount)
		{
			int num = charCount;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			byte[] array = null;
			int count = 0;
			int num5 = 0;
			while (num5 < bytes.Count && num > 0)
			{
				int num6 = bytes[num5].Offset + num4;
				int num7 = bytes[num5].Count - num4;
				byte[] array2 = bytes[num5].Array;
				int bytesOffsetJAEUCImpl = this.GetBytesOffsetJAEUCImpl(array2, num6, num7, ref num);
				num3 += bytesOffsetJAEUCImpl;
				num2 += num;
				num = charCount - num2;
				if (num > 0 && bytesOffsetJAEUCImpl < num7 && num5 < bytes.Count - 1)
				{
					if (array == null)
					{
						array = new byte[3];
					}
					byte[] array3 = bytes[num5 + 1].Array;
					int num8 = num6 + bytesOffsetJAEUCImpl;
					int num9 = (int)(array2[num8] & byte.MaxValue);
					int buffer1Bytes = num7 - bytesOffsetJAEUCImpl;
					if (num9 == 143)
					{
						UTF16ConvUtility.GetRemainingBytes(3, array2, num8, buffer1Bytes, bytes, ref num5, ref num4, array);
						count = 3;
					}
					else if (num9 > 127)
					{
						UTF16ConvUtility.GetRemainingBytes(2, array2, num8, buffer1Bytes, bytes, ref num5, ref num4, array);
						count = 2;
					}
					bytesOffsetJAEUCImpl = this.GetBytesOffsetJAEUCImpl(array, 0, count, ref num);
					num3 += bytesOffsetJAEUCImpl;
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

		// Token: 0x06000B35 RID: 2869 RVA: 0x0007DB94 File Offset: 0x0007BD94
		public override int ConvertBytesToChars(IList<ArraySegment<byte>> bytes, int bytesOffset, int bytesCount, char[] chars, int charOffset, ref int charCount, bool bUseReplacementChar)
		{
			if (bytes.Count == 1)
			{
				return this.ConvertBytesToChars(bytes[0].Array, bytes[0].Offset + bytesOffset, bytesCount, chars, charOffset, ref charCount, bUseReplacementChar);
			}
			return this.ConvertByteArraySegListToCharsImpl<char>(bytes, bytesOffset, bytesCount, chars, charOffset, ref charCount, bUseReplacementChar, new TLBConvLC.ConvertByteToCharsDelegate<char>(this.ConvertBytesToChars));
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000B36 RID: 2870 RVA: 0x0007DBF8 File Offset: 0x0007BDF8
		public override int MinBytesPerChar
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000B37 RID: 2871 RVA: 0x0007DBFC File Offset: 0x0007BDFC
		public override int MaxBytesPerChar
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x0007DC00 File Offset: 0x0007BE00
		public override void ExtractCodepoints(IList<int[]> vtable)
		{
			int num = 0;
			int num2 = 65535;
			for (int i = num; i <= num2; i++)
			{
				try
				{
					vtable.Add(new int[]
					{
						i,
						this.ToUnicodeJAEUC(i, true)
					});
				}
				catch
				{
				}
			}
			num = 9371648;
			num2 = num + 65535;
			for (int j = num; j <= num2; j++)
			{
				try
				{
					vtable.Add(new int[]
					{
						j,
						this.ToUnicodeJAEUC(j, true)
					});
				}
				catch
				{
				}
			}
		}

		// Token: 0x04000D12 RID: 3346
		private const int LEADINGCODE = 143;
	}
}
