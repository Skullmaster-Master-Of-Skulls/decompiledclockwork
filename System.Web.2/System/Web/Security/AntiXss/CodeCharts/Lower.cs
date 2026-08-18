using System;
using System.Collections;

namespace System.Web.Security.AntiXss.CodeCharts
{
	// Token: 0x0200061F RID: 1567
	internal static class Lower
	{
		// Token: 0x06004E2B RID: 20011 RVA: 0x0011107C File Offset: 0x0010F27C
		public static bool IsFlagSet(LowerCodeCharts flags, LowerCodeCharts flagToCheck)
		{
			return (flags & flagToCheck) > LowerCodeCharts.None;
		}

		// Token: 0x06004E2C RID: 20012 RVA: 0x00111085 File Offset: 0x0010F285
		public static IEnumerable BasicLatin()
		{
			return CodeChartHelper.GetRange(32, 126);
		}

		// Token: 0x06004E2D RID: 20013 RVA: 0x00111090 File Offset: 0x0010F290
		public static IEnumerable Latin1Supplement()
		{
			return CodeChartHelper.GetRange(161, 255, (int i) => i == 173);
		}

		// Token: 0x06004E2E RID: 20014 RVA: 0x001110C0 File Offset: 0x0010F2C0
		public static IEnumerable LatinExtendedA()
		{
			return CodeChartHelper.GetRange(256, 383);
		}

		// Token: 0x06004E2F RID: 20015 RVA: 0x001110D1 File Offset: 0x0010F2D1
		public static IEnumerable LatinExtendedB()
		{
			return CodeChartHelper.GetRange(384, 591);
		}

		// Token: 0x06004E30 RID: 20016 RVA: 0x001110E2 File Offset: 0x0010F2E2
		public static IEnumerable IpaExtensions()
		{
			return CodeChartHelper.GetRange(592, 687);
		}

		// Token: 0x06004E31 RID: 20017 RVA: 0x001110F3 File Offset: 0x0010F2F3
		public static IEnumerable SpacingModifierLetters()
		{
			return CodeChartHelper.GetRange(688, 767);
		}

		// Token: 0x06004E32 RID: 20018 RVA: 0x00111104 File Offset: 0x0010F304
		public static IEnumerable CombiningDiacriticalMarks()
		{
			return CodeChartHelper.GetRange(768, 879);
		}

		// Token: 0x06004E33 RID: 20019 RVA: 0x00111115 File Offset: 0x0010F315
		public static IEnumerable GreekAndCoptic()
		{
			return CodeChartHelper.GetRange(880, 1023, (int i) => i == 888 || i == 889 || (i >= 895 && i <= 899) || i == 907 || i == 909 || i == 930);
		}

		// Token: 0x06004E34 RID: 20020 RVA: 0x00111145 File Offset: 0x0010F345
		public static IEnumerable Cyrillic()
		{
			return CodeChartHelper.GetRange(1024, 1279);
		}

		// Token: 0x06004E35 RID: 20021 RVA: 0x00111156 File Offset: 0x0010F356
		public static IEnumerable CyrillicSupplement()
		{
			return CodeChartHelper.GetRange(1280, 1317);
		}

		// Token: 0x06004E36 RID: 20022 RVA: 0x00111167 File Offset: 0x0010F367
		public static IEnumerable Armenian()
		{
			return CodeChartHelper.GetRange(1329, 1418, (int i) => i == 1367 || i == 1368 || i == 1376 || i == 1416);
		}

		// Token: 0x06004E37 RID: 20023 RVA: 0x00111197 File Offset: 0x0010F397
		public static IEnumerable Hebrew()
		{
			return CodeChartHelper.GetRange(1425, 1524, (int i) => (i >= 1480 && i <= 1487) || (i >= 1515 && i <= 1519));
		}

		// Token: 0x06004E38 RID: 20024 RVA: 0x001111C7 File Offset: 0x0010F3C7
		public static IEnumerable Arabic()
		{
			return CodeChartHelper.GetRange(1536, 1791, (int i) => i == 1540 || i == 1541 || i == 1564 || i == 1565 || i == 1568 || i == 1631);
		}

