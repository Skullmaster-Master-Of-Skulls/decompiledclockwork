using System;

namespace Telerik.Web.UI
{
	// Token: 0x020012EA RID: 4842
	public class RecurrencePattern : IEquatable<RecurrencePattern>
	{
		// Token: 0x17004193 RID: 16787
		// (get) Token: 0x0600CB34 RID: 52020 RVA: 0x002D6F94 File Offset: 0x002D5194
		// (set) Token: 0x0600CB35 RID: 52021 RVA: 0x002D6F9C File Offset: 0x002D519C
		public RecurrenceFrequency Frequency
		{
			get
			{
				return this._frequency;
			}
			set
			{
				this._frequency = value;
			}
		}

		// Token: 0x17004194 RID: 16788
		// (get) Token: 0x0600CB36 RID: 52022 RVA: 0x002D6FA5 File Offset: 0x002D51A5
		// (set) Token: 0x0600CB37 RID: 52023 RVA: 0x002D6FAD File Offset: 0x002D51AD
		public int Interval
		{
			get
			{
				return this._interval;
			}
			set
			{
				this._interval = value;
			}
		}

		// Token: 0x17004195 RID: 16789
		// (get) Token: 0x0600CB38 RID: 52024 RVA: 0x002D6FB6 File Offset: 0x002D51B6
		// (set) Token: 0x0600CB39 RID: 52025 RVA: 0x002D6FBE File Offset: 0x002D51BE
		public RecurrenceDay DaysOfWeekMask
		{
			get
			{
				return this._daysOfWeekMask;
			}
			set
			{
				this._daysOfWeekMask = value;
			}
		}

		// Token: 0x17004196 RID: 16790
		// (get) Token: 0x0600CB3A RID: 52026 RVA: 0x002D6FC7 File Offset: 0x002D51C7
		// (set) Token: 0x0600CB3B RID: 52027 RVA: 0x002D6FCF File Offset: 0x002D51CF
		public int DayOfMonth
		{
			get
			{
				return this._dayOfMonth;
			}
			set
			{
				this._dayOfMonth = value;
			}
		}

		// Token: 0x17004197 RID: 16791
		// (get) Token: 0x0600CB3C RID: 52028 RVA: 0x002D6FD8 File Offset: 0x002D51D8
		// (set) Token: 0x0600CB3D RID: 52029 RVA: 0x002D6FE0 File Offset: 0x002D51E0
		public int DayOrdinal
		{
			get
			{
				return this._dayOrdinal;
			}
			set
			{
				this._dayOrdinal = value;
			}
		}

		// Token: 0x17004198 RID: 16792
		// (get) Token: 0x0600CB3E RID: 52030 RVA: 0x002D6FE9 File Offset: 0x002D51E9
		// (set) Token: 0x0600CB3F RID: 52031 RVA: 0x002D6FF1 File Offset: 0x002D51F1
		public RecurrenceMonth Month
		{
			get
			{
				return this._month;
			}
			set
			{
				this._month = value;
			}
		}

		// Token: 0x17004199 RID: 16793
		// (get) Token: 0x0600CB40 RID: 52032 RVA: 0x002D6FFA File Offset: 0x002D51FA
		// (set) Token: 0x0600CB41 RID: 52033 RVA: 0x002D7002 File Offset: 0x002D5202
		public DayOfWeek FirstDayOfWeek
		{
			get
			{
				return this._firstDayOfWeek;
			}
			set
			{
				this._firstDayOfWeek = value;
			}
		}

		// Token: 0x0600CB42 RID: 52034 RVA: 0x002D700C File Offset: 0x002D520C
		public override bool Equals(object obj)
		{
			RecurrencePattern other = obj as RecurrencePattern;
			return obj != null && this.Equals(other);
		}

		// Token: 0x0600CB43 RID: 52035 RVA: 0x002D702C File Offset: 0x002D522C
		public override int GetHashCode()
		{
			return this.Frequency.GetHashCode() ^ this.Interval.GetHashCode() ^ this.DaysOfWeekMask.GetHashCode() ^ this.DayOfMonth.GetHashCode() ^ this.DayOrdinal.GetHashCode() ^ this.Month.GetHashCode();
		}

		// Token: 0x0600CB44 RID: 52036 RVA: 0x002D7098 File Offset: 0x002D5298
		public bool Equals(RecurrencePattern other)
		{
			return !(other == null) && (this.Frequency == other.Frequency && this.Interval == other.Interval && this.DaysOfWeekMask == other.DaysOfWeekMask && this.DayOfMonth == other.DayOfMonth && this.DayOrdinal == other.DayOrdinal) && this.Month == other.Month;
		}

		// Token: 0x0600CB45 RID: 52037 RVA: 0x002D7106 File Offset: 0x002D5306
		public static bool operator ==(RecurrencePattern o1, RecurrencePattern o2)
		{
			if (o1 != null)
			{
				return o1.Equals(o2);
			}
			return o2 == null;
		}

		// Token: 0x0600CB46 RID: 52038 RVA: 0x002D7117 File Offset: 0x002D5317
		public static bool operator !=(RecurrencePattern o1, RecurrencePattern o2)
		{
			if (o1 != null)
			{
				return !o1.Equals(o2);
			}
			return o2 != null;
		}

		// Token: 0x0400356C RID: 13676
		private RecurrenceFrequency _frequency;

		// Token: 0x0400356D RID: 13677
		private int _interval;

		// Token: 0x0400356E RID: 13678
		private RecurrenceDay _daysOfWeekMask;

		// Token: 0x0400356F RID: 13679
		private int _dayOfMonth;

		// Token: 0x04003570 RID: 13680
		private int _dayOrdinal;

		// Token: 0x04003571 RID: 13681
		private RecurrenceMonth _month;

		// Token: 0x04003572 RID: 13682
		private DayOfWeek _firstDayOfWeek;
	}
}
