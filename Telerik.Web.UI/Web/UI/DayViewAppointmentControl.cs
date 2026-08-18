using System;
using System.Globalization;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020012CE RID: 4814
	internal class DayViewAppointmentControl : AppointmentControl
	{
		// Token: 0x0600CA5A RID: 51802 RVA: 0x002D2724 File Offset: 0x002D0924
		internal DayViewAppointmentControl(Appointment appointment, DateTime visibleStart, DateTime visibleEnd, int minutesPerRow, bool enableExactTimeRendering) : base(appointment)
		{
			this._visibleStart = visibleStart;
			this._visibleEnd = visibleEnd;
			this._endingLaterAppointment = (appointment.End > visibleEnd);
			this._startingEarlierAppointment = (appointment.Start < visibleStart);
			this._renderEndResizeGrip = true;
			this._renderStartResizeGrip = true;
			this._renderTime = true;
			this._minutesPerRow = minutesPerRow;
			if (this._startingEarlierAppointment)
			{
				this._renderStartResizeGrip = false;
				this._renderTopArrow = true;
			}
			if (this._endingLaterAppointment)
			{
				this._renderEndResizeGrip = false;
				this._renderBottomArrow = true;
			}
			if (!enableExactTimeRendering)
			{
				base.BoxStart = this.GetRoundedBoxStart();
				base.BoxEnd = this.GetRoundedBoxEnd();
			}
			this.Initialize();
		}

		// Token: 0x0600CA5B RID: 51803 RVA: 0x002D27D8 File Offset: 0x002D09D8
		internal override void CalculateSize()
		{
			base.CalculateSize();
			base.Style["left"] = Unit.Percentage((double)base.Column.Left).ToString(CultureInfo.InvariantCulture);
			int minutesPerRow = base.Appointment.Owner.MinutesPerRow;
			int num = (int)(base.BoxStart - this.GetRoundedBoxStart()).TotalMinutes;
			double num2 = (double)(num % minutesPerRow);
			double num3 = num2 / (double)minutesPerRow;
			int num4 = (int)Math.Round(num3 * base.Appointment.Owner.RowHeight.Value);
			if (num4 > 0)
			{
				base.Style["top"] = Unit.Pixel(num4).ToString();
			}
		}

		// Token: 0x0600CA5C RID: 51804 RVA: 0x002D289F File Offset: 0x002D0A9F
		protected override Unit GetWidth()
		{
			return Unit.Percentage((double)base.Column.Width);
		}

		// Token: 0x0600CA5D RID: 51805 RVA: 0x002D28B4 File Offset: 0x002D0AB4
		protected override Unit GetHeight()
		{
			DateTime utcDate = base.BoxStart;
			DateTime utcDate2 = base.BoxEnd;
			if (this._endingLaterAppointment)
			{
				utcDate2 = this._visibleEnd;
			}
			if (this._startingEarlierAppointment)
			{
				utcDate = this._visibleStart;
			}
			TimeSpan timeSpan = base.Appointment.Owner.UtcToDisplay(utcDate2) - base.Appointment.Owner.UtcToDisplay(utcDate);
			double num = (timeSpan.TotalMinutes > 0.0) ? (timeSpan.TotalMinutes / (double)this._minutesPerRow) : 1.0;
			double value = Math.Round(num * base.Appointment.Owner.RowHeight.Value);
			return new Unit(value, base.Appointment.Owner.RowHeight.Type);
		}

		// Token: 0x0600CA5E RID: 51806 RVA: 0x002D2984 File Offset: 0x002D0B84
		private DateTime GetRoundedBoxStart()
		{
			int num = (int)Math.Max((this._appointment.Start - this._visibleStart).TotalMinutes, 0.0);
			int num2 = num / this._minutesPerRow;
			return this._visibleStart.AddMinutes((double)(num2 * this._minutesPerRow));
		}

		// Token: 0x0600CA5F RID: 51807 RVA: 0x002D29DC File Offset: 0x002D0BDC
		private DateTime GetRoundedBoxEnd()
		{
			DateTime d = (this._appointment.End < this._visibleEnd) ? this._appointment.End : this._visibleEnd;
			int num = (int)Math.Ceiling((d - base.BoxStart).TotalMinutes / (double)this._minutesPerRow);
			DateTime dateTime = base.BoxStart.AddMinutes((double)(num * this._minutesPerRow));
			if (dateTime > this._visibleEnd)
			{
				return this._visibleEnd;
			}
			return dateTime;
		}

		// Token: 0x0400350F RID: 13583
		private DateTime _visibleStart;

		// Token: 0x04003510 RID: 13584
		private DateTime _visibleEnd;

		// Token: 0x04003511 RID: 13585
		private bool _endingLaterAppointment;

		// Token: 0x04003512 RID: 13586
		private bool _startingEarlierAppointment;

		// Token: 0x04003513 RID: 13587
		private int _minutesPerRow;
	}
}
