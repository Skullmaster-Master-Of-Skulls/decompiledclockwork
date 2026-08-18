using System;
using System.Runtime.Serialization;
using Telerik.Web.UI.Scheduling;

namespace Telerik.Web.UI
{
	// Token: 0x020012EE RID: 4846
	[Serializable]
	public class YearlyRecurrenceRule : RecurrenceRule
	{
		// Token: 0x0600CB66 RID: 52070 RVA: 0x002D75A7 File Offset: 0x002D57A7
		private YearlyRecurrenceRule(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600CB67 RID: 52071 RVA: 0x002D75B1 File Offset: 0x002D57B1
		public YearlyRecurrenceRule(RecurrenceMonth month, int dayOfMonth, RecurrenceRange range) : this(month, dayOfMonth, 0, RecurrenceDay.EveryDay, range, 1)
		{
		}

		// Token: 0x0600CB68 RID: 52072 RVA: 0x002D75C0 File Offset: 0x002D57C0
		public YearlyRecurrenceRule(int dayOrdinal, RecurrenceMonth month, RecurrenceDay daysOfWeekMask, RecurrenceRange range) : this(month, -1, dayOrdinal, daysOfWeekMask, range, 1)
		{
		}

		// Token: 0x0600CB69 RID: 52073 RVA: 0x002D75CF File Offset: 0x002D57CF
		public YearlyRecurrenceRule(RecurrenceMonth month, int dayOfMonth, RecurrenceRange range, int interval) : this(month, dayOfMonth, 0, RecurrenceDay.EveryDay, range, interval)
		{
		}

		// Token: 0x0600CB6A RID: 52074 RVA: 0x002D75DF File Offset: 0x002D57DF
		public YearlyRecurrenceRule(int dayOrdinal, RecurrenceMonth month, RecurrenceDay daysOfWeekMask, RecurrenceRange range, int interval) : this(month, -1, dayOrdinal, daysOfWeekMask, range, interval)
		{
		}

		// Token: 0x0600CB6B RID: 52075 RVA: 0x002D75F0 File Offset: 0x002D57F0
		private YearlyRecurrenceRule(RecurrenceMonth month, int dayOfMonth, int dayOrdinal, RecurrenceDay daysOfWeekMask, RecurrenceRange range, int interval)
		{
			this.rulePattern.Frequency = RecurrenceFrequency.Yearly;
			this.rulePattern.Interval = interval;
			this.rulePattern.DaysOfWeekMask = daysOfWeekMask;
			this.rulePattern.DayOfMonth = dayOfMonth;
			this.rulePattern.DayOrdinal = dayOrdinal;
			this.rulePattern.Month = month;
			this.ruleRange = range;
		}

		// Token: 0x170041A0 RID: 16800
		// (get) Token: 0x0600CB6C RID: 52076 RVA: 0x002D7655 File Offset: 0x002D5855
		public int DayOfMonth
		{
			get
			{
				return this.rulePattern.DayOfMonth;
			}
		}

		// Token: 0x170041A1 RID: 16801
		// (get) Token: 0x0600CB6D RID: 52077 RVA: 0x002D7662 File Offset: 0x002D5862
		public int DayOrdinal
		{
			get
			{
				return this.rulePattern.DayOrdinal;
			}
		}

		// Token: 0x170041A2 RID: 16802
		// (get) Token: 0x0600CB6E RID: 52078 RVA: 0x002D766F File Offset: 0x002D586F
		public RecurrenceMonth Month
		{
			get
			{
				return this.rulePattern.Month;
			}
		}

		// Token: 0x170041A3 RID: 16803
		// (get) Token: 0x0600CB6F RID: 52079 RVA: 0x002D767C File Offset: 0x002D587C
		public RecurrenceDay DaysOfWeekMask
		{
			get
			{
				return this.rulePattern.DaysOfWeekMask;
			}
		}

		// Token: 0x170041A4 RID: 16804
		// (get) Token: 0x0600CB70 RID: 52080 RVA: 0x002D7689 File Offset: 0x002D5889
		public int Interval
		{
			get
			{
				return this.rulePattern.Interval;
			}
		}

		// Token: 0x0600CB71 RID: 52081 RVA: 0x002D7698 File Offset: 0x002D5898
		protected override DateTime GetOccurrenceStart(int index)
		{
			int month = (int)this.rulePattern.Month;
			DateTime result = this.ruleRange.Start;
			if (result.Month != month)
			{
				int year = (result.Month < month) ? result.Year : (result.Year + this.rulePattern.Interval);
				result = new DateTime(year, month, 1, result.Hour, result.Minute, result.Second, 0, result.Kind);
			}
			if (0 < this.rulePattern.DayOfMonth)
			{
				int num = result.Year + index * this.rulePattern.Interval;
				int dayOfMonth = this.rulePattern.DayOfMonth;
				if (result.Month == month && dayOfMonth < result.Day)
				{
					num++;
				}
				int day = DateHelper.GetLastDayOfMonth(new DateTime(num, month, 1)).Day;
				return new DateTime(num, month, Math.Min(this.rulePattern.DayOfMonth, day), result.Hour, result.Minute, result.Second, 0, result.Kind);
			}
			for (int i = 0; i < index; i++)
			{
				int year2 = result.Year + this.rulePattern.Interval;
				result = result.AddDays(1.0);
				if (result.Month != month)
				{
					result = new DateTime(year2, month, 1, result.Hour, result.Minute, result.Second, 0, result.Kind);
				}
			}
			return result;
		}

		// Token: 0x0600CB72 RID: 52082 RVA: 0x002D781C File Offset: 0x002D5A1C
		protected override bool MatchAdvancedPattern(DateTime start)
		{
			bool flag = 0 < this.rulePattern.DayOfMonth;
			if (flag && start.Day != this.rulePattern.DayOfMonth)
			{
				int day = DateHelper.GetLastDayOfMonth(start).Day;
				if (this.rulePattern.DayOfMonth <= day)
				{
					return false;
				}
			}
			return base.MatchDayOfWeekMask(start) && base.MatchDayOrdinal(start);
		}
	}
}
