using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Scheduler.Views.Timeline
{
	// Token: 0x02001A98 RID: 6808
	internal class TimeSlotFactory : ITimelineTimeSlotFactory
	{
		// Token: 0x06010782 RID: 67458 RVA: 0x003AE846 File Offset: 0x003ACA46
		public TimeSlot CreateTimeSlot(IEnumerable<Appointment> appointmentsList, ISchedulerModel ownerModel, DateTime start, DateTime end)
		{
			return new TimeSlot(appointmentsList, ownerModel, start, end);
		}
	}
}
