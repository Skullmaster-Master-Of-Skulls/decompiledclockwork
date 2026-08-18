using System;

namespace Telerik.Web.UI.PivotGrid.Core.Filtering
{
	// Token: 0x020006DC RID: 1756
	internal interface ILabelGroupFilter : IConditionFactory
	{
		// Token: 0x17001481 RID: 5249
		// (get) Token: 0x06003EC2 RID: 16066
		// (set) Token: 0x06003EC3 RID: 16067
		Condition Condition { get; set; }
	}
}
