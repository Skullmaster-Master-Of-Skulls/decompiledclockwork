using System;
using Telerik.Web.UI.PivotGrid.Core.Filtering;

namespace Telerik.Web.UI
{
	// Token: 0x02000DBD RID: 3517
	[Serializable]
	public class PivotGridLabelGroupFilter : PivotGridSingleGroupFilter, IPivotLabelGroupFilter, IPivotConditionFilter, IPivotFilter
	{
		// Token: 0x06008357 RID: 33623 RVA: 0x001DF2B0 File Offset: 0x001DD4B0
		public PivotGridLabelGroupFilter()
		{
		}

		// Token: 0x06008358 RID: 33624 RVA: 0x001DF2B8 File Offset: 0x001DD4B8
		public PivotGridLabelGroupFilter(IFilterCondition cond)
		{
			base.Condition = cond;
		}

		// Token: 0x06008359 RID: 33625 RVA: 0x001DF2C8 File Offset: 0x001DD4C8
		public override GroupFilter GetDataEngineFilter()
		{
			return new LabelGroupFilter
			{
				Condition = (base.Condition.GetDataEngineFilterCondition() as LocalCondition)
			};
		}
	}
}