		// Token: 0x06004E39 RID: 20025 RVA: 0x001111F7 File Offset: 0x0010F3F7
		public static IEnumerable Syriac()
		{
			return CodeChartHelper.GetRange(1792, 1871, (int i) => i == 1806 || i == 1867 || i == 1868);
		}

		// Token: 0x06004E3A RID: 20026 RVA: 0x00111227 File Offset: 0x0010F427
		public static IEnumerable ArabicSupplement()
		{
			return CodeChartHelper.GetRange(1872, 1919);
		}

		// Token: 0x06004E3B RID: 20027 RVA: 0x00111238 File Offset: 0x0010F438
		public static IEnumerable Thaana()
		{
			return CodeChartHelper.GetRange(1920, 1969);
		}

		// Token: 0x06004E3C RID: 20028 RVA: 0x00111249 File Offset: 0x0010F449
		public static IEnumerable Nko()
		{
			return CodeChartHelper.GetRange(1984, 2042);
		}

		// Token: 0x06004E3D RID: 20029 RVA: 0x0011125A File Offset: 0x0010F45A
		public static IEnumerable Samaritan()
		{
			return CodeChartHelper.GetRange(2048, 2110, (int i) => i == 2094 || i == 2095);
		}

		// Token: 0x06004E3E RID: 20030 RVA: 0x0011128A File Offset: 0x0010F48A
		public static IEnumerable Devanagari()
		{
			return CodeChartHelper.GetRange(2304, 2431, (int i) => i == 2362 || i == 2363 || i == 2383 || i == 2390 || i == 2391 || (i >= 2419 && i <= 2424));
		}

		// Token: 0x06004E3F RID: 20031 RVA: 0x001112BA File Offset: 0x0010F4BA
		public static IEnumerable Bengali()
		{
			return CodeChartHelper.GetRange(2433, 2555, (int i) => i == 2436 || i == 2445 || i == 2446 || i == 2449 || i == 2450 || i == 2473 || i == 2481 || i == 2483 || i == 2484 || i == 2485 || i == 2490 || i == 2491 || i == 2501 || i == 2502 || i == 2505 || i == 2506 || (i >= 2511 && i <= 2518) || (i >= 2520 && i <= 2523) || i == 2526 || i == 2532 || i == 2533);
		}

		// Token: 0x06004E40 RID: 20032 RVA: 0x001112EA File Offset: 0x0010F4EA
		public static IEnumerable Gurmukhi()
		{
			return CodeChartHelper.GetRange(2561, 2677, (int i) => i == 2564 || (i >= 2571 && i <= 2574) || i == 2577 || i == 2578 || i == 2601 || i == 2609 || i == 2612 || i == 2615 || i == 2618 || i == 2619 || i == 2621 || (i >= 2627 && i <= 2630) || i == 2633 || i == 2634 || (i >= 2638 && i <= 2640) || (i >= 2642 && i <= 2648) || i == 2653 || (i >= 2655 && i <= 2661));
		}

		// Token: 0x06004E41 RID: 20033 RVA: 0x0011131A File Offset: 0x0010F51A
		public static IEnumerable Gujarati()
		{
			return CodeChartHelper.GetRange(2689, 2801, (int i) => i == 2692 || i == 2702 || i == 2706 || i == 2729 || i == 2737 || i == 2740 || i == 2746 || i == 2747 || i == 2758 || i == 2762 || i == 2766 || i == 2767 || (i >= 2769 && i <= 2783) || i == 2788 || i == 2789 || i == 2800);
		}

		// Token: 0x06004E42 RID: 20034 RVA: 0x0011134A File Offset: 0x0010F54A
		public static IEnumerable Oriya()
		{
			return CodeChartHelper.GetRange(2817, 2929, (int i) => i == 2820 || i == 2829 || i == 2830 || i == 2833 || i == 2834 || i == 2857 || i == 2865 || i == 2868 || i == 2874 || i == 2875 || i == 2885 || i == 2886 || i == 2889 || i == 2890 || (i >= 2894 && i <= 2901) || (i >= 2904 && i <= 2907) || i == 2910 || i == 2916 || i == 2917);
		}

