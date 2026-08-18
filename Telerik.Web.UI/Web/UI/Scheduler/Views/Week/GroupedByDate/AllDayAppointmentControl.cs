using System;

namespace Telerik.Web.UI.Scheduler.Views.Week.GroupedByDate
{
	// Token: 0x02001A9C RID: 6812
	internal class AllDayAppointmentControl : AllDayAppointmentControl
	{
		// Token: 0x06010798 RID: 67480 RVA: 0x003AECCC File Offset: 0x003ACECC
		public AllDayAppointmentControl(Appointment appointment, ISchedulerTimeSlot slot) : this(appointment, slot, true)
		{
		}

		// Token: 0x06010799 RID: 67481 RVA: 0x003AECD7 File Offset: 0x003ACED7
		public AllDayAppointmentControl(Appointment appointment, ISchedulerTimeSlot slot, bool registerWithAppointment) : base(appointment, slot, registerWithAppointment)
		{
		}

		// Token: 0x17004FFC RID: 20476
		// (get) Token: 0x0601079A RID: 67482 RVA: 0x003AECE2 File Offset: 0x003ACEE2
		protected override int AppointmentColSpan
		{
			get
			{
				return 1;
			}
		}
	}
}
