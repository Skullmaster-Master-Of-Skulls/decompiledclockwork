using System;
using System.Collections.Generic;

namespace Telerik.Web.Data
{
	// Token: 0x02001BA4 RID: 7076
	public interface IAggregateFunctionsProvider
	{
		// Token: 0x17005392 RID: 21394
		// (get) Token: 0x060111E9 RID: 70121
		IEnumerable<AggregateFunction> AggregateFunctions { get; }
	}
}
