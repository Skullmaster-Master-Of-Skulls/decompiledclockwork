using System;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000712 RID: 1810
	public interface IAggregateResultProvider
	{
		// Token: 0x170014F1 RID: 5361
		// (get) Token: 0x06004055 RID: 16469
		Coordinate Root { get; }

		// Token: 0x06004056 RID: 16470
		AggregateValue GetAggregateResult(int aggregateIndex, Coordinate groups);
	}
}
