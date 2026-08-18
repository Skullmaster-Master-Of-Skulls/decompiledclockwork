using System;
using System.ComponentModel;
using Telerik.Web.UI.Scheduler.Views;

namespace Telerik.Web.UI
{
	// Token: 0x02001A14 RID: 6676
	public class TimeSlotContextMenuItemClickingEventArgs : CancelEventArgs
	{
		// Token: 0x06010263 RID: 66147 RVA: 0x0039FAA9 File Offset: 0x0039DCA9
		public TimeSlotContextMenuItemClickingEventArgs(ISchedulerTimeSlot timeSlot, RadMenuItem menuItem, ISchedulerTimeSlot startSlot, ISchedulerTimeSlot endSlot)
		{
			this._menuItem = menuItem;
			this._timeSlot = timeSlot;
			this._startSlot = startSlot;
			this._endSlot = endSlot;
		}

		// Token: 0x17004DFA RID: 19962
		// (get) Token: 0x06010264 RID: 66148 RVA: 0x0039FACE File Offset: 0x0039DCCE
		public RadMenuItem MenuItem
		{
			get
			{
				return this._menuItem;
			}
		}

		// Token: 0x17004DFB RID: 19963
		// (get) Token: 0x06010265 RID: 66149 RVA: 0x0039FAD6 File Offset: 0x0039DCD6
		public ISchedulerTimeSlot TimeSlot
		{
			get
			{
				return this._timeSlot;
			}
		}

		// Token: 0x17004DFC RID: 19964
		// (get) Token: 0x06010266 RID: 66150 RVA: 0x0039FADE File Offset: 0x0039DCDE
		public ISchedulerTimeSlot StartSlot
		{
			get
			{
				return this._startSlot;
			}
		}

		// Token: 0x17004DFD RID: 19965
		// (get) Token: 0x06010267 RID: 66151 RVA: 0x0039FAE6 File Offset: 0x0039DCE6
		public ISchedulerTimeSlot EndSlot
		{
			get
			{
				return this._endSlot;
			}
		}

		// Token: 0x0400491B RID: 18715
		private readonly RadMenuItem _menuItem;

		// Token: 0x0400491C RID: 18716
		private readonly ISchedulerTimeSlot _timeSlot;

		// Token: 0x0400491D RID: 18717
		private readonly ISchedulerTimeSlot _startSlot;

		// Token: 0x0400491E RID: 18718
		private readonly ISchedulerTimeSlot _endSlot;
	}
}
