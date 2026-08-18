using System;
using Telerik.Web.UI.PivotGrid.Core.Filtering;

namespace Telerik.Web.UI
{
	// Token: 0x02000DBB RID: 3515
	[Serializable]
	public class PivotGridGroupsSumFilter : PivotGridSortedGroupsFilter
	{
		// Token: 0x1700297C RID: 10620
		// (get) Token: 0x06008349 RID: 33609 RVA: 0x001DF1CC File Offset: 0x001DD3CC
		// (set) Token: 0x0600834A RID: 33610 RVA: 0x001DF1D4 File Offset: 0x001DD3D4
		public double Sum { get; set; }

		// Token: 0x0600834B RID: 33611 RVA: 0x001DF1E0 File Offset: 0x001DD3E0
		public override GroupFilter GetDataEngineFilter()
		{
			return new GroupsSumFilter
			{
				Sum = this.Sum,
				Selection = base.Selection,
				AggregateIndex = base.AggregateIndex
			};
		}
	}
}
