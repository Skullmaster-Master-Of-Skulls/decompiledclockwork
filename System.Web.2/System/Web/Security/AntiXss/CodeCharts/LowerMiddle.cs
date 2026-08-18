using System;
using System.Collections;

namespace System.Web.Security.AntiXss.CodeCharts
{
	// Token: 0x02000620 RID: 1568
	internal static class LowerMiddle
	{
		// Token: 0x06004E4B RID: 20043 RVA: 0x0011107C File Offset: 0x0010F27C
		public static bool IsFlagSet(LowerMidCodeCharts flags, LowerMidCodeCharts flagToCheck)
		{
			return (flags & flagToCheck) > LowerMidCodeCharts.None;
		}

		// Token: 0x06004E4C RID: 20044 RVA: 0x001114FA File Offset: 0x0010F6FA
		public static IEnumerable Myanmar()
		{
			return CodeChartHelper.GetRange(4096, 4255);
		}

		// Token: 0x06004E4D RID: 20045 RVA: 0x0011150B File Offset: 0x0010F70B
		public static IEnumerable Georgian()
		{
			return CodeChartHelper.GetRange(4256, 4348, (int i) => i >= 4294 && i <= 4303);
		}

		// Token: 0x06004E4E RID: 20046 RVA: 0x0011153B File Offset: 0x0010F73B
		public static IEnumerable HangulJamo()
		{
			return CodeChartHelper.GetRange(4352, 4607);
		}

		// Token: 0x06004E4F RID: 20047 RVA: 0x0011154C File Offset: 0x0010F74C
		public static IEnumerable Ethiopic()
		{
			return CodeChartHelper.GetRange(4608, 4988, (int i) => i == 4681 || i == 4686 || i == 4687 || i == 4695 || i == 4697 || i == 4702 || i == 4703 || i == 4745 || i == 4750 || i == 4751 || i == 4785 || i == 4790 || i == 4791 || i == 4799 || i == 4801 || i == 4806 || i == 4807 || i == 4823 || i == 4881 || i == 4886 || i == 4887 || (i >= 4955 && i <= 4958));
		}

		// Token: 0x06004E50 RID: 20048 RVA: 0x0011157C File Offset: 0x0010F77C
		public static IEnumerable EthiopicSupplement()
		{
			return CodeChartHelper.GetRange(4992, 5017);
		}

		// Token: 0x06004E51 RID: 20049 RVA: 0x0011158D File Offset: 0x0010F78D
		public static IEnumerable Cherokee()
		{
			return CodeChartHelper.GetRange(5024, 5108);
		}

		// Token: 0x06004E52 RID: 20050 RVA: 0x0011159E File Offset: 0x0010F79E
		public static IEnumerable UnifiedCanadianAboriginalSyllabics()
		{
			return CodeChartHelper.GetRange(5120, 5759);
		}

		// Token: 0x06004E53 RID: 20051 RVA: 0x001115AF File Offset: 0x0010F7AF
		public static IEnumerable Ogham()
		{
			return CodeChartHelper.GetRange(5760, 5788);
		}

		// Token: 0x06004E54 RID: 20052 RVA: 0x001115C0 File Offset: 0x0010F7C0
		public static IEnumerable Runic()
		{
			return CodeChartHelper.GetRange(5792, 5872);
		}

		// Token: 0x06004E55 RID: 20053 RVA: 0x001115D1 File Offset: 0x0010F7D1
		public static IEnumerable Tagalog()
		{
			return CodeChartHelper.GetRange(5888, 5908, (int i) => i == 5901);
		}

		// Token: 0x06004E56 RID: 20054 RVA: 0x00111601 File Offset: 0x0010F801
		public static IEnumerable Hanunoo()
		{
			return CodeChartHelper.GetRange(5920, 5942);
		}

		// Token: 0x06004E57 RID: 20055 RVA: 0x00111612 File Offset: 0x0010F812
		public static IEnumerable Buhid()
		{
			return CodeChartHelper.GetRange(5952, 5971);
		}

		// Token: 0x06004E58 RID: 20056 RVA: 0x00111623 File Offset: 0x0010F823
		public static IEnumerable Tagbanwa()
		{
			return CodeChartHelper.GetRange(5984, 6003, (int i) => i == 5997 || i == 6001);
		}

		// Token: 0x06004E59 RID: 20057 RVA: 0x00111653 File Offset: 0x0010F853
		public static IEnumerable Khmer()
		{
			return CodeChartHelper.GetRange(6016, 6137, (int i) => i == 6110 || i == 6111 || (i >= 6122 && i <= 6127));
		}

		// Token: 0x06004E5A RID: 20058 RVA: 0x00111683 File Offset: 0x0010F883
		public static IEnumerable Mongolian()
		{
			return CodeChartHelper.GetRange(6144, 6314, (int i) => i == 6159 || (i >= 6170 && i <= 6175) || (i >= 6264 && i <= 6271));
		}

