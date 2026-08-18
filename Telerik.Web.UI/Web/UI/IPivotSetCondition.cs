using System;
using Telerik.Web.UI.PivotGrid.Core;
using Telerik.Web.UI.PivotGrid.Core.Filtering;

namespace Telerik.Web.UI
{
	// Token: 0x0200075F RID: 1887
	internal interface IPivotSetCondition
	{
		// Token: 0x170015B4 RID: 5556
		// (get) Token: 0x06004291 RID: 17041
		// (set) Token: 0x06004292 RID: 17042
		SetComparison Comparison { get; set; }

		// Token: 0x170015B5 RID: 5557
		// (get) Token: 0x06004293 RID: 17043
		SetConditionHashCollection Items { get; }
	}
}
