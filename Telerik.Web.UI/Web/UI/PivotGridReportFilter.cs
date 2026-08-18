using System;
using Telerik.Web.UI.PivotGrid.Core.Filtering;

namespace Telerik.Web.UI
{
	// Token: 0x02000766 RID: 1894
	[Serializable]
	public class PivotGridReportFilter : PivotGridFilter, IPivotConditionFilter, IPivotFilter
	{
		// Token: 0x060042CF RID: 17103 RVA: 0x000D098C File Offset: 0x000CEB8C
		public PivotGridReportFilter()
		{
		}

		// Token: 0x060042D0 RID: 17104 RVA: 0x000D0994 File Offset: 0x000CEB94
		public PivotGridReportFilter(IFilterCondition cond)
		{
			this.Condition = cond;
		}

		// Token: 0x170015C8 RID: 5576
		// (get) Token: 0x060042D1 RID: 17105 RVA: 0x000D09A3 File Offset: 0x000CEBA3
		// (set) Token: 0x060042D2 RID: 17106 RVA: 0x000D09AB File Offset: 0x000CEBAB
		public IFilterCondition Condition { get; set; }

		// Token: 0x060042D3 RID: 17107 RVA: 0x000D09B4 File Offset: 0x000CEBB4
		public override GroupFilter GetDataEngineFilter()
		{
			return null;
		}
	}
}
