using System;
using Telerik.Web.UI.Scheduler.Views;

namespace Telerik.Web.UI
{
	// Token: 0x02001A13 RID: 6675
	public class TimeSlotContextMenuItemClickedEventArgs : EventArgs
	{
		// Token: 0x0601025E RID: 66142 RVA: 0x0039FA64 File Offset: 0x0039DC64
		public TimeSlotContextMenuItemClickedEventArgs(ISchedulerTimeSlot timeSlot, RadMenuItem menuItem, ISchedulerTimeSlot startSlot, ISchedulerTimeSlot endSlot)
		{
			this._menuItem = menuItem;
			this._timeSlot = timeSlot;
			this._startSlot = startSlot;
			this._endSlot = endSlot;
		}

		// Token: 0x17004DF6 RID: 19958
		// (get) Token: 0x0601025F RID: 66143 RVA: 0x0039FA89 File Offset: 0x0039DC89
		public RadMenuItem MenuItem
		{
			get
			{
				return this._menuItem;
			}
		}

		// Token: 0x17004DF7 RID: 19959
		// (get) Token: 0x06010260 RID: 66144 RVA: 0x0039FA91 File Offset: 0x0039DC91
		public ISchedulerTimeSlot TimeSlot
		{
			get
			{
				return this._timeSlot;
			}
		}

		// Token: 0x17004DF8 RID: 19960
		// (get) Token: 0x06010261 RID: 66145 RVA: 0x0039FA99 File Offset: 0x0039DC99
		public ISchedulerTimeSlot StartSlot
		{
			get
			{
				return this._startSlot;
			}
		}

		// Token: 0x17004DF9 RID: 19961
		// (get) Token: 0x06010262 RID: 66146 RVA: 0x0039FAA1 File Offset: 0x0039DCA1
		public ISchedulerTimeSlot EndSlot
		{
			get
			{
				return this._endSlot;
			}
		}

		// Token: 0x04004917 RID: 18711
		private readonly RadMenuItem _menuItem;

		// Token: 0x04004918 RID: 18712
		private readonly ISchedulerTimeSlot _timeSlot;

		// Token: 0x04004919 RID: 18713
		private readonly ISchedulerTimeSlot _startSlot;

		// Token: 0x0400491A RID: 18714
		private readonly ISchedulerTimeSlot _endSlot;
	}
}
