using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Scheduler.Views.Week.GroupedByDate
{
	// Token: 0x02001A9D RID: 6813
	internal class AllDayLayout : AllDayLayout
	{
		// Token: 0x0601079B RID: 67483 RVA: 0x003AECE5 File Offset: 0x003ACEE5
		public AllDayLayout(IEnumerable<ISchedulerTimeSlot> slots) : this(slots, true)
		{
		}

		// Token: 0x0601079C RID: 67484 RVA: 0x003AECEF File Offset: 0x003ACEEF
		public AllDayLayout(IEnumerable<ISchedulerTimeSlot> slots, bool registerAppointmentControls) : base(slots, registerAppointmentControls)
		{
		}

		// Token: 0x0601079D RID: 67485 RVA: 0x003AECFC File Offset: 0x003ACEFC
		protected override AllDayAppointmentControl CreateAppointmentControl(Appointment appointment, ISchedulerTimeSlot slot, bool registerAppointmentControls)
		{
			AllDayAppointmentControl allDayAppointmentControl = new AllDayAppointmentControl(appointment, slot, registerAppointmentControls);
			this.AddToSizingBlocks(allDayAppointmentControl);
			return allDayAppointmentControl;
		}
	}
}
