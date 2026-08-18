using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000389 RID: 905
	public class GridGroupPanelItem : GridItem
	{
		// Token: 0x06001F3E RID: 7998 RVA: 0x00062A6D File Offset: 0x00060C6D
		public GridGroupPanelItem(GridTableView ownerTableView, int itemIndex, int dataSetIndex) : base(ownerTableView, itemIndex, dataSetIndex, GridItemType.GroupPanelItem)
		{
		}

		// Token: 0x06001F3F RID: 7999 RVA: 0x00062A7C File Offset: 0x00060C7C
		public override void SetupItem(bool dataBind, object dataItem, GridColumn[] columns, ControlCollection rows)
		{
			rows.Add(this);
			TableCell tableCell = new TableCell();
			this.Cells.Add(tableCell);
			if (base.OwnerTableView.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile)
			{
				base.OwnerTableView.OwnerGrid.CreateMobileGroupPanel(tableCell);
			}
			else
			{
				base.OwnerTableView.OwnerGrid.CreateGroupPanel(tableCell);
			}
			GridItemEventArgs e = new GridItemEventArgs(this, new GridItemCreated());
			base.OwnerTableView.OwnerGrid.CallOnItemCreated(e);
			if (dataBind)
			{
				this.DataBind();
				e = new GridItemEventArgs(this, new GridItemDataBound());
				base.OwnerTableView.OwnerGrid.CallOnItemDataBound(e);
			}
		}

		// Token: 0x06001F40 RID: 8000 RVA: 0x00062B1E File Offset: 0x00060D1E
		public override void PrepareItemStyle()
		{
			this.Cells[0].ColumnSpan = base.CalcColSpan(base.OwnerTableView.RenderColumns, 0, -1);
			base.PrepareItemStyle();
		}

		// Token: 0x06001F41 RID: 8001 RVA: 0x00062B4A File Offset: 0x00060D4A
		public override void PrepareItemVisibility()
		{
		}

		// Token: 0x06001F42 RID: 8002 RVA: 0x00062B4C File Offset: 0x00060D4C
		internal override void SetItemDecorator(GridItemDecorator newDecorator)
		{
			base.SetItemDecorator(new GridGroupPanelItemDecorator(this));
		}
	}
}
