using System;
using Telerik.Web.UI.PivotGrid.Core.Filtering;

namespace Telerik.Web.UI
{
	// Token: 0x02000DBA RID: 3514
	[Serializable]
	public class PivotGridGroupsPercentFilter : PivotGridSortedGroupsFilter
	{
		// Token: 0x1700297B RID: 10619
		// (get) Token: 0x06008345 RID: 33605 RVA: 0x001DF178 File Offset: 0x001DD378
		// (set) Token: 0x06008346 RID: 33606 RVA: 0x001DF180 File Offset: 0x001DD380
		public double Percent { get; set; }

		// Token: 0x06008347 RID: 33607 RVA: 0x001DF18C File Offset: 0x001DD38C
		public override GroupFilter GetDataEngineFilter()
		{
			return new GroupsPercentFilter
			{
				Percent = this.Percent,
				Selection = base.Selection,
				AggregateIndex = base.AggregateIndex
			};
		}
	}
}
