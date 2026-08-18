using System;

namespace Telerik.Web.UI.PivotGrid.Core.Filtering
{
	// Token: 0x020006DD RID: 1757
	internal interface IValueGroupFilter : IConditionFactory
	{
		// Token: 0x17001482 RID: 5250
		// (get) Token: 0x06003EC4 RID: 16068
		// (set) Token: 0x06003EC5 RID: 16069
		Condition Condition { get; set; }

		// Token: 0x17001483 RID: 5251
		// (get) Token: 0x06003EC6 RID: 16070
		// (set) Token: 0x06003EC7 RID: 16071
		int AggregateIndex { get; set; }
	}
}
