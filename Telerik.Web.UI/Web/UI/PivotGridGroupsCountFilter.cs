using System;
using Telerik.Web.UI.PivotGrid.Core.Filtering;

namespace Telerik.Web.UI
{
	// Token: 0x02000DB9 RID: 3513
	[Serializable]
	public class PivotGridGroupsCountFilter : PivotGridSortedGroupsFilter
	{
		// Token: 0x1700297A RID: 10618
		// (get) Token: 0x06008341 RID: 33601 RVA: 0x001DF124 File Offset: 0x001DD324
		// (set) Token: 0x06008342 RID: 33602 RVA: 0x001DF12C File Offset: 0x001DD32C
		public int Count { get; set; }

		// Token: 0x06008343 RID: 33603 RVA: 0x001DF138 File Offset: 0x001DD338
		public override GroupFilter GetDataEngineFilter()
		{
			return new GroupsCountFilter
			{
				Count = this.Count,
				Selection = base.Selection,
				AggregateIndex = base.AggregateIndex
			};
		}
	}
}
