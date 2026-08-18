using System;
using Telerik.Web.UI.PivotGrid.Core;
using Telerik.Web.UI.PivotGrid.Core.Filtering;

namespace Telerik.Web.UI
{
	// Token: 0x02000DBE RID: 3518
	[Serializable]
	public class PivotGridSetCondition : IFilterCondition, IPivotSetCondition
	{
		// Token: 0x0600835A RID: 33626 RVA: 0x001DF2F4 File Offset: 0x001DD4F4
		public Condition GetDataEngineFilterCondition()
		{
			SetCondition setCondition = new SetCondition
			{
				Comparison = this.Comparison
			};
			foreach (object value in this.Items)
			{
				setCondition.Items.Add(value);
			}
			return setCondition;
		}

		// Token: 0x17002981 RID: 10625
		// (get) Token: 0x0600835B RID: 33627 RVA: 0x001DF368 File Offset: 0x001DD568
		// (set) Token: 0x0600835C RID: 33628 RVA: 0x001DF370 File Offset: 0x001DD570
		public SetComparison Comparison { get; set; }

		// Token: 0x17002982 RID: 10626
		// (get) Token: 0x0600835D RID: 33629 RVA: 0x001DF379 File Offset: 0x001DD579
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

		// Token: 0x04002466 RID: 9318
		private SetConditionHashCollection items;
	}
}
