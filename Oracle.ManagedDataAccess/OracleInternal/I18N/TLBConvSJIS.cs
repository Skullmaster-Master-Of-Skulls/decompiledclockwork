using System;
using System.Text;

namespace OracleInternal.I18N
{
	// Token: 0x02000107 RID: 263
	[Serializable]
	internal class TLBConvSJIS : TLBConv12Byte
	{
		// Token: 0x06000B56 RID: 2902 RVA: 0x0007F174 File Offset: 0x0007D374
		public TLBConvSJIS()
		{
			this.m_groupId = 4;
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x0007F184 File Offset: 0x0007D384
		protected override int GetCharsLengthImpl(byte[] bytes, int offset, int count, ref int bytesCounted)
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
				if (num3 > 223 || (num3 > (int)this.MAX_7_8_BIT && num3 < 161))
				{
					if (i >= num - 1)
					{
						bytesCounted = i - offset;
						break;
					}
					num3 = (((int)bytes[i] << 8 & 65280) | (int)(bytes[i + 1] & byte.MaxValue));
					i++;
				}
				int num4 = base.ToUnicode(num3, true);
				if (((long)num4 & (long)((ulong)-1)) > 65535L)
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

		// Token: 0x06000B58 RID: 2904 RVA: 0x0007F228 File Offset: 0x0007D428
		protected override int GetBytesOffsetImpl(byte[] bytes, int offset, int count, ref int charCount)
		{
			int num = offset;
			int num2 = offset + count;
			int num3 = 0;
			if (num2 > bytes.Length && num3 < charCount)
			{
				num2 = bytes.Length;
			}
			while (num < num2 && num3 < charCount)
			{
				int num4 = (int)(bytes[num] & byte.MaxValue);
				if (num4 > 223 || (num4 > (int)this.MAX_7_8_BIT && num4 < 161))
				{
					if (num >= num2 - 1)
					{
						break;
					}
					num4 = (((int)bytes[num] << 8 & 65280) | (int)(bytes[num + 1] & byte.MaxValue));
					num++;
				}
				int num5 = base.ToUnicode(num4, true);
				if (((long)num5 & (long)((ulong)-1)) > 65535L)
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

		// Token: 0x06000B59 RID: 2905 RVA: 0x0007F2D0 File Offset: 0x0007D4D0
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
				if (num5 > 223 || (num5 > (int)this.MAX_7_8_BIT && num5 < 161))
				{
					if (num < num2 - 1)
					{
						num5 = (((int)bytes[num] << 8 & 65280) | (int)(bytes[num + 1] & byte.MaxValue));
						num++;
					}
					else
					{
						if (!ccb)
						{
							throw new DecoderFallbackException("Failed to convert bytes to Unicode");
						}
						break;
					}
				}
				int num6 = base.ToUnicode(num5, ccb);
				if (((long)num6 & (long)((ulong)-1)) > 65535L)
				{
					if (num3 >= num4)
					{
						break;
					}
					chars[num3++] = (char)(num6 >> 16);
					chars[num3++] = (char)(num6 & 65535);
				}
				else
				{
					chars[num3++] = (char)num6;
				}
				num++;
			}
			charCount = num3 - charOffset;
			return num - offset;
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x0007F3D4 File Offset: 0x0007D5D4
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
				if (num5 > 223 || (num5 > (int)this.MAX_7_8_BIT && num5 < 161))
				{
					if (num < num2 - 1)
					{
						num5 = (((int)bytes[num] << 8 & 65280) | (int)(bytes[num + 1] & byte.MaxValue));
						num++;
					}
					else
					{
						if (!ccb)
						{
							throw new DecoderFallbackException("Failed to convert bytes to Unicode");
						}
						break;
					}
				}
				int num6 = base.ToUnicode(num5, ccb);
				if (((long)num6 & (long)((ulong)-1)) > 65535L)
				{
					if (num3 + 3 >= num4)
					{
						break;
					}
					char[] array = new char[]
					{
						(char)(num6 >> 16),
						(char)(num6 & 65535)
					};
					utfbytes[num3++] = (byte)(array[0] & 'ÿ');
					utfbytes[num3++] = (byte)(array[0] >> 8);
					utfbytes[num3++] = (byte)(array[1] & 'ÿ');
					utfbytes[num3++] = (byte)(array[1] >> 8);
				}
				else
				{
					utfbytes[num3++] = (byte)(num6 & 255);
					utfbytes[num3++] = (byte)(num6 >> 8);
				}
				num++;
			}
			utfCount = num3 - utfOffset;
			return num - offset;
		}

		// Token: 0x04000D15 RID: 3349
		private const short MIN_8BIT_SB = 161;

		// Token: 0x04000D16 RID: 3350
		private const short MAX_8BIT_SB = 223;
	}
}
