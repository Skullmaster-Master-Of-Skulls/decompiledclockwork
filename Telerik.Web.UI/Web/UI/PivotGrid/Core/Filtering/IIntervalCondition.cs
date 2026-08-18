using System;

namespace Telerik.Web.UI.PivotGrid.Core.Filtering
{
	// Token: 0x020006C3 RID: 1731
	internal interface IIntervalCondition
	{
		// Token: 0x17001450 RID: 5200
		// (get) Token: 0x06003E0D RID: 15885
		// (set) Token: 0x06003E0E RID: 15886
		object From { get; set; }

		// Token: 0x17001451 RID: 5201
		// (get) Token: 0x06003E0F RID: 15887
		// (set) Token: 0x06003E10 RID: 15888
		object To { get; set; }

		// Token: 0x17001452 RID: 5202
		// (get) Token: 0x06003E11 RID: 15889
		// (set) Token: 0x06003E12 RID: 15890
		IntervalComparison Condition { get; set; }

		// Token: 0x17001453 RID: 5203
		// (get) Token: 0x06003E13 RID: 15891
		// (set) Token: 0x06003E14 RID: 15892
		bool IgnoreCase { get; set; }
	}
}
