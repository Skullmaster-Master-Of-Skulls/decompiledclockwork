using System;
using System.Collections;
using System.Linq;

namespace System.Web.Security.AntiXss.CodeCharts
{
	// Token: 0x02000623 RID: 1571
	internal static class UpperMiddle
	{
		// Token: 0x06004EA2 RID: 20130 RVA: 0x0011107C File Offset: 0x0010F27C
		public static bool IsFlagSet(UpperMidCodeCharts flags, UpperMidCodeCharts flagToCheck)
		{
			return (flags & flagToCheck) > UpperMidCodeCharts.None;
		}

		// Token: 0x06004EA3 RID: 20131 RVA: 0x00111F19 File Offset: 0x00110119
		public static IEnumerable CyrillicExtendedA()
		{
			return CodeChartHelper.GetRange(11744, 11775);
		}

		// Token: 0x06004EA4 RID: 20132 RVA: 0x00111F2A File Offset: 0x0011012A
		public static IEnumerable SupplementalPunctuation()
		{
			return CodeChartHelper.GetRange(11776, 11825);
		}

		// Token: 0x06004EA5 RID: 20133 RVA: 0x00111F3B File Offset: 0x0011013B
		public static IEnumerable CjkRadicalsSupplement()
		{
			return CodeChartHelper.GetRange(11904, 12019, (int i) => i == 11930);
		}

		// Token: 0x06004EA6 RID: 20134 RVA: 0x00111F6B File Offset: 0x0011016B
		public static IEnumerable KangxiRadicals()
		{
			return CodeChartHelper.GetRange(12032, 12245);
		}

		// Token: 0x06004EA7 RID: 20135 RVA: 0x00111F7C File Offset: 0x0011017C
		public static IEnumerable IdeographicDescriptionCharacters()
		{
			return CodeChartHelper.GetRange(12272, 12283);
		}

		// Token: 0x06004EA8 RID: 20136 RVA: 0x00111F8D File Offset: 0x0011018D
		public static IEnumerable CjkSymbolsAndPunctuation()
		{
			return CodeChartHelper.GetRange(12288, 12351);
		}

		// Token: 0x06004EA9 RID: 20137 RVA: 0x00111F9E File Offset: 0x0011019E
		public static IEnumerable Hiragana()
		{
			return CodeChartHelper.GetRange(12353, 12447, (int i) => i == 12439 || i == 12440);
		}

		// Token: 0x06004EAA RID: 20138 RVA: 0x00111FCE File Offset: 0x001101CE
		public static IEnumerable Katakana()
		{
			return CodeChartHelper.GetRange(12448, 12543);
		}

		// Token: 0x06004EAB RID: 20139 RVA: 0x00111FDF File Offset: 0x001101DF
		public static IEnumerable Bopomofo()
		{
			return CodeChartHelper.GetRange(12549, 12589);
		}

		// Token: 0x06004EAC RID: 20140 RVA: 0x00111FF0 File Offset: 0x001101F0
		public static IEnumerable HangulCompatibilityJamo()
		{
			return CodeChartHelper.GetRange(12593, 12686);
		}

		// Token: 0x06004EAD RID: 20141 RVA: 0x00112001 File Offset: 0x00110201
		public static IEnumerable Kanbun()
		{
			return CodeChartHelper.GetRange(12688, 12703);
		}

		// Token: 0x06004EAE RID: 20142 RVA: 0x00112012 File Offset: 0x00110212
		public static IEnumerable BopomofoExtended()
		{
			return CodeChartHelper.GetRange(12704, 12727);
		}

		// Token: 0x06004EAF RID: 20143 RVA: 0x00112023 File Offset: 0x00110223
		public static IEnumerable CjkStrokes()
		{
			return CodeChartHelper.GetRange(12736, 12771);
		}

		// Token: 0x06004EB0 RID: 20144 RVA: 0x00112034 File Offset: 0x00110234
		public static IEnumerable KatakanaPhoneticExtensions()
		{
			return CodeChartHelper.GetRange(12784, 12799);
		}

