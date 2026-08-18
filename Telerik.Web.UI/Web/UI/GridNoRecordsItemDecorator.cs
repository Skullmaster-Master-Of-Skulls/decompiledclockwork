using System;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001125 RID: 4389
	internal class GridNoRecordsItemDecorator : GridItemDecorator
	{
		// Token: 0x0600B355 RID: 45909 RVA: 0x00271458 File Offset: 0x0026F658
		public GridNoRecordsItemDecorator(GridItem gridItem) : base(gridItem)
		{
		}

		// Token: 0x0600B356 RID: 45910 RVA: 0x00271464 File Offset: 0x0026F664
		public override void SetItemVisibility(GridTableView owner, GridColumn[] columnArray)
		{
			int num = 0;
			foreach (object obj in base.Item.Cells)
			{
				TableCell tableCell = (TableCell)obj;
				GridColumn gridColumn = null;
				if (num >= 0 && columnArray.Length > num)
				{
					gridColumn = columnArray[num];
				}
				if (!(gridColumn is GridGroupSplitterColumn) && !(gridColumn is GridExpandColumn) && !(gridColumn is GridRowIndicatorColumn))
				{
					break;
				}
				if (gridColumn != null && !gridColumn.Visible)
				{
					tableCell.Visible = false;
				}
				num++;
			}
		}

		// Token: 0x0600B357 RID: 45911 RVA: 0x00271500 File Offset: 0x0026F700
		public override void DecorateItem(GridTableView owner, GridColumn[] columnArray)
		{
			base.Item.CssClass = "rgNoRecords";
			int num = 0;
			foreach (object obj in base.Item.Cells)
			{
				TableCell tableCell = (TableCell)obj;
				GridColumn gridColumn = null;
				if (num >= 0 && columnArray.Length > num)
				{
					gridColumn = columnArray[num];
				}
				if (!(gridColumn is GridGroupSplitterColumn) && !(gridColumn is GridExpandColumn) && !(gridColumn is GridRowIndicatorColumn))
				{
					break;
				}
				if (gridColumn != null && !gridColumn.Visible)
				{
					tableCell.Visible = false;
				}
				else
				{
					if (gridColumn != null)
					{
						this.PrepareCellInColumn(owner, gridColumn, tableCell);
						gridColumn.PrepareCell(tableCell, base.Item);
					}
					this.PrepareCell(owner, tableCell);
				}
				if (gridColumn != null && !gridColumn.Display)
				{
					tableCell.Style["display"] = "none";
				}
				num++;
			}
			if (base.Item != null && !base.Item.Display)
			{
				base.Item.Style["display"] = "none";
			}
		}
	}
}
