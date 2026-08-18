using System;
using System.Collections.Generic;
using System.Text;

namespace OracleInternal.I18N
{
	// Token: 0x02000102 RID: 258
	[Serializable]
	internal abstract class TLBConvLC : TLBConv
	{
		// Token: 0x06000B14 RID: 2836 RVA: 0x0007BD8C File Offset: 0x00079F8C
		public TLBConvLC()
		{
			this.m_groupId = 8;
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x0007BD9C File Offset: 0x00079F9C
		protected int ToUnicodeLC(int srcChar, bool ccb)
		{
			int num = 0;
			int num2 = srcChar >> 16 & 65535;
			int i;
			for (i = 0; i < this.m_ucsCharLeadingCode.Length; i++)
			{
				if (num2 == (int)this.m_ucsCharLeadingCode[i][0])
				{
					num = (int)this.m_ucsCharLeadingCode[i][1];
					break;
				}
			}
			if (i != this.m_ucsCharLeadingCode.Length)
			{
				int num3 = (srcChar >> 8 & 255) + num;
				int num4 = srcChar & 255;
				if (this.m_ucsCharLevel1[num3] == '�')
				{
					this.m_ucsCharLevel1[num3] = '\ud84b';
				}
				int result;
				if (this.m_ucsCharLevel1[num3] != '￿' && this.m_ucsCharLevel2[(int)this.m_ucsCharLevel1[num3] + num4] != 65535)
				{
					result = this.m_ucsCharLevel2[(int)this.m_ucsCharLevel1[num3] + num4];
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
			if (!ccb)
			{
				throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_UNICODE", new string[0]));
			}
			return this.m_ucsCharReplacement;
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x0007BEA0 File Offset: 0x0007A0A0
		protected virtual int ToOracleCharacterLC(char srcChar, char lowSurrogate, bool ccb)
		{
			int num = 65535;
			if (lowSurrogate != '\0')
			{
				int num2 = (int)(srcChar >> 8 & 'ÿ');
				int num3 = (int)(srcChar & 'ÿ');
				int num4 = (int)(lowSurrogate >> 8 & 'ÿ');
				int num5 = (int)(lowSurrogate & 'ÿ');
				if (this.m_oraCharLevel1[num2] != '￿' && this.m_oraCharSurrogateLevel[(int)this.m_oraCharLevel1[num2] + num3] != '￿' && this.m_oraCharSurrogateLevel[(int)this.m_oraCharSurrogateLevel[(int)this.m_oraCharLevel1[num2] + num3] + num4] != '￿' && this.m_oraCharLevel2[(int)this.m_oraCharSurrogateLevel[(int)this.m_oraCharSurrogateLevel[(int)this.m_oraCharLevel1[num2] + num3] + num4] + num5] != 65535)
				{
					num = this.m_oraCharLevel2[(int)this.m_oraCharSurrogateLevel[(int)this.m_oraCharSurrogateLevel[(int)this.m_oraCharLevel1[num2] + num3] + num4] + num5];
				}
			}
			else
			{
				int num6 = (int)(srcChar >> 8 & 'ÿ');
				int num7 = (int)(srcChar & 'ÿ');
				if (this.m_oraCharLevel1[num6] != '￿' && this.m_oraCharLevel2[(int)this.m_oraCharLevel1[num6] + num7] != 65535)
				{
					num = this.m_oraCharLevel2[(int)this.m_oraCharLevel1[num6] + num7];
				}
			}
			if (num != 65535)
			{
				return num;
			}
			if (!ccb)
			{
				throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_BYTES", new string[0]));
			}
			if (srcChar > '⿿')
			{
				return (int)this.m_2ByteOraCharReplacement;
			}
			return (int)this.m_1ByteOraCharReplacement;
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x0007C00C File Offset: 0x0007A20C
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
				if (chars[i] < '\ud800' || chars[i] > '\udbff')
				{
					num4 = this.ToOracleCharacterLC(chars[i], '\0', true);
					goto IL_82;
				}
				if (i + 1 < num2 && chars[i + 1] >= '\udc00' && chars[i + 1] <= '\udfff')
				{
					num4 = this.ToOracleCharacterLC(chars[i], chars[i + 1], true);
					i++;
					goto IL_82;
				}
				num3 += 2;
				IL_B1:
				i++;
				continue;
				IL_82:
				if (num4 >> 24 != 0)
				{
					num3 += 4;
					goto IL_B1;
				}
				if (num4 >> 16 != 0)
				{
					num3 += 3;
					goto IL_B1;
				}
				if (num4 >> 8 != 0)
				{
					num3 += 2;
					goto IL_B1;
				}
				num3++;
				goto IL_B1;
			}
			return num3;
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x0007C0D8 File Offset: 0x0007A2D8
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
				if (chars[i] < '\ud800' || chars[i] > '\udbff')
				{
					num4 = this.ToOracleCharacterLC(chars[i], '\0', true);
					goto IL_A4;
				}
				if (i + 1 < num2 && chars[i + 1] >= '\udc00' && chars[i + 1] <= '\udfff')
				{
					num4 = this.ToOracleCharacterLC(chars[i], chars[i + 1], true);
					i++;
					goto IL_A4;
				}
				num3 += 2;
				IL_D3:
				i++;
				continue;
				IL_A4:
				if (num4 >> 24 != 0)
				{
					num3 += 4;
					goto IL_D3;
				}
				if (num4 >> 16 != 0)
				{
					num3 += 3;
					goto IL_D3;
				}
				if (num4 >> 8 != 0)
				{
					num3 += 2;
					goto IL_D3;
				}
				num3++;
				goto IL_D3;
			}
			return num3;
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x0007C1C8 File Offset: 0x0007A3C8
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
				if (num5 < 55296 || num5 >= 56319)
				{
					num3 = this.ToOracleCharacterLC((char)num5, '\0', true);
					goto IL_94;
				}
				if (i + 3 < num2)
				{
					int num6 = (int)utf16Bytes[i + 3] << 8 | (int)utf16Bytes[i + 2];
					if (num6 >= 56320 && num6 <= 57343)
					{
						num3 = this.ToOracleCharacterLC((char)num5, (char)num6, true);
						i++;
						goto IL_94;
					}
					goto IL_94;
				}
				else
				{
					num4 += 2;
				}
				IL_C3:
				i += 2;
				continue;
				IL_94:
				if (num3 >> 24 != 0)
				{
					num4 += 4;
					goto IL_C3;
				}
				if (num3 >> 16 != 0)
				{
					num4 += 3;
					goto IL_C3;
				}
				if (num3 >> 8 != 0)
				{
					num4 += 2;
					goto IL_C3;
				}
				num4++;
				goto IL_C3;
			}
			return num4;
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x0007C2A8 File Offset: 0x0007A4A8
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
				if (chars[num5] < '\ud800' || chars[num5] > '\udbff')
				{
					num6 = this.ToOracleCharacterLC(chars[num5], '\0', ccb);
					goto IL_F0;
				}
				if (num5 + 1 < num2 && chars[num5 + 1] >= '\udc00' && chars[num5 + 1] <= '\udfff')
				{
					num6 = this.ToOracleCharacterLC(chars[num5], chars[num5 + 1], ccb);
					flag = true;
					num5++;
					goto IL_F0;
				}
				if (!ccb)
				{
					throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_BYTES", new string[0]));
				}
				if (num4 + 1 >= num3)
				{
					break;
				}
				bytes[num4++] = (byte)(this.m_2ByteOraCharReplacement >> 8);
				bytes[num4++] = (byte)this.m_2ByteOraCharReplacement;
				IL_1DD:
				num5++;
				continue;
				IL_F0:
				if (num6 >> 24 != 0)
				{
					if (num4 + 3 < num3)
					{
						bytes[num4++] = (byte)(num6 >> 24);
						bytes[num4++] = (byte)(num6 >> 16);
						bytes[num4++] = (byte)(num6 >> 8);
						bytes[num4++] = (byte)num6;
						goto IL_1DD;
					}
					if (flag)
					{
						num5--;
						break;
					}
					break;
				}
				else if (num6 >> 16 != 0)
				{
					if (num4 + 2 < num3)
					{
						bytes[num4++] = (byte)(num6 >> 16);
						bytes[num4++] = (byte)(num6 >> 8);
						bytes[num4++] = (byte)num6;
						goto IL_1DD;
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
						goto IL_1DD;
					}
					if (num4 + 1 < num3)
					{
						bytes[num4++] = (byte)(num6 >> 8);
						bytes[num4++] = (byte)num6;
						goto IL_1DD;
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

		// Token: 0x06000B1B RID: 2843 RVA: 0x0007C4B0 File Offset: 0x0007A6B0
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
				if (chars[num5] < '\ud800' || chars[num5] > '\udbff')
				{
					num6 = this.ToOracleCharacterLC(chars[num5], '\0', ccb);
					goto IL_112;
				}
				if (num5 + 1 < num2 && chars[num5 + 1] >= '\udc00' && chars[num5 + 1] <= '\udfff')
				{
					num6 = this.ToOracleCharacterLC(chars[num5], chars[num5 + 1], ccb);
					flag = true;
					num5++;
					goto IL_112;
				}
				if (!ccb)
				{
					throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_BYTES", new string[0]));
				}
				if (num4 + 1 >= num3)
				{
					break;
				}
				bytes[num4++] = (byte)(this.m_2ByteOraCharReplacement >> 8);
				bytes[num4++] = (byte)this.m_2ByteOraCharReplacement;
				IL_1FF:
				num5++;
				continue;
				IL_112:
				if (num6 >> 24 != 0)
				{
					if (num4 + 3 < num3)
					{
						bytes[num4++] = (byte)(num6 >> 24);
						bytes[num4++] = (byte)(num6 >> 16);
						bytes[num4++] = (byte)(num6 >> 8);
						bytes[num4++] = (byte)num6;
						goto IL_1FF;
					}
					if (flag)
					{
						num5--;
						break;
					}
					break;
				}
				else if (num6 >> 16 != 0)
				{
					if (num4 + 2 < num3)
					{
						bytes[num4++] = (byte)(num6 >> 16);
						bytes[num4++] = (byte)(num6 >> 8);
						bytes[num4++] = (byte)num6;
						goto IL_1FF;
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
						goto IL_1FF;
					}
					if (num4 + 1 < num3)
					{
						bytes[num4++] = (byte)(num6 >> 8);
						bytes[num4++] = (byte)num6;
						goto IL_1FF;
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

		// Token: 0x06000B1C RID: 2844 RVA: 0x0007C6D8 File Offset: 0x0007A8D8
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
					num4 = this.ToOracleCharacterLC((char)num7, '\0', bUseReplacementChar);
					goto IL_106;
				}
				if (num6 + 3 < num2)
				{
					int num8 = (int)utf16Bytes[num6 + 3] << 8 | (int)utf16Bytes[num6 + 2];
					if (num8 >= 56320 && num8 <= 57343)
					{
						num4 = this.ToOracleCharacterLC((char)num7, (char)num8, bUseReplacementChar);
						flag = true;
						num6 += 2;
						goto IL_106;
					}
					goto IL_106;
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
					bytes[num5++] = (byte)(this.m_2ByteOraCharReplacement >> 8);
					bytes[num5++] = (byte)this.m_2ByteOraCharReplacement;
				}
				IL_1F3:
				num6 += 2;
				continue;
				IL_106:
				if (num4 >> 24 != 0)
				{
					if (num5 + 3 < num3)
					{
						bytes[num5++] = (byte)(num4 >> 24);
						bytes[num5++] = (byte)(num4 >> 16);
						bytes[num5++] = (byte)(num4 >> 8);
						bytes[num5++] = (byte)num4;
						goto IL_1F3;
					}
					if (flag)
					{
						num6 -= 2;
						break;
					}
					break;
				}
				else if (num4 >> 16 != 0)
				{
					if (num5 + 2 < num3)
					{
						bytes[num5++] = (byte)(num4 >> 16);
						bytes[num5++] = (byte)(num4 >> 8);
						bytes[num5++] = (byte)num4;
						goto IL_1F3;
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
						goto IL_1F3;
					}
					if (num5 + 1 < num3)
					{
						bytes[num5++] = (byte)(num4 >> 8);
						bytes[num5++] = (byte)num4;
						goto IL_1F3;
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

		// Token: 0x06000B1D RID: 2845 RVA: 0x0007C8F8 File Offset: 0x0007AAF8
		public override bool IsOraCharacterReplacement(char ch, char lowsur)
		{
			int num = this.ToOracleCharacterLC(ch, lowsur, true);
			return num == (int)this.GetOraChar1ByteRep() || num == (int)this.GetOraChar2ByteRep();
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x0007C924 File Offset: 0x0007AB24
		public override void BuildUnicodeToOracleMapping()
		{
			this.m_oraCharLevel1 = new char[256];
			char[] array = null;
			int[] array2 = null;
			IList<int[]> list = new List<int[]>(45055);
			Dictionary<int, char[]> dictionary = new Dictionary<int, char[]>();
			Dictionary<int, char[]> dictionary2 = new Dictionary<int, char[]>();
			char c = '\0';
			char c2 = '\0';
			for (int i = 0; i < 256; i++)
			{
				this.m_oraCharLevel1[i] = char.MaxValue;
			}
			for (int j = 0; j < this.m_ucsCharLeadingCode.Length; j++)
			{
				int num = (int)((int)this.m_ucsCharLeadingCode[j][0] << 16);
				for (int k = 0; k < 65535; k++)
				{
					int num2 = this.ToUnicodeLC(num | k, true);
					if (num2 != this.m_ucsCharReplacement)
					{
						list.Add(new int[]
						{
							num2,
							num | k
						});
						base.StoreMappingRange(num2, dictionary, dictionary2);
					}
				}
			}
			if (this.extraUnicodeToOracleMapping != null)
			{
				int num3 = this.extraUnicodeToOracleMapping.Length;
				for (int l = 0; l < num3; l++)
				{
					int num4 = this.extraUnicodeToOracleMapping[l][0];
					base.StoreMappingRange(num4, dictionary, dictionary2);
				}
			}
			int num5 = 0;
			int num6 = 0;
			foreach (KeyValuePair<int, char[]> keyValuePair in dictionary)
			{
				char[] value = keyValuePair.Value;
				if (value != null)
				{
					num5 += 256;
				}
			}
			foreach (KeyValuePair<int, char[]> keyValuePair2 in dictionary2)
			{
				char[] value = keyValuePair2.Value;
				if (value != null)
				{
					num6 += 256;
				}
			}
			if (num5 != 0)
			{
				array = new char[num5];
			}
			if (num6 != 0)
			{
				array2 = new int[num6];
			}
			for (int m = 0; m < num5; m++)
			{
				array[m] = char.MaxValue;
			}
			for (int n = 0; n < num6; n++)
			{
				array2[n] = 65535;
			}
			for (int num7 = 0; num7 < list.Count; num7++)
			{
				int[] array3 = list[num7];
				int num8 = array3[0] >> 24 & 255;
				int num9 = array3[0] >> 16 & 255;
				int num10 = array3[0] >> 8 & 255;
				int num11 = array3[0] & 255;
				if (num8 >= 216 && num8 < 220)
				{
					if (this.m_oraCharLevel1[num8] == '￿')
					{
						this.m_oraCharLevel1[num8] = c2;
						c2 += 'Ā';
					}
					if (array[(int)this.m_oraCharLevel1[num8] + num9] == '￿')
					{
						array[(int)this.m_oraCharLevel1[num8] + num9] = c2;
						c2 += 'Ā';
					}
					if (array[(int)array[(int)this.m_oraCharLevel1[num8] + num9] + num10] == '￿')
					{
						array[(int)array[(int)this.m_oraCharLevel1[num8] + num9] + num10] = c;
						c += 'Ā';
					}
					if (array2[(int)array[(int)array[(int)this.m_oraCharLevel1[num8] + num9] + num10] + num11] == 65535)
					{
						array2[(int)array[(int)array[(int)this.m_oraCharLevel1[num8] + num9] + num10] + num11] = array3[1];
					}
				}
				else
				{
					if (this.m_oraCharLevel1[num10] == '￿')
					{
						this.m_oraCharLevel1[num10] = c;
						c += 'Ā';
					}
					if (array2[(int)this.m_oraCharLevel1[num10] + num11] == 65535)
					{
						array2[(int)this.m_oraCharLevel1[num10] + num11] = array3[1];
					}
				}
			}
			if (this.extraUnicodeToOracleMapping != null)
			{
				int num3 = this.extraUnicodeToOracleMapping.Length;
				for (int num12 = 0; num12 < num3; num12++)
				{
					int num4 = this.extraUnicodeToOracleMapping[num12][0];
					int num8 = num4 >> 24 & 255;
					int num9 = num4 >> 16 & 255;
					int num10 = num4 >> 8 & 255;
					int num11 = num4 & 255;
					if (num8 >= 216 && num8 < 220)
					{
						if (this.m_oraCharLevel1[num8] == '￿')
						{
							this.m_oraCharLevel1[num8] = c2;
							c2 += 'Ā';
						}
						if (array[(int)this.m_oraCharLevel1[num8] + num9] == '￿')
						{
							array[(int)this.m_oraCharLevel1[num8] + num9] = c2;
							c2 += 'Ā';
						}
						if (array[(int)array[(int)this.m_oraCharLevel1[num8] + num9] + num10] == '￿')
						{
							array[(int)array[(int)this.m_oraCharLevel1[num8] + num9] + num10] = c;
							c += 'Ā';
						}
						array2[(int)array[(int)array[(int)this.m_oraCharLevel1[num8] + num9] + num10] + num11] = this.extraUnicodeToOracleMapping[num12][1];
					}
					else
					{
						if (this.m_oraCharLevel1[num10] == '￿')
						{
							this.m_oraCharLevel1[num10] = c;
							c += 'Ā';
						}
						array2[(int)this.m_oraCharLevel1[num10] + num11] = this.extraUnicodeToOracleMapping[num12][1];
					}
				}
			}
			this.m_oraCharLevel2 = array2;
			this.m_oraCharSurrogateLevel = array;
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x0007CE48 File Offset: 0x0007B048
		public override void ExtractCodepoints(IList<int[]> vtable)
		{
			for (int i = 0; i < this.m_ucsCharLeadingCode.Length; i++)
			{
				int num = (int)this.m_ucsCharLeadingCode[i][0];
				int num2 = num << 16;
				int num3 = num2 + 65535;
				for (int j = num2; j <= num3; j++)
				{
					try
					{
						vtable.Add(new int[]
						{
							j,
							this.ToUnicodeLC(j, true)
						});
					}
					catch
					{
					}
				}
			}
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x0007CECC File Offset: 0x0007B0CC
		public override void ExtractExtraMappings(IList<int[]> vtable)
		{
			if (this.extraUnicodeToOracleMapping == null)
			{
				return;
			}
			for (int i = 0; i < this.extraUnicodeToOracleMapping.Length; i++)
			{
				vtable.Add(this.extraUnicodeToOracleMapping[i]);
			}
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x0007CF04 File Offset: 0x0007B104
		public override bool HasExtraMappings()
		{
			return this.extraUnicodeToOracleMapping != null;
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x0007CF14 File Offset: 0x0007B114
		public override char GetOraChar1ByteRep()
		{
			return this.m_1ByteOraCharReplacement;
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x0007CF1C File Offset: 0x0007B11C
		public override char GetOraChar2ByteRep()
		{
			return this.m_2ByteOraCharReplacement;
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x0007CF24 File Offset: 0x0007B124
		public override int GetUCS2CharRep()
		{
			return this.m_ucsCharReplacement;
		}

		// Token: 0x04000CF8 RID: 3320
		public const int MAX_7BIT = 127;

		// Token: 0x04000CF9 RID: 3321
		private const int ORACHARMASK = 255;

		// Token: 0x04000CFA RID: 3322
		private const int UCSCHARWIDTH = 16;

		// Token: 0x04000CFB RID: 3323
		private const int ORACHARWIDTH = 16;

		// Token: 0x04000CFC RID: 3324
		private const int ORACHARWITHLCWIDTH = 32;

		// Token: 0x04000CFD RID: 3325
		public const int BYTEWIDTH = 8;

		// Token: 0x04000CFE RID: 3326
		private const int LOW16BITMASK = 65535;

		// Token: 0x04000CFF RID: 3327
		public const int LEADINGCODEWIDTH = 16;

		// Token: 0x04000D00 RID: 3328
		public const int LEADINGCODESHIFT = 16;

		// Token: 0x04000D01 RID: 3329
		public const int LEADINGCODEMASK = 65535;

		// Token: 0x04000D02 RID: 3330
		private const int LCINDEXWIDTH = 4;

		// Token: 0x04000D03 RID: 3331
		private const int LCINDEXMASK = 15;

		// Token: 0x04000D04 RID: 3332
		private const int LCINDEXFACTOR = 2;

		// Token: 0x04000D05 RID: 3333
		private const int MAXBYTEPERCHAR = 4;

		// Token: 0x04000D06 RID: 3334
		public char[][] m_ucsCharLeadingCode;

		// Token: 0x04000D07 RID: 3335
		public char[] m_ucsCharLevel1;

		// Token: 0x04000D08 RID: 3336
		public int[] m_ucsCharLevel2;

		// Token: 0x04000D09 RID: 3337
		public int m_ucsCharReplacement;

		// Token: 0x04000D0A RID: 3338
		public char m_1ByteOraCharReplacement;

		// Token: 0x04000D0B RID: 3339
		public char m_2ByteOraCharReplacement;

		// Token: 0x04000D0C RID: 3340
		public char[] m_displayWidthLevel1;

		// Token: 0x04000D0D RID: 3341
		public byte[] m_displayWidthLevel2;

		// Token: 0x04000D0E RID: 3342
		public char[][] m_displayWidthLeadingCode;

		// Token: 0x04000D0F RID: 3343
		public char[] m_oraCharLevel1;

		// Token: 0x04000D10 RID: 3344
		public int[] m_oraCharLevel2;

		// Token: 0x04000D11 RID: 3345
		public char[] m_oraCharSurrogateLevel;

		// Token: 0x02000103 RID: 259
		// (Invoke) Token: 0x06000B26 RID: 2854
		protected delegate int ConvertByteToCharsDelegate<T>(byte[] bytes, int byteOffsets, int byteCounts, T[] chars, int charOffset, ref int charCount, bool bUseReplacementChar);
	}
}
