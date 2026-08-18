using System;
using Telerik.Web.UI.Calendar.View;

namespace Telerik.Web.UI.Calendar
{
	// Token: 0x02000A0B RID: 2571
	public class ChildViewRenderEventArgs : EventArgs
	{
		// Token: 0x06006192 RID: 24978 RVA: 0x00170229 File Offset: 0x0016E429
		public ChildViewRenderEventArgs(CalendarView calView)
		{
			this.calendarView = calView;
		}

		// Token: 0x17001FF8 RID: 8184
		// (get) Token: 0x06006193 RID: 24979 RVA: 0x00170238 File Offset: 0x0016E438
		public CalendarView CalendarView
		{
			get
			{
				return this.calendarView;
			}
		}

		// Token: 0x040017CA RID: 6090
		private CalendarView calendarView;
	}
}
