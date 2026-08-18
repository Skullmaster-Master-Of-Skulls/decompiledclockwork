using System;
using System.Collections.Generic;

namespace System.Windows.Forms
{
	// Token: 0x0200016C RID: 364
	public struct ImeModeConversion
	{
		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06001301 RID: 4865 RVA: 0x0003CE50 File Offset: 0x0003B050
		internal static ImeMode[] ChineseTable
		{
			get
			{
				return ImeModeConversion.chineseTable;
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06001302 RID: 4866 RVA: 0x0003CE57 File Offset: 0x0003B057
		internal static ImeMode[] JapaneseTable
		{
			get
			{
				return ImeModeConversion.japaneseTable;
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06001303 RID: 4867 RVA: 0x0003CE5E File Offset: 0x0003B05E
		internal static ImeMode[] KoreanTable
		{
			get
			{
				return ImeModeConversion.koreanTable;
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06001304 RID: 4868 RVA: 0x0003CE65 File Offset: 0x0003B065
		internal static ImeMode[] UnsupportedTable
		{
			get
			{
				return ImeModeConversion.unsupportedTable;
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06001305 RID: 4869 RVA: 0x0003CE6C File Offset: 0x0003B06C
		internal static ImeMode[] InputLanguageTable
		{
			get
			{
				InputLanguage currentInputLanguage = InputLanguage.CurrentInputLanguage;
				int num = (int)((long)currentInputLanguage.Handle & 65535L);
				if (num <= 2052)
				{
					if (num <= 1041)
					{
						if (num != 1028)
						{
							if (num != 1041)
							{
								goto IL_8A;
							}
							return ImeModeConversion.japaneseTable;
						}
					}
					else
					{
						if (num == 1042)
						{
							goto IL_7E;
						}
						if (num != 2052)
						{
							goto IL_8A;
						}
					}
				}
				else if (num <= 3076)
				{
					if (num == 2066)
					{
						goto IL_7E;
					}
					if (num != 3076)
					{
						goto IL_8A;
					}
				}
				else if (num != 4100 && num != 5124)
				{
					goto IL_8A;
				}
				return ImeModeConversion.chineseTable;
				IL_7E:
				return ImeModeConversion.koreanTable;
				IL_8A:
				return ImeModeConversion.unsupportedTable;
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06001306 RID: 4870 RVA: 0x0003CF08 File Offset: 0x0003B108
		public static Dictionary<ImeMode, ImeModeConversion> ImeModeConversionBits
		{
			get
			{
				if (ImeModeConversion.imeModeConversionBits == null)
				{
					ImeModeConversion.imeModeConversionBits = new Dictionary<ImeMode, ImeModeConversion>(7);
					ImeModeConversion value;
					value.setBits = 9;
					value.clearBits = 2;
					ImeModeConversion.imeModeConversionBits.Add(ImeMode.Hiragana, value);
					value.setBits = 11;
					value.clearBits = 0;
					ImeModeConversion.imeModeConversionBits.Add(ImeMode.Katakana, value);
					value.setBits = 3;
					value.clearBits = 8;
					ImeModeConversion.imeModeConversionBits.Add(ImeMode.KatakanaHalf, value);
					value.setBits = 8;
					value.clearBits = 3;
					ImeModeConversion.imeModeConversionBits.Add(ImeMode.AlphaFull, value);
					value.setBits = 0;
					value.clearBits = 11;
					ImeModeConversion.imeModeConversionBits.Add(ImeMode.Alpha, value);
					value.setBits = 9;
					value.clearBits = 0;
					ImeModeConversion.imeModeConversionBits.Add(ImeMode.HangulFull, value);
					value.setBits = 1;
					value.clearBits = 8;
					ImeModeConversion.imeModeConversionBits.Add(ImeMode.Hangul, value);
					value.setBits = 1;
					value.clearBits = 10;
					ImeModeConversion.imeModeConversionBits.Add(ImeMode.OnHalf, value);
				}
				return ImeModeConversion.imeModeConversionBits;
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06001307 RID: 4871 RVA: 0x0003D017 File Offset: 0x0003B217
		public static bool IsCurrentConversionTableSupported
		{
			get
			{
				return ImeModeConversion.InputLanguageTable != ImeModeConversion.UnsupportedTable;
			}
		}

		// Token: 0x040008FD RID: 2301
		private static Dictionary<ImeMode, ImeModeConversion> imeModeConversionBits;

		// Token: 0x040008FE RID: 2302
		internal int setBits;

		// Token: 0x040008FF RID: 2303
		internal int clearBits;

		// Token: 0x04000900 RID: 2304
		internal const int ImeDisabled = 1;

		// Token: 0x04000901 RID: 2305
		internal const int ImeDirectInput = 2;

		// Token: 0x04000902 RID: 2306
		internal const int ImeClosed = 3;

		// Token: 0x04000903 RID: 2307
		internal const int ImeNativeInput = 4;

		// Token: 0x04000904 RID: 2308
		internal const int ImeNativeFullHiragana = 4;

		// Token: 0x04000905 RID: 2309
		internal const int ImeNativeHalfHiragana = 5;

		// Token: 0x04000906 RID: 2310
		internal const int ImeNativeFullKatakana = 6;

		// Token: 0x04000907 RID: 2311
		internal const int ImeNativeHalfKatakana = 7;

		// Token: 0x04000908 RID: 2312
		internal const int ImeAlphaFull = 8;

		// Token: 0x04000909 RID: 2313
		internal const int ImeAlphaHalf = 9;

		// Token: 0x0400090A RID: 2314
		private static ImeMode[] japaneseTable = new ImeMode[]
		{
			ImeMode.Inherit,
			ImeMode.Disable,
			ImeMode.Off,
			ImeMode.Off,
			ImeMode.Hiragana,
			ImeMode.Hiragana,
			ImeMode.Katakana,
			ImeMode.KatakanaHalf,
			ImeMode.AlphaFull,
			ImeMode.Alpha
		};

		// Token: 0x0400090B RID: 2315
		private static ImeMode[] koreanTable = new ImeMode[]
		{
			ImeMode.Inherit,
			ImeMode.Disable,
			ImeMode.Alpha,
			ImeMode.Alpha,
			ImeMode.HangulFull,
			ImeMode.Hangul,
			ImeMode.HangulFull,
			ImeMode.Hangul,
			ImeMode.AlphaFull,
			ImeMode.Alpha
		};

		// Token: 0x0400090C RID: 2316
		private static ImeMode[] chineseTable = new ImeMode[]
		{
			ImeMode.Inherit,
			ImeMode.Disable,
			ImeMode.Off,
			ImeMode.Close,
			ImeMode.On,
			ImeMode.OnHalf,
			ImeMode.On,
			ImeMode.OnHalf,
			ImeMode.Off,
			ImeMode.Off
		};

		// Token: 0x0400090D RID: 2317
		private static ImeMode[] unsupportedTable = new ImeMode[0];
	}
}
