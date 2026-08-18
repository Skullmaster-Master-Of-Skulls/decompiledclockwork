using System;
using Telerik.Web.UI.PivotGrid.Core.Filtering;

namespace Telerik.Web.UI.PivotGrid.Filtering
{
	// Token: 0x020006C4 RID: 1732
	internal interface IItemsFilterCondition
	{
		// Token: 0x17001454 RID: 5204
		// (get) Token: 0x06003E15 RID: 15893
		// (set) Token: 0x06003E16 RID: 15894
		ISetCondition DistinctCondition { get; set; }

		// Token: 0x17001455 RID: 5205
		// (get) Token: 0x06003E17 RID: 15895
		// (set) Token: 0x06003E18 RID: 15896
		Condition Condition { get; set; }
	}
}
