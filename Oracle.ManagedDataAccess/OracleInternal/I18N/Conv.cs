using System;
using System.Collections.Generic;

namespace OracleInternal.I18N
{
	// Token: 0x020000F9 RID: 249
	[Serializable]
	internal abstract class Conv
	{
		// Token: 0x06000A65 RID: 2661 RVA: 0x00075574 File Offset: 0x00073774
		protected Conv()
		{
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x0007557C File Offset: 0x0007377C
		protected Conv(int oracleId)
		{
			this.OracleId = oracleId;
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000A67 RID: 2663 RVA: 0x0007558C File Offset: 0x0007378C
		// (set) Token: 0x06000A68 RID: 2664 RVA: 0x00075594 File Offset: 0x00073794
		public int OracleId { get; set; }

		// Token: 0x06000A69 RID: 2665 RVA: 0x000755A0 File Offset: 0x000737A0
		public static Conv GetInstance(int charsetId)
		{
			Conv conv = null;
			if (Conv.UNSUPPORTED_CHARSET.Contains(charsetId))
			{
				return null;
			}
			if (Conv.s_oraCharsetCache.TryGetValue(charsetId, out conv))
			{
				return conv;
			}
			Conv result;
			lock (Conv.s_oraCharsetCache)
			{
				if (Conv.s_oraCharsetCache.TryGetValue(charsetId, out conv))
				{
					result = conv;
				}
				else
				{
					switch (charsetId)
					{
					case 870:
					case 871:
						conv = new UTF16ConvUTF8(870);
						break;
					case 872:
						conv = new UTF16ConvUTFE(872);
						break;
					case 873:
						conv = new UTF16ConvAL32UTF8(873);
						break;
					default:
						switch (charsetId)
						{
						case 2000:
							conv = new UTF16ConvAL16UTF16(2000);
							goto IL_CB;
						case 2002:
							conv = new UTF16ConvAL16UTF16LE(2002);
							goto IL_CB;
						}
						conv = TLBConv.GetGLBInstance(charsetId);
						break;
					}
					IL_CB:
					Conv.s_oraCharsetCache.Add(charsetId, conv);
					result = conv;
				}
			}
			return result;
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000A6A RID: 2666
		public abstract int MinBytesPerChar { get; }

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000A6B RID: 2667
		public abstract int MaxBytesPerChar { get; }

		// Token: 0x06000A6C RID: 2668 RVA: 0x000756A4 File Offset: 0x000738A4
		public static int GetMaxBytesPerChar(int charsetId)
		{
			switch (charsetId)
			{
			case 870:
			case 871:
				return 3;
			case 872:
				return 4;
			case 873:
				return 4;
			default:
			{
				switch (charsetId)
				{
				case 2000:
				case 2002:
					return 2;
				}
				string charsetMaxCharLen = Conv.s_bootObj.GetCharsetMaxCharLen(Convert.ToString(charsetId));
				return Convert.ToInt32(charsetMaxCharLen);
			}
			}
		}

		// Token: 0x06000A6D RID: 2669
		public abstract int ConvertBytesToChars(byte[] bytes, int byteOffset, int byteCount, char[] chars, int charOffset, ref int charCount, bool bUseReplacementChar = true);

		// Token: 0x06000A6E RID: 2670 RVA: 0x00075708 File Offset: 0x00073908
		internal virtual int ConvertBytesToChars(byte[] bytes, int byteOffset, int byteCount, char[] chars, int charOffset, ref int charCount, ref bool shiftIn, bool bUseReplacementChar = true)
		{
			return 0;
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x0007570C File Offset: 0x0007390C
		public int ConvertBytesToChars(ArraySegment<byte> bytes, char[] chars, int charOffset, ref int charCount, bool bUseReplacementChar = true)
		{
			return this.ConvertBytesToChars(bytes.Array, bytes.Offset, bytes.Count, chars, charOffset, ref charCount, bUseReplacementChar);
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x00075730 File Offset: 0x00073930
		public int ConvertBytesToChars(ArraySegment<byte> bytes, int bytesOffset, int bytesCount, char[] chars, int charOffset, ref int charCount, bool bUseReplacementChar = true)
		{
			return this.ConvertBytesToChars(bytes.Array, bytes.Offset + bytesOffset, bytesCount, chars, charOffset, ref charCount, bUseReplacementChar);
		}

		// Token: 0x06000A71 RID: 2673
		public abstract int ConvertBytesToChars(IList<ArraySegment<byte>> bytes, int bytesOffset, int bytesCount, char[] chars, int charOffset, ref int charCount, bool bUseReplacementChar = true);

		// Token: 0x06000A72 RID: 2674 RVA: 0x00075754 File Offset: 0x00073954
		internal virtual int ConvertBytesToChars(IList<ArraySegment<byte>> bytes, int bytesOffset, int bytesCount, char[] chars, int charOffset, ref int charCount, ref bool shiftIn, bool bUseReplacementChar = true)
		{
			return 0;
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x00075758 File Offset: 0x00073958
		public int ConvertBytesToChars(IList<ArraySegment<byte>> bytes, char[] chars, int charOffset, ref int charCount, bool bUseReplacementChar = true)
		{
			int num = 0;
			for (int i = 0; i < bytes.Count; i++)
			{
				num += bytes[i].Count;
			}
			return this.ConvertBytesToChars(bytes, 0, num, chars, charOffset, ref charCount, bUseReplacementChar);
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x0007579C File Offset: 0x0007399C
		public string ConvertBytesToString(byte[] bytes, int byteOffset, int byteCount, char[] chars = null, bool bUseReplacementChar = true)
		{
			int num = 0;
			string text = string.Empty;
			bool flag = true;
			int num2;
			if (chars == null)
			{
				num2 = Math.Min(byteCount / this.MinBytesPerChar, 32768);
				chars = new char[num2];
			}
			else
			{
				num2 = chars.Length;
			}
			while (byteCount - num != 0)
			{
				int num3 = num2;
				if (this.IsShitCharset())
				{
					num += this.ConvertBytesToChars(bytes, byteOffset + num, byteCount - num, chars, 0, ref num3, ref flag, bUseReplacementChar);
				}
				else
				{
					num += this.ConvertBytesToChars(bytes, byteOffset + num, byteCount - num, chars, 0, ref num3, bUseReplacementChar);
				}
				text += new string(chars, 0, num3);
				if (num3 <= 0)
				{
					break;
				}
			}
			return text;
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x00075834 File Offset: 0x00073A34
		public string ConvertBytesToString(IList<ArraySegment<byte>> bytes, int bytesOffset, int bytesCount, char[] chars = null, bool bUseReplacementChar = true)
		{
			int num = 0;
			string text = string.Empty;
			bool flag = true;
			int num2;
			if (chars == null)
			{
				num2 = Math.Min(bytesCount / this.MinBytesPerChar, 32768);
				chars = new char[num2];
			}
			else
			{
				num2 = chars.Length;
			}
			while (bytesCount - num != 0)
			{
				int num3 = num2;
				if (this.IsShitCharset())
				{
					if (bytes.Count > 1)
					{
						num += this.ConvertBytesToChars(bytes, bytesOffset + num, bytesCount - num, chars, 0, ref num3, ref flag, bUseReplacementChar);
					}
					else
					{
						num += this.ConvertBytesToChars(bytes[0].Array, bytes[0].Offset + bytesOffset + num, bytesCount - num, chars, 0, ref num3, ref flag, bUseReplacementChar);
					}
				}
				else if (bytes.Count > 1)
				{
					num += this.ConvertBytesToChars(bytes, bytesOffset + num, bytesCount - num, chars, 0, ref num3, bUseReplacementChar);
				}
				else
				{
					num += this.ConvertBytesToChars(bytes[0].Array, bytes[0].Offset + bytesOffset + num, bytesCount - num, chars, 0, ref num3, bUseReplacementChar);
				}
				text += new string(chars, 0, num3);
				if (num3 <= 0)
				{
					break;
				}
			}
			return text;
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x0007595C File Offset: 0x00073B5C
		public string ConvertBytesToString(IList<ArraySegment<byte>> bytes, char[] chars = null, bool bUseReplacementChar = true)
		{
			int num = 0;
			for (int i = 0; i < bytes.Count; i++)
			{
				num += bytes[i].Count;
			}
			return this.ConvertBytesToString(bytes, 0, num, chars, true);
		}

		// Token: 0x06000A77 RID: 2679
		public abstract int ConvertBytesToUTF16(byte[] bytes, int byteOffset, int byteCount, byte[] utf16Bytes, int utf16BytesOffset, ref int utf16BytesCount, bool bUseReplacementChar = true);

		// Token: 0x06000A78 RID: 2680 RVA: 0x0007599C File Offset: 0x00073B9C
		public byte[] ConvertBytesToUTF16(byte[] bytes, int byteOffset, int byteCount, bool bUseReplacementChar = true)
		{
			int num = this.GetCharsLength(bytes, byteOffset, byteCount) * 2;
			byte[] array = new byte[num];
			this.ConvertBytesToUTF16(bytes, byteOffset, byteCount, array, 0, ref num, true);
			return array;
		}

		// Token: 0x06000A79 RID: 2681
		public abstract int ConvertCharsToBytes(char[] chars, int charOffset, int charCount, byte[] bytes, int byteOffset, ref int byteCount, bool bUseReplacementChar = true);

		// Token: 0x06000A7A RID: 2682
		public abstract int ConvertStringToBytes(string str, int strOffset, int strCount, byte[] bytes, int byteOffset, ref int byteCount, bool bUseReplacementChar = true);

		// Token: 0x06000A7B RID: 2683 RVA: 0x000759CC File Offset: 0x00073BCC
		public byte[] ConvertCharsToBytes(char[] chars, int charOffset, int charCount, bool bUseReplacementChar = true)
		{
			int bytesLength = this.GetBytesLength(chars, charOffset, charCount);
			byte[] array = new byte[bytesLength];
			this.ConvertCharsToBytes(chars, charOffset, charCount, array, 0, ref bytesLength, bUseReplacementChar);
			return array;
		}

		// Token: 0x06000A7C RID: 2684
		public abstract int ConvertUTF16ToBytes(byte[] utf16Bytes, int utf16BytesOffset, int utf16BytesCount, byte[] bytes, int byteOffset, ref int byteCount, bool bUseReplacementChar = true);

		// Token: 0x06000A7D RID: 2685 RVA: 0x000759FC File Offset: 0x00073BFC
		public byte[] ConvertUTF16ToBytes(byte[] utf16Bytes, int utf16BytesOffset, int utf16BytesCount, bool bUseReplacementChar = true)
		{
			int bytesLength = this.GetBytesLength(utf16Bytes, utf16BytesOffset, utf16BytesCount);
			byte[] array = new byte[bytesLength];
			this.ConvertUTF16ToBytes(utf16Bytes, utf16BytesOffset, utf16BytesCount, array, 0, ref bytesLength, bUseReplacementChar);
			return array;
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x00075A2C File Offset: 0x00073C2C
		public byte[] ConvertStringToBytes(string str, int strOffset, int strCount, bool bUseReplacementChar = true)
		{
			int bytesLength = this.GetBytesLength(str, strOffset, strCount);
			byte[] array = new byte[bytesLength];
			this.ConvertStringToBytes(str, strOffset, strCount, array, 0, ref bytesLength, bUseReplacementChar);
			return array;
		}

		// Token: 0x06000A7F RID: 2687
		public abstract int GetCharsLength(byte[] bytes, int byteOffset, int byteCount);

		// Token: 0x06000A80 RID: 2688
		public abstract int GetCharsLength(ArraySegment<byte> bytes, int bytesOffset, int bytesCount);

		// Token: 0x06000A81 RID: 2689 RVA: 0x00075A5C File Offset: 0x00073C5C
		public int GetCharsLength(ArraySegment<byte> bytes)
		{
			return this.GetCharsLength(bytes, 0, bytes.Count);
		}

		// Token: 0x06000A82 RID: 2690
		public abstract int GetCharsLength(IList<ArraySegment<byte>> bytes, int bytesOffset, int bytesCount);

		// Token: 0x06000A83 RID: 2691 RVA: 0x00075A70 File Offset: 0x00073C70
		public int GetCharsLength(IList<ArraySegment<byte>> bytes)
		{
			int num = 0;
			for (int i = 0; i < bytes.Count; i++)
			{
				num += bytes[i].Count;
			}
			return this.GetCharsLength(bytes, 0, num);
		}

		// Token: 0x06000A84 RID: 2692
		public abstract int GetBytesLength(char[] chars, int charOffset, int charCount);

		// Token: 0x06000A85 RID: 2693
		public abstract int GetBytesLength(string str, int strOffset, int strCount);

		// Token: 0x06000A86 RID: 2694
		public abstract int GetBytesLength(byte[] utf16Bytes, int utf16BytesOffset, int utf16BytesCount);

		// Token: 0x06000A87 RID: 2695
		public abstract int GetBytesOffset(byte[] bytes, int byteOffset, int byteCount, int charCount);

		// Token: 0x06000A88 RID: 2696
		public abstract int GetBytesOffset(IList<ArraySegment<byte>> bytes, int charCount);

		// Token: 0x06000A89 RID: 2697 RVA: 0x00075AAC File Offset: 0x00073CAC
		internal virtual bool IsShitCharset()
		{
			return false;
		}

		// Token: 0x04000C86 RID: 3206
		public const int CONCAIN_CHARSET_TABLE = 2;

		// Token: 0x04000C87 RID: 3207
		public const int AL16UTF16_CHARSET = 2000;

		// Token: 0x04000C88 RID: 3208
		public const int AL16UTF16LE_CHARSET = 2002;

		// Token: 0x04000C89 RID: 3209
		public const int UNICODE_1_CHARSET = 870;

		// Token: 0x04000C8A RID: 3210
		public const int UNICODE_2_CHARSET = 871;

		// Token: 0x04000C8B RID: 3211
		public const int UTFE_CHARSET = 872;

		// Token: 0x04000C8C RID: 3212
		public const int AL32UTF8_CHARSET = 873;

		// Token: 0x04000C8D RID: 3213
		public const int ISO2022JP_CHARSET = 9999;

		// Token: 0x04000C8E RID: 3214
		public const int ISO2022_JP_OUTLOOK_CHARSET = 9994;

		// Token: 0x04000C8F RID: 3215
		public const int ISO2022_JP_OUTLOOK_HWKANA_CHARSET = 9995;

		// Token: 0x04000C90 RID: 3216
		public const char UTF16_REPLACEMENT_CHAR = '�';

		// Token: 0x04000C91 RID: 3217
		public const byte UTF16_REPLACEMENT_HIGH_BYTE = 255;

		// Token: 0x04000C92 RID: 3218
		public const byte UTF16_REPLACEMENT_LOW_BYTE = 253;

		// Token: 0x04000C93 RID: 3219
		public const int WE8DECTST = 798;

		// Token: 0x04000C94 RID: 3220
		public const int ZHT32EUCTST = 993;

		// Token: 0x04000C95 RID: 3221
		public const int WE16DECTST2 = 994;

		// Token: 0x04000C96 RID: 3222
		public const int WE16DECTST = 995;

		// Token: 0x04000C97 RID: 3223
		public const int KO16TSTSET = 996;

		// Token: 0x04000C98 RID: 3224
		public const int JA16TSTSET2 = 997;

		// Token: 0x04000C99 RID: 3225
		public const int JA16TSTSET = 998;

		// Token: 0x04000C9A RID: 3226
		public const int US16TSTFIXED = 1001;

		// Token: 0x04000C9B RID: 3227
		public const int UTF16 = 1000;

		// Token: 0x04000C9C RID: 3228
		public const int HZ_GB_2312 = 9996;

		// Token: 0x04000C9D RID: 3229
		public const int ISO2022_KR = 9997;

		// Token: 0x04000C9E RID: 3230
		public const int ISO2022_CN = 9998;

		// Token: 0x04000C9F RID: 3231
		public const bool USE_REPLACEMENT = true;

		// Token: 0x04000CA0 RID: 3232
		private const int s_charsRequestedLength = 32768;

		// Token: 0x04000CA1 RID: 3233
		protected static readonly byte[] REP_CHAR_UTF8 = new byte[]
		{
			239,
			191,
			189
		};

		// Token: 0x04000CA2 RID: 3234
		private static Dictionary<int, Conv> s_oraCharsetCache = new Dictionary<int, Conv>();

		// Token: 0x04000CA3 RID: 3235
		private static readonly TLBConvBoot s_bootObj = TLBConvBoot.GetInstance();

		// Token: 0x04000CA4 RID: 3236
		private static readonly HashSet<int> UNSUPPORTED_CHARSET = new HashSet<int>
		{
			798,
			993,
			994,
			995,
			996,
			997,
			998,
			1001,
			1000,
			9996,
			9997,
			9998
		};
	}
}
