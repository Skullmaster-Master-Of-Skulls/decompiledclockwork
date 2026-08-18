using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Scheduler.Views.Month.GroupedByResource
{
	// Token: 0x02001A7D RID: 6781
	internal class TimeSlotFactory : IMonthTimeSlotFactory
	{
		// Token: 0x060106C7 RID: 67271 RVA: 0x003AB6B8 File Offset: 0x003A98B8
		public TimeSlotFactory(int modelIndex, Resource resource)
		{
			this._modelIndex = modelIndex;
			this._resource = resource;
		}

		// Token: 0x060106C8 RID: 67272 RVA: 0x003AB6D0 File Offset: 0x003A98D0
		public TimeSlot CreateTimeSlot(IEnumerable<Appointment> appointmentsList, ISchedulerModel ownerModel, DateTime start, DateTime end)
		{
			return new TimeSlot(appointmentsList, ownerModel, start, end)
			{
				ModelIndex = this._modelIndex,
				Resource = this._resource
			};
		}

		// Token: 0x040049A8 RID: 18856
		private readonly int _modelIndex;

		// Token: 0x040049A9 RID: 18857
		private readonly Resource _resource;
	}
}
