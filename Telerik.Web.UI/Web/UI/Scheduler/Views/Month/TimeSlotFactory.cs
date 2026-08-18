using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Scheduler.Views.Month
{
	// Token: 0x02001A80 RID: 6784
	internal class TimeSlotFactory : IMonthTimeSlotFactory
	{
		// Token: 0x060106DA RID: 67290 RVA: 0x003ABCE5 File Offset: 0x003A9EE5
		public TimeSlot CreateTimeSlot(IEnumerable<Appointment> appointmentsList, ISchedulerModel ownerModel, DateTime start, DateTime end)
		{
			return new TimeSlot(appointmentsList, ownerModel, start, end);
		}
	}
}
