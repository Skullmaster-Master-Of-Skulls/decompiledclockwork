using System;
using Telerik.Web.UI.PivotGrid.Core;

namespace Telerik.Web.UI
{
	// Token: 0x02000DB3 RID: 3507
	[Serializable]
	public abstract class PivotGridSortedGroupsFilter : PivotGridFilter
	{
		// Token: 0x1700295F RID: 10591
		// (get) Token: 0x060082F5 RID: 33525 RVA: 0x001DDA72 File Offset: 0x001DBC72
		// (set) Token: 0x060082F6 RID: 33526 RVA: 0x001DDA7A File Offset: 0x001DBC7A
		public int AggregateIndex { get; set; }

		// Token: 0x17002960 RID: 10592
		// (get) Token: 0x060082F7 RID: 33527 RVA: 0x001DDA83 File Offset: 0x001DBC83
		// (set) Token: 0x060082F8 RID: 33528 RVA: 0x001DDA8B File Offset: 0x001DBC8B
		public SortedListSelection Selection { get; set; }
	}
}
