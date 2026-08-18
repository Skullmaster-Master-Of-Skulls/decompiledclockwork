using System;
using Telerik.Web.UI.Scheduling;

namespace Telerik.Web.UI
{
	// Token: 0x020012EB RID: 4843
	public class RecurrenceRange : IEquatable<RecurrenceRange>
	{
		// Token: 0x1700419A RID: 16794
		// (get) Token: 0x0600CB48 RID: 52040 RVA: 0x002D7136 File Offset: 0x002D5336
		// (set) Token: 0x0600CB49 RID: 52041 RVA: 0x002D713E File Offset: 0x002D533E
		public DateTime Start
		{
			get
			{
				return this._start;
			}
			set
			{
				this._start = DateHelper.AssumeUtc(value);
			}
		}

		// Token: 0x1700419B RID: 16795
		// (get) Token: 0x0600CB4A RID: 52042 RVA: 0x002D714C File Offset: 0x002D534C
		// (set) Token: 0x0600CB4B RID: 52043 RVA: 0x002D7154 File Offset: 0x002D5354
		public TimeSpan EventDuration
		{
			get
			{
				return this._eventDuration;
			}
			set
			{
				this._eventDuration = value;
			}
		}

		// Token: 0x1700419C RID: 16796
		// (get) Token: 0x0600CB4C RID: 52044 RVA: 0x002D715D File Offset: 0x002D535D
		// (set) Token: 0x0600CB4D RID: 52045 RVA: 0x002D7165 File Offset: 0x002D5365
		public DateTime RecursUntil
		{
			get
			{
				return this._recursUntil;
			}
			set
			{
				this._recursUntil = ((value < DateTime.MaxValue) ? DateHelper.AssumeUtc(value) : DateTime.MaxValue);
			}
		}

		// Token: 0x1700419D RID: 16797
		// (get) Token: 0x0600CB4E RID: 52046 RVA: 0x002D7187 File Offset: 0x002D5387
		// (set) Token: 0x0600CB4F RID: 52047 RVA: 0x002D718F File Offset: 0x002D538F
		public int MaxOccurrences
		{
			get
			{
				return this._maxOccurrences;
			}
			set
			{
				this._maxOccurrences = value;
			}
		}

		// Token: 0x0600CB50 RID: 52048 RVA: 0x002D7198 File Offset: 0x002D5398
		public RecurrenceRange()
		{
		}

		// Token: 0x0600CB51 RID: 52049 RVA: 0x002D71CC File Offset: 0x002D53CC
		public RecurrenceRange(DateTime start, TimeSpan duration, DateTime recursUntil, int maxOccurrences)
		{
			this._start = start;
			this._eventDuration = duration;
			this._recursUntil = recursUntil;
			this._maxOccurrences = maxOccurrences;
		}

		// Token: 0x0600CB52 RID: 52050 RVA: 0x002D7228 File Offset: 0x002D5428
		public override bool Equals(object obj)
		{
			RecurrenceRange other = obj as RecurrenceRange;
			return obj != null && this.Equals(other);
		}

		// Token: 0x0600CB53 RID: 52051 RVA: 0x002D7248 File Offset: 0x002D5448
		public override int GetHashCode()
		{
			return this._start.GetHashCode() ^ this._eventDuration.GetHashCode() ^ this._recursUntil.GetHashCode() ^ this._maxOccurrences.GetHashCode();
		}

		// Token: 0x0600CB54 RID: 52052 RVA: 0x002D7298 File Offset: 0x002D5498
		public bool Equals(RecurrenceRange other)
		{
			return !(other == null) && (RecurrenceRange.DatesAreEqualIgnoringMillis(this._start, other.Start) && this._eventDuration == other.EventDuration && RecurrenceRange.DatesAreEqualIgnoringMillis(this._recursUntil, other.RecursUntil)) && this._maxOccurrences == other.MaxOccurrences;
		}

		// Token: 0x0600CB55 RID: 52053 RVA: 0x002D72F9 File Offset: 0x002D54F9
		public static bool operator ==(RecurrenceRange o1, RecurrenceRange o2)
		{
			if (o1 != null)
			{
				return o1.Equals(o2);
			}
			return o2 == null;
		}

		// Token: 0x0600CB56 RID: 52054 RVA: 0x002D730A File Offset: 0x002D550A
		public static bool operator !=(RecurrenceRange o1, RecurrenceRange o2)
		{
			if (o1 != null)
			{
				return !o1.Equals(o2);
			}
			return o2 != null;
		}

		// Token: 0x0600CB57 RID: 52055 RVA: 0x002D7324 File Offset: 0x002D5524
		private static bool DatesAreEqualIgnoringMillis(DateTime first, DateTime second)
		{
			return first.Year == second.Year && first.Month == second.Month && first.Hour == second.Hour && first.Minute == second.Minute && first.Second == second.Second;
		}

		// Token: 0x04003573 RID: 13683
		private DateTime _start = DateTime.MinValue;

		// Token: 0x04003574 RID: 13684
		private TimeSpan _eventDuration = TimeSpan.Zero;

		// Token: 0x04003575 RID: 13685
		private DateTime _recursUntil = DateTime.MaxValue;

		// Token: 0x04003576 RID: 13686
		private int _maxOccurrences = int.MaxValue;
	}
}
