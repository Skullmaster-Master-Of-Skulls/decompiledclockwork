using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Scheduler.Views.Agenda.GroupedByResource
{
	// Token: 0x0200083D RID: 2109
	internal class TimeSlotFactory : IAgendaTimeSlotFactory
	{
		// Token: 0x06004E1C RID: 19996 RVA: 0x000F4CB7 File Offset: 0x000F2EB7
		public TimeSlotFactory(int modelIndex, Resource resource)
		{
			this._modelIndex = modelIndex;
			this._resource = resource;
		}

		// Token: 0x06004E1D RID: 19997 RVA: 0x000F4CD0 File Offset: 0x000F2ED0
		public TimeSlot CreateTimeSlot(IEnumerable<Appointment> appointmentsList, ISchedulerModel ownerModel, DateTime start, DateTime end)
		{
			return new TimeSlot(appointmentsList, ownerModel, start, end)
			{
				ModelIndex = this._modelIndex,
				Resource = this._resource
			};
		}

		// Token: 0x04001378 RID: 4984
		private readonly int _modelIndex;

		// Token: 0x04001379 RID: 4985
		private readonly Resource _resource;
	}
}
