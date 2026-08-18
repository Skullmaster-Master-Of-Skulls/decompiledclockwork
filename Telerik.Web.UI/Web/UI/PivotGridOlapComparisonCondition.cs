using System;
using Telerik.Web.UI.PivotGrid.Core;
using Telerik.Web.UI.PivotGrid.Core.Filtering;
using Telerik.Web.UI.PivotGrid.Core.Olap;

namespace Telerik.Web.UI
{
	// Token: 0x02000756 RID: 1878
	[Serializable]
	public class PivotGridOlapComparisonCondition : IFilterCondition, IPivotComparisonCondition
	{
		// Token: 0x06004268 RID: 17000 RVA: 0x000D049C File Offset: 0x000CE69C
		public Condition GetDataEngineFilterCondition()
		{
			return new OlapComparisonCondition
			{
				Condition = this.Condition,
				IgnoreCase = this.IgnoreCase,
				Than = this.Than
			};
		}

		// Token: 0x170015A5 RID: 5541
		// (get) Token: 0x06004269 RID: 17001 RVA: 0x000D04D4 File Offset: 0x000CE6D4
		// (set) Token: 0x0600426A RID: 17002 RVA: 0x000D04DC File Offset: 0x000CE6DC
		public object Than { get; set; }

		// Token: 0x170015A6 RID: 5542
		// (get) Token: 0x0600426B RID: 17003 RVA: 0x000D04E5 File Offset: 0x000CE6E5
		// (set) Token: 0x0600426C RID: 17004 RVA: 0x000D04ED File Offset: 0x000CE6ED
		public Comparison Condition { get; set; }

		// Token: 0x170015A7 RID: 5543
		// (get) Token: 0x0600426D RID: 17005 RVA: 0x000D04F6 File Offset: 0x000CE6F6
		// (set) Token: 0x0600426E RID: 17006 RVA: 0x000D04FE File Offset: 0x000CE6FE
		public bool IgnoreCase { get; set; }
	}
}
