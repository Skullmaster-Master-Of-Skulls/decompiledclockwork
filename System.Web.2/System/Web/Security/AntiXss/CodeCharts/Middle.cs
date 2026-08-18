using System;
using System.Collections;

namespace System.Web.Security.AntiXss.CodeCharts
{
	// Token: 0x02000621 RID: 1569
	internal static class Middle
	{
		// Token: 0x06004E6B RID: 20075 RVA: 0x0011107C File Offset: 0x0010F27C
		public static bool IsFlagSet(MidCodeCharts flags, MidCodeCharts flagToCheck)
		{
			return (flags & flagToCheck) > MidCodeCharts.None;
		}

		// Token: 0x06004E6C RID: 20076 RVA: 0x001118DA File Offset: 0x0010FADA
		public static IEnumerable GreekExtended()
		{
			return CodeChartHelper.GetRange(7936, 8190, (int i) => i == 7958 || i == 7959 || i == 7966 || i == 7967 || i == 8006 || i == 8007 || i == 8014 || i == 8015 || i == 8024 || i == 8026 || i == 8028 || i == 8030 || i == 8062 || i == 8063 || i == 8117 || i == 8133 || i == 8148 || i == 8149 || i == 8156 || i == 8176 || i == 8177 || i == 8181);
		}

		// Token: 0x06004E6D RID: 20077 RVA: 0x0011190A File Offset: 0x0010FB0A
		public static IEnumerable GeneralPunctuation()
		{
			return CodeChartHelper.GetRange(8192, 8303, (int i) => i >= 8293 && i <= 8297);
		}

		// Token: 0x06004E6E RID: 20078 RVA: 0x0011193A File Offset: 0x0010FB3A
		public static IEnumerable SuperscriptsAndSubscripts()
		{
			return CodeChartHelper.GetRange(8304, 8340, (int i) => i == 8306 || i == 8307 || i == 8335);
		}

		// Token: 0x06004E6F RID: 20079 RVA: 0x0011196A File Offset: 0x0010FB6A
		public static IEnumerable CurrencySymbols()
		{
			return CodeChartHelper.GetRange(8352, 8376);
		}

		// Token: 0x06004E70 RID: 20080 RVA: 0x0011197B File Offset: 0x0010FB7B
		public static IEnumerable CombiningDiacriticalMarksForSymbols()
		{
			return CodeChartHelper.GetRange(8400, 8432);
		}

		// Token: 0x06004E71 RID: 20081 RVA: 0x0011198C File Offset: 0x0010FB8C
		public static IEnumerable LetterlikeSymbols()
		{
			return CodeChartHelper.GetRange(8448, 8527);
		}

		// Token: 0x06004E72 RID: 20082 RVA: 0x0011199D File Offset: 0x0010FB9D
		public static IEnumerable NumberForms()
		{
			return CodeChartHelper.GetRange(8528, 8585);
		}

		// Token: 0x06004E73 RID: 20083 RVA: 0x001119AE File Offset: 0x0010FBAE
		public static IEnumerable Arrows()
		{
			return CodeChartHelper.GetRange(8592, 8703);
		}

		// Token: 0x06004E74 RID: 20084 RVA: 0x001119BF File Offset: 0x0010FBBF
		public static IEnumerable MathematicalOperators()
		{
			return CodeChartHelper.GetRange(8704, 8959);
		}

		// Token: 0x06004E75 RID: 20085 RVA: 0x001119D0 File Offset: 0x0010FBD0
		public static IEnumerable MiscellaneousTechnical()
		{
			return CodeChartHelper.GetRange(8960, 9192);
		}

		// Token: 0x06004E76 RID: 20086 RVA: 0x001119E1 File Offset: 0x0010FBE1
		public static IEnumerable ControlPictures()
		{
			return CodeChartHelper.GetRange(9216, 9254);
		}

		// Token: 0x06004E77 RID: 20087 RVA: 0x001119F2 File Offset: 0x0010FBF2
		public static IEnumerable OpticalCharacterRecognition()
		{
			return CodeChartHelper.GetRange(9280, 9290);
		}

		// Token: 0x06004E78 RID: 20088 RVA: 0x00111A03 File Offset: 0x0010FC03
		public static IEnumerable EnclosedAlphanumerics()
		{
			return CodeChartHelper.GetRange(9312, 9471);
		}

		// Token: 0x06004E79 RID: 20089 RVA: 0x00111A14 File Offset: 0x0010FC14
		public static IEnumerable BoxDrawing()
		{
			return CodeChartHelper.GetRange(9472, 9599);
		}

		// Token: 0x06004E7A RID: 20090 RVA: 0x00111A25 File Offset: 0x0010FC25
		public static IEnumerable BlockElements()
		{
			return CodeChartHelper.GetRange(9600, 9631);
		}

