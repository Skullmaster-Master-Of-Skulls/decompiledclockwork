using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200111D RID: 4381
	internal class GridCommandItemDecorator : GridItemDecorator
	{
		// Token: 0x0600B339 RID: 45881 RVA: 0x002706C9 File Offset: 0x0026E8C9
		public GridCommandItemDecorator(GridItem item) : base(item)
		{
		}

		// Token: 0x0600B33A RID: 45882 RVA: 0x002706D2 File Offset: 0x0026E8D2
		public override void SetItemVisibility(GridTableView owner, GridColumn[] columnArray)
		{
		}

		// Token: 0x0600B33B RID: 45883 RVA: 0x002706D4 File Offset: 0x0026E8D4
		public override void DecorateItem(GridTableView owner, GridColumn[] columnArray)
		{
			if (base.Item != null && !base.Item.Display)
			{
				base.Item.Style["display"] = "none";
			}
			base.Item.MergeStyle(owner.RenderCommandItemStyle);
		}
	}
}
