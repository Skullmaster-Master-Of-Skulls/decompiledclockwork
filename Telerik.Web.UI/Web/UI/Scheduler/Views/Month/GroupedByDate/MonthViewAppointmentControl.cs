using System;

namespace Telerik.Web.UI.Scheduler.Views.Month.GroupedByDate
{
	// Token: 0x02000E75 RID: 3701
	internal class MonthViewAppointmentControl : MonthViewAppointmentControl
	{
		// Token: 0x06008C60 RID: 35936 RVA: 0x001FD9D6 File Offset: 0x001FBBD6
		internal MonthViewAppointmentControl(Appointment appointment, ISchedulerTimeSlot slot, bool registerAppointmentControls, int weekLength) : base(appointment, slot, registerAppointmentControls, weekLength)
		{
		}

		// Token: 0x17002C60 RID: 11360
		// (get) Token: 0x06008C61 RID: 35937 RVA: 0x001FD9E3 File Offset: 0x001FBBE3
		protected override bool SupportsResize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06008C62 RID: 35938 RVA: 0x001FD9E6 File Offset: 0x001FBBE6
		protected override DateTime GetStart()
		{
			return base.Slot.Start;
		}

		// Token: 0x06008C63 RID: 35939 RVA: 0x001FD9F3 File Offset: 0x001FBBF3
		protected override DateTime GetEnd(DateTime start)
		{
			return start.AddDays(1.0);
		}

		// Token: 0x17002C61 RID: 11361
		// (get) Token: 0x06008C64 RID: 35940 RVA: 0x001FDA05 File Offset: 0x001FBC05
		protected override int AppointmentColSpan
		{
			get
			{
				return 1;
			}
		}
	}
}
