using System;
using System.Collections.Generic;
using Telerik.Web.UI.Scheduling;

namespace Telerik.Web.UI.Scheduler.Views.Month
{
	// Token: 0x02001A4A RID: 6730
	internal class MonthWeekLayout : AllDayLayout
	{
		// Token: 0x06010532 RID: 66866 RVA: 0x003A4F98 File Offset: 0x003A3198
		public MonthWeekLayout(IEnumerable<ISchedulerTimeSlot> slots, bool registerAppointmentControls) : base(slots, registerAppointmentControls)
		{
		}

		// Token: 0x06010533 RID: 66867 RVA: 0x003A4FA4 File Offset: 0x003A31A4
		protected override AllDayAppointmentControl CreateAppointmentControl(Appointment appointment, ISchedulerTimeSlot slot, bool registerAppointmentControls)
		{
			int weekLength = DateHelper.GetWeekLength(slot.Start, appointment.Owner.FirstDayOfWeek, appointment.Owner.LastDayOfWeek);
			MonthViewAppointmentControl monthViewAppointmentControl = new MonthViewAppointmentControl(appointment, slot, registerAppointmentControls, weekLength);
			this.AddToSizingBlocks(monthViewAppointmentControl);
			return monthViewAppointmentControl;
		}
	}
}