		// Token: 0x06004E5B RID: 20059 RVA: 0x001116B3 File Offset: 0x0010F8B3
		public static IEnumerable UnifiedCanadianAboriginalSyllabicsExtended()
		{
			return CodeChartHelper.GetRange(6320, 6389);
		}

		// Token: 0x06004E5C RID: 20060 RVA: 0x001116C4 File Offset: 0x0010F8C4
		public static IEnumerable Limbu()
		{
			return CodeChartHelper.GetRange(6400, 6479, (int i) => i == 6429 || i == 6430 || i == 6431 || (i >= 6444 && i <= 6447) || (i >= 6460 && i <= 6463) || i == 6465 || i == 6466 || i == 6467);
		}

		// Token: 0x06004E5D RID: 20061 RVA: 0x001116F4 File Offset: 0x0010F8F4
		public static IEnumerable TaiLe()
		{
			return CodeChartHelper.GetRange(6480, 6516, (int i) => i == 6510 || i == 6511);
		}

		// Token: 0x06004E5E RID: 20062 RVA: 0x00111724 File Offset: 0x0010F924
		public static IEnumerable NewTaiLue()
		{
			return CodeChartHelper.GetRange(6528, 6623, (int i) => (i >= 6572 && i <= 6575) || (i >= 6602 && i <= 6607) || (i >= 6619 && i <= 6621));
		}

		// Token: 0x06004E5F RID: 20063 RVA: 0x00111754 File Offset: 0x0010F954
		public static IEnumerable KhmerSymbols()
		{
			return CodeChartHelper.GetRange(6624, 6655);
		}

		// Token: 0x06004E60 RID: 20064 RVA: 0x00111765 File Offset: 0x0010F965
		public static IEnumerable Buginese()
		{
			return CodeChartHelper.GetRange(6656, 6687, (int i) => i == 6684 || i == 6685);
		}

		// Token: 0x06004E61 RID: 20065 RVA: 0x00111795 File Offset: 0x0010F995
		public static IEnumerable TaiTham()
		{
			return CodeChartHelper.GetRange(6688, 6829, (int i) => i == 6751 || i == 6781 || i == 6782 || (i >= 6794 && i <= 6799) || (i >= 6810 && i <= 6815));
		}

		// Token: 0x06004E62 RID: 20066 RVA: 0x001117C5 File Offset: 0x0010F9C5
		public static IEnumerable Balinese()
		{
			return CodeChartHelper.GetRange(6912, 7036, (int i) => i >= 6988 && i <= 6991);
		}

		// Token: 0x06004E63 RID: 20067 RVA: 0x001117F5 File Offset: 0x0010F9F5
		public static IEnumerable Sudanese()
		{
			return CodeChartHelper.GetRange(7040, 7097, (int i) => i >= 7083 && i <= 7085);
		}

		// Token: 0x06004E64 RID: 20068 RVA: 0x00111825 File Offset: 0x0010FA25
		public static IEnumerable Lepcha()
		{
			return CodeChartHelper.GetRange(7168, 7247, (int i) => (i >= 7224 && i <= 7226) || (i >= 7242 && i <= 7244));
		}

		// Token: 0x06004E65 RID: 20069 RVA: 0x00111855 File Offset: 0x0010FA55
		public static IEnumerable OlChiki()
		{
			return CodeChartHelper.GetRange(7248, 7295);
		}

		// Token: 0x06004E66 RID: 20070 RVA: 0x00111866 File Offset: 0x0010FA66
		public static IEnumerable VedicExtensions()
		{
			return CodeChartHelper.GetRange(7376, 7410);
		}

		// Token: 0x06004E67 RID: 20071 RVA: 0x00111877 File Offset: 0x0010FA77
		public static IEnumerable PhoneticExtensions()
		{
			return CodeChartHelper.GetRange(7424, 7551);
		}

		// Token: 0x06004E68 RID: 20072 RVA: 0x00111888 File Offset: 0x0010FA88
		public static IEnumerable PhoneticExtensionsSupplement()
		{
			return CodeChartHelper.GetRange(7552, 7615);
		}

		// Token: 0x06004E69 RID: 20073 RVA: 0x00111899 File Offset: 0x0010FA99
		public static IEnumerable CombiningDiacriticalMarksSupplement()
		{
			return CodeChartHelper.GetRange(7616, 7679, (int i) => i >= 7655 && i <= 7676);
		}

		// Token: 0x06004E6A RID: 20074 RVA: 0x001118C9 File Offset: 0x0010FAC9
		public static IEnumerable LatinExtendedAdditional()
		{
			return CodeChartHelper.GetRange(7680, 7935);
		}
	}
}
