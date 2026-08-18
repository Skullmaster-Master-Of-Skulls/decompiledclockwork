using System;

namespace Oracle.DataAccess.Types
{
	// Token: 0x020000F8 RID: 248
	internal class Interval
	{
		// Token: 0x060008F9 RID: 2297 RVA: 0x00058B9F File Offset: 0x00057B9F
		private Interval()
		{
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x00058BA7 File Offset: 0x00057BA7
		internal static bool IsValidYears(int years)
		{
			return years >= -999999999 && years <= 999999999;
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x00058BBC File Offset: 0x00057BBC
		internal static bool IsValidMonths(int months)
		{
			return months >= -11 && months <= 11;
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x00058BCC File Offset: 0x00057BCC
		internal static bool IsValidYMMonths(long ymMonths)
		{
			long num = 11999999999L;
			long num2 = -((long)Math.Abs(-999999999) * 12L + (long)Math.Abs(-11));
			return ymMonths >= num2 && ymMonths <= num;
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x00058C08 File Offset: 0x00057C08
		internal static bool IsValidYMYears(double years)
		{
			return years > -1000000000.0 && years < 1000000000.0;
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x00058C25 File Offset: 0x00057C25
		internal static bool IsValidYM(int years, int months)
		{
			return Interval.IsValidYears(years) && Interval.IsValidMonths(months);
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x00058C3C File Offset: 0x00057C3C
		internal static bool IsValidDays(int days)
		{
			return days >= -999999999 && days <= 999999999;
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x00058C51 File Offset: 0x00057C51
		internal static bool IsValidHours(int hours)
		{
			return hours >= -23 && hours <= 23;
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x00058C60 File Offset: 0x00057C60
		internal static bool IsValidMinutes(int minutes)
		{
			return minutes >= -59 && minutes <= 59;
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x00058C6F File Offset: 0x00057C6F
		internal static bool IsValidSeconds(int seconds)
		{
			return seconds >= -59 && seconds <= 59;
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x00058C7E File Offset: 0x00057C7E
		internal static bool IsValidNanoseconds(int nanoseconds)
		{
			return nanoseconds >= -999999999 && nanoseconds <= 999999999;
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x00058C93 File Offset: 0x00057C93
		internal static bool IsValidDS(int days, int hours, int minutes, int seconds, int nanoseconds)
		{
			return Interval.IsValidDays(days) && Interval.IsValidHours(hours) && Interval.IsValidMinutes(minutes) && Interval.IsValidSeconds(seconds) && Interval.IsValidNanoseconds(nanoseconds);
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x00058CC9 File Offset: 0x00057CC9
		internal static bool IsValidDSDays(double days)
		{
			return days > -1000000000.0 && days < 1000000000.0;
		}

		// Token: 0x04000814 RID: 2068
		internal const int MaxYears = 999999999;

		// Token: 0x04000815 RID: 2069
		internal const int TotalMaxYears = 1000000000;

		// Token: 0x04000816 RID: 2070
		internal const byte MaxMonths = 11;

		// Token: 0x04000817 RID: 2071
		internal const long TotalMaxMonths = 12000000000L;

		// Token: 0x04000818 RID: 2072
		internal const int MaxDays = 999999999;

		// Token: 0x04000819 RID: 2073
		internal const double TotalMaxDays = 1000000000.0;

		// Token: 0x0400081A RID: 2074
		internal const byte MaxHours = 23;

		// Token: 0x0400081B RID: 2075
		internal const double TotalMaxHours = 24000000000.0;

		// Token: 0x0400081C RID: 2076
		internal const byte MaxMinutes = 59;

		// Token: 0x0400081D RID: 2077
		internal const double TotalMaxMinutes = 1440000000000.0;

		// Token: 0x0400081E RID: 2078
		internal const byte MaxSeconds = 59;

		// Token: 0x0400081F RID: 2079
		internal const double TotalMaxSeconds = 86400000000000.0;

		// Token: 0x04000820 RID: 2080
		internal const double MaxMilliseconds = 999.999999;

		// Token: 0x04000821 RID: 2081
		internal const double TotalMaxMilliseconds = 86400000000000000.0;

		// Token: 0x04000822 RID: 2082
		internal const int MaxFSeconds = 999999999;

		// Token: 0x04000823 RID: 2083
		internal const int MinYears = -999999999;

		// Token: 0x04000824 RID: 2084
		internal const int TotalMinYears = -1000000000;

		// Token: 0x04000825 RID: 2085
		internal const short MinMonths = -11;

		// Token: 0x04000826 RID: 2086
		internal const long TotalMinMonths = -12000000000L;

		// Token: 0x04000827 RID: 2087
		internal const int MinDays = -999999999;

		// Token: 0x04000828 RID: 2088
		internal const double TotalMinDays = -1000000000.0;

		// Token: 0x04000829 RID: 2089
		internal const short MinHours = -23;

		// Token: 0x0400082A RID: 2090
		internal const double TotalMinHours = -24000000000.0;

		// Token: 0x0400082B RID: 2091
		internal const short MinMinutes = -59;

		// Token: 0x0400082C RID: 2092
		internal const double TotalMinMinutes = -1440000000000.0;

		// Token: 0x0400082D RID: 2093
		internal const short MinSeconds = -59;

		// Token: 0x0400082E RID: 2094
		internal const double TotalMinSeconds = -86400000000000.0;

		// Token: 0x0400082F RID: 2095
		internal const double MinMilliseconds = -999.999999;

		// Token: 0x04000830 RID: 2096
		internal const double TotalMinMilliseconds = -86400000000000000.0;

		// Token: 0x04000831 RID: 2097
		internal const int MinFSeconds = -999999999;

		// Token: 0x04000832 RID: 2098
		internal const byte MonthsPerYear = 12;

		// Token: 0x04000833 RID: 2099
		internal const byte MaxYearPrec = 9;

		// Token: 0x04000834 RID: 2100
		internal const byte MaxDayPrec = 9;

		// Token: 0x04000835 RID: 2101
		internal const byte MaxFSecondPrec = 9;

		// Token: 0x04000836 RID: 2102
		internal const byte MinYearPrec = 0;

		// Token: 0x04000837 RID: 2103
		internal const byte MinDayPrec = 0;

		// Token: 0x04000838 RID: 2104
		internal const byte MinFSecondPrec = 0;
	}
}
