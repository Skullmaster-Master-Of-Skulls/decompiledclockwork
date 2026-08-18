using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Scheduler.Views.Timeline.GroupedByResource
{
	// Token: 0x02001A92 RID: 6802
	internal class TimeSlot : TimeSlot
	{
		// Token: 0x17004FF4 RID: 20468
		// (get) Token: 0x0601076D RID: 67437 RVA: 0x003AE280 File Offset: 0x003AC480
		// (set) Token: 0x0601076E RID: 67438 RVA: 0x003AE288 File Offset: 0x003AC488
		public int ModelIndex
		{
			get
			{
				return this._modelIndex;
			}
			set
			{
				this._modelIndex = value;
			}
		}

		// Token: 0x17004FF5 RID: 20469
		// (get) Token: 0x0601076F RID: 67439 RVA: 0x003AE291 File Offset: 0x003AC491
		public override string Index
		{
			get
			{
				return string.Format("{0}:{1}", this.ModelIndex, base.Index);
			}
		}

		// Token: 0x06010770 RID: 67440 RVA: 0x003AE2AE File Offset: 0x003AC4AE
		public TimeSlot(IEnumerable<Appointment> appointmentsList, ISchedulerModel ownerModel, DateTime start, DateTime end) : base(appointmentsList, ownerModel, start, end)
		{
		}

		// Token: 0x040049C3 RID: 18883
		private int _modelIndex;
	}
}
