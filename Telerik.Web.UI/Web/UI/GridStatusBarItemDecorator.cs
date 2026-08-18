using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001124 RID: 4388
	internal class GridStatusBarItemDecorator : GridItemDecorator
	{
		// Token: 0x0600B351 RID: 45905 RVA: 0x002713F8 File Offset: 0x0026F5F8
		public GridStatusBarItemDecorator(GridItem gridItem) : base(gridItem)
		{
		}

		// Token: 0x0600B352 RID: 45906 RVA: 0x00271401 File Offset: 0x0026F601
		public override void DecorateItem(GridTableView owner, GridColumn[] columnArray)
		{
			this.SetItemStyle(owner);
			if (base.Item != null && !base.Item.Display)
			{
				base.Item.Style["display"] = "none";
			}
		}

		// Token: 0x0600B353 RID: 45907 RVA: 0x00271439 File Offset: 0x0026F639
		public override void SetItemStyle(GridTableView owner)
		{
			base.Item.MergeStyle(base.Item.OwnerTableView.RenderPagerStyle);
		}

		// Token: 0x0600B354 RID: 45908 RVA: 0x00271456 File Offset: 0x0026F656
		public override void SetItemVisibility(GridTableView owner, GridColumn[] columnArray)
		{
		}
	}
}
