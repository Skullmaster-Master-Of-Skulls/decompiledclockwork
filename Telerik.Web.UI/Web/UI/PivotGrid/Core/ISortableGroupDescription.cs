using System;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x0200069C RID: 1692
	internal interface ISortableGroupDescription : IEditable, INamed
	{
		// Token: 0x17001414 RID: 5140
		// (get) Token: 0x06003D31 RID: 15665
		// (set) Token: 0x06003D32 RID: 15666
		SortOrder SortOrder { get; set; }

		// Token: 0x17001415 RID: 5141
		// (get) Token: 0x06003D33 RID: 15667
		// (set) Token: 0x06003D34 RID: 15668
		GroupComparer GroupComparer { get; set; }
	}
}
