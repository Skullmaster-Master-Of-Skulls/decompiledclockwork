using System;

namespace Telerik.Web.UI.Scheduler.Views.Month
{
	// Token: 0x02000E74 RID: 3700
	internal class MonthViewAppointmentControl : AllDayAppointmentControl
	{
		// Token: 0x17002C5C RID: 11356
		// (get) Token: 0x06008C58 RID: 35928 RVA: 0x001FD7F9 File Offset: 0x001FB9F9
		private int WeekLength
		{
			get
			{
				return this._weekLength;
			}
		}

		// Token: 0x17002C5D RID: 11357
		// (get) Token: 0x06008C59 RID: 35929 RVA: 0x001FD801 File Offset: 0x001FBA01
		private DateTime EffectiveAptEnd
		{
			get
			{
				return this._effectiveAptEnd;
			}
		}

		// Token: 0x17002C5E RID: 11358
		// (get) Token: 0x06008C5A RID: 35930 RVA: 0x001FD809 File Offset: 0x001FBA09
		protected virtual bool SupportsResize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06008C5B RID: 35931 RVA: 0x001FD80C File Offset: 0x001FBA0C
		internal MonthViewAppointmentControl(Appointment appointment, ISchedulerTimeSlot slot, bool registerAppointmentControls, int weekLength) : base(appointment, slot, registerAppointmentControls)
		{
			this._weekLength = weekLength;
			DateTime start = this.GetStart();
			DateTime end = this.GetEnd(start);
			DateTime t = base.Appointment.Owner.UtcDayStart(base.Appointment.Start);
			DateTime dateTime = base.Appointment.Owner.UtcDayStart(base.Appointment.End);
			if (dateTime != base.Appointment.End)
			{
				dateTime = dateTime.AddDays(1.0);
			}
			this._effectiveAptEnd = ((end < dateTime) ? end : dateTime);
			if (base.Appointment.End < end)
			{
				base.BoxEnd = dateTime;
			}
			else
			{
				base.BoxEnd = end;
			}
			this._renderRightArrow = (dateTime > end);
			this._renderLeftArrow = (t < start);
			this._renderEndResizeGrip = (this.SupportsResize && !this._renderRightArrow);
			this._renderStartResizeGrip = (this.SupportsResize && !this._renderLeftArrow);
			base.Initialize();
		}

		// Token: 0x06008C5C RID: 35932 RVA: 0x001FD920 File Offset: 0x001FBB20
		protected virtual DateTime GetStart()
		{
			int dayIndex = ((TimeSlot)base.Slot).DayIndex;
			int num = dayIndex % this.WeekLength;
			return base.Slot.Start.AddDays((double)(-(double)num));
		}

		// Token: 0x06008C5D RID: 35933 RVA: 0x001FD95D File Offset: 0x001FBB5D
		protected virtual DateTime GetEnd(DateTime start)
		{
			return start.AddDays((double)this.WeekLength);
		}

		// Token: 0x06008C5E RID: 35934 RVA: 0x001FD96D File Offset: 0x001FBB6D
		protected override void Initialize()
		{
		}

		// Token: 0x17002C5F RID: 11359
		// (get) Token: 0x06008C5F RID: 35935 RVA: 0x001FD970 File Offset: 0x001FBB70
		protected override int AppointmentColSpan
		{
			get
			{
				TimeSpan t = base.Appointment.Owner.UtcToDisplay(this.EffectiveAptEnd) - base.Appointment.Owner.UtcToDisplay(base.Slot.Start);
				if (t == TimeSpan.Zero)
				{
					return 1;
				}
				return Math.Min((int)t.TotalDays, this.WeekLength);
			}
		}

		// Token: 0x0400276E RID: 10094
		private readonly int _weekLength;

		// Token: 0x0400276F RID: 10095
		private readonly DateTime _effectiveAptEnd;
	}
}
