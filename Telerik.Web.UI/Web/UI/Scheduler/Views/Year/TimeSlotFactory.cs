using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Scheduler.Views.Year
{
	// Token: 0x02000859 RID: 2137
	internal class TimeSlotFactory : IYearTimeSlotFactory
	{
		// Token: 0x06004EC8 RID: 20168 RVA: 0x000F7161 File Offset: 0x000F5361
		public TimeSlot CreateTimeSlot(IEnumerable<Appointment> appointmentsList, ISchedulerModel ownerModel, DateTime start, DateTime end)
		{
			return new TimeSlot(appointmentsList, ownerModel, start, end);
		}
	}
}
