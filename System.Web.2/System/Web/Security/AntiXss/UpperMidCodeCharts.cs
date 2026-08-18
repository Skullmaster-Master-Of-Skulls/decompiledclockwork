using System;

namespace System.Web.Security.AntiXss
{
	// Token: 0x02000616 RID: 1558
	[Flags]
	public enum UpperMidCodeCharts : long
	{
		// Token: 0x04002A02 RID: 10754
		None = 0L,
		// Token: 0x04002A03 RID: 10755
		CyrillicExtendedA = 1L,
		// Token: 0x04002A04 RID: 10756
		SupplementalPunctuation = 2L,
		// Token: 0x04002A05 RID: 10757
		CjkRadicalsSupplement = 4L,
		// Token: 0x04002A06 RID: 10758
		KangxiRadicals = 8L,
		// Token: 0x04002A07 RID: 10759
		IdeographicDescriptionCharacters = 16L,
		// Token: 0x04002A08 RID: 10760
		CjkSymbolsAndPunctuation = 32L,
		// Token: 0x04002A09 RID: 10761
		Hiragana = 64L,
		// Token: 0x04002A0A RID: 10762
		Katakana = 128L,
		// Token: 0x04002A0B RID: 10763
		Bopomofo = 256L,
		// Token: 0x04002A0C RID: 10764
		HangulCompatibilityJamo = 512L,
		// Token: 0x04002A0D RID: 10765
		Kanbun = 1024L,
		// Token: 0x04002A0E RID: 10766
		BopomofoExtended = 2048L,
		// Token: 0x04002A0F RID: 10767
		CjkStrokes = 4096L,
		// Token: 0x04002A10 RID: 10768
		KatakanaPhoneticExtensions = 8192L,
		// Token: 0x04002A11 RID: 10769
		EnclosedCjkLettersAndMonths = 16384L,
		// Token: 0x04002A12 RID: 10770
		CjkCompatibility = 32768L,
		// Token: 0x04002A13 RID: 10771
		CjkUnifiedIdeographsExtensionA = 65536L,
		// Token: 0x04002A14 RID: 10772
		YijingHexagramSymbols = 131072L,
		// Token: 0x04002A15 RID: 10773
		CjkUnifiedIdeographs = 262144L,
		// Token: 0x04002A16 RID: 10774
		YiSyllables = 524288L,
		// Token: 0x04002A17 RID: 10775
		YiRadicals = 1048576L,
		// Token: 0x04002A18 RID: 10776
		Lisu = 2097152L,
		// Token: 0x04002A19 RID: 10777
		Vai = 4194304L,
		// Token: 0x04002A1A RID: 10778
		CyrillicExtendedB = 8388608L,
		// Token: 0x04002A1B RID: 10779
		Bamum = 16777216L,
		// Token: 0x04002A1C RID: 10780
		ModifierToneLetters = 33554432L,
		// Token: 0x04002A1D RID: 10781
		LatinExtendedD = 67108864L,
		// Token: 0x04002A1E RID: 10782
		SylotiNagri = 134217728L,
		// Token: 0x04002A1F RID: 10783
		CommonIndicNumberForms = 268435456L,
		// Token: 0x04002A20 RID: 10784
		Phagspa = 536870912L,
		// Token: 0x04002A21 RID: 10785
		Saurashtra = 1073741824L
	}
}
