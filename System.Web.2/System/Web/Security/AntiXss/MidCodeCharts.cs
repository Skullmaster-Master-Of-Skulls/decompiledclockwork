using System;

namespace System.Web.Security.AntiXss
{
	// Token: 0x02000615 RID: 1557
	[Flags]
	public enum MidCodeCharts : long
	{
		// Token: 0x040029E1 RID: 10721
		None = 0L,
		// Token: 0x040029E2 RID: 10722
		GreekExtended = 1L,
		// Token: 0x040029E3 RID: 10723
		GeneralPunctuation = 2L,
		// Token: 0x040029E4 RID: 10724
		SuperscriptsAndSubscripts = 4L,
		// Token: 0x040029E5 RID: 10725
		CurrencySymbols = 8L,
		// Token: 0x040029E6 RID: 10726
		CombiningDiacriticalMarksForSymbols = 16L,
		// Token: 0x040029E7 RID: 10727
		LetterlikeSymbols = 32L,
		// Token: 0x040029E8 RID: 10728
		NumberForms = 64L,
		// Token: 0x040029E9 RID: 10729
		Arrows = 128L,
		// Token: 0x040029EA RID: 10730
		MathematicalOperators = 256L,
		// Token: 0x040029EB RID: 10731
		MiscellaneousTechnical = 512L,
		// Token: 0x040029EC RID: 10732
		ControlPictures = 1024L,
		// Token: 0x040029ED RID: 10733
		OpticalCharacterRecognition = 2048L,
		// Token: 0x040029EE RID: 10734
		EnclosedAlphanumerics = 4096L,
		// Token: 0x040029EF RID: 10735
		BoxDrawing = 8192L,
		// Token: 0x040029F0 RID: 10736
		BlockElements = 16384L,
		// Token: 0x040029F1 RID: 10737
		GeometricShapes = 32768L,
		// Token: 0x040029F2 RID: 10738
		MiscellaneousSymbols = 65536L,
		// Token: 0x040029F3 RID: 10739
		Dingbats = 131072L,
		// Token: 0x040029F4 RID: 10740
		MiscellaneousMathematicalSymbolsA = 262144L,
		// Token: 0x040029F5 RID: 10741
		SupplementalArrowsA = 524288L,
		// Token: 0x040029F6 RID: 10742
		BraillePatterns = 1048576L,
		// Token: 0x040029F7 RID: 10743
		SupplementalArrowsB = 2097152L,
		// Token: 0x040029F8 RID: 10744
		MiscellaneousMathematicalSymbolsB = 4194304L,
		// Token: 0x040029F9 RID: 10745
		SupplementalMathematicalOperators = 8388608L,
		// Token: 0x040029FA RID: 10746
		MiscellaneousSymbolsAndArrows = 16777216L,
		// Token: 0x040029FB RID: 10747
		Glagolitic = 33554432L,
		// Token: 0x040029FC RID: 10748
		LatinExtendedC = 67108864L,
		// Token: 0x040029FD RID: 10749
		Coptic = 134217728L,
		// Token: 0x040029FE RID: 10750
		GeorgianSupplement = 268435456L,
		// Token: 0x040029FF RID: 10751
		Tifinagh = 536870912L,
		// Token: 0x04002A00 RID: 10752
		EthiopicExtended = 16384L
	}
}