		// Token: 0x06004EB1 RID: 20145 RVA: 0x00112045 File Offset: 0x00110245
		public static IEnumerable EnclosedCjkLettersAndMonths()
		{
			return CodeChartHelper.GetRange(12800, 13054, (int i) => i == 12831);
		}

		// Token: 0x06004EB2 RID: 20146 RVA: 0x00112075 File Offset: 0x00110275
		public static IEnumerable CjkCompatibility()
		{
			return CodeChartHelper.GetRange(13056, 13311);
		}

		// Token: 0x06004EB3 RID: 20147 RVA: 0x00112086 File Offset: 0x00110286
		public static IEnumerable CjkUnifiedIdeographsExtensionA()
		{
			return CodeChartHelper.GetRange(13312, 19893);
		}

		// Token: 0x06004EB4 RID: 20148 RVA: 0x00112097 File Offset: 0x00110297
		public static IEnumerable YijingHexagramSymbols()
		{
			return CodeChartHelper.GetRange(19904, 19967);
		}

		// Token: 0x06004EB5 RID: 20149 RVA: 0x001120A8 File Offset: 0x001102A8
		public static IEnumerable CjkUnifiedIdeographs()
		{
			return CodeChartHelper.GetRange(19968, 40907);
		}

		// Token: 0x06004EB6 RID: 20150 RVA: 0x001120B9 File Offset: 0x001102B9
		public static IEnumerable YiSyllables()
		{
			return CodeChartHelper.GetRange(40960, 42124);
		}

		// Token: 0x06004EB7 RID: 20151 RVA: 0x001120CA File Offset: 0x001102CA
		public static IEnumerable YiRadicals()
		{
			return CodeChartHelper.GetRange(42128, 42182);
		}

		// Token: 0x06004EB8 RID: 20152 RVA: 0x001120DB File Offset: 0x001102DB
		public static IEnumerable Lisu()
		{
			return CodeChartHelper.GetRange(42192, 42239);
		}

		// Token: 0x06004EB9 RID: 20153 RVA: 0x001120EC File Offset: 0x001102EC
		public static IEnumerable Vai()
		{
			return CodeChartHelper.GetRange(42240, 42539);
		}

		// Token: 0x06004EBA RID: 20154 RVA: 0x001120FD File Offset: 0x001102FD
		public static IEnumerable CyrillicExtendedB()
		{
			return CodeChartHelper.GetRange(42560, 42647, (int i) => i == 42592 || i == 42593 || (i >= 42612 && i <= 42619));
		}

		// Token: 0x06004EBB RID: 20155 RVA: 0x0011212D File Offset: 0x0011032D
		public static IEnumerable Bamum()
		{
			return CodeChartHelper.GetRange(42656, 42743);
		}

		// Token: 0x06004EBC RID: 20156 RVA: 0x0011213E File Offset: 0x0011033E
		public static IEnumerable ModifierToneLetters()
		{
			return CodeChartHelper.GetRange(42752, 42783);
		}

		// Token: 0x06004EBD RID: 20157 RVA: 0x0011214F File Offset: 0x0011034F
		public static IEnumerable LatinExtendedD()
		{
			return CodeChartHelper.GetRange(42784, 42892).Concat(CodeChartHelper.GetRange(43003, 43007));
		}

		// Token: 0x06004EBE RID: 20158 RVA: 0x00112174 File Offset: 0x00110374
		public static IEnumerable SylotiNagri()
		{
			return CodeChartHelper.GetRange(43008, 43051);
		}

		// Token: 0x06004EBF RID: 20159 RVA: 0x00112185 File Offset: 0x00110385
		public static IEnumerable CommonIndicNumberForms()
		{
			return CodeChartHelper.GetRange(43056, 43065);
		}

		// Token: 0x06004EC0 RID: 20160 RVA: 0x00112196 File Offset: 0x00110396
		public static IEnumerable Phagspa()
		{
			return CodeChartHelper.GetRange(43072, 43127);
		}

		// Token: 0x06004EC1 RID: 20161 RVA: 0x001121A7 File Offset: 0x001103A7
		public static IEnumerable Saurashtra()
		{
			return CodeChartHelper.GetRange(43136, 43225, (int i) => i >= 43205 && i <= 43213);
		}
	}
}
