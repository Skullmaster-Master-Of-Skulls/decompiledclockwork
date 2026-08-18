using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Scheduler.Views.Year
{
	// Token: 0x02000852 RID: 2130
	internal class TimeSlot : SchedulerTimeSlot
	{
		// Token: 0x170019B8 RID: 6584
		// (get) Token: 0x06004EA7 RID: 20135 RVA: 0x000F6B24 File Offset: 0x000F4D24
		// (set) Token: 0x06004EA8 RID: 20136 RVA: 0x000F6B2C File Offset: 0x000F4D2C
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

		// Token: 0x170019B9 RID: 6585
		// (get) Token: 0x06004EA9 RID: 20137 RVA: 0x000F6B35 File Offset: 0x000F4D35
		// (set) Token: 0x06004EAA RID: 20138 RVA: 0x000F6B3D File Offset: 0x000F4D3D
		public int MonthIndex
		{
			get
			{
				return this._monthIndex;
			}
			set
			{
				this._monthIndex = value;
			}
		}

		// Token: 0x170019BA RID: 6586
		// (get) Token: 0x06004EAB RID: 20139 RVA: 0x000F6B46 File Offset: 0x000F4D46
		// (set) Token: 0x06004EAC RID: 20140 RVA: 0x000F6B4E File Offset: 0x000F4D4E
		public bool IsOtherMonth
		{
			get
			{
				return this._isOtherMonth;
			}
			set
			{
				this._isOtherMonth = value;
			}
		}

		// Token: 0x170019BB RID: 6587
		// (get) Token: 0x06004EAD RID: 20141 RVA: 0x000F6B57 File Offset: 0x000F4D57
		public override string Index
		{
			get
			{
				return string.Format("{0}:{1}", this.MonthIndex, this.DayIndex);
			}
		}

		// Token: 0x06004EAE RID: 20142 RVA: 0x000F6B79 File Offset: 0x000F4D79
		public TimeSlot(IEnumerable<Appointment> appointmentsList, ISchedulerModel ownerModel, DateTime start, DateTime end) : base(appointmentsList, ownerModel, start, end)
		{
		}

		// Token: 0x06004EAF RID: 20143 RVA: 0x000F6B86 File Offset: 0x000F4D86
		protected TimeSlot()
		{
		}

		// Token: 0x04001392 RID: 5010
		private int _dayIndex;

		// Token: 0x04001393 RID: 5011
		private int _monthIndex;

		// Token: 0x04001394 RID: 5012
		private bool _isOtherMonth;
	}
}
