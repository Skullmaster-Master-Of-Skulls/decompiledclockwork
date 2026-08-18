using System;

namespace Telerik.Web.UI.PivotGrid.Core.Filtering
{
	// Token: 0x020006C6 RID: 1734
	internal interface ISetCondition
	{
		// Token: 0x1700145F RID: 5215
		// (get) Token: 0x06003E2E RID: 15918
		// (set) Token: 0x06003E2F RID: 15919
		SetComparison Comparison { get; set; }

		// Token: 0x17001460 RID: 5216
		// (get) Token: 0x06003E30 RID: 15920
		SetConditionHashCollection Items { get; }
	}
}
