using System;
using System.Collections.Generic;
using System.Text;

namespace OracleInternal.I18N
{
	// Token: 0x020000FC RID: 252
	[Serializable]
	internal class TLBConv12Byte : TLBConv
	{
		// Token: 0x06000A9F RID: 2719 RVA: 0x00075F48 File Offset: 0x00074148
		public TLBConv12Byte()
		{
			char[] 2ByteOraCharReplacement = new char[2];
			this.m_2ByteOraCharReplacement = 2ByteOraCharReplacement;
			base..ctor();
			this.m_groupId = 1;
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x00075F78 File Offset: 0x00074178
		protected int ToUnicode(int srcChar, bool ccb)
		{
			int num = srcChar >> 8 & 255;
			int num2 = srcChar & 255;
			int result;
			if (this.m_ucsCharLevel1[num] != '￿' && this.m_ucsCharLevel2[(int)this.m_ucsCharLevel1[num] + num2] != 65535)
			{
				result = this.m_ucsCharLevel2[(int)this.m_ucsCharLevel1[num] + num2];
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

		// Token: 0x06000AA1 RID: 2721 RVA: 0x00075FF8 File Offset: 0x000741F8
		private int ToUnicodeNoException(int srcChar)
		{
			int num = srcChar >> 8 & 255;
			int num2 = srcChar & 255;
			if (this.m_ucsCharLevel1[num] != '￿')
			{
				return this.m_ucsCharLevel2[(int)this.m_ucsCharLevel1[num] + num2];
			}
			return 65535;
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x00076040 File Offset: 0x00074240
		protected char ToOracleCharacter(char srcChar, char lowSurrogate, bool ccb)
		{
			char c = char.MaxValue;
			if (lowSurrogate != '\0')
			{
				int num = (int)(srcChar >> 8 & 'ÿ');
				int num2 = (int)(srcChar & 'ÿ');
				int num3 = (int)(lowSurrogate >> 8 & 'ÿ');
				int num4 = (int)(lowSurrogate & 'ÿ');
				if (this.m_oraCharLevel1[num] != 65535 && this.m_oraCharSurrogateLevel[this.m_oraCharLevel1[num] + num2] != 65535 && this.m_oraCharSurrogateLevel[this.m_oraCharSurrogateLevel[this.m_oraCharLevel1[num] + num2] + num3] != 65535 && this.m_oraCharLevel2[this.m_oraCharSurrogateLevel[this.m_oraCharSurrogateLevel[this.m_oraCharLevel1[num] + num2] + num3] + num4] != '￿')
				{
					c = this.m_oraCharLevel2[this.m_oraCharSurrogateLevel[this.m_oraCharSurrogateLevel[this.m_oraCharLevel1[num] + num2] + num3] + num4];
				}
			}
			else
			{
				int num5 = (int)(srcChar >> 8 & 'ÿ');
				int num6 = (int)(srcChar & 'ÿ');
				if (this.m_oraCharLevel1[num5] != 65535 && this.m_oraCharLevel2[this.m_oraCharLevel1[num5] + num6] != '￿')
				{
					c = this.m_oraCharLevel2[this.m_oraCharLevel1[num5] + num6];
				}
			}
			if (c != '￿')
			{
				return c;
			}
			if (!ccb)
			{
				throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_BYTES", new string[0]));
			}
			if (srcChar > '⿿')
			{
				return this.m_2ByteOraCharReplacement[0];
			}
			return this.m_1ByteOraCharReplacement;
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x000761B0 File Offset: 0x000743B0
		public override void BuildUnicodeToOracleMapping()
		{
			this.m_oraCharLevel1 = new int[256];
			this.m_oraCharSurrogateLevel = null;
			this.m_oraCharLevel2 = null;
			IList<int[]> list = new List<int[]>(45055);
			Dictionary<int, char[]> dictionary = new Dictionary<int, char[]>();
			Dictionary<int, char[]> dictionary2 = new Dictionary<int, char[]>();
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < 256; i++)
			{
				this.m_oraCharLevel1[i] = 65535;
			}
			for (int j = 0; j < 65535; j++)
			{
				int num3;
				if ((num3 = this.ToUnicodeNoException(j)) != 65535)
				{
					list.Add(new int[]
					{
						num3,
						j
					});
					base.StoreMappingRange(num3, dictionary, dictionary2);
				}
			}
			if (this.extraUnicodeToOracleMapping != null)
			{
				int num4 = this.extraUnicodeToOracleMapping.Length;
				for (int k = 0; k < num4; k++)
				{
					int num5 = this.extraUnicodeToOracleMapping[k][0];
					base.StoreMappingRange(num5, dictionary, dictionary2);
				}
			}
			int num6 = 0;
			int num7 = 0;
			foreach (KeyValuePair<int, char[]> keyValuePair in dictionary)
			{
				char[] value = keyValuePair.Value;
				if (value != null)
				{
					num6 += 256;
				}
			}
			foreach (KeyValuePair<int, char[]> keyValuePair2 in dictionary2)
			{
				char[] value = keyValuePair2.Value;
				if (value != null)
				{
					num7 += 256;
				}
			}
			if (num6 != 0)
			{
				this.m_oraCharSurrogateLevel = new int[num6];
			}
			if (num7 != 0)
			{
				this.m_oraCharLevel2 = new char[num7];
			}
			for (int l = 0; l < num6; l++)
			{
				this.m_oraCharSurrogateLevel[l] = 65535;
			}
			for (int m = 0; m < num7; m++)
			{
				this.m_oraCharLevel2[m] = char.MaxValue;
			}
			for (int n = 0; n < list.Count; n++)
			{
				int[] array = list[n];
				int num8 = array[0] >> 24 & 255;
				int num9 = array[0] >> 16 & 255;
				int num10 = array[0] >> 8 & 255;
				int num11 = array[0] & 255;
				if (num8 >= 216 && num8 < 220)
				{
					if (this.m_oraCharLevel1[num8] == 65535)
					{
						this.m_oraCharLevel1[num8] = num2;
						num2 += 256;
					}
					if (this.m_oraCharSurrogateLevel[this.m_oraCharLevel1[num8] + num9] == 65535)
					{
						this.m_oraCharSurrogateLevel[this.m_oraCharLevel1[num8] + num9] = num2;
						num2 += 256;
					}
					if (this.m_oraCharSurrogateLevel[this.m_oraCharSurrogateLevel[this.m_oraCharLevel1[num8] + num9] + num10] == 65535)
					{
						this.m_oraCharSurrogateLevel[this.m_oraCharSurrogateLevel[this.m_oraCharLevel1[num8] + num9] + num10] = num;
						num += 256;
					}
					if (this.m_oraCharLevel2[this.m_oraCharSurrogateLevel[this.m_oraCharSurrogateLevel[this.m_oraCharLevel1[num8] + num9] + num10] + num11] == '￿')
					{
						this.m_oraCharLevel2[this.m_oraCharSurrogateLevel[this.m_oraCharSurrogateLevel[this.m_oraCharLevel1[num8] + num9] + num10] + num11] = (char)(array[1] & 65535);
					}
				}
				else
				{
					if (this.m_oraCharLevel1[num10] == 65535)
					{
						this.m_oraCharLevel1[num10] = num;
						num += 256;
					}
					if (this.m_oraCharLevel2[this.m_oraCharLevel1[num10] + num11] == '￿')
					{
						this.m_oraCharLevel2[this.m_oraCharLevel1[num10] + num11] = (char)(array[1] & 65535);
					}
				}
			}
			if (this.extraUnicodeToOracleMapping != null)
			{
				int num4 = this.extraUnicodeToOracleMapping.Length;
				for (int num12 = 0; num12 < num4; num12++)
				{
					int num5 = this.extraUnicodeToOracleMapping[num12][0];
					int num8 = num5 >> 24 & 255;
					int num9 = num5 >> 16 & 255;
					int num10 = num5 >> 8 & 255;
					int num11 = num5 & 255;
					if (num8 >= 216 && num8 < 220)
					{
						if (this.m_oraCharLevel1[num8] == 65535)
						{
							this.m_oraCharLevel1[num8] = num2;
							num2 += 256;
						}
						if (this.m_oraCharSurrogateLevel[this.m_oraCharLevel1[num8] + num9] == 65535)
						{
							this.m_oraCharSurrogateLevel[this.m_oraCharLevel1[num8] + num9] = num2;
							num2 += 256;
						}
						if (this.m_oraCharSurrogateLevel[this.m_oraCharSurrogateLevel[this.m_oraCharLevel1[num8] + num9] + num10] == 65535)
						{
							this.m_oraCharSurrogateLevel[this.m_oraCharSurrogateLevel[this.m_oraCharLevel1[num8] + num9] + num10] = num;
							num += 256;
						}
						this.m_oraCharLevel2[this.m_oraCharSurrogateLevel[this.m_oraCharSurrogateLevel[this.m_oraCharLevel1[num8] + num9] + num10] + num11] = (char)(this.extraUnicodeToOracleMapping[num12][1] & 65535);
					}
					else
					{
						if (this.m_oraCharLevel1[num10] == 65535)
						{
							this.m_oraCharLevel1[num10] = num;
							num += 256;
						}
						this.m_oraCharLevel2[this.m_oraCharLevel1[num10] + num11] = (char)(this.extraUnicodeToOracleMapping[num12][1] & 65535);
					}
				}
			}
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x00076728 File Offset: 0x00074928
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
						this.ToUnicode(i, true)
					});
				}
				catch
				{
				}
			}
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x0007677C File Offset: 0x0007497C
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

		// Token: 0x06000AA6 RID: 2726 RVA: 0x000767D0 File Offset: 0x000749D0
		public override bool HasExtraMappings()
		{
			return this.extraUnicodeToOracleMapping != null;
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x000767E0 File Offset: 0x000749E0
		public override char GetOraChar1ByteRep()
		{
			return this.m_1ByteOraCharReplacement;
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x000767E8 File Offset: 0x000749E8
		public override char GetOraChar2ByteRep()
		{
			return this.m_2ByteOraCharReplacement[0];
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x000767F4 File Offset: 0x000749F4
		public override int GetUCS2CharRep()
		{
			return this.m_ucsCharReplacement;
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x000767FC File Offset: 0x000749FC
		public override bool IsOraCharacterReplacement(char ch, char lowsur)
		{
			char c = this.ToOracleCharacter(ch, lowsur, true);
			return c == this.GetOraChar1ByteRep() || c == this.GetOraChar2ByteRep();
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x00076828 File Offset: 0x00074A28
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
				char c;
				if (chars[i] < '\ud800' || chars[i] >= '\udc00')
				{
					c = this.ToOracleCharacter(chars[i], '\0', true);
					goto IL_7F;
				}
				if (i + 1 < num2 && chars[i + 1] >= '\udc00' && chars[i + 1] <= '\udfff')
				{
					c = this.ToOracleCharacter(chars[i], chars[i + 1], true);
					i++;
					goto IL_7F;
				}
				num3 += 2;
				IL_92:
				i++;
				continue;
				IL_7F:
				if (c >> 8 != '\0')
				{
					num3 += 2;
					goto IL_92;
				}
				num3++;
				goto IL_92;
			}
			return num3;
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x000768D4 File Offset: 0x00074AD4
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
				char c;
				if (chars[i] < '\ud800' || chars[i] >= '\udc00')
				{
					c = this.ToOracleCharacter(chars[i], '\0', true);
					goto IL_A4;
				}
				if (i + 1 < num2 && chars[i + 1] >= '\udc00' && chars[i + 1] <= '\udfff')
				{
					c = this.ToOracleCharacter(chars[i], chars[i + 1], true);
					i++;
					goto IL_A4;
				}
				num3 += 2;
				IL_B7:
				i++;
				continue;
				IL_A4:
				if (c >> 8 != '\0')
				{
					num3 += 2;
					goto IL_B7;
				}
				num3++;
				goto IL_B7;
			}
			return num3;
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x000769A8 File Offset: 0x00074BA8
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
			int num3 = 0;
			int i = num;
			while (i < num2 - 1)
			{
				int num4 = (int)utf16Bytes[i + 1] << 8 | (int)utf16Bytes[i];
				if (num4 < 55296 || num4 >= 56320)
				{
					c = this.ToOracleCharacter((char)num4, '\0', true);
					goto IL_94;
				}
				if (i + 3 < num2)
				{
					int num5 = (int)utf16Bytes[i + 3] << 8 | (int)utf16Bytes[i + 2];
					if (num5 >= 56320 && num5 <= 57343)
					{
						c = this.ToOracleCharacter((char)num4, (char)num5, true);
						i++;
						goto IL_94;
					}
					goto IL_94;
				}
				else
				{
					num3 += 2;
				}
				IL_A7:
				i += 2;
				continue;
				IL_94:
				if (c >> 8 != '\0')
				{
					num3 += 2;
					goto IL_A7;
				}
				num3++;
				goto IL_A7;
			}
			return num3;
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x00076A6C File Offset: 0x00074C6C
		protected virtual int GetCharsLengthImpl(byte[] bytes, int offset, int count, ref int bytesCounted)
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
					if (i >= num - 1)
					{
						bytesCounted = i - offset;
						break;
					}
					num3 = (((int)bytes[i] << 8 & 65280) | (int)(bytes[i + 1] & byte.MaxValue));
					i++;
				}
				int num4 = num3 >> 8 & 255;
				int num5 = num3 & 255;
				int num6;
				if (this.m_ucsCharLevel1[num4] != '￿' && this.m_ucsCharLevel2[(int)this.m_ucsCharLevel1[num4] + num5] != 65535)
				{
					num6 = this.m_ucsCharLevel2[(int)this.m_ucsCharLevel1[num4] + num5];
				}
				else
				{
					num6 = this.m_ucsCharReplacement;
				}
				if (((long)num6 & (long)((ulong)-1)) > 65535L)
				{
					num2 += 2;
				}
				else
				{
					num2++;
				}
				i++;
			}
			bytesCounted = i - offset;
			return num2;
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x00076B5C File Offset: 0x00074D5C
		public override int GetCharsLength(byte[] bytes, int byteOffset, int byteCount)
		{
			int num = 0;
			return this.GetCharsLengthImpl(bytes, byteOffset, byteCount, ref num);
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x00076B78 File Offset: 0x00074D78
		protected virtual int GetBytesOffsetImpl(byte[] bytes, int offset, int count, ref int charCount)
		{
			int num = offset;
			int num2 = offset + count;
			int num3 = 0;
			if (num2 > bytes.Length)
			{
				num2 = bytes.Length;
			}
			while (num < num2 && num3 < charCount)
			{
				int num4 = (int)(bytes[num] & byte.MaxValue);
				if (num4 > (int)this.MAX_7_8_BIT)
				{
					if (num >= num2 - 1)
					{
						break;
					}
					num4 = (((int)bytes[num] << 8 & 65280) | (int)(bytes[num + 1] & byte.MaxValue));
					num++;
				}
				int num5 = num4 >> 8 & 255;
				int num6 = num4 & 255;
				int num7;
				if (this.m_ucsCharLevel1[num5] != '￿' && this.m_ucsCharLevel2[(int)this.m_ucsCharLevel1[num5] + num6] != 65535)
				{
					num7 = this.m_ucsCharLevel2[(int)this.m_ucsCharLevel1[num5] + num6];
				}
				else
				{
					num7 = this.m_ucsCharReplacement;
				}
				if (((long)num7 & (long)((ulong)-1)) > 65535L)
				{
					num3 += 2;
				}
				else
				{
					num3++;
				}
				num++;
			}
			charCount = num3;
			return num - offset;
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x00076C6C File Offset: 0x00074E6C
		public override int GetBytesOffset(byte[] bytes, int byteOffset, int byteCount, int charCount)
		{
			int num = charCount;
			return this.GetBytesOffsetImpl(bytes, byteOffset, byteCount, ref num);
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x00076C88 File Offset: 0x00074E88
		public override int GetCharsLength(ArraySegment<byte> bytes, int bytesOffset, int bytesCount)
		{
			int num = 0;
			return this.GetCharsLengthImpl(bytes.Array, bytes.Offset + bytesOffset, bytesCount, ref num);
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x00076CB0 File Offset: 0x00074EB0
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
				int charsLengthImpl = this.GetCharsLengthImpl(bytes[num6].Array, num7, num8, ref num2);
				num += charsLengthImpl;
				num3 += num2;
				if (num2 < num8 && !flag && num6 < bytes.Count - 1)
				{
					if (array == null)
					{
						array = new byte[2];
					}
					byte[] array2 = bytes[num6 + 1].Array;
					int num9 = num7 + num2;
					array[0] = bytes[num6].Array[num9];
					array[1] = array2[bytes[num6 + 1].Offset];
					num4 = 1;
					int count = 2;
					charsLengthImpl = this.GetCharsLengthImpl(array, 0, count, ref num2);
					num += charsLengthImpl;
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

		// Token: 0x06000AB4 RID: 2740 RVA: 0x00076E40 File Offset: 0x00075040
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
				int bytesOffsetImpl = this.GetBytesOffsetImpl(array2, num6, num7, ref num);
				num3 += bytesOffsetImpl;
				num2 += num;
				num = charCount - num2;
				if (num > 0 && bytesOffsetImpl < num7 && num5 < bytes.Count - 1)
				{
					if (array == null)
					{
						array = new byte[2];
					}
					byte[] array3 = bytes[num5 + 1].Array;
					int num8 = num6 + bytesOffsetImpl;
					array[0] = array2[num8];
					array[1] = array3[bytes[num5 + 1].Offset];
					num4 = 1;
					int count = 2;
					bytesOffsetImpl = this.GetBytesOffsetImpl(array, 0, count, ref num);
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

		// Token: 0x06000AB5 RID: 2741 RVA: 0x00076F60 File Offset: 0x00075160
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
				bool flag = false;
				int num5 = (int)(bytes[num] & byte.MaxValue);
				if (num5 > (int)this.MAX_7_8_BIT)
				{
					if (num < num2 - 1)
					{
						num5 = (((int)bytes[num] << 8 & 65280) | (int)(bytes[num + 1] & byte.MaxValue));
						num++;
						flag = true;
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
				int num6 = num5 >> 8 & 255;
				int num7 = num5 & 255;
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
					if (num3 + 1 >= num4)
					{
						if (flag)
						{
							num--;
							break;
						}
						break;
					}
					else
					{
						chars[num3++] = (char)(num8 >> 16);
						chars[num3++] = (char)(num8 & 65535);
					}
				}
				else
				{
					chars[num3++] = (char)num8;
				}
				num++;
			}
			charCount = num3 - charOffset;
			return num - offset;
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x000770CC File Offset: 0x000752CC
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
				bool flag = false;
				int num5 = (int)(bytes[num] & byte.MaxValue);
				if (num5 > (int)this.MAX_7_8_BIT)
				{
					if (num < num2 - 1)
					{
						num5 = (((int)bytes[num] << 8 & 65280) | (int)(bytes[num + 1] & byte.MaxValue));
						num++;
						flag = true;
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
				int num6 = num5 >> 8 & 255;
				int num7 = num5 & 255;
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
					if (num3 + 3 >= num4)
					{
						if (flag)
						{
							num--;
							break;
						}
						break;
					}
					else
					{
						char[] array = new char[]
						{
							(char)(num8 >> 16),
							(char)(num8 & 65535)
						};
						utfbytes[num3++] = (byte)(array[0] & 'ÿ');
						utfbytes[num3++] = (byte)(array[0] >> 8);
						utfbytes[num3++] = (byte)(array[1] & 'ÿ');
						utfbytes[num3++] = (byte)(array[1] >> 8);
					}
				}
				else
				{
					utfbytes[num3++] = (byte)((ushort)num8 & 255);
					utfbytes[num3++] = (byte)((ushort)num8 >> 8);
				}
				num++;
			}
			utfCount = num3 - utfOffset;
			return num - offset;
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x000772A4 File Offset: 0x000754A4
		public override int ConvertCharsToBytes(char[] chars, int chars_offset, int chars_count, byte[] bytes, int bytes_begin, ref int bytes_count, bool bUseReplacementChar)
		{
			int num = chars_offset;
			int num2 = chars_offset + chars_count;
			char c = char.MaxValue;
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
				if (chars[num5] >= '\ud800' && chars[num5] < '\udc00')
				{
					if (num5 + 1 < num2 && chars[num5 + 1] >= '\udc00' && chars[num5 + 1] <= '\udfff')
					{
						c = this.ToOracleCharacter(chars[num5], chars[num5 + 1], bUseReplacementChar);
						flag = true;
						num5++;
						goto IL_163;
					}
					if (num4 + 1 >= num3)
					{
						break;
					}
					bytes[num4++] = (byte)(this.m_2ByteOraCharReplacement[0] >> 8);
					bytes[num4++] = (byte)this.m_2ByteOraCharReplacement[0];
				}
				else
				{
					int num6 = (int)(chars[num5] >> 8 & 'ÿ');
					int num7 = (int)(chars[num5] & 'ÿ');
					if (this.m_oraCharLevel1[num6] != 65535 && this.m_oraCharLevel2[this.m_oraCharLevel1[num6] + num7] != '￿')
					{
						c = this.m_oraCharLevel2[this.m_oraCharLevel1[num6] + num7];
					}
					if (c != '￿')
					{
						goto IL_163;
					}
					if (!bUseReplacementChar)
					{
						throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_BYTES", new string[0]));
					}
					if (chars[num5] > '⿿')
					{
						c = this.m_2ByteOraCharReplacement[0];
						goto IL_163;
					}
					c = this.m_1ByteOraCharReplacement;
					goto IL_163;
				}
				IL_1A2:
				num5++;
				continue;
				IL_163:
				if (c >> 8 == '\0')
				{
					bytes[num4++] = (byte)c;
					goto IL_1A2;
				}
				if (num4 + 1 < num3)
				{
					bytes[num4++] = (byte)(c >> 8);
					bytes[num4++] = (byte)c;
					goto IL_1A2;
				}
				if (flag)
				{
					num5--;
					break;
				}
				break;
			}
			bytes_count = num4 - bytes_begin;
			return num5 - num;
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x00077470 File Offset: 0x00075670
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
			char c = char.MaxValue;
			int num4 = byteOffset;
			int num5 = num;
			while (num5 < num2 - 1 && num4 < num3)
			{
				bool flag = false;
				int num6 = (int)utf16Bytes[num5 + 1] << 8 | (int)utf16Bytes[num5];
				if (num6 >= 55296 && num6 < 56320)
				{
					if (num5 + 3 < num2)
					{
						int num7 = (int)utf16Bytes[num5 + 3] << 8 | (int)utf16Bytes[num5 + 2];
						if (num7 >= 56320 && num7 <= 57343)
						{
							c = this.ToOracleCharacter((char)num6, (char)num7, bUseReplacementChar);
							flag = true;
							num5 += 2;
							goto IL_183;
						}
						goto IL_183;
					}
					else
					{
						if (num4 + 1 >= num3)
						{
							break;
						}
						bytes[num4++] = (byte)(this.m_2ByteOraCharReplacement[0] >> 8);
						bytes[num4++] = (byte)this.m_2ByteOraCharReplacement[0];
					}
				}
				else
				{
					int num8 = (ushort)num6 >> 8 & 255;
					int num9 = (int)((ushort)num6 & 255);
					if (this.m_oraCharLevel1[num8] != 65535 && this.m_oraCharLevel2[this.m_oraCharLevel1[num8] + num9] != '￿')
					{
						c = this.m_oraCharLevel2[this.m_oraCharLevel1[num8] + num9];
					}
					if (c != '￿')
					{
						goto IL_183;
					}
					if (!bUseReplacementChar)
					{
						throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_BYTES", new string[0]));
					}
					if ((ushort)num6 > 12287)
					{
						c = this.m_2ByteOraCharReplacement[0];
						goto IL_183;
					}
					c = this.m_1ByteOraCharReplacement;
					goto IL_183;
				}
				IL_1C6:
				num5 += 2;
				continue;
				IL_183:
				if (c >> 8 == '\0')
				{
					bytes[num4++] = (byte)c;
					goto IL_1C6;
				}
				if (num4 + 1 < num3)
				{
					bytes[num4++] = (byte)(c >> 8);
					bytes[num4++] = (byte)c;
					goto IL_1C6;
				}
				if (flag)
				{
					num5 -= 2;
					break;
				}
				break;
			}
			byteCount = num4 - byteOffset;
			return num5 - num;
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x00077664 File Offset: 0x00075864
		public override int ConvertStringToBytes(string chars, int chars_offset, int chars_count, byte[] bytes, int bytes_begin, ref int bytes_count, bool bUseReplacementChar)
		{
			int num = chars_offset;
			int num2 = chars_offset + chars_count;
			char c = char.MaxValue;
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
				if (chars[num5] >= '\ud800' && chars[num5] < '\udc00')
				{
					if (num5 + 1 < num2 && chars[num5 + 1] >= '\udc00' && chars[num5 + 1] <= '\udfff')
					{
						c = this.ToOracleCharacter(chars[num5], chars[num5 + 1], bUseReplacementChar);
						flag = true;
						num5++;
						goto IL_193;
					}
					if (num4 + 1 >= num3)
					{
						break;
					}
					bytes[num4++] = (byte)(this.m_2ByteOraCharReplacement[0] >> 8);
					bytes[num4++] = (byte)this.m_2ByteOraCharReplacement[0];
				}
				else
				{
					int num6 = (int)(chars[num5] >> 8 & 'ÿ');
					int num7 = (int)(chars[num5] & 'ÿ');
					if (this.m_oraCharLevel1[num6] != 65535 && this.m_oraCharLevel2[this.m_oraCharLevel1[num6] + num7] != '￿')
					{
						c = this.m_oraCharLevel2[this.m_oraCharLevel1[num6] + num7];
					}
					if (c != '￿')
					{
						goto IL_193;
					}
					if (!bUseReplacementChar)
					{
						throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_BYTES", new string[0]));
					}
					if (chars[num5] > '⿿')
					{
						c = this.m_2ByteOraCharReplacement[0];
						goto IL_193;
					}
					c = this.m_1ByteOraCharReplacement;
					goto IL_193;
				}
				IL_1D2:
				num5++;
				continue;
				IL_193:
				if (c >> 8 == '\0')
				{
					bytes[num4++] = (byte)c;
					goto IL_1D2;
				}
				if (num4 + 1 < num3)
				{
					bytes[num4++] = (byte)(c >> 8);
					bytes[num4++] = (byte)c;
					goto IL_1D2;
				}
				if (flag)
				{
					num5--;
					break;
				}
				break;
			}
			bytes_count = num4 - bytes_begin;
			return num5 - num;
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x00077860 File Offset: 0x00075A60
		protected virtual int ConvertByteArraySegListToCharsImpl<T>(IList<ArraySegment<byte>> bytes, int bytesOffset, int bytesCount, T[] chars, int charOffset, ref int charCount, bool bUseReplacementChar, TLBConv12Byte.ConvertByteToCharsDelegate<T> t)
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
						array = new byte[2];
					}
					byte[] array2 = bytes[num7 + 1].Array;
					int num11 = num8 + num10;
					array[0] = bytes[num7].Array[num11];
					array[1] = array2[bytes[num7 + 1].Offset];
					num5 = 1;
					int byteCounts = 2;
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

		// Token: 0x06000ABB RID: 2747 RVA: 0x00077A34 File Offset: 0x00075C34
		public override int ConvertBytesToChars(IList<ArraySegment<byte>> bytes, int bytesOffset, int bytesCount, char[] chars, int charOffset, ref int charCount, bool bUseReplacementChar)
		{
			if (bytes.Count == 1)
			{
				return this.ConvertBytesToChars(bytes[0].Array, bytes[0].Offset + bytesOffset, bytesCount, chars, charOffset, ref charCount, bUseReplacementChar);
			}
			return this.ConvertByteArraySegListToCharsImpl<char>(bytes, bytesOffset, bytesCount, chars, charOffset, ref charCount, bUseReplacementChar, new TLBConv12Byte.ConvertByteToCharsDelegate<char>(this.ConvertBytesToChars));
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000ABC RID: 2748 RVA: 0x00077A98 File Offset: 0x00075C98
		public override int MinBytesPerChar
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000ABD RID: 2749 RVA: 0x00077A9C File Offset: 0x00075C9C
		public override int MaxBytesPerChar
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x04000CC8 RID: 3272
		private const int ORACHARMASK = 255;

		// Token: 0x04000CC9 RID: 3273
		private const int UCSCHARWIDTH = 16;

		// Token: 0x04000CCA RID: 3274
		private const int ORACHARWIDTH = 16;

		// Token: 0x04000CCB RID: 3275
		protected const int BYTEWIDTH = 8;

		// Token: 0x04000CCC RID: 3276
		protected short MAX_7_8_BIT = 127;

		// Token: 0x04000CCD RID: 3277
		protected static int MAXLIMIT = 42000;

		// Token: 0x04000CCE RID: 3278
		public char[] m_ucsCharLevel1;

		// Token: 0x04000CCF RID: 3279
		public int[] m_ucsCharLevel2;

		// Token: 0x04000CD0 RID: 3280
		public int m_ucsCharReplacement;

		// Token: 0x04000CD1 RID: 3281
		public int[] m_oraCharLevel1;

		// Token: 0x04000CD2 RID: 3282
		public int[] m_oraCharSurrogateLevel;

		// Token: 0x04000CD3 RID: 3283
		public char[] m_oraCharLevel2;

		// Token: 0x04000CD4 RID: 3284
		public char m_1ByteOraCharReplacement;

		// Token: 0x04000CD5 RID: 3285
		public char[] m_2ByteOraCharReplacement;

		// Token: 0x020000FD RID: 253
		// (Invoke) Token: 0x06000AC0 RID: 2752
		protected delegate int ConvertByteToCharsDelegate<T>(byte[] bytes, int byteOffsets, int byteCounts, T[] chars, int charOffset, ref int charCount, bool bUseReplacementChar);
	}
}
