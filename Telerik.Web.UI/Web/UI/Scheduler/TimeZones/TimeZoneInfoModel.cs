using System;

namespace Telerik.Web.UI.Scheduler.TimeZones
{
	// Token: 0x02000E6D RID: 3693
	internal class TimeZoneInfoModel : ITimeZoneModel
	{
		// Token: 0x17002C4A RID: 11338
		// (get) Token: 0x06008C0E RID: 35854 RVA: 0x001FCE9F File Offset: 0x001FB09F
		// (set) Token: 0x06008C0F RID: 35855 RVA: 0x001FCEA7 File Offset: 0x001FB0A7
		public string TimeZoneId { get; set; }

		// Token: 0x17002C4B RID: 11339
		// (get) Token: 0x06008C10 RID: 35856 RVA: 0x001FCEB0 File Offset: 0x001FB0B0
		// (set) Token: 0x06008C11 RID: 35857 RVA: 0x001FCEB8 File Offset: 0x001FB0B8
		public string DisplayName { get; set; }

		// Token: 0x17002C4C RID: 11340
		// (get) Token: 0x06008C12 RID: 35858 RVA: 0x001FCEC1 File Offset: 0x001FB0C1
		// (set) Token: 0x06008C13 RID: 35859 RVA: 0x001FCEC9 File Offset: 0x001FB0C9
		public string StandardName { get; set; }

		// Token: 0x17002C4D RID: 11341
		// (get) Token: 0x06008C14 RID: 35860 RVA: 0x001FCED2 File Offset: 0x001FB0D2
		// (set) Token: 0x06008C15 RID: 35861 RVA: 0x001FCEDA File Offset: 0x001FB0DA
		public TimeSpan BaseUtcOffset { get; set; }

		// Token: 0x17002C4E RID: 11342
		// (get) Token: 0x06008C16 RID: 35862 RVA: 0x001FCEE3 File Offset: 0x001FB0E3
		// (set) Token: 0x06008C17 RID: 35863 RVA: 0x001FCEEB File Offset: 0x001FB0EB
		public bool SupportsDayLightSaving { get; set; }

		// Token: 0x17002C4F RID: 11343
		// (get) Token: 0x06008C18 RID: 35864 RVA: 0x001FCEF4 File Offset: 0x001FB0F4
		// (set) Token: 0x06008C19 RID: 35865 RVA: 0x001FCEFC File Offset: 0x001FB0FC
		public TimeZoneInfo.AdjustmentRule[] AdjustmentRules { get; set; }

		// Token: 0x06008C1A RID: 35866 RVA: 0x001FCF08 File Offset: 0x001FB108
		public TimeZoneInfoModel(TimeZoneInfo info)
		{
			this.TimeZoneInfo = info;
			this.AdjustmentRules = info.GetAdjustmentRules();
			this.TimeZoneId = info.Id;
			this.DisplayName = info.DisplayName;
			this.StandardName = info.StandardName;
			this.BaseUtcOffset = info.BaseUtcOffset;
			this.SupportsDayLightSaving = info.SupportsDaylightSavingTime;
		}

		// Token: 0x06008C1B RID: 35867 RVA: 0x001FCF6A File Offset: 0x001FB16A
		public TimeSpan GetUtcOffset(DateTime date)
		{
			return this.TimeZoneInfo.GetUtcOffset(date);
		}

		// Token: 0x06008C1C RID: 35868 RVA: 0x001FCF78 File Offset: 0x001FB178
		public bool IsUsingDayLightSaving(DateTime date)
		{
			return this.TimeZoneInfo.IsDaylightSavingTime(date);
		}

		// Token: 0x06008C1D RID: 35869 RVA: 0x001FCF86 File Offset: 0x001FB186
		public bool IsDefaultZone()
		{
			return this.DisplayName.ToLowerInvariant() == "UTC".ToLowerInvariant();
		}

		// Token: 0x06008C1E RID: 35870 RVA: 0x001FCFA2 File Offset: 0x001FB1A2
		public TimeSpan GetTransitionDelta(DateTime rangeStart, DateTime rangeEnd)
		{
			return this.GetUtcOffset(rangeStart) - this.GetUtcOffset(rangeEnd);
		}

		// Token: 0x06008C1F RID: 35871 RVA: 0x001FCFB7 File Offset: 0x001FB1B7
		public bool IsTransitionFrame(DateTime start, DateTime end)
		{
			return this.IsUsingDayLightSaving(start) != this.IsUsingDayLightSaving(end);
		}

		// Token: 0x06008C20 RID: 35872 RVA: 0x001FCFCC File Offset: 0x001FB1CC
		public bool IsTransitionTimeSlot(DateTime timeSlot)
		{
			return this.IsTransitionFrame(timeSlot.AddHours(-1.0), timeSlot);
		}

		// Token: 0x06008C21 RID: 35873 RVA: 0x001FCFE8 File Offset: 0x001FB1E8
		public TimeZoneInfo.AdjustmentRule GetAdjustmentRuleForDate(DateTime date)
		{
			foreach (TimeZoneInfo.AdjustmentRule adjustmentRule in this.AdjustmentRules)
			{
				if (adjustmentRule.DateStart < date && adjustmentRule.DateEnd > date)
				{
					return adjustmentRule;
				}
			}
			return this.AdjustmentRules[0];
		}

		// Token: 0x06008C22 RID: 35874 RVA: 0x001FD038 File Offset: 0x001FB238
		public DateTime GetTransitionStart(TimeZoneInfo.AdjustmentRule rule)
		{
			return this.GetTransitionTime(rule.DaylightTransitionStart);
		}

		// Token: 0x06008C23 RID: 35875 RVA: 0x001FD046 File Offset: 0x001FB246
		public DateTime GetTransitionEnd(TimeZoneInfo.AdjustmentRule rule)
		{
			return this.GetTransitionTime(rule.DaylightTransitionEnd);
		}

		// Token: 0x06008C24 RID: 35876 RVA: 0x001FD054 File Offset: 0x001FB254
		private DateTime GetTransitionTime(TimeZoneInfo.TransitionTime transitionTime)
		{
			int month = transitionTime.Month;
			int num = DateTime.DaysInMonth(DateTime.Now.Year, month);
			int num2 = 1;
			DateTime result = new DateTime(1601, month, 1);
			if (transitionTime.IsFixedDateRule)
			{
				result = result.AddDays((double)(transitionTime.Day - 1));
			}
			else
			{
				while (result.Day < num && (transitionTime.Week != num2 || result.DayOfWeek != transitionTime.DayOfWeek))
				{
					result = result.AddDays(1.0);
					if (result.DayOfWeek % (DayOfWeek)7 == DayOfWeek.Sunday)
					{
						num2++;
					}
				}
			}
			result = result.AddTicks(transitionTime.TimeOfDay.Ticks);
			return result;
		}

		// Token: 0x04002759 RID: 10073
		private readonly TimeZoneInfo TimeZoneInfo;
	}
}
