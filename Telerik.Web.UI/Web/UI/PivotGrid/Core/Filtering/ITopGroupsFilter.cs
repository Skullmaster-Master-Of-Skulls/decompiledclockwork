using System;

namespace Telerik.Web.UI.PivotGrid.Core.Filtering
{
	// Token: 0x020006D8 RID: 1752
	internal interface ITopGroupsFilter
	{
		// Token: 0x1700147C RID: 5244
		// (get) Token: 0x06003EB8 RID: 16056
		// (set) Token: 0x06003EB9 RID: 16057
		int AggregateIndex { get; set; }

		// Token: 0x1700147D RID: 5245
		// (get) Token: 0x06003EBA RID: 16058
		// (set) Token: 0x06003EBB RID: 16059
		SortedListSelection Selection { get; set; }
	}
}
