using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Scheduler.Views.Agenda
{
	// Token: 0x02000843 RID: 2115
	internal class TimeSlotFactory : IAgendaTimeSlotFactory
	{
		// Token: 0x06004E33 RID: 20019 RVA: 0x000F5116 File Offset: 0x000F3316
		public TimeSlot CreateTimeSlot(IEnumerable<Appointment> appointmentsList, ISchedulerModel ownerModel, DateTime start, DateTime end)
		{
			return new TimeSlot(appointmentsList, ownerModel, start, end);
		}
	}
}
