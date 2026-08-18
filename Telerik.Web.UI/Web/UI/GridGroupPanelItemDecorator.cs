using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200111E RID: 4382
	internal class GridGroupPanelItemDecorator : GridItemDecorator
	{
		// Token: 0x0600B33C RID: 45884 RVA: 0x00270721 File Offset: 0x0026E921
		public GridGroupPanelItemDecorator(GridItem item) : base(item)
		{
		}

		// Token: 0x0600B33D RID: 45885 RVA: 0x0027072A File Offset: 0x0026E92A
		public override void SetItemVisibility(GridTableView owner, GridColumn[] columnArray)
		{
		}

		// Token: 0x0600B33E RID: 45886 RVA: 0x0027072C File Offset: 0x0026E92C
		public override void DecorateItem(GridTableView owner, GridColumn[] columnArray)
		{
			if (base.Item != null && !base.Item.Display)
			{
				base.Item.Style["display"] = "none";
			}
			if (base.Item.OwnerTableView.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile)
			{
				base.Item.CssClass = "rgGroupPanelRow";
				if (base.Item.Cells.Count > 0)
				{
					base.Item.Cells[0].CssClass = "rgGroupPanelCell";
				}
			}
		}
	}
}
