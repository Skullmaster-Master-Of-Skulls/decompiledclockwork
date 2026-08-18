using System;
using System.Web;

namespace Telerik.Web.UI
{
	// Token: 0x02001118 RID: 4376
	internal class GridAlternatingItemDecorator : GridItemDecorator
	{
		// Token: 0x0600B327 RID: 45863 RVA: 0x0027008D File Offset: 0x0026E28D
		public GridAlternatingItemDecorator(GridItem item) : base(item)
		{
		}

		// Token: 0x0600B328 RID: 45864 RVA: 0x00270098 File Offset: 0x0026E298
		public override void SetItemStyle(GridTableView owner)
		{
			base.Item.MergeStyle(owner.RenderAlternatingItemStyle);
			GridDataItem gridDataItem = base.Item as GridDataItem;
			if (gridDataItem != null)
			{
				gridDataItem.Attributes["id"] = HttpUtility.HtmlEncode(string.Format("{0}__{1}", gridDataItem.OwnerTableView.ClientID, gridDataItem.ItemIndexHierarchical));
				if (gridDataItem.OwnerTableView.DetailItemTemplate != null)
				{
					(gridDataItem.DetailTemplateItemDataCell.Parent as GridDetailTemplateItem).MergeStyle(owner.RenderAlternatingItemStyle);
				}
			}
		}
	}
}
