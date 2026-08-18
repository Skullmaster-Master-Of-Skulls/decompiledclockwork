using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Scheduler.Views.Month.GroupedByResource
{
	// Token: 0x02001A7B RID: 6779
	internal class TimeSlot : TimeSlot
	{
		// Token: 0x17004FC3 RID: 20419
		// (get) Token: 0x060106C2 RID: 67266 RVA: 0x003AB67D File Offset: 0x003A987D
		// (set) Token: 0x060106C3 RID: 67267 RVA: 0x003AB685 File Offset: 0x003A9885
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

		// Token: 0x17004FC4 RID: 20420
		// (get) Token: 0x060106C4 RID: 67268 RVA: 0x003AB68E File Offset: 0x003A988E
		public override string Index
		{
			get
			{
				return string.Format("{0}:{1}", this.ModelIndex, base.Index);
			}
		}

		// Token: 0x060106C5 RID: 67269 RVA: 0x003AB6AB File Offset: 0x003A98AB
		public TimeSlot(IEnumerable<Appointment> appointmentsList, ISchedulerModel ownerModel, DateTime start, DateTime end) : base(appointmentsList, ownerModel, start, end)
		{
		}

		// Token: 0x040049A7 RID: 18855
		private int _modelIndex;
	}
}