		// Token: 0x06004E43 RID: 20035 RVA: 0x0011137A File Offset: 0x0010F57A
		public static IEnumerable Tamil()
		{
			return CodeChartHelper.GetRange(2946, 3066, (int i) => i == 2948 || i == 2955 || i == 2956 || i == 2957 || i == 2961 || i == 2966 || i == 2967 || i == 2968 || i == 2971 || i == 2973 || i == 2976 || i == 2977 || i == 2978 || i == 2981 || i == 2982 || i == 2983 || i == 2987 || i == 2988 || i == 2989 || (i >= 3002 && i <= 3005) || i == 3011 || i == 3012 || i == 3013 || i == 3017 || i == 3022 || i == 3023 || (i >= 3025 && i <= 3030) || (i >= 3032 && i <= 3045));
		}

		// Token: 0x06004E44 RID: 20036 RVA: 0x001113AA File Offset: 0x0010F5AA
		public static IEnumerable Telugu()
		{
			return CodeChartHelper.GetRange(3073, 3199, (int i) => i == 3076 || i == 3085 || i == 3089 || i == 3113 || i == 3124 || i == 3130 || i == 3131 || i == 3132 || i == 3141 || i == 3145 || (i >= 3150 && i <= 3156) || i == 3159 || (i >= 3162 && i <= 3167) || i == 3172 || i == 3173 || (i >= 3184 && i <= 3191));
		}

		// Token: 0x06004E45 RID: 20037 RVA: 0x001113DA File Offset: 0x0010F5DA
		public static IEnumerable Kannada()
		{
			return CodeChartHelper.GetRange(3202, 3314, (int i) => i == 3204 || i == 3213 || i == 3217 || i == 3241 || i == 3252 || i == 3258 || i == 3259 || i == 3269 || i == 3273 || (i >= 3278 && i <= 3284) || (i >= 3287 && i <= 3293) || i == 3295 || i == 3300 || i == 3301 || i == 3312);
		}

		// Token: 0x06004E46 RID: 20038 RVA: 0x0011140A File Offset: 0x0010F60A
		public static IEnumerable Malayalam()
		{
			return CodeChartHelper.GetRange(3330, 3455, (int i) => i == 3332 || i == 3341 || i == 3345 || i == 3369 || i == 3386 || i == 3387 || i == 3388 || i == 3397 || i == 3401 || (i >= 3406 && i <= 3414) || (i >= 3416 && i <= 3423) || i == 3428 || i == 3429 || i == 3446 || i == 3447 || i == 3448);
		}

		// Token: 0x06004E47 RID: 20039 RVA: 0x0011143A File Offset: 0x0010F63A
		public static IEnumerable Sinhala()
		{
			return CodeChartHelper.GetRange(3458, 3572, (int i) => i == 3460 || i == 3479 || i == 3480 || i == 3481 || i == 3506 || i == 3516 || i == 3518 || i == 3519 || i == 3527 || i == 3528 || i == 3529 || (i >= 3531 && i <= 3534) || i == 3541 || i == 3543 || (i >= 3552 && i <= 3569));
		}

		// Token: 0x06004E48 RID: 20040 RVA: 0x0011146A File Offset: 0x0010F66A
		public static IEnumerable Thai()
		{
			return CodeChartHelper.GetRange(3585, 3675, (int i) => i >= 3643 && i <= 3646);
		}

		// Token: 0x06004E49 RID: 20041 RVA: 0x0011149A File Offset: 0x0010F69A
		public static IEnumerable Lao()
		{
			return CodeChartHelper.GetRange(3713, 3805, (int i) => i == 3715 || i == 3717 || i == 3718 || i == 3721 || i == 3723 || i == 3724 || (i >= 3726 && i <= 3731) || i == 3736 || i == 3744 || i == 3748 || i == 3750 || i == 3752 || i == 3753 || i == 3756 || i == 3770 || i == 3774 || i == 3775 || i == 3781 || i == 3783 || i == 3790 || i == 3791 || i == 3802 || i == 3803);
		}

		// Token: 0x06004E4A RID: 20042 RVA: 0x001114CA File Offset: 0x0010F6CA
		public static IEnumerable Tibetan()
		{
			return CodeChartHelper.GetRange(3840, 4056, (int i) => i == 3912 || (i >= 3949 && i <= 3952) || (i >= 3980 && i <= 3983) || i == 3992 || i == 4029 || i == 4045);
		}
	}
}
