using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Scheduler.Views.Timeline.GroupedByResource
{
	// Token: 0x02001A94 RID: 6804
	internal sealed class TimeSlotFactory : ITimelineTimeSlotFactory
	{
		// Token: 0x06010772 RID: 67442 RVA: 0x003AE2BB File Offset: 0x003AC4BB
		public TimeSlotFactory(int modelIndex, Resource resource)
		{
			this._modelIndex = modelIndex;
			this._resource = resource;
		}

		// Token: 0x06010773 RID: 67443 RVA: 0x003AE2D4 File Offset: 0x003AC4D4
		public TimeSlot CreateTimeSlot(IEnumerable<Appointment> appointmentsList, ISchedulerModel ownerModel, DateTime start, DateTime end)
		{
			return new TimeSlot(appointmentsList, ownerModel, start, end)
			{
				ModelIndex = this._modelIndex,
				Resource = this._resource
			};
		}

		// Token: 0x040049C4 RID: 18884
		private readonly int _modelIndex;

		// Token: 0x040049C5 RID: 18885
		private readonly Resource _resource;
	}
}
