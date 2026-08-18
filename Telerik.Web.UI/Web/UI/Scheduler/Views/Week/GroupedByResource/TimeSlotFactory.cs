using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Scheduler.Views.Week.GroupedByResource
{
	// Token: 0x02001AA4 RID: 6820
	internal sealed class TimeSlotFactory : IWeekTimeSlotFactory
	{
		// Token: 0x060107C0 RID: 67520 RVA: 0x003AF471 File Offset: 0x003AD671
		public TimeSlotFactory(int modelIndex, Resource resource)
		{
			this._modelIndex = modelIndex;
			this._resource = resource;
		}

		// Token: 0x060107C1 RID: 67521 RVA: 0x003AF488 File Offset: 0x003AD688
		public TimeSlot CreateTimeSlot(IEnumerable<Appointment> appointmentsList, ISchedulerModel ownerModel, DateTime start, DateTime end)
		{
			return new TimeSlot(appointmentsList, ownerModel, start, end)
			{
				ModelIndex = this._modelIndex,
				Resource = this._resource
			};
		}

		// Token: 0x040049D3 RID: 18899
		private readonly int _modelIndex;

		// Token: 0x040049D4 RID: 18900
		private readonly Resource _resource;
	}
}
