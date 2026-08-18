using System;

namespace System.IdentityModel
{
	// Token: 0x02000032 RID: 50
	internal static class DateTimeUtil
	{
		// Token: 0x0600018F RID: 399 RVA: 0x00007D30 File Offset: 0x00005F30
		public static DateTime Add(DateTime time, TimeSpan timespan)
		{
			if (timespan >= TimeSpan.Zero && DateTime.MaxValue - time <= timespan)
			{
				return DateTimeUtil.GetMaxValue(time.Kind);
			}
			if (timespan <= TimeSpan.Zero && DateTime.MinValue - time >= timespan)
			{
				return DateTimeUtil.GetMinValue(time.Kind);
			}
			return time + timespan;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00007D9E File Offset: 0x00005F9E
		public static DateTime AddNonNegative(DateTime time, TimeSpan timespan)
		{
			if (timespan <= TimeSpan.Zero)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ID2082")));
			}
			return DateTimeUtil.Add(time, timespan);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00007DD0 File Offset: 0x00005FD0
		public static DateTime GetMaxValue(DateTimeKind kind)
		{
			return new DateTime(DateTime.MaxValue.Ticks, kind);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00007DF0 File Offset: 0x00005FF0
		public static DateTime GetMinValue(DateTimeKind kind)
		{
			return new DateTime(DateTime.MinValue.Ticks, kind);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00007E10 File Offset: 0x00006010
		public static DateTime? ToUniversalTime(DateTime? value)
		{
			if (value == null || value.Value.Kind == DateTimeKind.Utc)
			{
				return value;
			}
			return new DateTime?(DateTimeUtil.ToUniversalTime(value.Value));
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00007E4B File Offset: 0x0000604B
		public static DateTime ToUniversalTime(DateTime value)
		{
			if (value.Kind == DateTimeKind.Utc)
			{
				return value;
			}
			return value.ToUniversalTime();
		}
	}
}
