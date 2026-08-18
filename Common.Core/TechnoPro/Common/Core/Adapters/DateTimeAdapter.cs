using System;

namespace TechnoPro.Common.Core.Adapters
{
	// Token: 0x0200016D RID: 365
	public static class DateTimeAdapter
	{
		// Token: 0x06001035 RID: 4149 RVA: 0x00076ECC File Offset: 0x000750CC
		public static int CompareTimeSpans(this TimeSpan ts1, TimeSpan ts2)
		{
			return ts1.TotalMinutes.CompareTo(ts2.TotalMinutes);
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x00076EF4 File Offset: 0x000750F4
		public static string TimeSpanToString(this TimeSpan ts, string dateTimeFormatString)
		{
			return DateTime.Now.Date.Add(ts).ToString(dateTimeFormatString);
		}

		// Token: 0x06001037 RID: 4151 RVA: 0x00076F28 File Offset: 0x00075128
		public static DateTime? SkipWeekendsIfNecessary(this DateTime? dateTime, bool skipWeekends)
		{
			bool flag = !skipWeekends || dateTime == null;
			DateTime? result;
			if (flag)
			{
				result = dateTime;
			}
			else
			{
				DayOfWeek dayOfWeek = dateTime.Value.DayOfWeek;
				bool flag2 = dayOfWeek == DayOfWeek.Saturday;
				if (flag2)
				{
					result = new DateTime?(dateTime.Value.AddDays(2.0));
				}
				else
				{
					bool flag3 = dayOfWeek == DayOfWeek.Sunday;
					if (flag3)
					{
						result = new DateTime?(dateTime.Value.AddDays(1.0));
					}
					else
					{
						result = dateTime;
					}
				}
			}
			return result;
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x00076FB8 File Offset: 0x000751B8
		public static DateTime SkipWeekendsIfNecessary(this DateTime dateTime, bool skipWeekends)
		{
			bool flag = !skipWeekends || dateTime == DateTime.MinValue;
			DateTime result;
			if (flag)
			{
				result = dateTime;
			}
			else
			{
				DayOfWeek dayOfWeek = dateTime.DayOfWeek;
				bool flag2 = dayOfWeek == DayOfWeek.Saturday;
				if (flag2)
				{
					result = dateTime.AddDays(2.0);
				}
				else
				{
					bool flag3 = dayOfWeek == DayOfWeek.Sunday;
					if (flag3)
					{
						result = dateTime.AddDays(1.0);
					}
					else
					{
						result = dateTime;
					}
				}
			}
			return result;
		}
	}
}
