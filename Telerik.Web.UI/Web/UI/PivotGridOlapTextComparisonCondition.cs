using System;
using Telerik.Web.UI.PivotGrid.Core.Filtering;
using Telerik.Web.UI.PivotGrid.Core.Olap;

namespace Telerik.Web.UI
{
	// Token: 0x02000762 RID: 1890
	[Serializable]
	public class PivotGridOlapTextComparisonCondition : IFilterCondition, IPivotTextCondition
	{
		// Token: 0x0600429F RID: 17055 RVA: 0x000D06B4 File Offset: 0x000CE8B4
		public Condition GetDataEngineFilterCondition()
		{
			return new OlapTextCondition
			{
				Comparison = this.Comparison,
				Pattern = this.Pattern
			};
		}

		// Token: 0x170015BB RID: 5563
		// (get) Token: 0x060042A0 RID: 17056 RVA: 0x000D06E0 File Offset: 0x000CE8E0
		// (set) Token: 0x060042A1 RID: 17057 RVA: 0x000D06E8 File Offset: 0x000CE8E8
		public bool IgnoreCase { get; set; }

		// Token: 0x170015BC RID: 5564
		// (get) Token: 0x060042A2 RID: 17058 RVA: 0x000D06F1 File Offset: 0x000CE8F1
		// (set) Token: 0x060042A3 RID: 17059 RVA: 0x000D06F9 File Offset: 0x000CE8F9
		public string Pattern { get; set; }

		// Token: 0x170015BD RID: 5565
		// (get) Token: 0x060042A4 RID: 17060 RVA: 0x000D0702 File Offset: 0x000CE902
		// (set) Token: 0x060042A5 RID: 17061 RVA: 0x000D070A File Offset: 0x000CE90A
		public TextComparison Comparison { get; set; }
	}
}