		// Token: 0x06004E7B RID: 20091 RVA: 0x00111A36 File Offset: 0x0010FC36
		public static IEnumerable GeometricShapes()
		{
			return CodeChartHelper.GetRange(9632, 9727);
		}

		// Token: 0x06004E7C RID: 20092 RVA: 0x00111A47 File Offset: 0x0010FC47
		public static IEnumerable MiscellaneousSymbols()
		{
			return CodeChartHelper.GetRange(9728, 9983, (int i) => i == 9934 || i == 9954 || (i >= 9956 && i <= 9959));
		}

		// Token: 0x06004E7D RID: 20093 RVA: 0x00111A77 File Offset: 0x0010FC77
		public static IEnumerable Dingbats()
		{
			return CodeChartHelper.GetRange(9985, 10174, (int i) => i == 9989 || i == 9994 || i == 9995 || i == 10024 || i == 10060 || i == 10062 || i == 10067 || i == 10068 || i == 10069 || i == 10079 || i == 10080 || i == 10133 || i == 10134 || i == 10135 || i == 10160);
		}

		// Token: 0x06004E7E RID: 20094 RVA: 0x00111AA7 File Offset: 0x0010FCA7
		public static IEnumerable MiscellaneousMathematicalSymbolsA()
		{
			return CodeChartHelper.GetRange(10176, 10223, (int i) => i == 10187 || i == 10189 || i == 10190 || i == 10191);
		}

		// Token: 0x06004E7F RID: 20095 RVA: 0x00111AD7 File Offset: 0x0010FCD7
		public static IEnumerable SupplementalArrowsA()
		{
			return CodeChartHelper.GetRange(10224, 10239);
		}

		// Token: 0x06004E80 RID: 20096 RVA: 0x00111AE8 File Offset: 0x0010FCE8
		public static IEnumerable BraillePatterns()
		{
			return CodeChartHelper.GetRange(10240, 10495);
		}

		// Token: 0x06004E81 RID: 20097 RVA: 0x00111AF9 File Offset: 0x0010FCF9
		public static IEnumerable SupplementalArrowsB()
		{
			return CodeChartHelper.GetRange(10496, 10623);
		}

		// Token: 0x06004E82 RID: 20098 RVA: 0x00111B0A File Offset: 0x0010FD0A
		public static IEnumerable MiscellaneousMathematicalSymbolsB()
		{
			return CodeChartHelper.GetRange(10624, 10751);
		}

		// Token: 0x06004E83 RID: 20099 RVA: 0x00111B1B File Offset: 0x0010FD1B
		public static IEnumerable SupplementalMathematicalOperators()
		{
			return CodeChartHelper.GetRange(10752, 11007);
		}

		// Token: 0x06004E84 RID: 20100 RVA: 0x00111B2C File Offset: 0x0010FD2C
		public static IEnumerable MiscellaneousSymbolsAndArrows()
		{
			return CodeChartHelper.GetRange(11008, 11097, (int i) => i == 11085 || i == 11086 || i == 11087);
		}

		// Token: 0x06004E85 RID: 20101 RVA: 0x00111B5C File Offset: 0x0010FD5C
		public static IEnumerable Glagolitic()
		{
			return CodeChartHelper.GetRange(11264, 11358, (int i) => i == 11311);
		}

		// Token: 0x06004E86 RID: 20102 RVA: 0x00111B8C File Offset: 0x0010FD8C
		public static IEnumerable LatinExtendedC()
		{
			return CodeChartHelper.GetRange(11360, 11391);
		}

		// Token: 0x06004E87 RID: 20103 RVA: 0x00111B9D File Offset: 0x0010FD9D
		public static IEnumerable Coptic()
		{
			return CodeChartHelper.GetRange(11392, 11519, (int i) => i >= 11506 && i <= 11512);
		}

		// Token: 0x06004E88 RID: 20104 RVA: 0x00111BCD File Offset: 0x0010FDCD
		public static IEnumerable GeorgianSupplement()
		{
			return CodeChartHelper.GetRange(11520, 11557);
		}

		// Token: 0x06004E89 RID: 20105 RVA: 0x00111BDE File Offset: 0x0010FDDE
		public static IEnumerable Tifinagh()
		{
			return CodeChartHelper.GetRange(11568, 11631, (int i) => i >= 11622 && i <= 11630);
		}

		// Token: 0x06004E8A RID: 20106 RVA: 0x00111C0E File Offset: 0x0010FE0E
		public static IEnumerable EthiopicExtended()
		{
			return CodeChartHelper.GetRange(11648, 11742, (int i) => (i >= 11671 && i <= 11679) || i == 11687 || i == 11695 || i == 11703 || i == 11711 || i == 11719 || i == 11727 || i == 11735 || i == 11743);
		}
	}
}
