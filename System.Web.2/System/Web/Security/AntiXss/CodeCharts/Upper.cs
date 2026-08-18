using System;
using System.Collections;
using System.Linq;

namespace System.Web.Security.AntiXss.CodeCharts
{
	// Token: 0x02000622 RID: 1570
	internal static class Upper
	{
		// Token: 0x06004E8B RID: 20107 RVA: 0x00111C3E File Offset: 0x0010FE3E
		public static bool IsFlagSet(UpperCodeCharts flags, UpperCodeCharts flagToCheck)
		{
			return (flags & flagToCheck) > UpperCodeCharts.None;
		}

		// Token: 0x06004E8C RID: 20108 RVA: 0x00111C46 File Offset: 0x0010FE46
		public static IEnumerable DevanagariExtended()
		{
			return CodeChartHelper.GetRange(43232, 43259);
		}

		// Token: 0x06004E8D RID: 20109 RVA: 0x00111C57 File Offset: 0x0010FE57
		public static IEnumerable KayahLi()
		{
			return CodeChartHelper.GetRange(43264, 43311);
		}

		// Token: 0x06004E8E RID: 20110 RVA: 0x00111C68 File Offset: 0x0010FE68
		public static IEnumerable Rejang()
		{
			return CodeChartHelper.GetRange(43312, 43347).Concat(new int[]
			{
				43359
			});
		}

		// Token: 0x06004E8F RID: 20111 RVA: 0x00111C8C File Offset: 0x0010FE8C
		public static IEnumerable HangulJamoExtendedA()
		{
			return CodeChartHelper.GetRange(43360, 43388);
		}

		// Token: 0x06004E90 RID: 20112 RVA: 0x00111C9D File Offset: 0x0010FE9D
		public static IEnumerable Javanese()
		{
			return CodeChartHelper.GetRange(43392, 43487, (int i) => i == 43470 || (i >= 43482 && i <= 43485));
		}

		// Token: 0x06004E91 RID: 20113 RVA: 0x00111CCD File Offset: 0x0010FECD
		public static IEnumerable Cham()
		{
			return CodeChartHelper.GetRange(43520, 43615, (int i) => (i >= 43575 && i <= 43583) || i == 43598 || i == 43599 || i == 43610 || i == 43611);
		}

		// Token: 0x06004E92 RID: 20114 RVA: 0x00111CFD File Offset: 0x0010FEFD
		public static IEnumerable MyanmarExtendedA()
		{
			return CodeChartHelper.GetRange(43616, 43643);
		}

		// Token: 0x06004E93 RID: 20115 RVA: 0x00111D0E File Offset: 0x0010FF0E
		public static IEnumerable TaiViet()
		{
			return CodeChartHelper.GetRange(43648, 43714).Concat(CodeChartHelper.GetRange(43739, 43743));
		}

		// Token: 0x06004E94 RID: 20116 RVA: 0x00111D33 File Offset: 0x0010FF33
		public static IEnumerable MeeteiMayek()
		{
			return CodeChartHelper.GetRange(43968, 44025, (int i) => i == 44014 || i == 44015);
		}

		// Token: 0x06004E95 RID: 20117 RVA: 0x00111D63 File Offset: 0x0010FF63
		public static IEnumerable HangulSyllables()
		{
			return CodeChartHelper.GetRange(44032, 55203);
		}

		// Token: 0x06004E96 RID: 20118 RVA: 0x00111D74 File Offset: 0x0010FF74
		public static IEnumerable HangulJamoExtendedB()
		{
			return CodeChartHelper.GetRange(55216, 55291, (int i) => i == 55239 || i == 55240 || i == 55241 || i == 55242);
		}

		// Token: 0x06004E97 RID: 20119 RVA: 0x00111DA4 File Offset: 0x0010FFA4
		public static IEnumerable CjkCompatibilityIdeographs()
		{
			return CodeChartHelper.GetRange(63744, 64217, (int i) => i == 64046 || i == 64047 || i == 64110 || i == 64111);
		}

		// Token: 0x06004E98 RID: 20120 RVA: 0x00111DD4 File Offset: 0x0010FFD4
		public static IEnumerable AlphabeticPresentationForms()
		{
			return CodeChartHelper.GetRange(64256, 64335, (int i) => (i >= 64263 && i <= 64274) || (i >= 64280 && i <= 64284) || i == 64311 || i == 64317 || i == 64319 || i == 64322 || i == 64325);
		}

		// Token: 0x06004E99 RID: 20121 RVA: 0x00111E04 File Offset: 0x00110004
		public static IEnumerable ArabicPresentationFormsA()
		{
			return CodeChartHelper.GetRange(64336, 65021, (int i) => (i >= 64434 && i <= 64466) || (i >= 64832 && i <= 64847) || i == 64912 || i == 64913 || (i >= 64968 && i <= 65007));
		}

		// Token: 0x06004E9A RID: 20122 RVA: 0x00111E34 File Offset: 0x00110034
		public static IEnumerable VariationSelectors()
		{
			return CodeChartHelper.GetRange(65024, 65039);
		}

		// Token: 0x06004E9B RID: 20123 RVA: 0x00111E45 File Offset: 0x00110045
		public static IEnumerable VerticalForms()
		{
			return CodeChartHelper.GetRange(65040, 65049);
		}

		// Token: 0x06004E9C RID: 20124 RVA: 0x00111E56 File Offset: 0x00110056
		public static IEnumerable CombiningHalfMarks()
		{
			return CodeChartHelper.GetRange(65056, 65062);
		}

		// Token: 0x06004E9D RID: 20125 RVA: 0x00111E67 File Offset: 0x00110067
		public static IEnumerable CjkCompatibilityForms()
		{
			return CodeChartHelper.GetRange(65072, 65103);
		}

		// Token: 0x06004E9E RID: 20126 RVA: 0x00111E78 File Offset: 0x00110078
		public static IEnumerable SmallFormVariants()
		{
			return CodeChartHelper.GetRange(65104, 65131, (int i) => i == 65107 || i == 65127);
		}

		// Token: 0x06004E9F RID: 20127 RVA: 0x00111EA8 File Offset: 0x001100A8
		public static IEnumerable ArabicPresentationFormsB()
		{
			return CodeChartHelper.GetRange(65136, 65276, (int i) => i == 65141);
		}

		// Token: 0x06004EA0 RID: 20128 RVA: 0x00111ED8 File Offset: 0x001100D8
		public static IEnumerable HalfWidthAndFullWidthForms()
		{
			return CodeChartHelper.GetRange(65281, 65518, (int i) => i == 65471 || i == 65472 || i == 65473 || i == 65480 || i == 65481 || i == 65488 || i == 65489 || i == 65496 || i == 65497 || i == 65501 || i == 65502 || i == 65503 || i == 65511);
		}

		// Token: 0x06004EA1 RID: 20129 RVA: 0x00111F08 File Offset: 0x00110108
		public static IEnumerable Specials()
		{
			return CodeChartHelper.GetRange(65529, 65533);
		}
	}
}
