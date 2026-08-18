using System;
using Telerik.Web.UI.PivotGrid.Core.Filtering;
using Telerik.Web.UI.PivotGrid.Core.Olap;

namespace Telerik.Web.UI
{
	// Token: 0x0200075E RID: 1886
	[Serializable]
	public class PivotGridOlapLabelGroupFilter : PivotGridSingleGroupFilter, IPivotLabelGroupFilter, IPivotConditionFilter, IPivotFilter
	{
		// Token: 0x0600428E RID: 17038 RVA: 0x000D05C6 File Offset: 0x000CE7C6
		public PivotGridOlapLabelGroupFilter()
		{
		}

		// Token: 0x0600428F RID: 17039 RVA: 0x000D05CE File Offset: 0x000CE7CE
		public PivotGridOlapLabelGroupFilter(IFilterCondition cond)
		{
			base.Condition = cond;
		}

		// Token: 0x06004290 RID: 17040 RVA: 0x000D05E0 File Offset: 0x000CE7E0
		public override GroupFilter GetDataEngineFilter()
		{
			return new OlapLabelGroupFilter
			{
				Condition = (base.Condition.GetDataEngineFilterCondition() as OlapCondition)
			};
		}
	}
}
