using System;
using System.Collections;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x0200111B RID: 4379
	internal class GridHeaderItemDecorator : GridItemDecorator
	{
		// Token: 0x0600B32D RID: 45869 RVA: 0x00270326 File Offset: 0x0026E526
		public GridHeaderItemDecorator(GridItem item) : base(item)
		{
		}

		// Token: 0x0600B32E RID: 45870 RVA: 0x00270330 File Offset: 0x0026E530
		public override void SetItemVisibility(GridTableView owner, GridColumn[] columnArray)
		{
			if (owner.HasMultiHeaders)
			{
				GridHeaderItem gridHeaderItem = (GridHeaderItem)base.Item;
				ArrayList multiHeaderCells = gridHeaderItem.MultiHeaderCells;
				for (int i = 0; i < multiHeaderCells.Count; i++)
				{
					TableCell tableCell = base.Item.Cells[i];
					object obj = multiHeaderCells[i];
					GridColumnGroup gridColumnGroup = obj as GridColumnGroup;
					if (gridColumnGroup != null)
					{
						tableCell.Visible = gridColumnGroup.Visible;
						if (!gridColumnGroup.Display)
						{
							tableCell.Style["display"] = "none";
						}
					}
					else
					{
						GridColumn gridColumn = obj as GridColumn;
						tableCell.Visible = gridColumn.Visible;
					}
				}
				return;
			}
			base.SetItemVisibility(owner, columnArray);
		}

		// Token: 0x0600B32F RID: 45871 RVA: 0x002703E0 File Offset: 0x0026E5E0
		public override void DecorateItem(GridTableView owner, GridColumn[] columnArray)
		{
			if (!owner.ShowHeader)
			{
				base.Item.Style["display"] = "none";
				return;
			}
			if (owner.ShowHeader && !base.Item.Visible)
			{
				base.Item.Parent.Visible = true;
				base.Item.Visible = true;
			}
			if (owner.HasMultiHeaders)
			{
				base.Item.MergeStyle(owner.RenderMultiHeaderItemStyle);
				GridHeaderItem gridHeaderItem = (GridHeaderItem)base.Item;
				ArrayList multiHeaderCells = gridHeaderItem.MultiHeaderCells;
				for (int i = 0; i < multiHeaderCells.Count; i++)
				{
					TableCell tableCell = base.Item.Cells[i];
					object obj = multiHeaderCells[i];
					GridColumnGroup gridColumnGroup = obj as GridColumnGroup;
					if (gridColumnGroup != null)
					{
						tableCell.Visible = gridColumnGroup.Visible;
						if (!gridColumnGroup.Display)
						{
							tableCell.Style["display"] = "none";
						}
						this.PrepareCell(owner, tableCell);
						this.PrepareMultiCell(gridColumnGroup, tableCell);
					}
					else
					{
						GridColumn gridColumn = obj as GridColumn;
						tableCell.Visible = gridColumn.Visible;
						if (gridColumn != null)
						{
							this.PrepareCellInColumn(owner, gridColumn, tableCell);
							gridColumn.PrepareCell(tableCell, base.Item);
						}
						this.PrepareCell(owner, tableCell);
						if (gridColumn != null && !gridColumn.Display)
						{
							tableCell.Style["display"] = "none";
						}
					}
				}
				return;
			}
			base.DecorateItem(owner, columnArray);
		}

		// Token: 0x0600B330 RID: 45872 RVA: 0x00270553 File Offset: 0x0026E753
		public override void SetItemStyle(GridTableView owner)
		{
		}

		// Token: 0x0600B331 RID: 45873 RVA: 0x00270555 File Offset: 0x0026E755
		public override void PrepareCellInColumn(GridTableView owner, GridColumn column, TableCell cell)
		{
			cell.MergeStyle(column.HeaderStyleInternal);
			cell.Height = Unit.Empty;
		}

		// Token: 0x0600B332 RID: 45874 RVA: 0x00270570 File Offset: 0x0026E770
		private void PrepareMultiCell(GridColumnGroup columnGroup, TableCell cell)
		{
			cell.MergeStyle(columnGroup.HeaderStyle);
			string cssClass = columnGroup.HeaderStyle.CssClass;
			if (!string.IsNullOrEmpty(cssClass) && !cell.CssClass.Contains(cssClass))
			{
				cell.CssClass = cell.CssClass + " " + cssClass;
			}
			cell.Height = Unit.Empty;
		}

		// Token: 0x0600B333 RID: 45875 RVA: 0x002705D0 File Offset: 0x0026E7D0
		public override void PrepareCell(GridTableView owner, TableCell cell)
		{
			cell.MergeStyle(owner.RenderHeaderStyle);
			cell.Width = Unit.Empty;
			if (!cell.CssClass.Contains("rgHeader"))
			{
				cell.CssClass += " rgHeader";
			}
		}
	}
}
