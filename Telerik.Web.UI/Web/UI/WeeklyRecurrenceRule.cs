using System;
using System.Runtime.Serialization;
using Telerik.Web.UI.Scheduling;

namespace Telerik.Web.UI
{
	// Token: 0x020012ED RID: 4845
	[Serializable]
	public class WeeklyRecurrenceRule : RecurrenceRule
	{
		// Token: 0x0600CB5E RID: 52062 RVA: 0x002D7481 File Offset: 0x002D5681
		private WeeklyRecurrenceRule(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600CB5F RID: 52063 RVA: 0x002D748C File Offset: 0x002D568C
		public WeeklyRecurrenceRule(int interval, RecurrenceDay daysOfWeekMask, RecurrenceRange range)
		{
			this.rulePattern.Frequency = RecurrenceFrequency.Weekly;
			this.rulePattern.Interval = interval;
			this.rulePattern.DaysOfWeekMask = daysOfWeekMask;
			this.rulePattern.DayOfMonth = 0;
			this.rulePattern.DayOrdinal = 0;
			this.rulePattern.Month = RecurrenceMonth.None;
			this.ruleRange = range;
		}

		// Token: 0x0600CB60 RID: 52064 RVA: 0x002D74EE File Offset: 0x002D56EE
		public WeeklyRecurrenceRule(int interval, RecurrenceDay daysOfWeekMask, RecurrenceRange range, DayOfWeek firstDayOfWeek) : this(interval, daysOfWeekMask, range)
		{
			base.Pattern.FirstDayOfWeek = firstDayOfWeek;
		}

		// Token: 0x1700419E RID: 16798
		// (get) Token: 0x0600CB61 RID: 52065 RVA: 0x002D7506 File Offset: 0x002D5706
		public int Interval
		{
			get
			{
				return this.rulePattern.Interval;
			}
		}

		// Token: 0x1700419F RID: 16799
		// (get) Token: 0x0600CB62 RID: 52066 RVA: 0x002D7513 File Offset: 0x002D5713
		public RecurrenceDay DaysOfWeekMask
		{
			get
			{
				return this.rulePattern.DaysOfWeekMask;
			}
		}

		// Token: 0x0600CB63 RID: 52067 RVA: 0x002D7520 File Offset: 0x002D5720
		protected override DateTime GetOccurrenceStart(int index)
		{
			return this.ruleRange.Start.Add(new TimeSpan(index, 0, 0, 0));
		}

		// Token: 0x0600CB64 RID: 52068 RVA: 0x002D7549 File Offset: 0x002D5749
		protected override bool MatchAdvancedPattern(DateTime start)
		{
			return this.GetWeekIndex(start) % this.rulePattern.Interval == 0 && base.MatchDayOfWeekMask(start);
		}

		// Token: 0x0600CB65 RID: 52069 RVA: 0x002D756C File Offset: 0x002D576C
		private int GetWeekIndex(DateTime current)
		{
			DateTime startOfWeek = DateHelper.GetStartOfWeek(this.ruleRange.Start, this.rulePattern.FirstDayOfWeek);
			return current.Subtract(startOfWeek).Days / 7;
		}
	}
}
