using System;

namespace Telerik.Web.UI.PivotGrid.Core.Filtering
{
	// Token: 0x020006BD RID: 1725
	internal interface IComparisonCondition
	{
		// Token: 0x17001444 RID: 5188
		// (get) Token: 0x06003DF1 RID: 15857
		// (set) Token: 0x06003DF2 RID: 15858
		object Than { get; set; }

		// Token: 0x17001445 RID: 5189
		// (get) Token: 0x06003DF3 RID: 15859
		// (set) Token: 0x06003DF4 RID: 15860
		Comparison Condition { get; set; }

		// Token: 0x17001446 RID: 5190
		// (get) Token: 0x06003DF5 RID: 15861
		// (set) Token: 0x06003DF6 RID: 15862
		bool IgnoreCase { get; set; }
	}
}
