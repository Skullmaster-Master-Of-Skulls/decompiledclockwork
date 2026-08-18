using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Scheduler.Views.Agenda.GroupedByResource
{
	// Token: 0x0200083B RID: 2107
	internal class TimeSlot : TimeSlot
	{
		// Token: 0x1700198B RID: 6539
		// (get) Token: 0x06004E17 RID: 19991 RVA: 0x000F4C7C File Offset: 0x000F2E7C
		// (set) Token: 0x06004E18 RID: 19992 RVA: 0x000F4C84 File Offset: 0x000F2E84
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

		// Token: 0x1700198C RID: 6540
		// (get) Token: 0x06004E19 RID: 19993 RVA: 0x000F4C8D File Offset: 0x000F2E8D
		public override string Index
		{
			get
			{
				return string.Format("{0}:{1}", this.ModelIndex, base.Index);
			}
		}

		// Token: 0x06004E1A RID: 19994 RVA: 0x000F4CAA File Offset: 0x000F2EAA
		public TimeSlot(IEnumerable<Appointment> appointmentsList, ISchedulerModel ownerModel, DateTime start, DateTime end) : base(appointmentsList, ownerModel, start, end)
		{
		}

		// Token: 0x04001377 RID: 4983
		private int _modelIndex;
	}
}
