using System;
using Telerik.Web.UI.PivotGrid.Core.Filtering;

namespace Telerik.Web.UI
{
	// Token: 0x02000757 RID: 1879
	internal interface IPivotIntervalCondition
	{
		// Token: 0x170015A8 RID: 5544
		// (get) Token: 0x06004270 RID: 17008
		// (set) Token: 0x06004271 RID: 17009
		object From { get; set; }

		// Token: 0x170015A9 RID: 5545
		// (get) Token: 0x06004272 RID: 17010
		// (set) Token: 0x06004273 RID: 17011
		object To { get; set; }

		// Token: 0x170015AA RID: 5546
		// (get) Token: 0x06004274 RID: 17012
		// (set) Token: 0x06004275 RID: 17013
		IntervalComparison Condition { get; set; }

		// Token: 0x170015AB RID: 5547
		// (get) Token: 0x06004276 RID: 17014
		// (set) Token: 0x06004277 RID: 17015
		bool IgnoreCase { get; set; }
	}
}
