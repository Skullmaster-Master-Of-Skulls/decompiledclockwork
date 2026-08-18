using System;
using System.Collections.Generic;
using System.Text;

namespace OracleInternal.I18N
{
	// Token: 0x020000FE RID: 254
	[Serializable]
	internal class TLBConv1Byte : TLBConv
	{
		// Token: 0x06000AC3 RID: 2755 RVA: 0x00077AAC File Offset: 0x00075CAC
		public TLBConv1Byte()
		{
			this.m_groupId = 0;
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x00077ACC File Offset: 0x00075CCC
		private int ByteToChar(byte srcChar, bool useReplacement)
		{
			int num = this.m_ucsChar[(int)(srcChar & byte.MaxValue)];
			if (num != 65535)
			{
				return num;
			}
			if (!useReplacement)
			{
				throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_UNICODE", new string[0]));
			}
			return (int)this.m_oraCharReplacement;
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x00077B14 File Offset: 0x00075D14
		private byte CharToByte(char srcChar, char lowSurrogate, bool useReplacement)
		{
			int num = (int)(srcChar >> 8 & 'ÿ');
			int num2 = (int)(srcChar & 'ÿ');
			int num3 = (int)(lowSurrogate >> 8 & 'ÿ');
			int num4 = (int)(lowSurrogate & 'ÿ');
			if (this.m_oraCharLevel1[num] != (char)this.m_oraCharLevel2Size && this.m_oraCharSurrogateLevel[(int)this.m_oraCharLevel1[num] + num2] != '￿' && this.m_oraCharSurrogateLevel[(int)this.m_oraCharSurrogateLevel[(int)this.m_oraCharLevel1[num] + num2] + num3] != '￿' && this.m_oraCharLevel2[(int)this.m_oraCharSurrogateLevel[(int)this.m_oraCharSurrogateLevel[(int)this.m_oraCharLevel1[num] + num2] + num3] + num4] != '￿')
			{
				return (byte)this.m_oraCharLevel2[(int)this.m_oraCharSurrogateLevel[(int)this.m_oraCharSurrogateLevel[(int)this.m_oraCharLevel1[num] + num2] + num3] + num4];
			}
			if (useReplacement)
			{
				return this.m_oraCharReplacement;
			}
			throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_BYTES", new string[0]));
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x00077C00 File Offset: 0x00075E00
		private byte CharToByte(char srcChar, bool useReplacement)
		{
			int num = (int)(srcChar >> 8);
			int num2 = (int)(srcChar & 'ÿ');
			char c;
			if ((c = this.m_oraCharLevel2[(int)this.m_oraCharLevel1[num] + num2]) != '￿')
			{
				return (byte)c;
			}
			if (useReplacement)
			{
				return this.m_oraCharReplacement;
			}
			throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_BYTES", new string[0]));
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x00077C58 File Offset: 0x00075E58
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
				int num5 = this.m_ucsChar[(int)(bytes[num++] & byte.MaxValue)];
				if (num5 == 65535)
				{
					if (!ccb)
					{
						throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_UNICODE", new string[0]));
					}
					chars[num3++] = (char)this.m_ucsReplacement;
				}
				else
				{
					chars[num3++] = (char)num5;
				}
			}
			charCount = num3 - charOffset;
			return num - offset;
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x00077D00 File Offset: 0x00075F00
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
				int num5 = this.m_ucsChar[(int)(bytes[num++] & byte.MaxValue)];
				if (num5 == 65535)
				{
					if (!ccb)
					{
						throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_UNICODE", new string[0]));
					}
					utfbytes[num3++] = (byte)((ushort)this.m_ucsReplacement & 255);
					utfbytes[num3++] = (byte)((ushort)this.m_ucsReplacement >> 8);
				}
				else
				{
					utfbytes[num3++] = (byte)((ushort)num5 & 255);
					utfbytes[num3++] = (byte)((ushort)num5 >> 8);
				}
			}
			utfCount = num3 - utfOffset;
			return num - offset;
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x00077DE0 File Offset: 0x00075FE0
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
				if (chars[num5] >= '\ud800' && chars[num5] < '\udc00')
				{
					if (num5 + 1 < num2 && chars[num5 + 1] >= '\udc00' && chars[num5 + 1] <= '\udfff')
					{
						if (this.noSurrogate)
						{
							if (!ccb)
							{
								throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_BYTES", new string[0]));
							}
							bytes[num4++] = this.m_oraCharReplacement;
						}
						else
						{
							bytes[num4++] = this.CharToByte(chars[num5], chars[num5 + 1], ccb);
						}
						num5++;
					}
					else
					{
						if (!ccb)
						{
							throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_BYTES", new string[0]));
						}
						bytes[num4++] = this.m_oraCharReplacement;
					}
				}
				else if (chars[num5] < '\u0080' && this.strictASCII)
				{
					bytes[num4++] = (byte)chars[num5];
				}
				else
				{
					int num6 = (int)(chars[num5] >> 8);
					int num7 = (int)(chars[num5] & 'ÿ');
					char c;
					if ((c = this.m_oraCharLevel2[(int)this.m_oraCharLevel1[num6] + num7]) != '￿')
					{
						bytes[num4++] = (byte)c;
					}
					else
					{
						if (!ccb)
						{
							throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_BYTES", new string[0]));
						}
						bytes[num4++] = this.m_oraCharReplacement;
					}
				}
				num5++;
			}
			bytes_count = num4 - bytes_begin;
			return num5 - num;
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x00077F90 File Offset: 0x00076190
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
				if (chars[num5] >= '\ud800' && chars[num5] < '\udc00')
				{
					if (num5 + 1 < num2 && chars[num5 + 1] >= '\udc00' && chars[num5 + 1] <= '\udfff')
					{
						if (this.noSurrogate)
						{
							if (!ccb)
							{
								throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_BYTES", new string[0]));
							}
							bytes[num4++] = this.m_oraCharReplacement;
						}
						else
						{
							bytes[num4++] = this.CharToByte(chars[num5], chars[num5 + 1], ccb);
						}
						num5++;
					}
					else
					{
						if (!ccb)
						{
							throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_BYTES", new string[0]));
						}
						bytes[num4++] = this.m_oraCharReplacement;
					}
				}
				else if (chars[num5] < '\u0080' && this.strictASCII)
				{
					bytes[num4++] = (byte)chars[num5];
				}
				else
				{
					int num6 = (int)(chars[num5] >> 8);
					int num7 = (int)(chars[num5] & 'ÿ');
					char c;
					if ((c = this.m_oraCharLevel2[(int)this.m_oraCharLevel1[num6] + num7]) != '￿')
					{
						bytes[num4++] = (byte)c;
					}
					else
					{
						if (!ccb)
						{
							throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_BYTES", new string[0]));
						}
						bytes[num4++] = this.m_oraCharReplacement;
					}
				}
				num5++;
			}
			bytes_count = num4 - bytes_begin;
			return num5 - num;
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x00078170 File Offset: 0x00076370
		public override int ConvertUTF16ToBytes(byte[] utf16Bytes, int utf16BytesOffset, int utf16BytesCount, byte[] bytes, int byteOffset, ref int byteCount, bool ccb)
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
				char c = (char)((int)utf16Bytes[num5 + 1] << 8 | (int)utf16Bytes[num5]);
				if (c >= '\ud800' && c < '\udc00')
				{
					if (num5 + 3 < num2)
					{
						char c2 = (char)((int)utf16Bytes[num5 + 3] << 8 | (int)utf16Bytes[num5 + 2]);
						if (c2 >= '\udc00' && c2 <= '\udfff')
						{
							if (this.noSurrogate)
							{
								if (!ccb)
								{
									throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_BYTES", new string[0]));
								}
								bytes[num4++] = this.m_oraCharReplacement;
							}
							else
							{
								bytes[num4++] = this.CharToByte(c, c2, ccb);
							}
							num5 += 2;
						}
						else
						{
							if (!ccb)
							{
								throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_BYTES", new string[0]));
							}
							bytes[num4++] = this.m_oraCharReplacement;
						}
					}
				}
				else if (c < '\u0080' && this.strictASCII)
				{
					bytes[num4++] = (byte)c;
				}
				else
				{
					int num6 = (int)(c >> 8);
					int num7 = (int)(c & 'ÿ');
					char c3;
					if ((c3 = this.m_oraCharLevel2[(int)this.m_oraCharLevel1[num6] + num7]) != '￿')
					{
						bytes[num4++] = (byte)c3;
					}
					else
					{
						if (!ccb)
						{
							throw new DecoderFallbackException(I18nStringResourceManager.GetErrorMesg("FAIL_CONV_TO_BYTES", new string[0]));
						}
						bytes[num4++] = this.m_oraCharReplacement;
					}
				}
				num5 += 2;
			}
			byteCount = num4 - byteOffset;
			return num5 - num;
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x00078334 File Offset: 0x00076534
		public override bool IsOraCharacterReplacement(char ch, char lowsur)
		{
			if (lowsur != '\0')
			{
				return this.CharToByte(ch, lowsur, true) == this.m_oraCharReplacement;
			}
			return this.CharToByte(ch, true) == this.m_oraCharReplacement;
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x0007835C File Offset: 0x0007655C
		public override void BuildUnicodeToOracleMapping()
		{
			this.m_oraCharLevel1 = new char[256];
			this.m_oraCharSurrogateLevel = null;
			this.m_oraCharLevel2 = null;
			IList<int[]> list = new List<int[]>(45055);
			Dictionary<int, char[]> dictionary = new Dictionary<int, char[]>();
			Dictionary<int, char[]> dictionary2 = new Dictionary<int, char[]>();
			int num = this.m_ucsChar.Length;
			char c = '\0';
			char c2 = '\0';
			for (int i = 0; i < 256; i++)
			{
				this.m_oraCharLevel1[i] = char.MaxValue;
			}
			for (int i = 0; i < num; i++)
			{
				int num2 = this.m_ucsChar[i];
				if (num2 != 65535 && num2 != this.m_ucsReplacement)
				{
					list.Add(new int[]
					{
						num2,
						i
					});
					base.StoreMappingRange(num2, dictionary, dictionary2);
				}
			}
			if (this.extraUnicodeToOracleMapping != null)
			{
				num = this.extraUnicodeToOracleMapping.Length;
				for (int i = 0; i < num; i++)
				{
					int num2 = this.extraUnicodeToOracleMapping[i][0];
					if (num2 != this.m_ucsReplacement)
					{
						base.StoreMappingRange(num2, dictionary, dictionary2);
					}
				}
			}
			int num3 = 0;
			int num4 = 0;
			foreach (KeyValuePair<int, char[]> keyValuePair in dictionary)
			{
				char[] value = keyValuePair.Value;
				if (value != null)
				{
					num3 += 256;
				}
			}
			foreach (KeyValuePair<int, char[]> keyValuePair2 in dictionary2)
			{
				char[] value = keyValuePair2.Value;
				if (value != null)
				{
					num4 += 256;
				}
			}
			if (num3 != 0)
			{
				this.m_oraCharSurrogateLevel = new char[num3];
			}
			if (num4 != 0)
			{
				this.m_oraCharLevel2 = new char[num4 + 256];
			}
			for (int i = 0; i < num3; i++)
			{
				this.m_oraCharSurrogateLevel[i] = char.MaxValue;
			}
			for (int i = 0; i < num4 + 256; i++)
			{
				this.m_oraCharLevel2[i] = char.MaxValue;
			}
			for (int i = 0; i < list.Count; i++)
			{
				int[] array = list[i];
				if (array[0] != this.m_ucsReplacement)
				{
					int num5 = array[0] >> 24 & 255;
					int num6 = array[0] >> 16 & 255;
					int num7 = array[0] >> 8 & 255;
					int num8 = array[0] & 255;
					if (num5 >= 216 && num5 < 220)
					{
						if (this.m_oraCharLevel1[num5] == '￿')
						{
							this.m_oraCharLevel1[num5] = c2;
							c2 += 'Ā';
						}
						if (this.m_oraCharSurrogateLevel[(int)this.m_oraCharLevel1[num5] + num6] == '￿')
						{
							this.m_oraCharSurrogateLevel[(int)this.m_oraCharLevel1[num5] + num6] = c2;
							c2 += 'Ā';
						}
						if (this.m_oraCharSurrogateLevel[(int)this.m_oraCharSurrogateLevel[(int)this.m_oraCharLevel1[num5] + num6] + num7] == '￿')
						{
							this.m_oraCharSurrogateLevel[(int)this.m_oraCharSurrogateLevel[(int)this.m_oraCharLevel1[num5] + num6] + num7] = c;
							c += 'Ā';
						}
						if (this.m_oraCharLevel2[(int)this.m_oraCharSurrogateLevel[(int)this.m_oraCharSurrogateLevel[(int)this.m_oraCharLevel1[num5] + num6] + num7] + num8] == '￿')
						{
							this.m_oraCharLevel2[(int)this.m_oraCharSurrogateLevel[(int)this.m_oraCharSurrogateLevel[(int)this.m_oraCharLevel1[num5] + num6] + num7] + num8] = (char)(array[1] & 65535);
						}
					}
					else
					{
						if (this.m_oraCharLevel1[num7] == '￿')
						{
							this.m_oraCharLevel1[num7] = c;
							c += 'Ā';
						}
						if (this.m_oraCharLevel2[(int)this.m_oraCharLevel1[num7] + num8] == '￿')
						{
							this.m_oraCharLevel2[(int)this.m_oraCharLevel1[num7] + num8] = (char)(array[1] & 65535);
						}
					}
				}
			}
			if (this.extraUnicodeToOracleMapping != null)
			{
				num = this.extraUnicodeToOracleMapping.Length;
				for (int i = 0; i < num; i++)
				{
					int num2 = this.extraUnicodeToOracleMapping[i][0];
					if (num2 != this.m_ucsReplacement)
					{
						int num5 = num2 >> 24 & 255;
						int num6 = num2 >> 16 & 255;
						int num7 = num2 >> 8 & 255;
						int num8 = num2 & 255;
						if (num5 >= 216 && num5 < 220)
						{
							if (this.m_oraCharLevel1[num5] == '￿')
							{
								this.m_oraCharLevel1[num5] = c2;
								c2 += 'Ā';
							}
							if (this.m_oraCharSurrogateLevel[(int)this.m_oraCharLevel1[num5] + num6] == '￿')
							{
								this.m_oraCharSurrogateLevel[(int)this.m_oraCharLevel1[num5] + num6] = c2;
								c2 += 'Ā';
							}
							if (this.m_oraCharSurrogateLevel[(int)this.m_oraCharSurrogateLevel[(int)this.m_oraCharLevel1[num5] + num6] + num7] == '￿')
							{
								this.m_oraCharSurrogateLevel[(int)this.m_oraCharSurrogateLevel[(int)this.m_oraCharLevel1[num5] + num6] + num7] = c;
								c += 'Ā';
							}
							this.m_oraCharLevel2[(int)this.m_oraCharSurrogateLevel[(int)this.m_oraCharSurrogateLevel[(int)this.m_oraCharLevel1[num5] + num6] + num7] + num8] = (char)(this.extraUnicodeToOracleMapping[i][1] & 255);
						}
						else
						{
							if (this.m_oraCharLevel1[num7] == '￿')
							{
								this.m_oraCharLevel1[num7] = c;
								c += 'Ā';
							}
							this.m_oraCharLevel2[(int)this.m_oraCharLevel1[num7] + num8] = (char)(this.extraUnicodeToOracleMapping[i][1] & 65535);
						}
					}
				}
			}
			if (this.m_oraCharSurrogateLevel == null)
			{
				this.noSurrogate = true;
			}
			else
			{
				this.noSurrogate = false;
			}
			this.strictASCII = true;
			for (int i = 0; i < 128; i++)
			{
				if ((int)this.m_oraCharLevel2[i] != i)
				{
					this.strictASCII = false;
					break;
				}
			}
			for (int i = 0; i < 256; i++)
			{
				if (this.m_oraCharLevel1[i] == '￿')
				{
					this.m_oraCharLevel1[i] = (char)num4;
				}
			}
			this.m_oraCharLevel2Size = num4;
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x000789A0 File Offset: 0x00076BA0
		public override void ExtractCodepoints(IList<int[]> vtable)
		{
			int num = 0;
			int num2 = 255;
			for (int i = num; i <= num2; i++)
			{
				try
				{
					vtable.Add(new int[]
					{
						i,
						this.ByteToChar((byte)i, true)
					});
				}
				catch
				{
				}
			}
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x000789F4 File Offset: 0x00076BF4
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

		// Token: 0x06000AD0 RID: 2768 RVA: 0x00078A48 File Offset: 0x00076C48
		public override bool HasExtraMappings()
		{
			return this.extraUnicodeToOracleMapping != null;
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x00078A58 File Offset: 0x00076C58
		public override char GetOraChar1ByteRep()
		{
			return (char)(this.m_oraCharReplacement & byte.MaxValue);
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x00078A68 File Offset: 0x00076C68
		public override char GetOraChar2ByteRep()
		{
			return '\0';
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x00078A6C File Offset: 0x00076C6C
		public override int GetUCS2CharRep()
		{
			return this.m_ucsReplacement;
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x00078A74 File Offset: 0x00076C74
		public override int ConvertBytesToChars(IList<ArraySegment<byte>> bytes, int offset, int count, char[] chars, int charOffset, ref int charCount, bool bUseReplacementChar)
		{
			int num = charOffset;
			int num2 = charCount;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			int offset2 = bytes[0].Offset;
			bool flag = false;
			if (offset > 0)
			{
				UTF16ConvUtility.GetSegementIndices(bytes, offset, ref num5, ref offset2);
			}
			int num6 = num5;
			while (num6 < bytes.Count && !flag && num2 > 0)
			{
				int byteOffset = bytes[num6].Offset;
				int num7 = bytes[num6].Count;
				if (num6 == num5)
				{
					byteOffset = offset2;
					num7 = bytes[num6].Count - (offset2 - bytes[num6].Offset);
				}
				if (count - num4 <= num7)
				{
					num7 = count - num4;
					flag = true;
				}
				num4 += this.ConvertBytesToChars(bytes[num6].Array, byteOffset, num7, chars, num, ref num2, bUseReplacementChar);
				num += num2;
				num3 += num2;
				num2 = charCount - num3;
				num6++;
			}
			charCount = num3;
			return num4;
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x00078B78 File Offset: 0x00076D78
		public override int GetCharsLength(byte[] bytes, int byteOffset, int byteCount)
		{
			if (byteOffset + byteCount > bytes.Length)
			{
				return bytes.Length - byteOffset;
			}
			return byteCount;
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x00078B8C File Offset: 0x00076D8C
		public override int GetCharsLength(ArraySegment<byte> bytes, int bytesOffset, int bytesCount)
		{
			if (bytesCount > bytes.Count)
			{
				return bytes.Count - bytesOffset;
			}
			return bytesCount;
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x00078BA4 File Offset: 0x00076DA4
		public override int GetCharsLength(IList<ArraySegment<byte>> bytes, int bytesOffset, int bytesCount)
		{
			return bytesCount;
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000AD8 RID: 2776 RVA: 0x00078BA8 File Offset: 0x00076DA8
		public override int MinBytesPerChar
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000AD9 RID: 2777 RVA: 0x00078BAC File Offset: 0x00076DAC
		public override int MaxBytesPerChar
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x00078BB0 File Offset: 0x00076DB0
		public override int GetBytesLength(char[] chars, int charOffset, int charCount)
		{
			if (charOffset + charCount > chars.Length)
			{
				return chars.Length - charOffset;
			}
			return charCount;
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x00078BC4 File Offset: 0x00076DC4
		public override int GetBytesLength(string str, int strOffset, int strCount)
		{
			if (strOffset + strCount > str.Length)
			{
				return str.Length - strOffset;
			}
			return strCount;
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x00078BDC File Offset: 0x00076DDC
		public override int GetBytesLength(byte[] utf16Bytes, int utf16BytesOffset, int utf16BytesCount)
		{
			if (utf16BytesOffset + utf16BytesCount > utf16Bytes.Length)
			{
				return (utf16Bytes.Length - utf16BytesOffset) / 2;
			}
			return utf16BytesCount / 2;
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x00078BF4 File Offset: 0x00076DF4
		public override int GetBytesOffset(byte[] bytes, int byteOffset, int byteCount, int charCount)
		{
			return charCount;
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x00078BF8 File Offset: 0x00076DF8
		public override int GetBytesOffset(IList<ArraySegment<byte>> bytes, int charCount)
		{
			return charCount;
		}

		// Token: 0x04000CD6 RID: 3286
		private const int ORACHARMASK = 255;

		// Token: 0x04000CD7 RID: 3287
		private const int UCSCHARWIDTH = 16;

		// Token: 0x04000CD8 RID: 3288
		public int m_ucsReplacement;

		// Token: 0x04000CD9 RID: 3289
		public int[] m_ucsChar;

		// Token: 0x04000CDA RID: 3290
		public char[] m_oraCharLevel1;

		// Token: 0x04000CDB RID: 3291
		public char[] m_oraCharSurrogateLevel;

		// Token: 0x04000CDC RID: 3292
		public char[] m_oraCharLevel2;

		// Token: 0x04000CDD RID: 3293
		public byte m_oraCharReplacement;

		// Token: 0x04000CDE RID: 3294
		protected bool noSurrogate = true;

		// Token: 0x04000CDF RID: 3295
		protected bool strictASCII = true;

		// Token: 0x04000CE0 RID: 3296
		protected int m_oraCharLevel2Size;
	}
}
