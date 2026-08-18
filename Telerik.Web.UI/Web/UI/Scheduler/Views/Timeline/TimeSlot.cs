using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Scheduler.Views.Timeline
{
	// Token: 0x02001A91 RID: 6801
	internal class TimeSlot : SchedulerTimeSlot
	{
		// Token: 0x17004FF2 RID: 20466
		// (get) Token: 0x06010768 RID: 67432 RVA: 0x003AE23C File Offset: 0x003AC43C
		// (set) Token: 0x06010769 RID: 67433 RVA: 0x003AE244 File Offset: 0x003AC444
		public int IntervalIndex
		{
			get
			{
				return this._intervalIndex;
			}
			set
			{
				this._intervalIndex = value;
			}
		}

		// Token: 0x17004FF3 RID: 20467
		// (get) Token: 0x0601076A RID: 67434 RVA: 0x003AE250 File Offset: 0x003AC450
		public override string Index
		{
			get
			{
				return this.IntervalIndex.ToString();
			}
		}

		// Token: 0x0601076B RID: 67435 RVA: 0x003AE26B File Offset: 0x003AC46B
		public TimeSlot(IEnumerable<Appointment> appointmentsList, ISchedulerModel ownerModel, DateTime start, DateTime end) : base(appointmentsList, ownerModel, start, end)
		{
		}

		// Token: 0x0601076C RID: 67436 RVA: 0x003AE278 File Offset: 0x003AC478
		protected TimeSlot()
		{
		}

		// Token: 0x040049C2 RID: 18882
		private int _intervalIndex;
	}
}
