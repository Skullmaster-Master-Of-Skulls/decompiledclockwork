using System;
using Telerik.Web.UI.Scheduler.Views;

namespace Telerik.Web.UI
{
	// Token: 0x02001A38 RID: 6712
	public class TimeSlotCreatedEventArgs : EventArgs
	{
		// Token: 0x06010489 RID: 66697 RVA: 0x003A38AA File Offset: 0x003A1AAA
		public TimeSlotCreatedEventArgs(ISchedulerTimeSlot timeSlot)
		{
			this._timeSlot = timeSlot;
		}

		// Token: 0x17004EEF RID: 20207
		// (get) Token: 0x0601048A RID: 66698 RVA: 0x003A38B9 File Offset: 0x003A1AB9
		public ISchedulerTimeSlot TimeSlot
		{
			get
			{
				return this._timeSlot;
			}
		}

		// Token: 0x04004959 RID: 18777
		private readonly ISchedulerTimeSlot _timeSlot;
	}
}
