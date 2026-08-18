using System;
using System.Runtime.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x020012E4 RID: 4836
	[Serializable]
	public class DailyRecurrenceRule : RecurrenceRule
	{
		// Token: 0x0600CB1B RID: 51995 RVA: 0x002D6C24 File Offset: 0x002D4E24
		private DailyRecurrenceRule(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600CB1C RID: 51996 RVA: 0x002D6C2E File Offset: 0x002D4E2E
		public DailyRecurrenceRule(int interval, RecurrenceRange range) : this(interval, RecurrenceDay.EveryDay, range)
		{
		}

		// Token: 0x0600CB1D RID: 51997 RVA: 0x002D6C3A File Offset: 0x002D4E3A
		public DailyRecurrenceRule(RecurrenceDay daysOfWeekMask, RecurrenceRange range) : this(1, daysOfWeekMask, range)
		{
		}

		// Token: 0x0600CB1E RID: 51998 RVA: 0x002D6C48 File Offset: 0x002D4E48
		private DailyRecurrenceRule(int interval, RecurrenceDay daysOfWeekMask, RecurrenceRange range)
		{
			this.rulePattern.Frequency = RecurrenceFrequency.Daily;
			this.rulePattern.Interval = interval;
			this.rulePattern.DaysOfWeekMask = ((daysOfWeekMask == RecurrenceDay.None) ? RecurrenceDay.EveryDay : daysOfWeekMask);
			this.rulePattern.DayOfMonth = 0;
			this.rulePattern.DayOrdinal = 0;
			this.rulePattern.Month = RecurrenceMonth.None;
			this.ruleRange = range;
		}

		// Token: 0x1700418C RID: 16780
		// (get) Token: 0x0600CB1F RID: 51999 RVA: 0x002D6CB1 File Offset: 0x002D4EB1
		public int Interval
		{
			get
			{
				return this.rulePattern.Interval;
			}
		}

		// Token: 0x1700418D RID: 16781
		// (get) Token: 0x0600CB20 RID: 52000 RVA: 0x002D6CBE File Offset: 0x002D4EBE
		// (set) Token: 0x0600CB21 RID: 52001 RVA: 0x002D6CCB File Offset: 0x002D4ECB
		public RecurrenceDay DaysOfWeekMask
		{
			get
			{
				return this.rulePattern.DaysOfWeekMask;
			}
			set
			{
				this.rulePattern.DaysOfWeekMask = value;
			}
		}

		// Token: 0x0600CB22 RID: 52002 RVA: 0x002D6CDC File Offset: 0x002D4EDC
		protected override DateTime GetOccurrenceStart(int index)
		{
			return this.ruleRange.Start.Add(new TimeSpan(index * this.rulePattern.Interval, 0, 0, 0));
		}

		// Token: 0x0600CB23 RID: 52003 RVA: 0x002D6D11 File Offset: 0x002D4F11
		protected override bool MatchAdvancedPattern(DateTime start)
		{
			return base.MatchDayOfWeekMask(start);
		}
	}
}
