using System;
using Telerik.Web.UI.PivotGrid.Core.Filtering;

namespace Telerik.Web.UI
{
	// Token: 0x02000DC0 RID: 3520
	[Serializable]
	public class PivotGridValueGroupFilter : PivotGridSingleGroupFilter, IPivotValueGroupFilter, IPivotConditionFilter, IPivotFilter
	{
		// Token: 0x06008367 RID: 33639 RVA: 0x001DF40F File Offset: 0x001DD60F
		public PivotGridValueGroupFilter()
		{
		}

		// Token: 0x06008368 RID: 33640 RVA: 0x001DF417 File Offset: 0x001DD617
		public PivotGridValueGroupFilter(IFilterCondition cond)
		{
			base.Condition = cond;
		}

		// Token: 0x17002986 RID: 10630
		// (get) Token: 0x06008369 RID: 33641 RVA: 0x001DF426 File Offset: 0x001DD626
		// (set) Token: 0x0600836A RID: 33642 RVA: 0x001DF42E File Offset: 0x001DD62E
		public int AggregateIndex { get; set; }

		// Token: 0x0600836B RID: 33643 RVA: 0x001DF438 File Offset: 0x001DD638
		public override GroupFilter GetDataEngineFilter()
		{
			return new ValueGroupFilter
			{
				Condition = (base.Condition.GetDataEngineFilterCondition() as LocalCondition),
				AggregateIndex = this.AggregateIndex
			};
		}
	}
}
