using System;

namespace Telerik.Web.UI.PivotGrid.Core.Aggregates
{
	// Token: 0x02000689 RID: 1673
	public interface IAggregateContext
	{
		// Token: 0x17001402 RID: 5122
		// (get) Token: 0x06003CDF RID: 15583
		Type DataType { get; }

		// Token: 0x17001403 RID: 5123
		// (get) Token: 0x06003CE0 RID: 15584
		bool HasCalculatedGroups { get; }
	}
}
