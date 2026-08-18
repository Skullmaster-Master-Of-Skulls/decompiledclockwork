using System;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000DCC RID: 3532
	public class PivotGridRowHeaderItemDecorator : PivotGridItemDecorator
	{
		// Token: 0x06008387 RID: 33671 RVA: 0x001DFAA3 File Offset: 0x001DDCA3
		public PivotGridRowHeaderItemDecorator(PivotGridItem item) : base(item)
		{
		}

		// Token: 0x06008388 RID: 33672 RVA: 0x001DFAAC File Offset: 0x001DDCAC
		public override void DecorateItem(RadPivotGrid owner)
		{
			this.SetItemStyle(owner);
			TableCellCollection cells = (base.Item as PivotGridRowHeaderItem).Cells;
			foreach (object obj in cells)
			{
				TableCell tableCell = (TableCell)obj;
				PivotGridRowHeaderCell pivotGridRowHeaderCell = tableCell as PivotGridRowHeaderCell;
				if (pivotGridRowHeaderCell != null)
				{
					if (pivotGridRowHeaderCell.IsGrandTotalCell)
					{
						pivotGridRowHeaderCell.MergeStyle(owner.RowGrandTotalCellStyle);
					}
					if (pivotGridRowHeaderCell.IsTotalCell)
					{
						pivotGridRowHeaderCell.MergeStyle(owner.RowTotalCellStyle);
					}
					if (!pivotGridRowHeaderCell.IsGrandTotalCell && pivotGridRowHeaderCell.Field != null)
					{
						pivotGridRowHeaderCell.MergeStyle(pivotGridRowHeaderCell.Field.CellStyle);
					}
					pivotGridRowHeaderCell.MergeStyle(owner.RowHeaderCellStyle);
					pivotGridRowHeaderCell.Width = Unit.Empty;
					if (pivotGridRowHeaderCell.IsTotalCell)
					{
						pivotGridRowHeaderCell.CssClass = owner.FormatCssClass("rpgRowHeaderTotal", pivotGridRowHeaderCell.CssClass);
					}
					else if (pivotGridRowHeaderCell.IsGrandTotalCell)
					{
						pivotGridRowHeaderCell.CssClass = owner.FormatCssClass("rpgRowHeaderGrandTotal", pivotGridRowHeaderCell.CssClass);
					}
					else
					{
						pivotGridRowHeaderCell.CssClass = owner.FormatCssClass("rpgRowHeader", pivotGridRowHeaderCell.CssClass);
						if (pivotGridRowHeaderCell.ColumnSpan < 2)
						{
							pivotGridRowHeaderCell.CssClass = owner.FormatCssClass("rpgRowHeaderField", pivotGridRowHeaderCell.CssClass);
						}
					}
					if (pivotGridRowHeaderCell.Field != null && !pivotGridRowHeaderCell.IsGrandTotalCell && !string.IsNullOrEmpty(pivotGridRowHeaderCell.Field.CellStyle.CssClass))
					{
						pivotGridRowHeaderCell.CssClass = owner.FormatCssClass(pivotGridRowHeaderCell.Field.CellStyle.CssClass, pivotGridRowHeaderCell.CssClass);
					}
				}
				else if (owner.RowTableLayout == PivotGridLayout.Compact)
				{
					tableCell.CssClass = "rpgEC";
				}
			}
		}
	}
}
