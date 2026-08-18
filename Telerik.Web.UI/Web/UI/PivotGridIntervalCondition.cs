using System;
using Telerik.Web.UI.PivotGrid.Core.Filtering;

namespace Telerik.Web.UI
{
	// Token: 0x02000DBC RID: 3516
	[Serializable]
	public class PivotGridIntervalCondition : IFilterCondition, IPivotIntervalCondition
	{
		// Token: 0x0600834D RID: 33613 RVA: 0x001DF220 File Offset: 0x001DD420
		public Condition GetDataEngineFilterCondition()
		{
			return new IntervalCondition
			{
				To = this.To,
				From = this.From,
				Condition = this.Condition,
				IgnoreCase = this.IgnoreCase
			};
		}

		// Token: 0x1700297D RID: 10621
		// (get) Token: 0x0600834E RID: 33614 RVA: 0x001DF264 File Offset: 0x001DD464
		// (set) Token: 0x0600834F RID: 33615 RVA: 0x001DF26C File Offset: 0x001DD46C
		public object From { get; set; }

		// Token: 0x1700297E RID: 10622
		// (get) Token: 0x06008350 RID: 33616 RVA: 0x001DF275 File Offset: 0x001DD475
		// (set) Token: 0x06008351 RID: 33617 RVA: 0x001DF27D File Offset: 0x001DD47D
		public object To { get; set; }

		// Token: 0x1700297F RID: 10623
		// (get) Token: 0x06008352 RID: 33618 RVA: 0x001DF286 File Offset: 0x001DD486
		// (set) Token: 0x06008353 RID: 33619 RVA: 0x001DF28E File Offset: 0x001DD48E
		public IntervalComparison Condition { get; set; }

		// Token: 0x17002980 RID: 10624
		// (get) Token: 0x06008354 RID: 33620 RVA: 0x001DF297 File Offset: 0x001DD497
		// (set) Token: 0x06008355 RID: 33621 RVA: 0x001DF29F File Offset: 0x001DD49F
		public bool IgnoreCase { get; set; }
	}
}
