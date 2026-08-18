using System;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x020006D0 RID: 1744
	public interface IAggregateSummaryValues
	{
		// Token: 0x17001474 RID: 5236
		// (get) Token: 0x06003EA3 RID: 16035
		Coordinate Coordinate { get; }

		// Token: 0x06003EA4 RID: 16036
		AggregateValue GetAggregateValue(object groupName);
	}
}
