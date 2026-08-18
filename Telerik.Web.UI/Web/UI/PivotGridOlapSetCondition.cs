using System;
using Telerik.Web.UI.PivotGrid.Core;
using Telerik.Web.UI.PivotGrid.Core.Filtering;
using Telerik.Web.UI.PivotGrid.Core.Olap;

namespace Telerik.Web.UI
{
	// Token: 0x02000760 RID: 1888
	[Serializable]
	public class PivotGridOlapSetCondition : IFilterCondition, IPivotSetCondition
	{
		// Token: 0x170015B6 RID: 5558
		// (get) Token: 0x06004294 RID: 17044 RVA: 0x000D060A File Offset: 0x000CE80A
		// (set) Token: 0x06004295 RID: 17045 RVA: 0x000D0612 File Offset: 0x000CE812
		public SetComparison Comparison { get; set; }

		// Token: 0x170015B7 RID: 5559
		// (get) Token: 0x06004296 RID: 17046 RVA: 0x000D061B File Offset: 0x000CE81B
		public SetConditionHashCollection Items
		{
			get
			{
				if (this.items == null)
				{
					this.items = new SetConditionHashCollection();
				}
				return this.items;
			}
		}

		// Token: 0x06004297 RID: 17047 RVA: 0x000D0638 File Offset: 0x000CE838
		public Condition GetDataEngineFilterCondition()
		{
			OlapSetCondition olapSetCondition = new OlapSetCondition
			{
				Comparison = this.Comparison
			};
			foreach (object value in this.Items)
			{
				olapSetCondition.Items.Add(value);
			}
			return olapSetCondition;
		}

		// Token: 0x040011B4 RID: 4532
		private SetConditionHashCollection items;
	}
}
