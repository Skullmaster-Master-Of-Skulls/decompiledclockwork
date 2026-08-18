using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.Scheduler.Views.Timeline
{
	// Token: 0x02001A96 RID: 6806
	internal class TimelineAppointmentControl : AllDayAppointmentControl
	{
		// Token: 0x06010777 RID: 67447 RVA: 0x003AE444 File Offset: 0x003AC644
		public TimelineAppointmentControl(Appointment appointment, ISchedulerTimeSlot slot, bool registerWithAppointment, bool enableExactTimeRendering) : base(appointment, slot, registerWithAppointment)
		{
			base.BoxStart = this.GetBoxStart(enableExactTimeRendering);
			base.BoxEnd = this.GetBoxEnd(enableExactTimeRendering);
			this._renderRightArrow = (base.Appointment.End > base.VisibleEnd);
			this._renderLeftArrow = (base.Appointment.Start < base.VisibleStart);
			this._renderEndResizeGrip = !this._renderRightArrow;
			this._renderStartResizeGrip = !this._renderLeftArrow;
			base.Initialize();
		}

		// Token: 0x06010778 RID: 67448 RVA: 0x003AE4D2 File Offset: 0x003AC6D2
		protected override void Initialize()
		{
		}

		// Token: 0x06010779 RID: 67449 RVA: 0x003AE4D4 File Offset: 0x003AC6D4
		private DateTime GetBoxStart(bool exactTime)
		{
			if (!exactTime)
			{
				return base.Slot.Start;
			}
			return base.Appointment.Start;
		}

		// Token: 0x0601077A RID: 67450 RVA: 0x003AE4F0 File Offset: 0x003AC6F0
		private DateTime GetBoxEnd(bool exactTime)
		{
			if (exactTime)
			{
				return base.Appointment.End;
			}
			DateTime utcDate = (base.Appointment.End < base.VisibleEnd) ? base.Appointment.End : base.VisibleEnd;
			int num = (int)Math.Ceiling((base.Appointment.Owner.UtcToDisplay(utcDate) - base.Appointment.Owner.UtcToDisplay(this.GetBoxStart(false))).TotalMinutes / this.SlotDurationDisplay.TotalMinutes);
			DateTime displayDate = base.Appointment.Owner.UtcToDisplay(base.Slot.Start).AddMinutes((double)num * this.SlotDurationDisplay.TotalMinutes);
			DateTime dateTime = base.Appointment.Owner.DisplayToUtc(displayDate);
			if (dateTime > base.VisibleEnd)
			{
				return base.VisibleEnd;
			}
			return dateTime;
		}

		// Token: 0x0601077B RID: 67451 RVA: 0x003AE5E8 File Offset: 0x003AC7E8
		protected override Unit GetWidth()
		{
			DateTime utcDate = base.BoxStart;
			DateTime utcDate2 = base.BoxEnd;
			if (this._renderRightArrow)
			{
				utcDate2 = base.VisibleEnd;
			}
			if (this._renderLeftArrow)
			{
				utcDate = base.VisibleStart;
			}
			TimeSpan timeSpan = base.Appointment.Owner.UtcToDisplay(utcDate2) - base.Appointment.Owner.UtcToDisplay(utcDate);
			double num = (timeSpan.TotalMinutes > 0.0) ? (timeSpan.TotalMinutes / this.SlotDurationDisplay.TotalMinutes) : 1.0;
			return Unit.Percentage(Math.Round(100.0 * num, 8));
		}

		// Token: 0x0601077C RID: 67452 RVA: 0x003AE694 File Offset: 0x003AC894
		internal override void CalculateSize()
		{
			base.CalculateSize();
			if (base.StartingEarlier)
			{
				return;
			}
			string[] array = base.Slot.Index.Split(new char[]
			{
				':'
			});
			int num = int.Parse(array[array.Length - 1]);
			double num2 = (double)(num * 100);
			int num3 = (int)(base.Appointment.Owner.UtcToDisplay(base.BoxStart) - base.Appointment.Owner.UtcToDisplay(base.Slot.Start)).TotalMinutes;
			double num4 = (double)num3 % this.SlotDurationDisplay.TotalMinutes;
			double num5 = Math.Round(num4 / this.SlotDurationDisplay.TotalMinutes * 100.0, 8);
			num2 += num5;
			if (num2 != 0.0)
			{
				base.Style.Add(HtmlTextWriterStyle.Left, Unit.Percentage(num2).ToString(CultureInfo.InvariantCulture));
			}
		}

		// Token: 0x17004FF7 RID: 20471
		// (get) Token: 0x0601077D RID: 67453 RVA: 0x003AE78F File Offset: 0x003AC98F
		private TimeSpan SlotDurationDisplay
		{
			get
			{
				return base.Appointment.Owner.UtcToDisplay(base.Slot.End) - base.Appointment.Owner.UtcToDisplay(base.Slot.Start);
			}
		}

		// Token: 0x17004FF8 RID: 20472
		// (get) Token: 0x0601077E RID: 67454 RVA: 0x003AE7CC File Offset: 0x003AC9CC
		protected override int AppointmentColSpan
		{
			get
			{
				double a = base.Duration.TotalMinutes / this.SlotDurationDisplay.TotalMinutes;
				return Math.Max(1, (int)Math.Ceiling(a));
			}
		}
	}
}
