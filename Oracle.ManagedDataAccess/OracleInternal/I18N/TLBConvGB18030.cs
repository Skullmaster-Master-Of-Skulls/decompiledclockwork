using System;
using System.Collections.Generic;
using System.Text;

namespace OracleInternal.I18N
{
	// Token: 0x02000100 RID: 256
	[Serializable]
	internal class TLBConvGB18030 : TLBConv12Byte
	{
		// Token: 0x06000AF3 RID: 2803 RVA: 0x00078E84 File Offset: 0x00077084
		public TLBConvGB18030()
		{
			this.m_groupId = 9;
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x00078E94 File Offset: 0x00077094
		private int ToUnicodeGB(int srcChar, bool ccb)
		{
			if (this.BMPLinear(srcChar) >= 39419)
			{
				int ucs = 1752754 + this.BMPLinear(srcChar) - 1876218;
				return this.SurrogateUcs4ToUtf16(ucs);
			}
			if (srcChar >> 16 != 0)
			{
				int num = this.BMPOracle2Unicode(srcChar);
				if (num == 0)
				{
					if (!ccb)
					{
						throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_UNICODE", new string[0]));
					}
					num = this.m_ucsCharReplacement;
				}
				return num;
			}
			int num2 = srcChar >> 8 & 255;
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

		// Token: 0x06000AF5 RID: 2805 RVA: 0x00078F70 File Offset: 0x00077170
		private int ToOracleCharacterGB(char srcChar, char lowSurrogate, bool ccb)
		{
			if (lowSurrogate == '\0')
			{
				int num = (int)(srcChar >> 8 & 'ÿ');
				int num2 = (int)(srcChar & 'ÿ');
				int num3;
				if (this.m_oraCharLevel1[num] != 65535 && this.m_oraCharLevel2[this.m_oraCharLevel1[num] + num2] != '￿')
				{
					num3 = (int)(this.m_oraCharLevel2[this.m_oraCharLevel1[num] + num2] & char.MaxValue);
				}
				else
				{
					num3 = this.BMPUnicode2Oracle(srcChar);
					if (num3 == 0)
					{
						if (!ccb)
						{
							throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_BYTES", new string[0]));
						}
						if (srcChar > '⿿')
						{
							return (int)this.m_2ByteOraCharReplacement[0];
						}
						return (int)this.m_1ByteOraCharReplacement;
					}
				}
				return num3;
			}
			int num4 = this.SurrogateUtf16ToUcs4(srcChar, lowSurrogate);
			if (num4 >= 65536 && num4 <= 1114111)
			{
				return this.BMPunLinear(1876218 + (num4 - 65536) - 1687218);
			}
			if (!ccb)
			{
				throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_BYTES", new string[0]));
			}
			return (int)this.m_2ByteOraCharReplacement[0];
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x00079068 File Offset: 0x00077268
		private int GetCharsLengthGBImpl(byte[] bytes, int offset, int count, ref int bytesCounted)
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
				if (num3 > (int)this.MAX_7_8_BIT)
				{
					if (i + 1 >= num)
					{
						bytesCounted = i - offset;
						break;
					}
					if ((bytes[i] & 255) >= 129 && (bytes[i] & 255) <= 254 && (bytes[i + 1] & 255) >= 48 && (bytes[i + 1] & 255) <= 57)
					{
						if (i + 3 >= num)
						{
							bytesCounted = i - offset;
							break;
						}
						if ((bytes[i + 2] & 255) >= 129 && (bytes[i + 2] & 255) <= 254 && (bytes[i + 3] & 255) >= 48 && (bytes[i + 3] & 255) <= 57)
						{
							num3 = ((int)(bytes[i] & byte.MaxValue) << 24 | (int)(bytes[i + 1] & byte.MaxValue) << 16 | (int)(bytes[i + 2] & byte.MaxValue) << 8 | (int)(bytes[i + 3] & byte.MaxValue));
							i += 4;
							int num4 = this.ToUnicodeGB(num3, true);
							if (num4 >> 16 == 0)
							{
								num2++;
							}
							else
							{
								num2 += 2;
							}
						}
						else
						{
							num2++;
						}
					}
					else
					{
						num2++;
						i += 2;
					}
				}
				else
				{
					num2++;
					i++;
				}
			}
			bytesCounted = i - offset;
			return num2;
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x000791D0 File Offset: 0x000773D0
		public override int GetCharsLength(byte[] bytes, int byteOffset, int byteCount)
		{
			int num = 0;
			return this.GetCharsLengthGBImpl(bytes, byteOffset, byteCount, ref num);
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x000791EC File Offset: 0x000773EC
		private int GetBytesOffsetGBImpl(byte[] bytes, int offset, int count, ref int charCount)
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
				if (num4 > (int)this.MAX_7_8_BIT)
				{
					if (num + 1 >= num2)
					{
						break;
					}
					if ((bytes[num] & 255) >= 129 && (bytes[num] & 255) <= 254 && (bytes[num + 1] & 255) >= 48 && (bytes[num + 1] & 255) <= 57)
					{
						if (num + 3 >= num2)
						{
							break;
						}
						if ((bytes[num + 2] & 255) >= 129 && (bytes[num + 2] & 255) <= 254 && (bytes[num + 3] & 255) >= 48 && (bytes[num + 3] & 255) <= 57)
						{
							num4 = ((int)(bytes[num] & byte.MaxValue) << 24 | (int)(bytes[num + 1] & byte.MaxValue) << 16 | (int)(bytes[num + 2] & byte.MaxValue) << 8 | (int)(bytes[num + 3] & byte.MaxValue));
							num += 4;
							int num5 = this.ToUnicodeGB(num4, true);
							if (num5 >> 16 == 0)
							{
								num3++;
							}
							else
							{
								num3 += 2;
							}
						}
						else
						{
							num3++;
						}
					}
					else
					{
						num3++;
						num += 2;
					}
				}
				else
				{
					num3++;
					num++;
				}
			}
			charCount = num3;
			return num - offset;
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x0007934C File Offset: 0x0007754C
		public override int GetBytesOffset(byte[] bytes, int byteOffset, int byteCount, int charCount)
		{
			int num = charCount;
			return this.GetBytesOffsetGBImpl(bytes, byteOffset, byteCount, ref num);
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x00079368 File Offset: 0x00077568
		public override int GetCharsLength(ArraySegment<byte> bytes, int bytesOffset, int bytesCount)
		{
			int num = 0;
			return this.GetCharsLengthGBImpl(bytes.Array, bytes.Offset + bytesOffset, bytesCount, ref num);
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x00079390 File Offset: 0x00077590
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
				byte[] array2 = bytes[num6].Array;
				int charsLengthGBImpl = this.GetCharsLengthGBImpl(array2, num7, num8, ref num2);
				num += charsLengthGBImpl;
				num3 += num2;
				if (num2 < num8 && !flag && num6 < bytes.Count - 1)
				{
					if (array == null)
					{
						array = new byte[4];
					}
					byte[] array3 = bytes[num6 + 1].Array;
					int num9 = num7 + num2;
					byte b = array2[num9];
					byte b2;
					if (num2 == num8 - 1)
					{
						b2 = array3[bytes[num6 + 1].Offset];
					}
					else
					{
						b2 = array2[num9 + 1];
					}
					int buffer1Bytes = num8 - num2;
					int count;
					if ((b & 255) >= 129 && (b & 255) <= 254 && (b2 & 255) >= 48 && (b2 & 255) <= 57)
					{
						UTF16ConvUtility.GetRemainingBytes(4, array2, num9, buffer1Bytes, bytes, ref num6, ref num4, array);
						count = 4;
					}
					else
					{
						UTF16ConvUtility.GetRemainingBytes(2, array2, num9, buffer1Bytes, bytes, ref num6, ref num4, array);
						count = 2;
					}
					charsLengthGBImpl = this.GetCharsLengthGBImpl(array, 0, count, ref num2);
					num += charsLengthGBImpl;
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

		// Token: 0x06000AFC RID: 2812 RVA: 0x00079590 File Offset: 0x00077790
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
				if (num5 > (int)this.MAX_7_8_BIT)
				{
					if (num + 1 < num2)
					{
						if ((bytes[num] & 255) >= 129 && (bytes[num] & 255) <= 254 && (bytes[num + 1] & 255) >= 48 && (bytes[num + 1] & 255) <= 57)
						{
							if (num + 3 >= num2)
							{
								break;
							}
							if ((bytes[num + 2] & 255) >= 129 && (bytes[num + 2] & 255) <= 254 && (bytes[num + 3] & 255) >= 48 && (bytes[num + 3] & 255) <= 57)
							{
								num5 = ((int)(bytes[num] & byte.MaxValue) << 24 | (int)(bytes[num + 1] & byte.MaxValue) << 16 | (int)(bytes[num + 2] & byte.MaxValue) << 8 | (int)(bytes[num + 3] & byte.MaxValue));
								num += 4;
								int num6 = this.ToUnicodeGB(num5, ccb);
								if (num6 >> 16 == 0)
								{
									chars[num3++] = (char)num6;
								}
								else
								{
									if (num3 >= num4)
									{
										num -= 4;
										break;
									}
									chars[num3++] = (char)(num6 >> 16 & 65535);
									chars[num3++] = (char)(num6 & 65535);
								}
							}
							else
							{
								if (!ccb)
								{
									throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_UNICODE", new string[0]));
								}
								chars[num3++] = (char)this.m_ucsCharReplacement;
							}
						}
						else
						{
							num5 = (((int)bytes[num] << 8 & 65280) | (int)(bytes[num + 1] & byte.MaxValue));
							chars[num3++] = (char)this.ToUnicodeGB(num5, ccb);
							num += 2;
						}
					}
					else
					{
						if (!ccb)
						{
							throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_UNICODE", new string[0]));
						}
						break;
					}
				}
				else
				{
					chars[num3++] = (char)this.ToUnicodeGB(num5, ccb);
					num++;
				}
			}
			charCount = num3 - charOffset;
			return num - offset;
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x000797D0 File Offset: 0x000779D0
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
				if (num5 > (int)this.MAX_7_8_BIT)
				{
					if (num + 1 < num2)
					{
						if ((bytes[num] & 255) >= 129 && (bytes[num] & 255) <= 254 && (bytes[num + 1] & 255) >= 48 && (bytes[num + 1] & 255) <= 57)
						{
							if (num + 3 >= num2)
							{
								break;
							}
							if ((bytes[num + 2] & 255) >= 129 && (bytes[num + 2] & 255) <= 254 && (bytes[num + 3] & 255) >= 48 && (bytes[num + 3] & 255) <= 57)
							{
								num5 = ((int)(bytes[num] & byte.MaxValue) << 24 | (int)(bytes[num + 1] & byte.MaxValue) << 16 | (int)(bytes[num + 2] & byte.MaxValue) << 8 | (int)(bytes[num + 3] & byte.MaxValue));
								num += 4;
								int num6 = this.ToUnicodeGB(num5, ccb);
								if (num6 >> 16 == 0)
								{
									utfbytes[num3++] = (byte)((ushort)num6 & 255);
									utfbytes[num3++] = (byte)((ushort)num6 >> 8);
								}
								else
								{
									if (num3 + 3 >= num4)
									{
										num -= 4;
										break;
									}
									char[] array = new char[]
									{
										(char)(num6 >> 16 & 65535),
										(char)(num6 & 65535)
									};
									utfbytes[num3++] = (byte)(array[0] & 'ÿ');
									utfbytes[num3++] = (byte)(array[0] >> 8);
									utfbytes[num3++] = (byte)(array[1] & 'ÿ');
									utfbytes[num3++] = (byte)(array[1] >> 8);
								}
							}
							else
							{
								if (!ccb)
								{
									throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_UNICODE", new string[0]));
								}
								utfbytes[num3++] = (byte)((ushort)this.m_ucsCharReplacement & 255);
								utfbytes[num3++] = (byte)((ushort)this.m_ucsCharReplacement >> 8);
							}
						}
						else
						{
							num5 = (((int)bytes[num] << 8 & 65280) | (int)(bytes[num + 1] & byte.MaxValue));
							char c = (char)this.ToUnicodeGB(num5, ccb);
							utfbytes[num3++] = (byte)(c & 'ÿ');
							utfbytes[num3++] = (byte)(c >> 8);
							num += 2;
						}
					}
					else
					{
						if (!ccb)
						{
							throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_UNICODE", new string[0]));
						}
						break;
					}
				}
				else
				{
					char c2 = (char)this.ToUnicodeGB(num5, ccb);
					utfbytes[num3++] = (byte)(c2 & 'ÿ');
					utfbytes[num3++] = (byte)(c2 >> 8);
					num++;
				}
			}
			utfCount = num3 - utfOffset;
			return num - offset;
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x00079ABC File Offset: 0x00077CBC
		protected override int ConvertByteArraySegListToCharsImpl<T>(IList<ArraySegment<byte>> bytes, int bytesOffset, int bytesCount, T[] chars, int charOffset, ref int charCount, bool bUseReplacementChar, TLBConv12Byte.ConvertByteToCharsDelegate<T> t)
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
					byte b = bytes[num7].Array[num11];
					byte b2;
					if (num10 == num9 - 1)
					{
						b2 = array2[bytes[num7 + 1].Offset];
					}
					else
					{
						b2 = bytes[num7].Array[num11 + 1];
					}
					int buffer1Bytes = num9 - num10;
					int byteCounts;
					if ((b & 255) >= 129 && (b & 255) <= 254 && (b2 & 255) >= 48 && (b2 & 255) <= 57)
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

		// Token: 0x06000AFF RID: 2815 RVA: 0x00079D34 File Offset: 0x00077F34
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
				int bytesOffsetGBImpl = this.GetBytesOffsetGBImpl(array2, num6, num7, ref num);
				num3 += bytesOffsetGBImpl;
				num2 += num;
				num = charCount - num2;
				if (num > 0 && bytesOffsetGBImpl < num7 && num5 < bytes.Count - 1)
				{
					if (array == null)
					{
						array = new byte[4];
					}
					byte[] array3 = bytes[num5 + 1].Array;
					int num8 = num6 + bytesOffsetGBImpl;
					byte b = array2[num8];
					byte b2;
					if (bytesOffsetGBImpl == num7 - 1)
					{
						b2 = array3[bytes[num5 + 1].Offset];
					}
					else
					{
						b2 = array2[num8 + 1];
					}
					int buffer1Bytes = num7 - bytesOffsetGBImpl;
					int count;
					if ((b & 255) >= 129 && (b & 255) <= 254 && (b2 & 255) >= 48 && (b2 & 255) <= 57)
					{
						UTF16ConvUtility.GetRemainingBytes(4, bytes[num5].Array, num8, buffer1Bytes, bytes, ref num5, ref num4, array);
						count = 4;
					}
					else
					{
						UTF16ConvUtility.GetRemainingBytes(2, bytes[num5].Array, num8, buffer1Bytes, bytes, ref num5, ref num4, array);
						count = 2;
					}
					bytesOffsetGBImpl = this.GetBytesOffsetGBImpl(array, 0, count, ref num);
					num3 += bytesOffsetGBImpl;
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

		// Token: 0x06000B00 RID: 2816 RVA: 0x00079EF0 File Offset: 0x000780F0
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
			int i = num;
			while (i < num2)
			{
				int num4;
				if (chars[i] < '\ud800' || chars[i] >= '\udc00')
				{
					num4 = this.ToOracleCharacterGB(chars[i], '\0', true);
					goto IL_82;
				}
				if (i + 1 < num2 && chars[i + 1] >= '\udc00' && chars[i + 1] <= '\udfff')
				{
					num4 = this.ToOracleCharacterGB(chars[i], chars[i + 1], true);
					i++;
					goto IL_82;
				}
				num3 += 2;
				IL_A3:
				i++;
				continue;
				IL_82:
				if (num4 >> 16 != 0)
				{
					num3 += 4;
					goto IL_A3;
				}
				if (num4 >> 8 != 0)
				{
					num3 += 2;
					goto IL_A3;
				}
				num3++;
				goto IL_A3;
			}
			return num3;
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x00079FB0 File Offset: 0x000781B0
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
			int i = num;
			while (i < num2)
			{
				int num4;
				if (chars[i] < '\ud800' || chars[i] >= '\udc00')
				{
					num4 = this.ToOracleCharacterGB(chars[i], '\0', true);
					goto IL_A4;
				}
				if (i + 1 < num2 && chars[i + 1] >= '\udc00' && chars[i + 1] <= '\udfff')
				{
					num4 = this.ToOracleCharacterGB(chars[i], chars[i + 1], true);
					i++;
					goto IL_A4;
				}
				num3 += 2;
				IL_C5:
				i++;
				continue;
				IL_A4:
				if (num4 >> 16 != 0)
				{
					num3 += 4;
					goto IL_C5;
				}
				if (num4 >> 8 != 0)
				{
					num3 += 2;
					goto IL_C5;
				}
				num3++;
				goto IL_C5;
			}
			return num3;
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x0007A090 File Offset: 0x00078290
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
			int num3 = 65535;
			int num4 = 0;
			int i = num;
			while (i < num2 - 1)
			{
				int num5 = (int)utf16Bytes[i + 1] << 8 | (int)utf16Bytes[i];
				if (num5 < 55296 || num5 >= 56320)
				{
					num3 = this.ToOracleCharacterGB((char)num5, '\0', true);
					goto IL_94;
				}
				if (i + 3 < num2)
				{
					int num6 = (int)utf16Bytes[i + 3] << 8 | (int)utf16Bytes[i + 2];
					if (num6 >= 56320 && num6 <= 57343)
					{
						num3 = this.ToOracleCharacterGB((char)num5, (char)num6, true);
						i++;
						goto IL_94;
					}
					goto IL_94;
				}
				else
				{
					num4 += 2;
				}
				IL_B5:
				i += 2;
				continue;
				IL_94:
				if (num3 >> 16 != 0)
				{
					num4 += 4;
					goto IL_B5;
				}
				if (num3 >> 8 != 0)
				{
					num4 += 2;
					goto IL_B5;
				}
				num4++;
				goto IL_B5;
			}
			return num4;
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x0007A164 File Offset: 0x00078364
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
			int num5 = num;
			while (num5 < num2 && num4 < num3)
			{
				bool flag = false;
				int num6;
				if (chars[num5] < '\ud800' || chars[num5] >= '\udbff')
				{
					num6 = this.ToOracleCharacterGB(chars[num5], '\0', ccb);
					goto IL_F4;
				}
				if (num5 + 1 < num2 && chars[num5 + 1] >= '\udc00' && chars[num5 + 1] <= '\udfff')
				{
					num6 = this.ToOracleCharacterGB(chars[num5], chars[num5 + 1], ccb);
					flag = true;
					num5++;
					goto IL_F4;
				}
				if (!ccb)
				{
					throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_BYTES", new string[0]));
				}
				if (num4 + 1 >= num3)
				{
					break;
				}
				bytes[num4++] = (byte)(this.m_2ByteOraCharReplacement[0] >> 8);
				bytes[num4++] = (byte)this.m_2ByteOraCharReplacement[0];
				IL_192:
				num5++;
				continue;
				IL_F4:
				if (num6 >> 16 != 0)
				{
					if (num4 + 3 < num3)
					{
						bytes[num4++] = (byte)(num6 >> 24);
						bytes[num4++] = (byte)(num6 >> 16);
						bytes[num4++] = (byte)(num6 >> 8);
						bytes[num4++] = (byte)num6;
						goto IL_192;
					}
					if (flag)
					{
						num5--;
						break;
					}
					break;
				}
				else
				{
					if (num6 >> 8 == 0)
					{
						bytes[num4++] = (byte)num6;
						goto IL_192;
					}
					if (num4 + 1 < num3)
					{
						bytes[num4++] = (byte)(num6 >> 8);
						bytes[num4++] = (byte)num6;
						goto IL_192;
					}
					if (flag)
					{
						num5--;
						break;
					}
					break;
				}
			}
			bytes_count = num4 - bytes_begin;
			return num5 - num;
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x0007A320 File Offset: 0x00078520
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
			int num5 = num;
			while (num5 < num2 && num4 < num3)
			{
				bool flag = false;
				int num6;
				if (chars[num5] < '\ud800' || chars[num5] >= '\udbff')
				{
					num6 = this.ToOracleCharacterGB(chars[num5], '\0', ccb);
					goto IL_116;
				}
				if (num5 + 1 < num2 && chars[num5 + 1] >= '\udc00' && chars[num5 + 1] <= '\udfff')
				{
					num6 = this.ToOracleCharacterGB(chars[num5], chars[num5 + 1], ccb);
					flag = true;
					num5++;
					goto IL_116;
				}
				if (!ccb)
				{
					throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_BYTES", new string[0]));
				}
				if (num4 + 1 >= num3)
				{
					break;
				}
				bytes[num4++] = (byte)(this.m_2ByteOraCharReplacement[0] >> 8);
				bytes[num4++] = (byte)this.m_2ByteOraCharReplacement[0];
				IL_1B4:
				num5++;
				continue;
				IL_116:
				if (num6 >> 16 != 0)
				{
					if (num4 + 3 < num3)
					{
						bytes[num4++] = (byte)(num6 >> 24);
						bytes[num4++] = (byte)(num6 >> 16);
						bytes[num4++] = (byte)(num6 >> 8);
						bytes[num4++] = (byte)num6;
						goto IL_1B4;
					}
					if (flag)
					{
						num5--;
						break;
					}
					break;
				}
				else
				{
					if (num6 >> 8 == 0)
					{
						bytes[num4++] = (byte)num6;
						goto IL_1B4;
					}
					if (num4 + 1 < num3)
					{
						bytes[num4++] = (byte)(num6 >> 8);
						bytes[num4++] = (byte)num6;
						goto IL_1B4;
					}
					if (flag)
					{
						num5--;
						break;
					}
					break;
				}
			}
			bytes_count = num4 - bytes_begin;
			return num5 - num;
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x0007A4FC File Offset: 0x000786FC
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
			int num4 = 65535;
			int num5 = byteOffset;
			int num6 = num;
			while (num6 < num2 - 1 && num5 < num3)
			{
				bool flag = false;
				int num7 = (int)utf16Bytes[num6 + 1] << 8 | (int)utf16Bytes[num6];
				if (num7 < 55296 || num7 >= 56319)
				{
					num4 = this.ToOracleCharacterGB((char)num7, '\0', bUseReplacementChar);
					goto IL_10D;
				}
				if (num6 + 3 < num2)
				{
					int num8 = (int)utf16Bytes[num6 + 3] << 8 | (int)utf16Bytes[num6 + 2];
					if (num8 >= 56320 && num8 <= 57343)
					{
						num4 = this.ToOracleCharacterGB((char)num7, (char)num8, bUseReplacementChar);
						flag = true;
						num6 += 2;
						goto IL_10D;
					}
					goto IL_10D;
				}
				else
				{
					if (!bUseReplacementChar)
					{
						throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_BYTES", new string[0]));
					}
					if (num5 + 1 >= num3)
					{
						break;
					}
					bytes[num5++] = (byte)(this.m_2ByteOraCharReplacement[0] >> 8);
					bytes[num5++] = (byte)this.m_2ByteOraCharReplacement[0];
				}
				IL_1AB:
				num6 += 2;
				continue;
				IL_10D:
				if (num4 >> 16 != 0)
				{
					if (num5 + 3 < num3)
					{
						bytes[num5++] = (byte)(num4 >> 24);
						bytes[num5++] = (byte)(num4 >> 16);
						bytes[num5++] = (byte)(num4 >> 8);
						bytes[num5++] = (byte)num4;
						goto IL_1AB;
					}
					if (flag)
					{
						num6 -= 2;
						break;
					}
					break;
				}
				else
				{
					if (num4 >> 8 == 0)
					{
						bytes[num5++] = (byte)num4;
						goto IL_1AB;
					}
					if (num5 + 1 < num3)
					{
						bytes[num5++] = (byte)(num4 >> 8);
						bytes[num5++] = (byte)num4;
						goto IL_1AB;
					}
					if (flag)
					{
						num6 -= 2;
						break;
					}
					break;
				}
			}
			byteCount = num5 - byteOffset;
			return num6 - num;
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000B06 RID: 2822 RVA: 0x0007A6D4 File Offset: 0x000788D4
		public override int MinBytesPerChar
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000B07 RID: 2823 RVA: 0x0007A6D8 File Offset: 0x000788D8
		public override int MaxBytesPerChar
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x0007A6DC File Offset: 0x000788DC
		private int SurrogateUtf16ToUcs4(char highSur, char lowSur)
		{
			return (int)((int)('Ͽ' & highSur) << 10 | (lowSur & 'Ͽ')) + 65536;
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x0007A6F8 File Offset: 0x000788F8
		private int SurrogateUcs4ToUtf16(int ucs4)
		{
			return (ucs4 - 65536 >> 10 | 55296) << 16 | ((ucs4 & 1023) | 56320);
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x0007A71C File Offset: 0x0007891C
		private int BMPUnicode2Oracle(char uCodepoint)
		{
			int num = this.SearchgbMapping(uCodepoint, true);
			if (num == 65535)
			{
				return 0;
			}
			return this.BMPunLinear(num);
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x0007A744 File Offset: 0x00078944
		private int BMPOracle2Unicode(int oCodepoint)
		{
			int num = this.SearchgbMapping((char)this.BMPLinear(oCodepoint), false);
			if (num == 65535)
			{
				return 0;
			}
			return num;
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x0007A76C File Offset: 0x0007896C
		private int BMPLinear(int codepoint)
		{
			return (((codepoint >> 24 & 255) * 10 + (codepoint >> 16 & 255)) * 126 + (codepoint >> 8 & 255)) * 10 + (codepoint & 255) - 1687218;
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x0007A7A4 File Offset: 0x000789A4
		private int BMPunLinear(int lin)
		{
			int num = 48 + lin % 10 & 255;
			lin /= 10;
			num += (129 + lin % 126 & 255) << 8;
			lin /= 126;
			num += (48 + lin % 10 & 255) << 16;
			lin /= 10;
			return num + ((129 + lin & 255) << 24);
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x0007A810 File Offset: 0x00078A10
		private int SearchgbMapping(char codepoint, bool u2o)
		{
			int num;
			int num2;
			int num3;
			if (u2o)
			{
				num = 1;
				num2 = 0;
				num3 = 2;
			}
			else
			{
				num = 3;
				num2 = 2;
				num3 = 0;
			}
			int i = TLBConvGB18030.gbMapping.Length - 1;
			int num4 = 0;
			while (i >= num4)
			{
				int num5 = (i + num4) / 2;
				if (TLBConvGB18030.gbMapping[num5][num2] <= codepoint && codepoint <= TLBConvGB18030.gbMapping[num5][num])
				{
					return (int)(codepoint - TLBConvGB18030.gbMapping[num5][num2] + TLBConvGB18030.gbMapping[num5][num3]);
				}
				if (codepoint < TLBConvGB18030.gbMapping[num5][num2])
				{
					i = num5 - 1;
				}
				else if (codepoint > TLBConvGB18030.gbMapping[num5][num])
				{
					num4 = num5 + 1;
				}
			}
			return 65535;
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x0007A8A8 File Offset: 0x00078AA8
		public override void BuildUnicodeToOracleMapping()
		{
			this.m_oraCharLevel1 = new int[256];
			char[] array = new char[64000];
			int num = 0;
			int[][] array2 = new int[64000][];
			int num2 = 0;
			for (int i = 0; i < 64000; i++)
			{
				array2[i] = new int[2];
			}
			for (int j = 0; j < 256; j++)
			{
				this.m_oraCharLevel1[j] = 65535;
			}
			for (int k = 0; k < 64000; k++)
			{
				array[k] = char.MaxValue;
			}
			for (int l = 0; l < 65535; l++)
			{
				int num3 = this.ToUnicodeGB(l, true);
				if (num3 != this.m_ucsCharReplacement)
				{
					array2[num2][0] = num3;
					array2[num2][1] = l;
					num2++;
				}
			}
			for (int m = 0; m < array2.Length; m++)
			{
				int num4 = array2[m][0] >> 8 & 255;
				int num5 = array2[m][0] & 255;
				if (this.m_oraCharLevel1[num4] == 65535)
				{
					this.m_oraCharLevel1[num4] = num;
					num += 256;
				}
				if (array[this.m_oraCharLevel1[num4] + num5] == '￿')
				{
					array[this.m_oraCharLevel1[num4] + num5] = (char)(array2[m][1] & 65535);
				}
			}
			if (this.extraUnicodeToOracleMapping != null)
			{
				int num6 = this.extraUnicodeToOracleMapping.Length;
				for (int n = 0; n < num6; n++)
				{
					int num7 = this.extraUnicodeToOracleMapping[n][0];
					int num4 = num7 >> 8 & 255;
					int num5 = num7 & 255;
					num4 = (array2[n][0] >> 8 & 255);
					num5 = (array2[n][0] & 255);
					if (this.m_oraCharLevel1[num4] == 65535)
					{
						this.m_oraCharLevel1[num4] = num;
						num += 256;
					}
					if (array[this.m_oraCharLevel1[num4] + num5] == '￿')
					{
						array[this.m_oraCharLevel1[num4] + num5] = (char)(array2[n][1] & 65535);
					}
				}
			}
			this.m_oraCharLevel2 = new char[num];
			for (int num8 = 0; num8 < num; num8++)
			{
				this.m_oraCharLevel2[num8] = array[num8];
			}
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x0007AAE8 File Offset: 0x00078CE8
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
						(int)((ushort)this.ToUnicodeGB(i, true))
					});
				}
				catch
				{
				}
			}
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x0007AB3C File Offset: 0x00078D3C
		public override void ExtractExtraMappings(IList<int[]> vtable)
		{
			if (this.extraUnicodeToOracleMapping == null)
			{
				return;
			}
			for (int i = 0; i < this.extraUnicodeToOracleMapping.Length; i++)
			{
				vtable.Add(new int[]
				{
					this.extraUnicodeToOracleMapping[i][0],
					this.extraUnicodeToOracleMapping[i][1]
				});
			}
		}

		// Token: 0x04000CEE RID: 3310
		private const int GB18030_MAXLIMIT = 64000;

		// Token: 0x04000CEF RID: 3311
		private const long BMPSTART = 2167439664L;

		// Token: 0x04000CF0 RID: 3312
		private const long BMPEND = 2217845817L;

		// Token: 0x04000CF1 RID: 3313
		private const int GBBMPSTART = 1687218;

		// Token: 0x04000CF2 RID: 3314
		private const int GBBMPEND = 1726637;

		// Token: 0x04000CF3 RID: 3315
		private const int USURSTART = 65536;

		// Token: 0x04000CF4 RID: 3316
		private const int USUREND = 1114111;

		// Token: 0x04000CF5 RID: 3317
		private const int GBSURSTART = 1876218;

		// Token: 0x04000CF6 RID: 3318
		private const int GBSUREND = 2924793;

		// Token: 0x04000CF7 RID: 3319
		private static readonly char[][] gbMapping = new char[][]
		{
			new char[]
			{
				'\u0080',
				'£',
				'\0',
				'#'
			},
			new char[]
			{
				'¥',
				'¦',
				'$',
				'%'
			},
			new char[]
			{
				'©',
				'¯',
				'&',
				','
			},
			new char[]
			{
				'²',
				'¶',
				'-',
				'1'
			},
			new char[]
			{
				'¸',
				'Ö',
				'2',
				'P'
			},
			new char[]
			{
				'Ø',
				'ß',
				'Q',
				'X'
			},
			new char[]
			{
				'â',
				'ç',
				'Y',
				'^'
			},
			new char[]
			{
				'ë',
				'ë',
				'_',
				'_'
			},
			new char[]
			{
				'î',
				'ñ',
				'`',
				'c'
			},
			new char[]
			{
				'ô',
				'ö',
				'd',
				'f'
			},
			new char[]
			{
				'ø',
				'ø',
				'g',
				'g'
			},
			new char[]
			{
				'û',
				'û',
				'h',
				'h'
			},
			new char[]
			{
				'ý',
				'Ā',
				'i',
				'l'
			},
			new char[]
			{
				'Ă',
				'Ē',
				'm',
				'}'
			},
			new char[]
			{
				'Ĕ',
				'Ě',
				'~',
				'\u0084'
			},
			new char[]
			{
				'Ĝ',
				'Ī',
				'\u0085',
				'\u0093'
			},
			new char[]
			{
				'Ĭ',
				'Ń',
				'\u0094',
				'«'
			},
			new char[]
			{
				'Ņ',
				'Ň',
				'¬',
				'®'
			},
			new char[]
			{
				'ŉ',
				'Ō',
				'¯',
				'²'
			},
			new char[]
			{
				'Ŏ',
				'Ū',
				'³',
				'Ï'
			},
			new char[]
			{
				'Ŭ',
				'Ǎ',
				'Ð',
				'ı'
			},
			new char[]
			{
				'Ǐ',
				'Ǐ',
				'Ĳ',
				'Ĳ'
			},
			new char[]
			{
				'Ǒ',
				'Ǒ',
				'ĳ',
				'ĳ'
			},
			new char[]
			{
				'Ǔ',
				'Ǔ',
				'Ĵ',
				'Ĵ'
			},
			new char[]
			{
				'Ǖ',
				'Ǖ',
				'ĵ',
				'ĵ'
			},
			new char[]
			{
				'Ǘ',
				'Ǘ',
				'Ķ',
				'Ķ'
			},
			new char[]
			{
				'Ǚ',
				'Ǚ',
				'ķ',
				'ķ'
			},
			new char[]
			{
				'Ǜ',
				'Ǜ',
				'ĸ',
				'ĸ'
			},
			new char[]
			{
				'ǝ',
				'Ǹ',
				'Ĺ',
				'Ŕ'
			},
			new char[]
			{
				'Ǻ',
				'ɐ',
				'ŕ',
				'ƫ'
			},
			new char[]
			{
				'ɒ',
				'ɠ',
				'Ƭ',
				'ƺ'
			},
			new char[]
			{
				'ɢ',
				'ˆ',
				'ƻ',
				'ȟ'
			},
			new char[]
			{
				'ˈ',
				'ˈ',
				'Ƞ',
				'Ƞ'
			},
			new char[]
			{
				'ˌ',
				'˘',
				'ȡ',
				'ȭ'
			},
			new char[]
			{
				'˚',
				'ΐ',
				'Ȯ',
				'ˤ'
			},
			new char[]
			{
				'΢',
				'΢',
				'˥',
				'˥'
			},
			new char[]
			{
				'Ϊ',
				'ΰ',
				'˦',
				'ˬ'
			},
			new char[]
			{
				'ς',
				'ς',
				'˭',
				'˭'
			},
			new char[]
			{
				'ϊ',
				'Ѐ',
				'ˮ',
				'̤'
			},
			new char[]
			{
				'Ђ',
				'Џ',
				'̥',
				'̲'
			},
			new char[]
			{
				'ѐ',
				'ѐ',
				'̳',
				'̳'
			},
			new char[]
			{
				'ђ',
				'‏',
				'̴',
				'ự'
			},
			new char[]
			{
				'‑',
				'‒',
				'Ỳ',
				'ỳ'
			},
			new char[]
			{
				'‗',
				'‗',
				'Ỵ',
				'Ỵ'
			},
			new char[]
			{
				'‚',
				'‛',
				'ỵ',
				'Ỷ'
			},
			new char[]
			{
				'„',
				'․',
				'ỷ',
				'ỽ'
			},
			new char[]
			{
				'‧',
				'\u202f',
				'Ỿ',
				'ἆ'
			},
			new char[]
			{
				'‱',
				'‱',
				'ἇ',
				'ἇ'
			},
			new char[]
			{
				'‴',
				'‴',
				'Ἀ',
				'Ἀ'
			},
			new char[]
			{
				'‶',
				'›',
				'Ἁ',
				'Ἅ'
			},
			new char[]
			{
				'‼',
				'₫',
				'Ἆ',
				'ώ'
			},
			new char[]
			{
				'₭',
				'ℂ',
				'὾',
				'ΐ'
			},
			new char[]
			{
				'℄',
				'℄',
				'῔',
				'῔'
			},
			new char[]
			{
				'℆',
				'℈',
				'῕',
				'ῗ'
			},
			new char[]
			{
				'ℊ',
				'ℕ',
				'Ῐ',
				'ΰ'
			},
			new char[]
			{
				'℗',
				'℠',
				'ῤ',
				'῭'
			},
			new char[]
			{
				'™',
				'⅟',
				'΅',
				'‫'
			},
			new char[]
			{
				'Ⅼ',
				'Ⅿ',
				'‬',
				'\u202f'
			},
			new char[]
			{
				'ⅺ',
				'↏',
				'‰',
				'⁅'
			},
			new char[]
			{
				'↔',
				'↕',
				'⁆',
				'⁇'
			},
			new char[]
			{
				'↚',
				'∇',
				'⁈',
				'₵'
			},
			new char[]
			{
				'∉',
				'∎',
				'₶',
				'₻'
			},
			new char[]
			{
				'∐',
				'∐',
				'₼',
				'₼'
			},
			new char[]
			{
				'−',
				'∔',
				'₽',
				'₿'
			},
			new char[]
			{
				'∖',
				'∙',
				'⃀',
				'⃃'
			},
			new char[]
			{
				'∛',
				'∜',
				'⃄',
				'⃅'
			},
			new char[]
			{
				'∡',
				'∢',
				'⃆',
				'⃇'
			},
			new char[]
			{
				'∤',
				'∤',
				'⃈',
				'⃈'
			},
			new char[]
			{
				'∦',
				'∦',
				'⃉',
				'⃉'
			},
			new char[]
			{
				'∬',
				'∭',
				'⃊',
				'⃋'
			},
			new char[]
			{
				'∯',
				'∳',
				'⃌',
				'⃐'
			},
			new char[]
			{
				'∸',
				'∼',
				'⃑',
				'⃕'
			},
			new char[]
			{
				'∾',
				'≇',
				'⃖',
				'⃟'
			},
			new char[]
			{
				'≉',
				'≋',
				'⃠',
				'⃢'
			},
			new char[]
			{
				'≍',
				'≑',
				'⃣',
				'⃧'
			},
			new char[]
			{
				'≓',
				'≟',
				'⃨',
				'⃴'
			},
			new char[]
			{
				'≢',
				'≣',
				'⃵',
				'⃶'
			},
			new char[]
			{
				'≨',
				'≭',
				'⃷',
				'⃼'
			},
			new char[]
			{
				'≰',
				'⊔',
				'⃽',
				'℡'
			},
			new char[]
			{
				'⊖',
				'⊘',
				'™',
				'ℤ'
			},
			new char[]
			{
				'⊚',
				'⊤',
				'℥',
				'ℯ'
			},
			new char[]
			{
				'⊦',
				'⊾',
				'ℰ',
				'ⅈ'
			},
			new char[]
			{
				'⋀',
				'⌑',
				'ⅉ',
				'↚'
			},
			new char[]
			{
				'⌓',
				'⑟',
				'↛',
				'⋧'
			},
			new char[]
			{
				'⑪',
				'⑳',
				'⋨',
				'⋱'
			},
			new char[]
			{
				'⒜',
				'⓿',
				'⋲',
				'⍕'
			},
			new char[]
			{
				'╌',
				'╏',
				'⍖',
				'⍙'
			},
			new char[]
			{
				'╴',
				'▀',
				'⍚',
				'⍦'
			},
			new char[]
			{
				'▐',
				'▒',
				'⍧',
				'⍩'
			},
			new char[]
			{
				'▖',
				'▟',
				'⍪',
				'⍳'
			},
			new char[]
			{
				'▢',
				'▱',
				'⍴',
				'⎃'
			},
			new char[]
			{
				'▴',
				'▻',
				'⎄',
				'⎋'
			},
			new char[]
			{
				'▾',
				'◅',
				'⎌',
				'⎓'
			},
			new char[]
			{
				'◈',
				'◊',
				'⎔',
				'⎖'
			},
			new char[]
			{
				'◌',
				'◍',
				'⎗',
				'⎘'
			},
			new char[]
			{
				'◐',
				'◡',
				'⎙',
				'⎪'
			},
			new char[]
			{
				'◦',
				'☄',
				'⎫',
				'⏉'
			},
			new char[]
			{
				'☇',
				'☈',
				'⏊',
				'⏋'
			},
			new char[]
			{
				'☊',
				'☿',
				'⏌',
				'␁'
			},
			new char[]
			{
				'♁',
				'♁',
				'␂',
				'␂'
			},
			new char[]
			{
				'♃',
				'⺀',
				'␃',
				'ⱀ'
			},
			new char[]
			{
				'⺂',
				'⺃',
				'ⱁ',
				'ⱂ'
			},
			new char[]
			{
				'⺅',
				'⺇',
				'ⱃ',
				'ⱅ'
			},
			new char[]
			{
				'⺉',
				'⺊',
				'ⱆ',
				'ⱇ'
			},
			new char[]
			{
				'⺍',
				'⺖',
				'ⱈ',
				'ⱑ'
			},
			new char[]
			{
				'⺘',
				'⺦',
				'ⱒ',
				'Ⱡ'
			},
			new char[]
			{
				'⺨',
				'⺩',
				'ⱡ',
				'Ɫ'
			},
			new char[]
			{
				'⺫',
				'⺭',
				'Ᵽ',
				'ⱥ'
			},
			new char[]
			{
				'⺯',
				'⺲',
				'ⱦ',
				'Ⱪ'
			},
			new char[]
			{
				'⺴',
				'⺵',
				'ⱪ',
				'Ⱬ'
			},
			new char[]
			{
				'⺸',
				'⺺',
				'ⱬ',
				'Ɱ'
			},
			new char[]
			{
				'⺼',
				'⻉',
				'Ɐ',
				'ⱼ'
			},
			new char[]
			{
				'⻋',
				'⿯',
				'ⱽ',
				'ⶡ'
			},
			new char[]
			{
				'⿼',
				'⿿',
				'ⶢ',
				'ⶥ'
			},
			new char[]
			{
				'〄',
				'〄',
				'ⶦ',
				'ⶦ'
			},
			new char[]
			{
				'〘',
				'〜',
				'⶧',
				'ⶫ'
			},
			new char[]
			{
				'〟',
				'〠',
				'ⶬ',
				'ⶭ'
			},
			new char[]
			{
				'〪',
				'〽',
				'ⶮ',
				'ⷁ'
			},
			new char[]
			{
				'〿',
				'぀',
				'ⷂ',
				'ⷃ'
			},
			new char[]
			{
				'ゔ',
				'゚',
				'ⷄ',
				'ⷊ'
			},
			new char[]
			{
				'ゟ',
				'゠',
				'ⷋ',
				'ⷌ'
			},
			new char[]
			{
				'ヷ',
				'・',
				'ⷍ',
				'ⷑ'
			},
			new char[]
			{
				'ヿ',
				'㄄',
				'ⷒ',
				'⷗'
			},
			new char[]
			{
				'ㄪ',
				'㈟',
				'ⷘ',
				'⻍'
			},
			new char[]
			{
				'㈪',
				'㈰',
				'⻎',
				'⻔'
			},
			new char[]
			{
				'㈲',
				'㊢',
				'⻕',
				'⽅'
			},
			new char[]
			{
				'㊤',
				'㎍',
				'⽆',
				'〯'
			},
			new char[]
			{
				'㎐',
				'㎛',
				'〰',
				'〻'
			},
			new char[]
			{
				'㎟',
				'㎠',
				'〼',
				'〽'
			},
			new char[]
			{
				'㎢',
				'㏃',
				'〾',
				'た'
			},
			new char[]
			{
				'㏅',
				'㏍',
				'だ',
				'と'
			},
			new char[]
			{
				'㏏',
				'㏐',
				'ど',
				'な'
			},
			new char[]
			{
				'㏓',
				'㏔',
				'に',
				'ぬ'
			},
			new char[]
			{
				'㏖',
				'㑆',
				'ね',
				'ポ'
			},
			new char[]
			{
				'㑈',
				'㑲',
				'マ',
				'ㄈ'
			},
			new char[]
			{
				'㑴',
				'㖝',
				'ㄉ',
				'㈲'
			},
			new char[]
			{
				'㖟',
				'㘍',
				'㈳',
				'㊡'
			},
			new char[]
			{
				'㘏',
				'㘙',
				'㊢',
				'㊬'
			},
			new char[]
			{
				'㘛',
				'㤗',
				'㊭',
				'㖩'
			},
			new char[]
			{
				'㤙',
				'㥭',
				'㖪',
				'㗾'
			},
			new char[]
			{
				'㥯',
				'㧎',
				'㗿',
				'㙞'
			},
			new char[]
			{
				'㧑',
				'㧞',
				'㙟',
				'㙬'
			},
			new char[]
			{
				'㧠',
				'㩲',
				'㙭',
				'㛿'
			},
			new char[]
			{
				'㩴',
				'㭍',
				'㜀',
				'㟙'
			},
			new char[]
			{
				'㭏',
				'㱭',
				'㟚',
				'㣸'
			},
			new char[]
			{
				'㱯',
				'㳟',
				'㣹',
				'㥩'
			},
			new char[]
			{
				'㳡',
				'䁕',
				'㥪',
				'㳞'
			},
			new char[]
			{
				'䁗',
				'䅞',
				'㳟',
				'㷦'
			},
			new char[]
			{
				'䅠',
				'䌶',
				'㷧',
				'㾽'
			},
			new char[]
			{
				'䌸',
				'䎫',
				'㾾',
				'䀱'
			},
			new char[]
			{
				'䎭',
				'䎰',
				'䀲',
				'䀵'
			},
			new char[]
			{
				'䎲',
				'䏜',
				'䀶',
				'䁠'
			},
			new char[]
			{
				'䏞',
				'䓕',
				'䁡',
				'䅘'
			},
			new char[]
			{
				'䓗',
				'䙋',
				'䅙',
				'䋍'
			},
			new char[]
			{
				'䙍',
				'䙠',
				'䋎',
				'䋡'
			},
			new char[]
			{
				'䙢',
				'䜢',
				'䋢',
				'䎢'
			},
			new char[]
			{
				'䜤',
				'䜨',
				'䎣',
				'䎧'
			},
			new char[]
			{
				'䜪',
				'䝻',
				'䎨',
				'䏹'
			},
			new char[]
			{
				'䝽',
				'䞌',
				'䏺',
				'䐉'
			},
			new char[]
			{
				'䞎',
				'䥆',
				'䐊',
				'䗂'
			},
			new char[]
			{
				'䥈',
				'䥹',
				'䗃',
				'䗴'
			},
			new char[]
			{
				'䥻',
				'䥼',
				'䗵',
				'䗶'
			},
			new char[]
			{
				'䥾',
				'䦁',
				'䗷',
				'䗺'
			},
			new char[]
			{
				'䦄',
				'䦄',
				'䗻',
				'䗻'
			},
			new char[]
			{
				'䦇',
				'䦚',
				'䗼',
				'䘏'
			},
			new char[]
			{
				'䦜',
				'䦞',
				'䘐',
				'䘒'
			},
			new char[]
			{
				'䦠',
				'䦵',
				'䘓',
				'䘨'
			},
			new char[]
			{
				'䦸',
				'䱶',
				'䘩',
				'䣧'
			},
			new char[]
			{
				'䱸',
				'䲞',
				'䣨',
				'䤎'
			},
			new char[]
			{
				'䲤',
				'䴒',
				'䤏',
				'䥽'
			},
			new char[]
			{
				'䴚',
				'䶭',
				'䥾',
				'䨑'
			},
			new char[]
			{
				'䶯',
				'䷿',
				'䨒',
				'䩢'
			},
			new char[]
			{
				'龦',
				'퟿',
				'䩣',
				'芼'
			},
			new char[]
			{
				'',
				'',
				'芽',
				'芽'
			},
			new char[]
			{
				'',
				'',
				'芾',
				'芾'
			},
			new char[]
			{
				'',
				'',
				'芿',
				'苋'
			},
			new char[]
			{
				'',
				'',
				'苌',
				'苌'
			},
			new char[]
			{
				'',
				'',
				'苍',
				'苑'
			},
			new char[]
			{
				'',
				'',
				'苒',
				'苘'
			},
			new char[]
			{
				'',
				'',
				'苙',
				'苜'
			},
			new char[]
			{
				'',
				'',
				'苝',
				'苠'
			},
			new char[]
			{
				'',
				'',
				'苡',
				'苨'
			},
			new char[]
			{
				'',
				'',
				'苩',
				'苯'
			},
			new char[]
			{
				'',
				'',
				'苰',
				'苿'
			},
			new char[]
			{
				'',
				'',
				'茀',
				'茍'
			},
			new char[]
			{
				'',
				'狼',
				'茎',
				'鏔'
			},
			new char[]
			{
				'來',
				'兩',
				'鏕',
				'鐠'
			},
			new char[]
			{
				'梁',
				'璉',
				'鐡',
				'鐻'
			},
			new char[]
			{
				'練',
				'罹',
				'鐼',
				'钌'
			},
			new char[]
			{
				'裡',
				'藺',
				'钍',
				'钕'
			},
			new char[]
			{
				'鱗',
				'廓',
				'钖',
				'钯'
			},
			new char[]
			{
				'塚',
				'塚',
				'钰',
				'钰'
			},
			new char[]
			{
				'晴',
				'晴',
				'钱',
				'钱'
			},
			new char[]
			{
				'凞',
				'益',
				'钲',
				'钴'
			},
			new char[]
			{
				'神',
				'羽',
				'钵',
				'钺'
			},
			new char[]
			{
				'諸',
				'諸',
				'钻',
				'钻'
			},
			new char[]
			{
				'逸',
				'都',
				'钼',
				'钽'
			},
			new char[]
			{
				'飯',
				'︯',
				'钾',
				'飃'
			},
			new char[]
			{
				'︲',
				'︲',
				'飄',
				'飄'
			},
			new char[]
			{
				'﹅',
				'﹈',
				'飅',
				'飈'
			},
			new char[]
			{
				'﹓',
				'﹓',
				'飉',
				'飉'
			},
			new char[]
			{
				'﹘',
				'﹘',
				'飊',
				'飊'
			},
			new char[]
			{
				'﹧',
				'﹧',
				'飋',
				'飋'
			},
			new char[]
			{
				'﹬',
				'＀',
				'飌',
				'饠'
			},
			new char[]
			{
				'｟',
				'￟',
				'饡',
				'駡'
			},
			new char[]
			{
				'￦',
				'￮',
				'駢',
				'駪'
			}
		};
	}
}
