using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Scheduler.Views
{
	// Token: 0x02000827 RID: 2087
	internal interface ISchedulerView
	{
		// Token: 0x17001937 RID: 6455
		// (get) Token: 0x06004D34 RID: 19764
		IList<ViewHeader> RowHeaders { get; }

		// Token: 0x17001938 RID: 6456
		// (get) Token: 0x06004D35 RID: 19765
		IList<ViewHeader> ColumnHeaders { get; }

		// Token: 0x17001939 RID: 6457
		// (get) Token: 0x06004D36 RID: 19766
		int ColumnHeadersDepth { get; }

		// Token: 0x1700193A RID: 6458
		// (get) Token: 0x06004D37 RID: 19767
		ISchedulerModel Model { get; }

		// Token: 0x1700193B RID: 6459
		// (get) Token: 0x06004D38 RID: 19768
		int RowHeadersDepth { get; }

		// Token: 0x1700193C RID: 6460
		// (get) Token: 0x06004D39 RID: 19769
		RadScheduler Owner { get; }
	}
}
