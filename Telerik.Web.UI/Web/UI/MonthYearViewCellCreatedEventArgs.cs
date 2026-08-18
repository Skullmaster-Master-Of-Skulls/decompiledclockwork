using System;
using Telerik.Web.UI.Calendar.View;

namespace Telerik.Web.UI
{
	// Token: 0x02000A32 RID: 2610
	public sealed class MonthYearViewCellCreatedEventArgs : EventArgs
	{
		// Token: 0x17002060 RID: 8288
		// (get) Token: 0x060062C0 RID: 25280 RVA: 0x00173B74 File Offset: 0x00171D74
		// (set) Token: 0x060062C1 RID: 25281 RVA: 0x00173B7C File Offset: 0x00171D7C
		public MonthYearViewCell Cell { get; set; }

		// Token: 0x060062C2 RID: 25282 RVA: 0x00173B85 File Offset: 0x00171D85
		public MonthYearViewCellCreatedEventArgs(MonthYearViewCell cell)
		{
			this.Cell = cell;
		}
	}
}
