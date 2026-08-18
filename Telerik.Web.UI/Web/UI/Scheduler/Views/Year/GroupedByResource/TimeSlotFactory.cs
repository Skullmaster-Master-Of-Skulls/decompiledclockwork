using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Scheduler.Views.Year.GroupedByResource
{
	// Token: 0x02000855 RID: 2133
	internal class TimeSlotFactory : IYearTimeSlotFactory
	{
		// Token: 0x06004EB5 RID: 20149 RVA: 0x000F6BC9 File Offset: 0x000F4DC9
		public TimeSlotFactory(int modelIndex, Resource resource)
		{
			this._modelIndex = modelIndex;
			this._resource = resource;
		}

		// Token: 0x06004EB6 RID: 20150 RVA: 0x000F6BE0 File Offset: 0x000F4DE0
		public TimeSlot CreateTimeSlot(IEnumerable<Appointment> appointmentsList, ISchedulerModel ownerModel, DateTime start, DateTime end)
		{
			return new TimeSlot(appointmentsList, ownerModel, start, end)
			{
				ModelIndex = this._modelIndex,
				Resource = this._resource
			};
		}

		// Token: 0x04001396 RID: 5014
		private readonly int _modelIndex;

		// Token: 0x04001397 RID: 5015
		private readonly Resource _resource;
	}
}
