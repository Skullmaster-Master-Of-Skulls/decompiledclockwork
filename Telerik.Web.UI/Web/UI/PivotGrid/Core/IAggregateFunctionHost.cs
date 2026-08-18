using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000696 RID: 1686
	internal interface IAggregateFunctionHost
	{
		// Token: 0x17001411 RID: 5137
		// (get) Token: 0x06003D28 RID: 15656
		// (set) Token: 0x06003D29 RID: 15657
		object AggregateFunction { get; set; }

		// Token: 0x17001412 RID: 5138
		// (get) Token: 0x06003D2A RID: 15658
		IEnumerable<object> SupportedAggregateFunctions { get; }
	}
}
