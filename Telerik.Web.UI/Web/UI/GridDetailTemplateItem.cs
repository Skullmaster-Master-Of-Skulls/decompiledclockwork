using System;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000B7E RID: 2942
	public class GridDetailTemplateItem : GridItem
	{
		// Token: 0x17002470 RID: 9328
		// (get) Token: 0x06006F18 RID: 28440 RVA: 0x0019BDBB File Offset: 0x00199FBB
		// (set) Token: 0x06006F19 RID: 28441 RVA: 0x0019BDC3 File Offset: 0x00199FC3
		internal TableCell DataCell
		{
			get
			{
				return this.dataCell;
			}
			set
			{
				this.dataCell = value;
			}
		}

		// Token: 0x06006F1A RID: 28442 RVA: 0x0019BDCC File Offset: 0x00199FCC
		public GridDetailTemplateItem(GridTableView ownerTableView) : base(ownerTableView, -1, -1, GridItemType.DetailTemplateItem)
		{
		}

		// Token: 0x06006F1B RID: 28443 RVA: 0x0019BDDC File Offset: 0x00199FDC
		public virtual void Initialize()
		{
			this.DataCell = new GridTableCell();
			GridColumn[] renderColumns = base.OwnerTableView.RenderColumns;
			int num = 0;
			foreach (GridColumn gridColumn in base.OwnerTableView.RenderColumns)
			{
				if (gridColumn.Visible && gridColumn.Display && !(gridColumn is GridExpandColumn) && !(gridColumn is GridGroupSplitterColumn) && !(gridColumn is GridRowIndicatorColumn))
				{
					break;
				}
				num++;
			}
			if (base.OwnerTableView.MaxColumnSpan == -1)
			{
				base.OwnerTableView.MaxColumnSpan = renderColumns.Length - num;
			}
			for (int j = 0; j < num; j++)
			{
				this.Cells.Add(new GridTableCell());
			}
			this.DataCell.ColumnSpan = base.OwnerTableView.MaxColumnSpan;
			base.OwnerTableView.DetailItemTemplate.InstantiateIn(this.DataCell);
			this.Cells.Add(this.DataCell);
		}

		// Token: 0x06006F1C RID: 28444 RVA: 0x0019BECD File Offset: 0x0019A0CD
		public override void PrepareItemVisibility()
		{
		}

		// Token: 0x04001DFC RID: 7676
		private TableCell dataCell;
	}
}
