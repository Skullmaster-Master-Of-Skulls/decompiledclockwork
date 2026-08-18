using System;
using System.Web;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001119 RID: 4377
	internal class GridEditItemDecorator : GridItemDecorator
	{
		// Token: 0x0600B329 RID: 45865 RVA: 0x0027011D File Offset: 0x0026E31D
		public GridEditItemDecorator(GridItem item) : base(item)
		{
		}

		// Token: 0x0600B32A RID: 45866 RVA: 0x00270128 File Offset: 0x0026E328
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
			if (base.Item.Selected)
			{
				tableItemStyle.CopyFrom(owner.SelectedItemStyle);
			}
			if (base.Item.OwnerTableView.OwnerGrid.ResolvedRenderMode != RenderMode.Mobile)
			{
				tableItemStyle.CopyFrom(owner.RenderEditItemStyle);
			}
			base.Item.MergeStyle(tableItemStyle);
			if (base.Item is GridDataItem && !(base.Item is GridDataInsertItem))
			{
				base.Item.Attributes["id"] = HttpUtility.HtmlEncode(string.Format("{0}__{1}", base.Item.OwnerTableView.ClientID, base.Item.ItemIndexHierarchical));
			}
			if (base.Item.Cells[0].CssClass.IndexOf("rgGroupHeader") > -1)
			{
				base.Item.Cells[0].CssClass = HttpUtility.HtmlEncode(string.Empty);
			}
		}
	}
}
