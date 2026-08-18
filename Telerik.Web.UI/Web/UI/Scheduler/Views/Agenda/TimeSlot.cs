using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Scheduler.Views.Agenda
{
	// Token: 0x0200083A RID: 2106
	internal class TimeSlot : SchedulerTimeSlot
	{
		// Token: 0x17001989 RID: 6537
		// (get) Token: 0x06004E12 RID: 19986 RVA: 0x000F4C38 File Offset: 0x000F2E38
		// (set) Token: 0x06004E13 RID: 19987 RVA: 0x000F4C40 File Offset: 0x000F2E40
		public int DayIndex
		{
			get
			{
				return this._dayIndex;
			}
			set
			{
				this._dayIndex = value;
			}
		}

		// Token: 0x1700198A RID: 6538
		// (get) Token: 0x06004E14 RID: 19988 RVA: 0x000F4C4C File Offset: 0x000F2E4C
		public override string Index
		{
			get
			{
				return this.DayIndex.ToString();
			}
		}

		// Token: 0x06004E15 RID: 19989 RVA: 0x000F4C67 File Offset: 0x000F2E67
		public TimeSlot(IEnumerable<Appointment> appointmentsList, ISchedulerModel ownerModel, DateTime start, DateTime end) : base(appointmentsList, ownerModel, start, end)
		{
		}

		// Token: 0x06004E16 RID: 19990 RVA: 0x000F4C74 File Offset: 0x000F2E74
		protected TimeSlot()
		{
		}

		// Token: 0x04001376 RID: 4982
		private int _dayIndex;
	}
}
