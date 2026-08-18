using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Scheduler.Views.Week
{
	// Token: 0x02001AA1 RID: 6817
	internal interface IWeekTimeSlotFactory
	{
		// Token: 0x060107B0 RID: 67504
		TimeSlot CreateTimeSlot(IEnumerable<Appointment> appointmentsList, ISchedulerModel ownerModel, DateTime start, DateTime end);
	}
}
