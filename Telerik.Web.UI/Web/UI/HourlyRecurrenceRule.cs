using System;
using System.Runtime.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x020012E5 RID: 4837
	[Serializable]
	public class HourlyRecurrenceRule : RecurrenceRule
	{
		// Token: 0x0600CB24 RID: 52004 RVA: 0x002D6D1A File Offset: 0x002D4F1A
		private HourlyRecurrenceRule(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600CB25 RID: 52005 RVA: 0x002D6D24 File Offset: 0x002D4F24
		public HourlyRecurrenceRule(int interval, RecurrenceRange range)
		{
			this.rulePattern.Frequency = RecurrenceFrequency.Hourly;
			this.rulePattern.Interval = interval;
			this.rulePattern.DaysOfWeekMask = RecurrenceDay.None;
			this.rulePattern.DayOfMonth = 0;
			this.rulePattern.DayOrdinal = 0;
			this.rulePattern.Month = RecurrenceMonth.None;
			this.ruleRange = range;
		}

		// Token: 0x1700418E RID: 16782
		// (get) Token: 0x0600CB26 RID: 52006 RVA: 0x002D6D86 File Offset: 0x002D4F86
		public int Interval
		{
			get
			{
				return this.rulePattern.Interval;
			}
		}

		// Token: 0x0600CB27 RID: 52007 RVA: 0x002D6D94 File Offset: 0x002D4F94
		protected override DateTime GetOccurrenceStart(int index)
		{
			return this.ruleRange.Start.Add(new TimeSpan(index * this.rulePattern.Interval, 0, 0));
		}

		// Token: 0x0600CB28 RID: 52008 RVA: 0x002D6DC8 File Offset: 0x002D4FC8
		protected override bool MatchAdvancedPattern(DateTime start)
		{
			return true;
		}
	}
}
