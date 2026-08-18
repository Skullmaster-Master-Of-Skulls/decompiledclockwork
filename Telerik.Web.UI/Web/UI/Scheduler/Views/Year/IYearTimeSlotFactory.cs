using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Scheduler.Views.Year
{
	// Token: 0x02000854 RID: 2132
	internal interface IYearTimeSlotFactory
	{
		// Token: 0x06004EB4 RID: 20148
		TimeSlot CreateTimeSlot(IEnumerable<Appointment> appointmentsList, ISchedulerModel ownerModel, DateTime start, DateTime end);
	}
}
