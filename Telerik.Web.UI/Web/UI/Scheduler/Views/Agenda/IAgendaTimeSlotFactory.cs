using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Scheduler.Views.Agenda
{
	// Token: 0x0200083C RID: 2108
	internal interface IAgendaTimeSlotFactory
	{
		// Token: 0x06004E1B RID: 19995
		TimeSlot CreateTimeSlot(IEnumerable<Appointment> appointmentsList, ISchedulerModel ownerModel, DateTime start, DateTime end);
	}
}
