using System;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Calendar.View;

namespace Telerik.Web.UI.Calendar
{
	// Token: 0x02000FFA RID: 4090
	public sealed class DayRenderEventArgs : EventArgs
	{
		// Token: 0x0600A000 RID: 40960 RVA: 0x0023A300 File Offset: 0x00238500
		public DayRenderEventArgs(TableCell cell, RadCalendarDay day, MonthView currentView)
		{
			this._Day = day;
			this._Cell = cell;
			this._View = currentView;
		}

		// Token: 0x1700328F RID: 12943
		// (get) Token: 0x0600A001 RID: 40961 RVA: 0x0023A31D File Offset: 0x0023851D
		public TableCell Cell
		{
			get
			{
				return this._Cell;
			}
		}

		// Token: 0x17003290 RID: 12944
		// (get) Token: 0x0600A002 RID: 40962 RVA: 0x0023A325 File Offset: 0x00238525
		public RadCalendarDay Day
		{
			get
			{
				return this._Day;
			}
		}

		// Token: 0x17003291 RID: 12945
		// (get) Token: 0x0600A003 RID: 40963 RVA: 0x0023A32D File Offset: 0x0023852D
		public MonthView View
		{
			get
			{
				return this._View;
			}
		}

		// Token: 0x04002CCF RID: 11471
		private TableCell _Cell;

		// Token: 0x04002CD0 RID: 11472
		private RadCalendarDay _Day;

		// Token: 0x04002CD1 RID: 11473
		private MonthView _View;
	}
}
