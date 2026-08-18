using System;
using System.Runtime.Serialization;
using Telerik.Web.UI.Scheduling;

namespace Telerik.Web.UI
{
	// Token: 0x020012E6 RID: 4838
	[Serializable]
	public class MonthlyRecurrenceRule : RecurrenceRule
	{
		// Token: 0x0600CB29 RID: 52009 RVA: 0x002D6DCB File Offset: 0x002D4FCB
		private MonthlyRecurrenceRule(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600CB2A RID: 52010 RVA: 0x002D6DD5 File Offset: 0x002D4FD5
		public MonthlyRecurrenceRule(int dayOfMonth, int interval, RecurrenceRange range) : this(dayOfMonth, interval, 0, RecurrenceDay.None, range)
		{
		}

		// Token: 0x0600CB2B RID: 52011 RVA: 0x002D6DE2 File Offset: 0x002D4FE2
		public MonthlyRecurrenceRule(int dayOrdinal, RecurrenceDay daysOfWeekMask, int interval, RecurrenceRange range) : this(0, interval, dayOrdinal, daysOfWeekMask, range)
		{
		}

		// Token: 0x0600CB2C RID: 52012 RVA: 0x002D6DF0 File Offset: 0x002D4FF0
		private MonthlyRecurrenceRule(int dayOfMonth, int interval, int dayOrdinal, RecurrenceDay daysOfWeekMask, RecurrenceRange range)
		{
			this.rulePattern.Frequency = RecurrenceFrequency.Monthly;
			this.rulePattern.Interval = interval;
			this.rulePattern.DaysOfWeekMask = daysOfWeekMask;
			this.rulePattern.DayOfMonth = dayOfMonth;
			this.rulePattern.DayOrdinal = dayOrdinal;
			this.rulePattern.Month = RecurrenceMonth.None;
			this.ruleRange = range;
		}

		// Token: 0x1700418F RID: 16783
		// (get) Token: 0x0600CB2D RID: 52013 RVA: 0x002D6E54 File Offset: 0x002D5054
		public int DayOfMonth
		{
			get
			{
				return this.rulePattern.DayOfMonth;
			}
		}

		// Token: 0x17004190 RID: 16784
		// (get) Token: 0x0600CB2E RID: 52014 RVA: 0x002D6E61 File Offset: 0x002D5061
		public int DayOrdinal
		{
			get
			{
				return this.rulePattern.DayOrdinal;
			}
		}

		// Token: 0x17004191 RID: 16785
		// (get) Token: 0x0600CB2F RID: 52015 RVA: 0x002D6E6E File Offset: 0x002D506E
		public RecurrenceMonth Month
		{
			get
			{
				return this.rulePattern.Month;
			}
		}

		// Token: 0x17004192 RID: 16786
		// (get) Token: 0x0600CB30 RID: 52016 RVA: 0x002D6E7B File Offset: 0x002D507B
		public int Interval
		{
			get
			{
				return this.rulePattern.Interval;
			}
		}

		// Token: 0x0600CB31 RID: 52017 RVA: 0x002D6E88 File Offset: 0x002D5088
		protected override DateTime GetOccurrenceStart(int index)
		{
			return this.ruleRange.Start.AddDays((double)index);
		}

		// Token: 0x0600CB32 RID: 52018 RVA: 0x002D6EAC File Offset: 0x002D50AC
		protected override bool MatchAdvancedPattern(DateTime start)
		{
			if (this.GetMonthIndex(start) % this.rulePattern.Interval != 0)
			{
				return false;
			}
			bool flag = 0 < this.rulePattern.DayOfMonth;
			if (flag)
			{
				int day = DateHelper.GetLastDayOfMonth(start).Day;
				bool flag2 = this.rulePattern.DayOfMonth > day;
				return start.Day == (flag2 ? day : this.rulePattern.DayOfMonth);
			}
			return base.MatchDayOfWeekMask(start) && base.MatchDayOrdinal(start);
		}

		// Token: 0x0600CB33 RID: 52019 RVA: 0x002D6F34 File Offset: 0x002D5134
		private int GetMonthIndex(DateTime start)
		{
			if (start < this.ruleRange.Start)
			{
				return 0;
			}
			int num = start.Month - this.ruleRange.Start.Month;
			int num2 = start.Year - this.ruleRange.Start.Year;
			return num + num2 * 12;
		}
	}
}
