using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Scheduler.Views.Week
{
	// Token: 0x02001AA2 RID: 6818
	internal class TimeSlot : SchedulerTimeSlot
	{
		// Token: 0x17005001 RID: 20481
		// (get) Token: 0x060107B1 RID: 67505 RVA: 0x003AF3B0 File Offset: 0x003AD5B0
		// (set) Token: 0x060107B2 RID: 67506 RVA: 0x003AF3B8 File Offset: 0x003AD5B8
		public int PartIndex
		{
			get
			{
				return this._partIndex;
			}
			set
			{
				this._partIndex = value;
			}
		}

		// Token: 0x17005002 RID: 20482
		// (get) Token: 0x060107B3 RID: 67507 RVA: 0x003AF3C1 File Offset: 0x003AD5C1
		// (set) Token: 0x060107B4 RID: 67508 RVA: 0x003AF3C9 File Offset: 0x003AD5C9
		public int RowIndex
		{
			get
			{
				return this._rowIndex;
			}
			set
			{
				this._rowIndex = value;
			}
		}

		// Token: 0x17005003 RID: 20483
		// (get) Token: 0x060107B5 RID: 67509 RVA: 0x003AF3D2 File Offset: 0x003AD5D2
		// (set) Token: 0x060107B6 RID: 67510 RVA: 0x003AF3DA File Offset: 0x003AD5DA
		public int CellIndex
		{
			get
			{
				return this._cellIndex;
			}
			set
			{
				this._cellIndex = value;
			}
		}

		// Token: 0x17005004 RID: 20484
		// (get) Token: 0x060107B7 RID: 67511 RVA: 0x003AF3E3 File Offset: 0x003AD5E3
		// (set) Token: 0x060107B8 RID: 67512 RVA: 0x003AF3EB File Offset: 0x003AD5EB
		public bool IsAllDaySlot
		{
			get
			{
				return this._isAllDaySlot;
			}
			set
			{
				this._isAllDaySlot = value;
			}
		}

		// Token: 0x17005005 RID: 20485
		// (get) Token: 0x060107B9 RID: 67513 RVA: 0x003AF3F4 File Offset: 0x003AD5F4
		public override string Index
		{
			get
			{
				return string.Format("{0}:{1}:{2}", this.PartIndex, this.RowIndex, this.CellIndex);
			}
		}

		// Token: 0x060107BA RID: 67514 RVA: 0x003AF421 File Offset: 0x003AD621
		public TimeSlot(IEnumerable<Appointment> appointmentsList, ISchedulerModel ownerModel, DateTime start, DateTime end) : base(appointmentsList, ownerModel, start, end)
		{
		}

		// Token: 0x060107BB RID: 67515 RVA: 0x003AF42E File Offset: 0x003AD62E
		protected TimeSlot()
		{
		}

		// Token: 0x040049CE RID: 18894
		private int _partIndex;

		// Token: 0x040049CF RID: 18895
		private int _rowIndex;

		// Token: 0x040049D0 RID: 18896
		private int _cellIndex;

		// Token: 0x040049D1 RID: 18897
		private bool _isAllDaySlot;
	}
}
