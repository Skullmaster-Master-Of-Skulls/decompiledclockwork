using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Scheduler.Views.Month
{
	// Token: 0x02001A7A RID: 6778
	internal class TimeSlot : SchedulerTimeSlot
	{
		// Token: 0x17004FC0 RID: 20416
		// (get) Token: 0x060106BB RID: 67259 RVA: 0x003AB629 File Offset: 0x003A9829
		// (set) Token: 0x060106BC RID: 67260 RVA: 0x003AB631 File Offset: 0x003A9831
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

		// Token: 0x17004FC1 RID: 20417
		// (get) Token: 0x060106BD RID: 67261 RVA: 0x003AB63C File Offset: 0x003A983C
		public override string Index
		{
			get
			{
				return this.DayIndex.ToString();
			}
		}

		// Token: 0x17004FC2 RID: 20418
		// (get) Token: 0x060106BE RID: 67262 RVA: 0x003AB657 File Offset: 0x003A9857
		// (set) Token: 0x060106BF RID: 67263 RVA: 0x003AB65F File Offset: 0x003A985F
		public bool HasMoreAppointments
		{
			get
			{
				return this._hasMoreAppointments;
			}
			set
			{
				this._hasMoreAppointments = value;
			}
		}

		// Token: 0x060106C0 RID: 67264 RVA: 0x003AB668 File Offset: 0x003A9868
		public TimeSlot(IEnumerable<Appointment> appointmentsList, ISchedulerModel ownerModel, DateTime start, DateTime end) : base(appointmentsList, ownerModel, start, end)
		{
		}

		// Token: 0x060106C1 RID: 67265 RVA: 0x003AB675 File Offset: 0x003A9875
		protected TimeSlot()
		{
		}

		// Token: 0x040049A5 RID: 18853
		private int _dayIndex;

		// Token: 0x040049A6 RID: 18854
		private bool _hasMoreAppointments;
	}
}
