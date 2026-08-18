using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Scheduler.Views.Week
{
	// Token: 0x02001AA5 RID: 6821
	internal class TimeSlotFactory : IWeekTimeSlotFactory
	{
		// Token: 0x060107C2 RID: 67522 RVA: 0x003AF4B9 File Offset: 0x003AD6B9
		public TimeSlot CreateTimeSlot(IEnumerable<Appointment> appointmentsList, ISchedulerModel ownerModel, DateTime start, DateTime end)
		{
			return new TimeSlot(appointmentsList, ownerModel, start, end);
		}
	}
}
