using System;
using Telerik.Web.UI.PivotGrid.Core;

namespace Telerik.Web.UI
{
	// Token: 0x02000755 RID: 1877
	internal interface IPivotComparisonCondition
	{
		// Token: 0x170015A2 RID: 5538
		// (get) Token: 0x06004262 RID: 16994
		// (set) Token: 0x06004263 RID: 16995
		object Than { get; set; }

		// Token: 0x170015A3 RID: 5539
		// (get) Token: 0x06004264 RID: 16996
		// (set) Token: 0x06004265 RID: 16997
		Comparison Condition { get; set; }

		// Token: 0x170015A4 RID: 5540
		// (get) Token: 0x06004266 RID: 16998
		// (set) Token: 0x06004267 RID: 16999
		bool IgnoreCase { get; set; }
	}
}
