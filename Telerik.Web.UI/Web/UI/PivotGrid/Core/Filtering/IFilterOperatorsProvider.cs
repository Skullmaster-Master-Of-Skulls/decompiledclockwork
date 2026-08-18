using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.PivotGrid.Core.Filtering
{
	// Token: 0x020006C1 RID: 1729
	internal interface IFilterOperatorsProvider
	{
		// Token: 0x06003E0A RID: 15882
		IEnumerable<object> GetAvailableConditions();
	}
}
