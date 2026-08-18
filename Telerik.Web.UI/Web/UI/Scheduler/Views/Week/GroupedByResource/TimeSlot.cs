using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Scheduler.Views.Week.GroupedByResource
{
	// Token: 0x02001AA3 RID: 6819
	internal class TimeSlot : TimeSlot
	{
		// Token: 0x17005006 RID: 20486
		// (get) Token: 0x060107BC RID: 67516 RVA: 0x003AF436 File Offset: 0x003AD636
		// (set) Token: 0x060107BD RID: 67517 RVA: 0x003AF43E File Offset: 0x003AD63E
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

		// Token: 0x17005007 RID: 20487
		// (get) Token: 0x060107BE RID: 67518 RVA: 0x003AF447 File Offset: 0x003AD647
		public override string Index
		{
			get
			{
				return string.Format("{0}:{1}", this.ModelIndex, base.Index);
			}
		}

		// Token: 0x060107BF RID: 67519 RVA: 0x003AF464 File Offset: 0x003AD664
		public TimeSlot(IEnumerable<Appointment> appointmentsList, ISchedulerModel ownerModel, DateTime start, DateTime end) : base(appointmentsList, ownerModel, start, end)
		{
		}

		// Token: 0x040049D2 RID: 18898
		private int _modelIndex;
	}
}
