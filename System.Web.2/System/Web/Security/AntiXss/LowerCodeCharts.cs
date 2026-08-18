using System;

namespace System.Web.Security.AntiXss
{
	// Token: 0x02000613 RID: 1555
	[Flags]
	public enum LowerCodeCharts : long
	{
		// Token: 0x0400299E RID: 10654
		None = 0L,
		// Token: 0x0400299F RID: 10655
		BasicLatin = 1L,
		// Token: 0x040029A0 RID: 10656
		C1ControlsAndLatin1Supplement = 2L,
		// Token: 0x040029A1 RID: 10657
		LatinExtendedA = 4L,
		// Token: 0x040029A2 RID: 10658
		LatinExtendedB = 8L,
		// Token: 0x040029A3 RID: 10659
		IpaExtensions = 16L,
		// Token: 0x040029A4 RID: 10660
		SpacingModifierLetters = 32L,
		// Token: 0x040029A5 RID: 10661
		CombiningDiacriticalMarks = 64L,
		// Token: 0x040029A6 RID: 10662
		GreekAndCoptic = 128L,
		// Token: 0x040029A7 RID: 10663
		Cyrillic = 256L,
		// Token: 0x040029A8 RID: 10664
		CyrillicSupplement = 512L,
		// Token: 0x040029A9 RID: 10665
		Armenian = 1024L,
		// Token: 0x040029AA RID: 10666
		Hebrew = 2048L,
		// Token: 0x040029AB RID: 10667
		Arabic = 4096L,
		// Token: 0x040029AC RID: 10668
		Syriac = 8192L,
		// Token: 0x040029AD RID: 10669
		ArabicSupplement = 16384L,
		// Token: 0x040029AE RID: 10670
		Thaana = 32768L,
		// Token: 0x040029AF RID: 10671
		Nko = 65536L,
		// Token: 0x040029B0 RID: 10672
		Samaritan = 131072L,
		// Token: 0x040029B1 RID: 10673
		Devanagari = 262144L,
		// Token: 0x040029B2 RID: 10674
		Bengali = 524288L,
		// Token: 0x040029B3 RID: 10675
		Gurmukhi = 1048576L,
		// Token: 0x040029B4 RID: 10676
		Gujarati = 2097152L,
		// Token: 0x040029B5 RID: 10677
		Oriya = 4194304L,
		// Token: 0x040029B6 RID: 10678
		Tamil = 8388608L,
		// Token: 0x040029B7 RID: 10679
		Telugu = 16777216L,
		// Token: 0x040029B8 RID: 10680
		Kannada = 33554432L,
		// Token: 0x040029B9 RID: 10681
		Malayalam = 67108864L,
		// Token: 0x040029BA RID: 10682
		Sinhala = 134217728L,
		// Token: 0x040029BB RID: 10683
		Thai = 268435456L,
		// Token: 0x040029BC RID: 10684
		Lao = 536870912L,
		// Token: 0x040029BD RID: 10685
		Tibetan = 1073741824L,
		// Token: 0x040029BE RID: 10686
		Default = 127L
	}
}
