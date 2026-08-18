using System;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x0200068A RID: 1674
	public interface IAggregateValues
	{
		// Token: 0x17001404 RID: 5124
		// (get) Token: 0x06003CE1 RID: 15585
		Coordinate Coordinate { get; }

		// Token: 0x06003CE2 RID: 15586
		AggregateValue GetAggregateValue(RequiredField calculatedFieldSettings);
	}
}
