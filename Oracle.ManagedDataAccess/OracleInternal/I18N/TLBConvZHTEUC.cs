using System;
using System.Collections.Generic;
using System.Text;

namespace OracleInternal.I18N
{
	// Token: 0x02000108 RID: 264
	[Serializable]
	internal class TLBConvZHTEUC : TLBConvLC
	{
		// Token: 0x06000B5B RID: 2907 RVA: 0x0007F538 File Offset: 0x0007D738
		public TLBConvZHTEUC()
		{
			this.m_groupId = 5;
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x0007F548 File Offset: 0x0007D748
		private int GetCharsLengthZHTEUCImpl(byte[] bytes, int offset, int count, ref int bytesCounted)
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
				int num4 = 1;
				int num5 = 0;
				bool flag = true;
				if (num3 > 127)
				{
					if (i + 1 >= num)
					{
						break;
					}
					num3 = (num3 << 8 | (int)(bytes[i + 1] & byte.MaxValue));
					num4 = 2;
					int j = 0;
					while (j < this.m_ucsCharLeadingCode.Length)
					{
						if (num3 == (int)this.m_ucsCharLeadingCode[j][0])
						{
							if (i + 3 >= num)
							{
								flag = false;
								break;
							}
							num5 = (int)this.m_ucsCharLeadingCode[j][1];
							num3 = (num3 << 16 | ((int)bytes[i + 2] << 8 & 65280) | (int)(bytes[i + 3] & byte.MaxValue));
							num4 = 4;
							break;
						}
						else
						{
							j++;
						}
					}
				}
				if (!flag)
				{
					break;
				}
				int num6 = (num3 >> 8 & 255) + num5;
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
				i += num4;
			}
			bytesCounted = i - offset;
			return num2;
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x0007F6A4 File Offset: 0x0007D8A4
		private int GetBytesOffsetZHTEUCImpl(byte[] bytes, int offset, int count, ref int charCount)
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
				int num5 = 1;
				int num6 = 0;
				bool flag = true;
				if (num4 > 127)
				{
					if (num + 1 >= num2)
					{
						break;
					}
					num4 = (num4 << 8 | (int)(bytes[num + 1] & byte.MaxValue));
					num5 = 2;
					int i = 0;
					while (i < this.m_ucsCharLeadingCode.Length)
					{
						if (num4 == (int)this.m_ucsCharLeadingCode[i][0])
						{
							if (num + 3 >= num2)
							{
								flag = false;
								break;
							}
							num6 = (int)this.m_ucsCharLeadingCode[i][1];
							num4 = (num4 << 16 | ((int)bytes[num + 2] << 8 & 65280) | (int)(bytes[num + 3] & byte.MaxValue));
							num5 = 4;
							break;
						}
						else
						{
							i++;
						}
					}
				}
				if (!flag)
				{
					break;
				}
				int num7 = (num4 >> 8 & 255) + num6;
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
				num += num5;
			}
			charCount = num3;
			return num - offset;
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x0007F808 File Offset: 0x0007DA08
		public override int GetBytesOffset(byte[] bytes, int byteOffset, int byteCount, int charCount)
		{
			int num = charCount;
			return this.GetBytesOffsetZHTEUCImpl(bytes, byteOffset, byteCount, ref num);
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x0007F824 File Offset: 0x0007DA24
		private int GetRemainingBytes(int oraChar)
		{
			int result = 2;
			for (int i = 0; i < this.m_ucsCharLeadingCode.Length; i++)
			{
				if (oraChar == (int)this.m_ucsCharLeadingCode[i][0])
				{
					result = 4;
					break;
				}
			}
			return result;
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x0007F858 File Offset: 0x0007DA58
		public override int GetCharsLength(byte[] bytes, int byteOffset, int byteCount)
		{
			int num = 0;
			return this.GetCharsLengthZHTEUCImpl(bytes, byteOffset, byteCount, ref num);
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x0007F874 File Offset: 0x0007DA74
		public override int GetCharsLength(ArraySegment<byte> bytes, int bytesOffset, int bytesCount)
		{
			int num = 0;
			return this.GetCharsLengthZHTEUCImpl(bytes.Array, bytes.Offset + bytesOffset, bytesCount, ref num);
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x0007F89C File Offset: 0x0007DA9C
		public override int GetCharsLength(IList<ArraySegment<byte>> bytes, int bytesOffset, int bytesCount)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			byte[] array = null;
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
				int charsLengthZHTEUCImpl = this.GetCharsLengthZHTEUCImpl(bytes[num6].Array, num7, num8, ref num2);
				num += charsLengthZHTEUCImpl;
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
					byte b;
					if (num2 == num8 - 1)
					{
						b = array2[bytes[num6 + 1].Offset];
					}
					else
					{
						b = bytes[num6].Array[num9 + 1];
					}
					num10 = (num10 << 8 | (int)(b & byte.MaxValue));
					int remainingBytes = this.GetRemainingBytes(num10);
					int buffer1Bytes = num8 - num2;
					int count;
					if (remainingBytes == 4)
					{
						UTF16ConvUtility.GetRemainingBytes(4, bytes[num6].Array, num9, buffer1Bytes, bytes, ref num6, ref num4, array);
						count = 4;
					}
					else
					{
						UTF16ConvUtility.GetRemainingBytes(2, bytes[num6].Array, num9, buffer1Bytes, bytes, ref num6, ref num4, array);
						count = 2;
					}
					charsLengthZHTEUCImpl = this.GetCharsLengthZHTEUCImpl(array, 0, count, ref num2);
					num += charsLengthZHTEUCImpl;
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

		// Token: 0x06000B63 RID: 2915 RVA: 0x0007FAC0 File Offset: 0x0007DCC0
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
				int num6 = 1;
				int num7 = 0;
				bool flag = true;
				if (num5 > 127)
				{
					if (num + 1 >= num2)
					{
						if (!ccb)
						{
							throw new DecoderFallbackException("Failed to convert bytes to Unicode");
						}
						break;
					}
					else
					{
						num5 = (num5 << 8 | (int)(bytes[num + 1] & byte.MaxValue));
						num6 = 2;
						int i = 0;
						while (i < this.m_ucsCharLeadingCode.Length)
						{
							if (num5 == (int)this.m_ucsCharLeadingCode[i][0])
							{
								if (num + 3 < num2)
								{
									num7 = (int)this.m_ucsCharLeadingCode[i][1];
									num5 = (num5 << 16 | ((int)bytes[num + 2] << 8 & 65280) | (int)(bytes[num + 3] & byte.MaxValue));
									num6 = 4;
									break;
								}
								if (!ccb)
								{
									throw new DecoderFallbackException("Failed to convert bytes to Unicode");
								}
								flag = false;
								break;
							}
							else
							{
								i++;
							}
						}
					}
				}
				if (!flag)
				{
					break;
				}
				int num8 = (num5 >> 8 & 255) + num7;
				int num9 = num5 & 255;
				int num10;
				if (this.m_ucsCharLevel1[num8] != '￿' && this.m_ucsCharLevel2[(int)this.m_ucsCharLevel1[num8] + num9] != 65535)
				{
					num10 = this.m_ucsCharLevel2[(int)this.m_ucsCharLevel1[num8] + num9];
				}
				else
				{
					if (!ccb)
					{
						throw new DecoderFallbackException("Failed to convert bytes to Unicode");
					}
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
				num += num6;
			}
			charCount = num3 - charOffset;
			return num - offset;
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x0007FCA4 File Offset: 0x0007DEA4
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
				int num6 = 1;
				int num7 = 0;
				bool flag = true;
				if (num5 > 127)
				{
					if (num + 1 >= num2)
					{
						if (!ccb)
						{
							throw new DecoderFallbackException("Failed to convert bytes to Unicode");
						}
						break;
					}
					else
					{
						num5 = (num5 << 8 | (int)(bytes[num + 1] & byte.MaxValue));
						num6 = 2;
						int i = 0;
						while (i < this.m_ucsCharLeadingCode.Length)
						{
							if (num5 == (int)this.m_ucsCharLeadingCode[i][0])
							{
								if (num + 3 < num2)
								{
									num7 = (int)this.m_ucsCharLeadingCode[i][1];
									num5 = (num5 << 16 | ((int)bytes[num + 2] << 8 & 65280) | (int)(bytes[num + 3] & byte.MaxValue));
									num6 = 4;
									break;
								}
								if (!ccb)
								{
									throw new DecoderFallbackException("Failed to convert bytes to Unicode");
								}
								flag = false;
								break;
							}
							else
							{
								i++;
							}
						}
					}
				}
				if (!flag)
				{
					break;
				}
				int num8 = (num5 >> 8 & 255) + num7;
				int num9 = num5 & 255;
				int num10;
				if (this.m_ucsCharLevel1[num8] != '￿' && this.m_ucsCharLevel2[(int)this.m_ucsCharLevel1[num8] + num9] != 65535)
				{
					num10 = this.m_ucsCharLevel2[(int)this.m_ucsCharLevel1[num8] + num9];
				}
				else
				{
					if (!ccb)
					{
						throw new DecoderFallbackException("Failed to convert bytes to Unicode");
					}
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
				num += num6;
			}
			utfCount = num3 - utfOffset;
			return num - offset;
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x0007FEE0 File Offset: 0x0007E0E0
		private int ConvertByteArraySegListToCharsImpl<T>(IList<ArraySegment<byte>> bytes, int bytesOffset, int bytesCount, T[] chars, int charOffset, ref int charCount, bool bUseReplacementChar, TLBConvLC.ConvertByteToCharsDelegate<T> t)
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
					int num12 = (int)(bytes[num7].Array[num11] & byte.MaxValue);
					byte b;
					if (num10 == num9 - 1)
					{
						b = array2[bytes[num7 + 1].Offset];
					}
					else
					{
						b = bytes[num7].Array[num11 + 1];
					}
					num12 = (num12 << 8 | (int)(b & byte.MaxValue));
					int remainingBytes = this.GetRemainingBytes(num12);
					int buffer1Bytes = num9 - num10;
					int byteCounts;
					if (remainingBytes == 4)
					{
						UTF16ConvUtility.GetRemainingBytes(4, bytes[num7].Array, num11, buffer1Bytes, bytes, ref num7, ref num5, array);
						byteCounts = 4;
					}
					else
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

		// Token: 0x06000B66 RID: 2918 RVA: 0x00080148 File Offset: 0x0007E348
		public override int GetBytesOffset(IList<ArraySegment<byte>> bytes, int charCount)
		{
			int num = charCount;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			byte[] array = null;
			int num5 = 0;
			while (num5 < bytes.Count && num2 < charCount)
			{
				int num6 = bytes[num5].Offset + num4;
				int num7 = bytes[num5].Count - num4;
				byte[] array2 = bytes[num5].Array;
				int bytesOffsetZHTEUCImpl = this.GetBytesOffsetZHTEUCImpl(array2, num6, num7, ref num);
				num3 += bytesOffsetZHTEUCImpl;
				num2 += num;
				num = charCount - num2;
				if (num > 0 && bytesOffsetZHTEUCImpl < num7 && num5 < bytes.Count - 1)
				{
					if (array == null)
					{
						array = new byte[4];
					}
					byte[] array3 = bytes[num5 + 1].Array;
					int num8 = num6 + bytesOffsetZHTEUCImpl;
					int num9 = (int)(array2[num8] & byte.MaxValue);
					byte b;
					if (bytesOffsetZHTEUCImpl == num7 - 1)
					{
						b = array3[bytes[num5 + 1].Offset];
					}
					else
					{
						b = array2[num8 + 1];
					}
					num9 = (num9 << 8 | (int)(b & byte.MaxValue));
					int remainingBytes = this.GetRemainingBytes(num9);
					int buffer1Bytes = num7 - bytesOffsetZHTEUCImpl;
					int count;
					if (remainingBytes == 4)
					{
						UTF16ConvUtility.GetRemainingBytes(4, array2, num8, buffer1Bytes, bytes, ref num5, ref num4, array);
						count = 4;
					}
					else
					{
						UTF16ConvUtility.GetRemainingBytes(2, array2, num8, buffer1Bytes, bytes, ref num5, ref num4, array);
						count = 2;
					}
					bytesOffsetZHTEUCImpl = this.GetBytesOffsetZHTEUCImpl(array, 0, count, ref num);
					num3 += bytesOffsetZHTEUCImpl;
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

		// Token: 0x06000B67 RID: 2919 RVA: 0x000802D4 File Offset: 0x0007E4D4
		public override int ConvertBytesToChars(IList<ArraySegment<byte>> bytes, int bytesOffset, int bytesCount, char[] chars, int charOffset, ref int charCount, bool bUseReplacementChar)
		{
			if (bytes.Count == 1)
			{
				return this.ConvertBytesToChars(bytes[0].Array, bytes[0].Offset + bytesOffset, bytesCount, chars, charOffset, ref charCount, bUseReplacementChar);
			}
			return this.ConvertByteArraySegListToCharsImpl<char>(bytes, bytesOffset, bytesCount, chars, charOffset, ref charCount, bUseReplacementChar, new TLBConvLC.ConvertByteToCharsDelegate<char>(this.ConvertBytesToChars));
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000B68 RID: 2920 RVA: 0x00080338 File Offset: 0x0007E538
		public override int MinBytesPerChar
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000B69 RID: 2921 RVA: 0x0008033C File Offset: 0x0007E53C
		public override int MaxBytesPerChar
		{
			get
			{
				return 4;
			}
		}
	}
}
