using System;
using System.Web;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x0200111A RID: 4378
	internal class GridSelectedItemDecorator : GridItemDecorator
	{
		// Token: 0x0600B32B RID: 45867 RVA: 0x00270247 File Offset: 0x0026E447
		public GridSelectedItemDecorator(GridItem item) : base(item)
		{
		}

		// Token: 0x0600B32C RID: 45868 RVA: 0x00270250 File Offset: 0x0026E450
		public override void SetItemStyle(GridTableView owner)
		{
			TableItemStyle tableItemStyle = new TableItemStyle();
			if (base.Item.IsAlternatingItem())
			{
				tableItemStyle.CopyFrom(owner.RenderAlternatingItemStyle);
			}
			else
			{
				tableItemStyle.CopyFrom(owner.RenderItemStyle);
			}
			tableItemStyle.MergeWith(owner.SelectedItemStyle);
			if (!string.IsNullOrEmpty(owner.SelectedItemStyle.CssClass))
			{
				tableItemStyle.CssClass = HttpUtility.HtmlEncode(string.Format("{0} {1}", tableItemStyle.CssClass, owner.SelectedItemStyle.CssClass));
			}
			base.Item.MergeStyle(tableItemStyle);
			if (base.Item is GridDataItem)
			{
				base.Item.Attributes["id"] = HttpUtility.HtmlEncode(string.Format("{0}__{1}", base.Item.OwnerTableView.ClientID, base.Item.ItemIndexHierarchical));
			}
		}
	}
}
