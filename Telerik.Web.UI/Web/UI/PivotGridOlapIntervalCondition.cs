using System;
using Telerik.Web.UI.PivotGrid.Core.Filtering;
using Telerik.Web.UI.PivotGrid.Core.Olap;

namespace Telerik.Web.UI
{
	// Token: 0x02000758 RID: 1880
	[Serializable]
	public class PivotGridOlapIntervalCondition : IFilterCondition, IPivotIntervalCondition
	{
		// Token: 0x06004278 RID: 17016 RVA: 0x000D0510 File Offset: 0x000CE710
		public Condition GetDataEngineFilterCondition()
		{
			return new OlapIntervalCondition
			{
				To = this.To,
				From = this.From,
				Condition = this.Condition
			};
		}

		// Token: 0x170015AC RID: 5548
		// (get) Token: 0x06004279 RID: 17017 RVA: 0x000D0548 File Offset: 0x000CE748
		// (set) Token: 0x0600427A RID: 17018 RVA: 0x000D0550 File Offset: 0x000CE750
		public object From { get; set; }

		// Token: 0x170015AD RID: 5549
		// (get) Token: 0x0600427B RID: 17019 RVA: 0x000D0559 File Offset: 0x000CE759
		// (set) Token: 0x0600427C RID: 17020 RVA: 0x000D0561 File Offset: 0x000CE761
		public object To { get; set; }

		// Token: 0x170015AE RID: 5550
		// (get) Token: 0x0600427D RID: 17021 RVA: 0x000D056A File Offset: 0x000CE76A
		// (set) Token: 0x0600427E RID: 17022 RVA: 0x000D0572 File Offset: 0x000CE772
		public IntervalComparison Condition { get; set; }

		// Token: 0x170015AF RID: 5551
		// (get) Token: 0x0600427F RID: 17023 RVA: 0x000D057B File Offset: 0x000CE77B
		// (set) Token: 0x06004280 RID: 17024 RVA: 0x000D0583 File Offset: 0x000CE783
		public bool IgnoreCase { get; set; }
	}
}
