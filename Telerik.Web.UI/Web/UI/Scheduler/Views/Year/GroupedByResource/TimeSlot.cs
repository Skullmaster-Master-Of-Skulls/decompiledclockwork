using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Scheduler.Views.Year.GroupedByResource
{
	// Token: 0x02000853 RID: 2131
	internal class TimeSlot : TimeSlot
	{
		// Token: 0x170019BC RID: 6588
		// (get) Token: 0x06004EB0 RID: 20144 RVA: 0x000F6B8E File Offset: 0x000F4D8E
		// (set) Token: 0x06004EB1 RID: 20145 RVA: 0x000F6B96 File Offset: 0x000F4D96
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

		// Token: 0x170019BD RID: 6589
		// (get) Token: 0x06004EB2 RID: 20146 RVA: 0x000F6B9F File Offset: 0x000F4D9F
		public override string Index
		{
			get
			{
				return string.Format("{0}:{1}", this.ModelIndex, base.Index);
			}
		}

		// Token: 0x06004EB3 RID: 20147 RVA: 0x000F6BBC File Offset: 0x000F4DBC
		public TimeSlot(IEnumerable<Appointment> appointmentsList, ISchedulerModel ownerModel, DateTime start, DateTime end) : base(appointmentsList, ownerModel, start, end)
		{
		}

		// Token: 0x04001395 RID: 5013
		private int _modelIndex;
	}
}
