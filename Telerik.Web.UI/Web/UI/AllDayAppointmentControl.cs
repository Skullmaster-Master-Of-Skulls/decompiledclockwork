using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Scheduler.Views;

namespace Telerik.Web.UI
{
	// Token: 0x02000E73 RID: 3699
	internal class AllDayAppointmentControl : AppointmentControl
	{
		// Token: 0x17002C54 RID: 11348
		// (get) Token: 0x06008C45 RID: 35909 RVA: 0x001FD529 File Offset: 0x001FB729
		// (set) Token: 0x06008C46 RID: 35910 RVA: 0x001FD531 File Offset: 0x001FB731
		public AllDayRow Row
		{
			get
			{
				return this._row;
			}
			set
			{
				this._row = value;
			}
		}

		// Token: 0x17002C55 RID: 11349
		// (get) Token: 0x06008C47 RID: 35911 RVA: 0x001FD53A File Offset: 0x001FB73A
		// (set) Token: 0x06008C48 RID: 35912 RVA: 0x001FD542 File Offset: 0x001FB742
		private protected ISchedulerTimeSlot Slot
		{
			protected get
			{
				return this._slot;
			}
			private set
			{
				this._slot = value;
			}
		}

		// Token: 0x17002C56 RID: 11350
		// (get) Token: 0x06008C49 RID: 35913 RVA: 0x001FD54B File Offset: 0x001FB74B
		// (set) Token: 0x06008C4A RID: 35914 RVA: 0x001FD553 File Offset: 0x001FB753
		private protected bool StartingEarlier
		{
			protected get
			{
				return this._startingEarlier;
			}
			private set
			{
				this._startingEarlier = value;
			}
		}

		// Token: 0x17002C57 RID: 11351
		// (get) Token: 0x06008C4B RID: 35915 RVA: 0x001FD55C File Offset: 0x001FB75C
		// (set) Token: 0x06008C4C RID: 35916 RVA: 0x001FD564 File Offset: 0x001FB764
		private protected bool EndingLater
		{
			protected get
			{
				return this._endingLater;
			}
			private set
			{
				this._endingLater = value;
			}
		}

		// Token: 0x17002C58 RID: 11352
		// (get) Token: 0x06008C4D RID: 35917 RVA: 0x001FD56D File Offset: 0x001FB76D
		// (set) Token: 0x06008C4E RID: 35918 RVA: 0x001FD575 File Offset: 0x001FB775
		private protected DateTime VisibleStart
		{
			protected get
			{
				return this._visibleStart;
			}
			private set
			{
				this._visibleStart = value;
			}
		}

		// Token: 0x17002C59 RID: 11353
		// (get) Token: 0x06008C4F RID: 35919 RVA: 0x001FD57E File Offset: 0x001FB77E
		// (set) Token: 0x06008C50 RID: 35920 RVA: 0x001FD586 File Offset: 0x001FB786
		private protected DateTime VisibleEnd
		{
			protected get
			{
				return this._visibleEnd;
			}
			private set
			{
				this._visibleEnd = value;
			}
		}

		// Token: 0x06008C51 RID: 35921 RVA: 0x001FD590 File Offset: 0x001FB790
		internal AllDayAppointmentControl(Appointment appointment, ISchedulerTimeSlot slot, bool registerWithAppointment) : base(appointment, registerWithAppointment)
		{
			this.Slot = slot;
			this.VisibleStart = appointment.Owner.VisibleRangeStart;
			this.VisibleEnd = appointment.Owner.VisibleRangeEnd;
			base.BoxStart = slot.Start;
			if (base.Appointment.End < this.VisibleEnd)
			{
				base.BoxEnd = base.Appointment.Owner.UtcDayStart(base.Appointment.End);
			}
			else
			{
				base.BoxEnd = this.VisibleEnd;
			}
			if (base.Appointment.End > this.VisibleEnd)
			{
				this.EndingLater = true;
				this._renderRightArrow = true;
			}
			DateTime d = appointment.Owner.UtcToDisplay(base.Appointment.End);
			if (d.Date != d)
			{
				this._renderRightArrow = true;
			}
			if (base.Appointment.Start < this.VisibleStart)
			{
				this.StartingEarlier = true;
				this._renderLeftArrow = true;
			}
			DateTime d2 = appointment.Owner.UtcToDisplay(base.Appointment.Start);
			if (d2.Date != d2)
			{
				this._renderLeftArrow = true;
			}
			this.Initialize();
		}

		// Token: 0x06008C52 RID: 35922 RVA: 0x001FD6CD File Offset: 0x001FB8CD
		internal AllDayAppointmentControl(Appointment appointment, ISchedulerTimeSlot slot) : this(appointment, slot, true)
		{
		}

		// Token: 0x17002C5A RID: 11354
		// (get) Token: 0x06008C53 RID: 35923 RVA: 0x001FD6D8 File Offset: 0x001FB8D8
		protected override int AppointmentColSpan
		{
			get
			{
				return Math.Max(1, this.Duration.Days);
			}
		}

		// Token: 0x06008C54 RID: 35924 RVA: 0x001FD6F9 File Offset: 0x001FB8F9
		protected override Unit GetHeight()
		{
			return base.Appointment.Owner.RowHeight;
		}

		// Token: 0x06008C55 RID: 35925 RVA: 0x001FD70B File Offset: 0x001FB90B
		protected override Unit GetWidth()
		{
			return Unit.Percentage((double)(100 * this.AppointmentColSpan));
		}

		// Token: 0x17002C5B RID: 11355
		// (get) Token: 0x06008C56 RID: 35926 RVA: 0x001FD71C File Offset: 0x001FB91C
		protected TimeSpan Duration
		{
			get
			{
				DateTime dateTime;
				if (this.EndingLater)
				{
					dateTime = this.VisibleEnd;
				}
				else
				{
					dateTime = base.BoxEnd;
				}
				DateTime value;
				if (this.StartingEarlier)
				{
					value = this.VisibleStart;
				}
				else
				{
					value = base.BoxStart;
				}
				return dateTime.Subtract(value);
			}
		}

		// Token: 0x06008C57 RID: 35927 RVA: 0x001FD764 File Offset: 0x001FB964
		protected override WebControl CreateResizeGrip(bool resizeFromStart)
		{
			WebControl webControl = base.CreateResizeGrip(resizeFromStart);
			if (base.Appointment.Owner.RowHeight.Type == UnitType.Pixel)
			{
				int num;
				if (base.Appointment.Owner.RowHeight.Value < 25.0)
				{
					num = 0;
				}
				else
				{
					num = ((int)base.Appointment.Owner.RowHeight.Value - 25) / 2;
				}
				webControl.Style[HtmlTextWriterStyle.Top] = num + "px";
			}
			return webControl;
		}

		// Token: 0x04002768 RID: 10088
		private AllDayRow _row;

		// Token: 0x04002769 RID: 10089
		private DateTime _visibleStart;

		// Token: 0x0400276A RID: 10090
		private DateTime _visibleEnd;

		// Token: 0x0400276B RID: 10091
		private bool _startingEarlier;

		// Token: 0x0400276C RID: 10092
		private bool _endingLater;

		// Token: 0x0400276D RID: 10093
		private ISchedulerTimeSlot _slot;
	}
}
