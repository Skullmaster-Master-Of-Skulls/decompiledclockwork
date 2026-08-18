using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000D0F RID: 3343
	public interface IPivotResults : IAggregateResultProvider
	{
		// Token: 0x170027BA RID: 10170
		// (get) Token: 0x06007C96 RID: 31894
		IReadOnlyList<GroupDescription> RowGroupDescriptions { get; }

		// Token: 0x170027BB RID: 10171
		// (get) Token: 0x06007C97 RID: 31895
		IReadOnlyList<GroupDescription> ColumnGroupDescriptions { get; }

		// Token: 0x170027BC RID: 10172
		// (get) Token: 0x06007C98 RID: 31896
		IReadOnlyList<IAggregateDescription> AggregateDescriptions { get; }

		// Token: 0x170027BD RID: 10173
		// (get) Token: 0x06007C99 RID: 31897
		IReadOnlyList<FilterDescription> FilterDescriptions { get; }

		// Token: 0x06007C9A RID: 31898
		AggregateValue GetAggregateResult(int aggregateIndex, IGroup row, IGroup column);

		// Token: 0x06007C9B RID: 31899
		IEnumerable<object> GetUniqueKeys(PivotAxis axis, int index);

		// Token: 0x06007C9C RID: 31900
		IEnumerable<object> GetUniqueFilterItems(int filterIndex);
	}
}
