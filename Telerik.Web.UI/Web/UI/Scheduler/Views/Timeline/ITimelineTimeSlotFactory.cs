using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Scheduler.Views.Timeline
{
	// Token: 0x02001A93 RID: 6803
	internal interface ITimelineTimeSlotFactory
	{
		// Token: 0x06010771 RID: 67441
		TimeSlot CreateTimeSlot(IEnumerable<Appointment> appointmentsList, ISchedulerModel ownerModel, DateTime start, DateTime end);
	}
}
