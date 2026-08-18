using System;

namespace Telerik.Web.UI.Scheduler.Views.Timeline.GroupedByDate
{
	// Token: 0x02001A8D RID: 6797
	internal class TimelineAppointmentControl : AllDayAppointmentControl
	{
		// Token: 0x06010757 RID: 67415 RVA: 0x003ADDF9 File Offset: 0x003ABFF9
		public TimelineAppointmentControl(Appointment appointment, ISchedulerTimeSlot slot, bool registerWithAppointment) : base(appointment, slot, registerWithAppointment)
		{
		}

		// Token: 0x17004FEF RID: 20463
		// (get) Token: 0x06010758 RID: 67416 RVA: 0x003ADE04 File Offset: 0x003AC004
		protected override int AppointmentColSpan
		{
			get
			{
				return 1;
			}
		}
	}
}
