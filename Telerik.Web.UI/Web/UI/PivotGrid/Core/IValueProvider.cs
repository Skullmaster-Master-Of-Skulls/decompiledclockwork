using System;
using System.Collections;
using System.Collections.Generic;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000C92 RID: 3218
	internal interface IValueProvider
	{
		// Token: 0x060078F4 RID: 30964
		IEnumerable GetRowGroupNames(object item);

		// Token: 0x060078F5 RID: 30965
		IEnumerable GetColumnGroupNames(object item);

		// Token: 0x060078F6 RID: 30966
		object GetAggregateValue(int aggregateDescriptionIndex, object item);

		// Token: 0x060078F7 RID: 30967
		AggregateValue CreateAggregateValue(int aggregateDescriptionIndex, bool hasCalculatedGroups);

		// Token: 0x060078F8 RID: 30968
		string GetAggregateStringFormat(int aggregateDescriptionIndex);

		// Token: 0x060078F9 RID: 30969
		bool PassesFilter(object[] items);

		// Token: 0x060078FA RID: 30970
		object[] GetFilterItems(object fact);

		// Token: 0x060078FB RID: 30971
		int GetFiltersCount();

		// Token: 0x060078FC RID: 30972
		IEnumerable<CalculatedItem> GetRowCalculatedItems(int level);

		// Token: 0x060078FD RID: 30973
		IEnumerable<CalculatedItem> GetColumnCalculatedItems(int level);
	}
}
