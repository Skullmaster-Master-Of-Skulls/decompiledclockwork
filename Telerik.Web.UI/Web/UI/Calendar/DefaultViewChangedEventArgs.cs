using System;
using Telerik.Web.UI.Calendar.View;

namespace Telerik.Web.UI.Calendar
{
	// Token: 0x02000FFC RID: 4092
	public sealed class DefaultViewChangedEventArgs : EventArgs
	{
		// Token: 0x0600A008 RID: 40968 RVA: 0x0023A335 File Offset: 0x00238535
		public DefaultViewChangedEventArgs(CalendarView previousView, CalendarView nextView)
		{
			this._PreviousView = previousView;
			this._NextView = nextView;
		}

		// Token: 0x17003292 RID: 12946
		// (get) Token: 0x0600A009 RID: 40969 RVA: 0x0023A34B File Offset: 0x0023854B
		public CalendarView OldView
		{
			get
			{
				return this._PreviousView;
			}
		}

		// Token: 0x17003293 RID: 12947
		// (get) Token: 0x0600A00A RID: 40970 RVA: 0x0023A353 File Offset: 0x00238553
		public CalendarView NewView
		{
			get
			{
				return this._NextView;
			}
		}

		// Token: 0x04002CD2 RID: 11474
		private CalendarView _PreviousView;

		// Token: 0x04002CD3 RID: 11475
		private CalendarView _NextView;
	}
}
