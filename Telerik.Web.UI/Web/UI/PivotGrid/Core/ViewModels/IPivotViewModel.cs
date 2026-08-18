using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.PivotGrid.Core.ViewModels
{
	// Token: 0x0200071D RID: 1821
	internal interface IPivotViewModel
	{
		// Token: 0x140000AD RID: 173
		// (add) Token: 0x060040A9 RID: 16553
		// (remove) Token: 0x060040AA RID: 16554
		event EventHandler<EventArgs> Completed;

		// Token: 0x17001521 RID: 5409
		// (get) Token: 0x060040AB RID: 16555
		IEnumerable<IGroup> RowGroups { get; }

		// Token: 0x17001522 RID: 5410
		// (get) Token: 0x060040AC RID: 16556
		IEnumerable<IGroup> ColumnGroups { get; }

		// Token: 0x17001523 RID: 5411
		// (set) Token: 0x060040AD RID: 16557
		TotalsPosition RowsSubTotalsPosition { set; }

		// Token: 0x17001524 RID: 5412
		// (set) Token: 0x060040AE RID: 16558
		TotalsPosition RowGrandTotalsPosition { set; }

		// Token: 0x17001525 RID: 5413
		// (set) Token: 0x060040AF RID: 16559
		TotalsPosition ColumnGrandTotalsPosition { set; }

		// Token: 0x17001526 RID: 5414
		// (set) Token: 0x060040B0 RID: 16560
		TotalsPosition ColumnsSubTotalsPosition { set; }

		// Token: 0x17001527 RID: 5415
		// (set) Token: 0x060040B1 RID: 16561
		IDataProvider DataProvider { set; }

		// Token: 0x060040B2 RID: 16562
		CellAggregateValue GetAggregateValue(IGroup group1, IGroup group2, bool p1, bool p2);

		// Token: 0x060040B3 RID: 16563
		void Refresh();
	}
}
