using System;

namespace System.Web.Util
{
	// Token: 0x020001F2 RID: 498
	internal sealed class DateTimeUtil
	{
		// Token: 0x060018BE RID: 6334 RVA: 0x000030B5 File Offset: 0x000012B5
		private DateTimeUtil()
		{
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x0004CB30 File Offset: 0x0004AD30
		internal static DateTime FromFileTimeToUtc(long filetime)
		{
			long ticks = filetime + 504911232000000000L;
			return new DateTime(ticks, DateTimeKind.Utc);
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x0004CB50 File Offset: 0x0004AD50
		internal static DateTime ConvertToUniversalTime(DateTime localTime)
		{
			if (localTime < DateTimeUtil.MinValuePlusOneDay)
			{
				return DateTime.MinValue;
			}
			if (localTime > DateTimeUtil.MaxValueMinusOneDay)
			{
				return DateTime.MaxValue;
			}
			return localTime.ToUniversalTime();
		}

		// Token: 0x060018C1 RID: 6337 RVA: 0x0004CB7F File Offset: 0x0004AD7F
		internal static DateTime ConvertToLocalTime(DateTime utcTime)
		{
			if (utcTime < DateTimeUtil.MinValuePlusOneDay)
			{
				return DateTime.MinValue;
			}
			if (utcTime > DateTimeUtil.MaxValueMinusOneDay)
			{
				return DateTime.MaxValue;
			}
			return utcTime.ToLocalTime();
		}

		// Token: 0x060018C2 RID: 6338 RVA: 0x0004CBB0 File Offset: 0x0004ADB0
		internal static TimeSpan GetTimeoutFromTimeUnit(int timeoutValue, TimeUnit timeoutUnit)
		{
			switch (timeoutUnit)
			{
			case TimeUnit.Days:
				return new TimeSpan(timeoutValue, 0, 0, 0);
			case TimeUnit.Hours:
				return new TimeSpan(timeoutValue, 0, 0);
			case TimeUnit.Minutes:
				return new TimeSpan(0, timeoutValue, 0);
			case TimeUnit.Seconds:
				return new TimeSpan(0, 0, timeoutValue);
			case TimeUnit.Milliseconds:
				return new TimeSpan(0, 0, 0, 0, timeoutValue);
			}
			throw new ArgumentException(SR.GetString("InvalidArgumentValue", new object[]
			{
				"timeoutUnit"
			}));
		}

		// Token: 0x0400178B RID: 6027
		private const long FileTimeOffset = 504911232000000000L;

		// Token: 0x0400178C RID: 6028
		private static readonly DateTime MinValuePlusOneDay = DateTime.MinValue.AddDays(1.0);

		// Token: 0x0400178D RID: 6029
		private static readonly DateTime MaxValueMinusOneDay = DateTime.MaxValue.AddDays(-1.0);
	}
}
