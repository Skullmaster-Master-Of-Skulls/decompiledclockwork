using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000672 RID: 1650
	public abstract class PivotGridHeaderCell : PivotGridCell
	{
		// Token: 0x06003C4E RID: 15438 RVA: 0x000C3AE4 File Offset: 0x000C1CE4
		public PivotGridHeaderCell(RadPivotGrid ownerPivotGrid) : base(ownerPivotGrid)
		{
		}

		// Token: 0x170013E0 RID: 5088
		// (get) Token: 0x06003C4F RID: 15439 RVA: 0x000C3AED File Offset: 0x000C1CED
		// (set) Token: 0x06003C50 RID: 15440 RVA: 0x000C3AF5 File Offset: 0x000C1CF5
		public bool IsTotalCell { get; set; }

		// Token: 0x170013E1 RID: 5089
		// (get) Token: 0x06003C51 RID: 15441 RVA: 0x000C3AFE File Offset: 0x000C1CFE
		// (set) Token: 0x06003C52 RID: 15442 RVA: 0x000C3B06 File Offset: 0x000C1D06
		public bool IsGrandTotalCell { get; set; }

		// Token: 0x170013E2 RID: 5090
		// (get) Token: 0x06003C53 RID: 15443 RVA: 0x000C3B0F File Offset: 0x000C1D0F
		// (set) Token: 0x06003C54 RID: 15444 RVA: 0x000C3B17 File Offset: 0x000C1D17
		public int GroupLevel { get; set; }

		// Token: 0x170013E3 RID: 5091
		// (get) Token: 0x06003C55 RID: 15445 RVA: 0x000C3B20 File Offset: 0x000C1D20
		// (set) Token: 0x06003C56 RID: 15446 RVA: 0x000C3B28 File Offset: 0x000C1D28
		public int Slot { get; set; }

		// Token: 0x170013E4 RID: 5092
		// (get) Token: 0x06003C57 RID: 15447 RVA: 0x000C3B31 File Offset: 0x000C1D31
		// (set) Token: 0x06003C58 RID: 15448 RVA: 0x000C3B39 File Offset: 0x000C1D39
		public object[] ParentIndexes { get; set; }

		// Token: 0x170013E5 RID: 5093
		// (get) Token: 0x06003C59 RID: 15449 RVA: 0x000C3B44 File Offset: 0x000C1D44
		public override bool CanExpand
		{
			get
			{
				bool result = false;
				if (base.HasChildren)
				{
					result = true;
				}
				return result;
			}
		}

		// Token: 0x170013E6 RID: 5094
		// (get) Token: 0x06003C5A RID: 15450 RVA: 0x000C3B5E File Offset: 0x000C1D5E
		// (set) Token: 0x06003C5B RID: 15451 RVA: 0x000C3B66 File Offset: 0x000C1D66
		public bool Expanded
		{
			get
			{
				return this.IsExpanded;
			}
			set
			{
				this.SetExpandedState(value);
			}
		}

		// Token: 0x170013E7 RID: 5095
		// (get) Token: 0x06003C5C RID: 15452
		protected abstract bool IsExpanded { get; }

		// Token: 0x06003C5D RID: 15453
		protected abstract void SetExpandedState(bool shouldExpand);

		// Token: 0x06003C5E RID: 15454
		public abstract string GetToolTipString();
	}
}
