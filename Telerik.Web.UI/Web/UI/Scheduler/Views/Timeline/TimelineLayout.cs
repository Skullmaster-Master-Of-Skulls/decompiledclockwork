using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Scheduler.Views.Timeline
{
	// Token: 0x02001A97 RID: 6807
	internal class TimelineLayout : AllDayLayout
	{
		// Token: 0x0601077F RID: 67455 RVA: 0x003AE804 File Offset: 0x003ACA04
		public TimelineLayout(IEnumerable<ISchedulerTimeSlot> slots) : this(slots, true)
		{
		}

		// Token: 0x06010780 RID: 67456 RVA: 0x003AE80E File Offset: 0x003ACA0E
		public TimelineLayout(IEnumerable<ISchedulerTimeSlot> slots, bool registerAppointmentControls) : base(slots, registerAppointmentControls)
		{
		}

		// Token: 0x06010781 RID: 67457 RVA: 0x003AE818 File Offset: 0x003ACA18
		protected override AllDayAppointmentControl CreateAppointmentControl(Appointment appointment, ISchedulerTimeSlot slot, bool registerAppointmentControls)
		{
			TimelineAppointmentControl timelineAppointmentControl = new TimelineAppointmentControl(appointment, slot, registerAppointmentControls, appointment.Owner.ActiveModel.EnableExactTimeRendering);
			this.AddToSizingBlocks(timelineAppointmentControl);
			return timelineAppointmentControl;
		}
	}
}
