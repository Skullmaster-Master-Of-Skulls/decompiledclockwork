using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Scheduler.Views.Month
{
	// Token: 0x02001A7C RID: 6780
	internal interface IMonthTimeSlotFactory
	{
		// Token: 0x060106C6 RID: 67270
		TimeSlot CreateTimeSlot(IEnumerable<Appointment> appointmentsList, ISchedulerModel ownerModel, DateTime start, DateTime end);
	}
}
