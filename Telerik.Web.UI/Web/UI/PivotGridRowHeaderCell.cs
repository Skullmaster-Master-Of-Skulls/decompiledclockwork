using System;
using System.Text;

namespace Telerik.Web.UI
{
	// Token: 0x02000C2A RID: 3114
	public class PivotGridRowHeaderCell : PivotGridHeaderCell
	{
		// Token: 0x06007640 RID: 30272 RVA: 0x001B7430 File Offset: 0x001B5630
		public PivotGridRowHeaderCell(RadPivotGrid ownerPivotGrid) : base(ownerPivotGrid)
		{
		}

		// Token: 0x17002674 RID: 9844
		// (get) Token: 0x06007641 RID: 30273 RVA: 0x001B743C File Offset: 0x001B563C
		protected override bool IsExpanded
		{
			get
			{
				return this.CanExpand && !base.OwnerPivotGrid.CollapsedRowIndexes.Contains(base.ParentIndexes) == base.OwnerPivotGrid.RowGroupsDefaultExpanded;
			}
		}

		// Token: 0x06007642 RID: 30274 RVA: 0x001B7480 File Offset: 0x001B5680
		protected override void SetExpandedState(bool shouldExpand)
		{
			if (!this.CanExpand)
			{
				return;
			}
			if (!shouldExpand)
			{
				if (!this.IsExpanded)
				{
					base.OwnerPivotGrid.CollapsedRowIndexes.Add(base.ParentIndexes);
					base.OwnerPivotGrid.SetRequiresDataBindingIfInitialized();
					return;
				}
			}
			else if (this.IsExpanded)
			{
				base.OwnerPivotGrid.CollapsedRowIndexes.Remove(base.ParentIndexes);
				base.OwnerPivotGrid.SetRequiresDataBindingIfInitialized();
			}
		}

		// Token: 0x06007643 RID: 30275 RVA: 0x001B74F0 File Offset: 0x001B56F0
		public override string GetToolTipString()
		{
			if (base.ParentIndexes == null || base.ParentIndexes.Length == 0 || base.Field == null)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("{0} ({1});", base.ParentIndexes[base.ParentIndexes.Length - 1], base.Field.DataField);
			stringBuilder.Append(base.OwnerPivotGrid.Localization.ToolTipsRowText);
			for (int i = 0; i < base.ParentIndexes.Length; i++)
			{
				object obj = base.ParentIndexes[i];
				stringBuilder.Append(obj.ToString());
				if (i < base.ParentIndexes.Length - 1)
				{
					stringBuilder.Append(" - ");
				}
			}
			return stringBuilder.ToString();
		}
	}
}
