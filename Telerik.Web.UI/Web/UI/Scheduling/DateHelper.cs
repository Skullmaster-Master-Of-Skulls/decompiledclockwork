using System;

namespace Telerik.Web.UI.Scheduling
{
	// Token: 0x02001324 RID: 4900
	internal static class DateHelper
	{
		// Token: 0x0600CCBA RID: 52410 RVA: 0x002DA228 File Offset: 0x002D8428
		public static DateTime GetStartOfWeek(DateTime selectedDate, DayOfWeek weekStart)
		{
			int num = (int)selectedDate.DayOfWeek;
			int num2 = 0;
			while (num != (int)weekStart)
			{
				if (num == 0)
				{
					num = 6;
				}
				else
				{
					num--;
				}
				num2++;
			}
			return new DateTime(selectedDate.Subtract(TimeSpan.FromDays((double)num2)).Ticks, selectedDate.Kind);
		}

		// Token: 0x0600CCBB RID: 52411 RVA: 0x002DA278 File Offset: 0x002D8478
		public static DateTime GetEndOfWeek(DateTime selectedDate, DayOfWeek weekStart, int numDays)
		{
			return new DateTime(DateHelper.GetStartOfWeek(selectedDate, weekStart).AddDays((double)numDays).Ticks, selectedDate.Kind);
		}

		// Token: 0x0600CCBC RID: 52412 RVA: 0x002DA2AC File Offset: 0x002D84AC
		public static DateTime GetFirstDayOfMonth(DateTime date)
		{
			DateTime dateTime = new DateTime(date.Year, date.Month, 1);
			return new DateTime(dateTime.Ticks, date.Kind);
		}

		// Token: 0x0600CCBD RID: 52413 RVA: 0x002DA2E4 File Offset: 0x002D84E4
		public static DateTime GetLastDayOfMonth(DateTime date)
		{
			DateTime dateTime = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
			return new DateTime(dateTime.Ticks, date.Kind);
		}

		// Token: 0x0600CCBE RID: 52414 RVA: 0x002DA32C File Offset: 0x002D852C
		public static DateTime GetFirstDayOfYear(DateTime date)
		{
			DateTime dateTime = new DateTime(date.Year, 1, 1);
			return new DateTime(dateTime.Ticks, date.Kind);
		}

		// Token: 0x0600CCBF RID: 52415 RVA: 0x002DA35C File Offset: 0x002D855C
		public static DateTime AssumeUtc(DateTime date)
		{
			return DateTime.SpecifyKind(date, DateTimeKind.Utc);
		}

		// Token: 0x0600CCC0 RID: 52416 RVA: 0x002DA365 File Offset: 0x002D8565
		public static DateTime AssumeUnspecified(DateTime date)
		{
			return DateTime.SpecifyKind(date, DateTimeKind.Unspecified);
		}

		// Token: 0x0600CCC1 RID: 52417 RVA: 0x002DA370 File Offset: 0x002D8570
		public static int GetWeekLength(DateTime date, DayOfWeek firstDayOfWeek, DayOfWeek lastDayOfWeek)
		{
			DateTime startOfWeek = DateHelper.GetStartOfWeek(date, firstDayOfWeek);
			DateTime d = startOfWeek;
			while (d.DayOfWeek != lastDayOfWeek)
			{
				d = d.AddDays(1.0);
			}
			return (int)(d - startOfWeek).TotalDays + 1;
		}

		// Token: 0x0600CCC2 RID: 52418 RVA: 0x002DA3B8 File Offset: 0x002D85B8
		public static DateTime GetStartOfDay(DateTime dateStart, TimeSpan effectiveDayStartTime, ITimeZoneModel model)
		{
			DateTime dateTime = dateStart.Add(effectiveDayStartTime);
			if (model.IsTransitionFrame(dateTime, dateStart))
			{
				TimeSpan ts = model.GetUtcOffset(dateTime) - model.GetUtcOffset(dateStart);
				effectiveDayStartTime = effectiveDayStartTime.Subtract(ts);
			}
			return dateStart.Add(effectiveDayStartTime);
		}
	}
}
