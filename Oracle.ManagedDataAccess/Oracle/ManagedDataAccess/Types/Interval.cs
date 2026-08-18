using System;

namespace Oracle.ManagedDataAccess.Types
{
	// Token: 0x0200025F RID: 607
	internal static class Interval
	{
		// Token: 0x06001872 RID: 6258 RVA: 0x00102494 File Offset: 0x00100694
		internal static bool IsValidYears(int years)
		{
			return years >= -999999999 && years <= 999999999;
		}

		// Token: 0x06001873 RID: 6259 RVA: 0x001024AC File Offset: 0x001006AC
		internal static bool IsValidMonths(int months)
		{
			return months >= -11 && months <= 11;
		}

		// Token: 0x06001874 RID: 6260 RVA: 0x001024BC File Offset: 0x001006BC
		internal static bool IsValidYMMonths(long ymMonths)
		{
			long num = 11999999999L;
			long num2 = -((long)Math.Abs(-999999999) * 12L + (long)Math.Abs(-11));
			return ymMonths >= num2 && ymMonths <= num;
		}

		// Token: 0x06001875 RID: 6261 RVA: 0x001024F8 File Offset: 0x001006F8
		internal static bool IsValidYM(int years, int months)
		{
			return Interval.IsValidYears(years) && Interval.IsValidMonths(months);
		}

		// Token: 0x06001876 RID: 6262 RVA: 0x00102510 File Offset: 0x00100710
		internal static bool IsValidDays(int days)
		{
			return days >= -999999999 && days <= 999999999;
		}

		// Token: 0x06001877 RID: 6263 RVA: 0x00102528 File Offset: 0x00100728
		internal static bool IsValidHours(int hours)
		{
			return hours >= -23 && hours <= 23;
		}

		// Token: 0x06001878 RID: 6264 RVA: 0x00102538 File Offset: 0x00100738
		internal static bool IsValidMinutes(int minutes)
		{
			return minutes >= -59 && minutes <= 59;
		}

		// Token: 0x06001879 RID: 6265 RVA: 0x00102548 File Offset: 0x00100748
		internal static bool IsValidSeconds(int seconds)
		{
			return seconds >= -59 && seconds <= 59;
		}

		// Token: 0x0600187A RID: 6266 RVA: 0x00102558 File Offset: 0x00100758
		internal static bool IsValidNanoseconds(int nanoseconds)
		{
			return nanoseconds >= -999999999 && nanoseconds <= 999999999;
		}

		// Token: 0x0600187B RID: 6267 RVA: 0x00102570 File Offset: 0x00100770
		internal static bool IsValidDS(int days, int hours, int minutes, int seconds, int nanoseconds)
		{
			return Interval.IsValidDays(days) && Interval.IsValidHours(hours) && Interval.IsValidMinutes(minutes) && Interval.IsValidSeconds(seconds) && Interval.IsValidNanoseconds(nanoseconds);
		}

		// Token: 0x04001AB7 RID: 6839
		internal const int MaxYears = 999999999;

		// Token: 0x04001AB8 RID: 6840
		internal const int TotalMaxYears = 1000000000;

		// Token: 0x04001AB9 RID: 6841
		internal const byte MaxMonths = 11;

		// Token: 0x04001ABA RID: 6842
		internal const long TotalMaxMonths = 12000000000L;

		// Token: 0x04001ABB RID: 6843
		internal const int MaxDays = 999999999;

		// Token: 0x04001ABC RID: 6844
		internal const double TotalMaxDays = 1000000000.0;

		// Token: 0x04001ABD RID: 6845
		internal const byte MaxHours = 23;

		// Token: 0x04001ABE RID: 6846
		internal const double TotalMaxHours = 24000000000.0;

		// Token: 0x04001ABF RID: 6847
		internal const byte MaxMinutes = 59;

		// Token: 0x04001AC0 RID: 6848
		internal const double TotalMaxMinutes = 1440000000000.0;

		// Token: 0x04001AC1 RID: 6849
		internal const byte MaxSeconds = 59;

		// Token: 0x04001AC2 RID: 6850
		internal const double TotalMaxSeconds = 86400000000000.0;

		// Token: 0x04001AC3 RID: 6851
		internal const double MaxMilliseconds = 999.999999;

		// Token: 0x04001AC4 RID: 6852
		internal const double TotalMaxMilliseconds = 86400000000000000.0;

		// Token: 0x04001AC5 RID: 6853
		internal const int MaxFSeconds = 999999999;

		// Token: 0x04001AC6 RID: 6854
		internal const int MinYears = -999999999;

		// Token: 0x04001AC7 RID: 6855
		internal const int TotalMinYears = -1000000000;

		// Token: 0x04001AC8 RID: 6856
		internal const short MinMonths = -11;

		// Token: 0x04001AC9 RID: 6857
		internal const long TotalMinMonths = -12000000000L;

		// Token: 0x04001ACA RID: 6858
		internal const int MinDays = -999999999;

		// Token: 0x04001ACB RID: 6859
		internal const double TotalMinDays = -1000000000.0;

		// Token: 0x04001ACC RID: 6860
		internal const short MinHours = -23;

		// Token: 0x04001ACD RID: 6861
		internal const double TotalMinHours = -24000000000.0;

		// Token: 0x04001ACE RID: 6862
		internal const short MinMinutes = -59;

		// Token: 0x04001ACF RID: 6863
		internal const double TotalMinMinutes = -1440000000000.0;

		// Token: 0x04001AD0 RID: 6864
		internal const short MinSeconds = -59;

		// Token: 0x04001AD1 RID: 6865
		internal const double TotalMinSeconds = -86400000000000.0;

		// Token: 0x04001AD2 RID: 6866
		internal const double MinMilliseconds = -999.999999;

		// Token: 0x04001AD3 RID: 6867
		internal const double TotalMinMilliseconds = -86400000000000000.0;

		// Token: 0x04001AD4 RID: 6868
		internal const int MinFSeconds = -999999999;

		// Token: 0x04001AD5 RID: 6869
		internal const byte MonthsPerYear = 12;

		// Token: 0x04001AD6 RID: 6870
		internal const byte MaxYearPrec = 9;

		// Token: 0x04001AD7 RID: 6871
		internal const byte MaxDayPrec = 9;

		// Token: 0x04001AD8 RID: 6872
		internal const byte MaxFSecondPrec = 9;

		// Token: 0x04001AD9 RID: 6873
		internal const byte MinYearPrec = 0;

		// Token: 0x04001ADA RID: 6874
		internal const byte MinDayPrec = 0;

		// Token: 0x04001ADB RID: 6875
		internal const byte MinFSecondPrec = 0;

		// Token: 0x04001ADC RID: 6876
		internal const byte MaxSingleDigit = 9;

		// Token: 0x04001ADD RID: 6877
		internal const int HoursPerDay = 24;

		// Token: 0x04001ADE RID: 6878
		internal const int MinutesPerDay = 1440;

		// Token: 0x04001ADF RID: 6879
		internal const int SecondsPerDay = 86400;

		// Token: 0x04001AE0 RID: 6880
		internal const double FractionSecsPerDay = 86400000000000.0;
	}
}
