using System;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000DC6 RID: 3526
	public class PivotGridColumnHeaderItemDecorator : PivotGridItemDecorator
	{
		// Token: 0x0600837C RID: 33660 RVA: 0x001DF7CD File Offset: 0x001DD9CD
		public PivotGridColumnHeaderItemDecorator(PivotGridItem item) : base(item)
		{
		}

		// Token: 0x0600837D RID: 33661 RVA: 0x001DF7D8 File Offset: 0x001DD9D8
		public override void DecorateItem(RadPivotGrid owner)
		{
			this.SetItemStyle(owner);
			TableCellCollection cells = (base.Item as PivotGridColumnHeaderItem).Cells;
			foreach (object obj in cells)
			{
				TableCell tableCell = (TableCell)obj;
				PivotGridColumnHeaderCell pivotGridColumnHeaderCell = tableCell as PivotGridColumnHeaderCell;
				if (pivotGridColumnHeaderCell != null)
				{
					if (pivotGridColumnHeaderCell.IsGrandTotalCell)
					{
						pivotGridColumnHeaderCell.MergeStyle(owner.ColumnGrandTotalCellStyle);
					}
					if (pivotGridColumnHeaderCell.IsTotalCell)
					{
						pivotGridColumnHeaderCell.MergeStyle(owner.ColumnTotalCellStyle);
					}
					if (!pivotGridColumnHeaderCell.IsGrandTotalCell && pivotGridColumnHeaderCell.Field != null)
					{
						pivotGridColumnHeaderCell.MergeStyle(pivotGridColumnHeaderCell.Field.CellStyle);
					}
					pivotGridColumnHeaderCell.MergeStyle(owner.ColumnHeaderCellStyle);
					if (pivotGridColumnHeaderCell.IsTotalCell)
					{
						pivotGridColumnHeaderCell.CssClass = owner.FormatCssClass("rpgColumnHeaderTotal", pivotGridColumnHeaderCell.CssClass);
					}
					else if (pivotGridColumnHeaderCell.IsGrandTotalCell)
					{
						pivotGridColumnHeaderCell.CssClass = owner.FormatCssClass("rpgColumnHeaderGrandTotal", pivotGridColumnHeaderCell.CssClass);
					}
					else
					{
						pivotGridColumnHeaderCell.CssClass = owner.FormatCssClass("rpgColumnHeader", pivotGridColumnHeaderCell.CssClass);
					}
				}
				if (!pivotGridColumnHeaderCell.IsGrandTotalCell && pivotGridColumnHeaderCell.Field != null && !string.IsNullOrEmpty(pivotGridColumnHeaderCell.Field.CellStyle.CssClass))
				{
					pivotGridColumnHeaderCell.CssClass = owner.FormatCssClass(pivotGridColumnHeaderCell.Field.CellStyle.CssClass, pivotGridColumnHeaderCell.CssClass);
				}
			}
		}
	}
}
