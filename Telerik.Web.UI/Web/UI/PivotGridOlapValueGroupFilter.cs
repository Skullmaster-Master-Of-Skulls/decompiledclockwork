using System;
using Telerik.Web.UI.PivotGrid.Core.Filtering;
using Telerik.Web.UI.PivotGrid.Core.Olap;

namespace Telerik.Web.UI
{
	// Token: 0x02000764 RID: 1892
	[Serializable]
	public class PivotGridOlapValueGroupFilter : PivotGridSingleGroupFilter, IPivotValueGroupFilter, IPivotConditionFilter, IPivotFilter
	{
		// Token: 0x060042A9 RID: 17065 RVA: 0x000D071B File Offset: 0x000CE91B
		public PivotGridOlapValueGroupFilter()
		{
		}

		// Token: 0x060042AA RID: 17066 RVA: 0x000D0723 File Offset: 0x000CE923
		public PivotGridOlapValueGroupFilter(IFilterCondition cond)
		{
			base.Condition = cond;
		}

		// Token: 0x170015BF RID: 5567
		// (get) Token: 0x060042AB RID: 17067 RVA: 0x000D0732 File Offset: 0x000CE932
		// (set) Token: 0x060042AC RID: 17068 RVA: 0x000D073A File Offset: 0x000CE93A
		public int AggregateIndex { get; set; }

		// Token: 0x060042AD RID: 17069 RVA: 0x000D0744 File Offset: 0x000CE944
		public override GroupFilter GetDataEngineFilter()
		{
			return new OlapValueGroupFilter
			{
				Condition = (base.Condition.GetDataEngineFilterCondition() as OlapCondition),
				AggregateIndex = this.AggregateIndex
			};
		}
	}
}
