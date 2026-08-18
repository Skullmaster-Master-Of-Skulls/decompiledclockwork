using System;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000DC3 RID: 3523
	public class PivotGridItemDecorator
	{
		// Token: 0x17002989 RID: 10633
		// (get) Token: 0x06008374 RID: 33652 RVA: 0x001DF498 File Offset: 0x001DD698
		// (set) Token: 0x06008375 RID: 33653 RVA: 0x001DF4A0 File Offset: 0x001DD6A0
		public PivotGridItem Item { get; set; }

		// Token: 0x06008376 RID: 33654 RVA: 0x001DF4A9 File Offset: 0x001DD6A9
		public PivotGridItemDecorator(PivotGridItem gridItem)
		{
			this.Item = gridItem;
		}

		// Token: 0x06008377 RID: 33655 RVA: 0x001DF4B8 File Offset: 0x001DD6B8
		public virtual void DecorateItem(RadPivotGrid owner)
		{
			this.SetItemStyle(owner);
			TableCellCollection cells = this.Item.Cells;
			foreach (object obj in cells)
			{
				TableCell tableCell = (TableCell)obj;
				PivotGridDataCell pivotGridDataCell = tableCell as PivotGridDataCell;
				if (pivotGridDataCell != null)
				{
					string cssClass = pivotGridDataCell.CssClass;
					pivotGridDataCell.CssClass = "";
					pivotGridDataCell.MergeStyle(owner.DataCellStyle);
					if (pivotGridDataCell.Field != null)
					{
						pivotGridDataCell.MergeStyle(pivotGridDataCell.Field.CellStyle);
					}
					if (pivotGridDataCell.CellType == PivotGridDataCellType.RowAndColumnGrandTotal)
					{
						pivotGridDataCell.CssClass = string.Format("{0} {1}", "rpgRowGrandTotalDataCell", "rpgColumnGrandTotalDataCell");
					}
					else if (pivotGridDataCell.CellType == PivotGridDataCellType.RowAndColumnTotal)
					{
						pivotGridDataCell.CssClass = string.Format("{0} {1}", "rpgRowTotalDataCell", "rpgColumnTotalDataCell");
					}
					else if (pivotGridDataCell.CellType == PivotGridDataCellType.RowGrandTotalDataCell || pivotGridDataCell.CellType == PivotGridDataCellType.RowGrandTotalColumnTotal)
					{
						pivotGridDataCell.CssClass = "rpgRowGrandTotalDataCell";
					}
					else if (pivotGridDataCell.CellType == PivotGridDataCellType.ColumnGrandTotalDataCell || pivotGridDataCell.CellType == PivotGridDataCellType.ColumnGrandTotalRowTotal)
					{
						pivotGridDataCell.CssClass = "rpgColumnGrandTotalDataCell";
					}
					else if (pivotGridDataCell.CellType == PivotGridDataCellType.RowTotalDataCell)
					{
						pivotGridDataCell.CssClass = "rpgRowTotalDataCell";
					}
					else if (pivotGridDataCell.CellType == PivotGridDataCellType.ColumnTotalDataCell)
					{
						pivotGridDataCell.CssClass = "rpgColumnTotalDataCell";
					}
					pivotGridDataCell.CssClass = owner.FormatCssClass("rpgDataCell", pivotGridDataCell.CssClass);
					if (pivotGridDataCell.Field != null && !string.IsNullOrEmpty(pivotGridDataCell.Field.CellStyle.CssClass))
					{
						pivotGridDataCell.CssClass = owner.FormatCssClass(pivotGridDataCell.Field.CellStyle.CssClass, pivotGridDataCell.CssClass);
					}
					if (!string.IsNullOrEmpty(cssClass))
					{
						PivotGridDataCell pivotGridDataCell2 = pivotGridDataCell;
						pivotGridDataCell2.CssClass = pivotGridDataCell2.CssClass + " " + cssClass;
					}
				}
			}
		}

		// Token: 0x06008378 RID: 33656 RVA: 0x001DF6A4 File Offset: 0x001DD8A4
		protected virtual void SetItemStyle(RadPivotGrid owner)
		{
		}
	}
}
