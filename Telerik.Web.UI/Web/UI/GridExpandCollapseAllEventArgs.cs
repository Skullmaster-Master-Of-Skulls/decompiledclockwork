using System;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020010DE RID: 4318
	public class GridExpandCollapseAllEventArgs : GridCommandEventArgs
	{
		// Token: 0x0600B0D8 RID: 45272 RVA: 0x00264760 File Offset: 0x00262960
		public GridExpandCollapseAllEventArgs(GridItem item, object commandSource, object argument) : base(item, commandSource, "ExpandCollapseAll", argument)
		{
			GridHeaderItem gridHeaderItem = commandSource as GridHeaderItem;
			WebControl webControl;
			if (gridHeaderItem != null)
			{
				webControl = (gridHeaderItem[base.CommandArgument.ToString()].Controls[0] as WebControl);
			}
			else
			{
				webControl = (commandSource as WebControl);
			}
			if ((item.OwnerTableView.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile || item.OwnerTableView.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight) && !(webControl is ImageButton))
			{
				if (webControl.CssClass.Contains("rgCollapse"))
				{
					this.Expanded = false;
					webControl.CssClass = "t-button rgActionButton rgExpand";
					(webControl as ElasticButton).FirstSpanClass = "t-font-icon rgIcon rgExpandIcon";
					return;
				}
				this.Expanded = true;
				webControl.CssClass = "t-button rgActionButton rgCollapse";
				(webControl as ElasticButton).FirstSpanClass = "t-font-icon rgIcon rgCollapseIcon";
				return;
			}
			else
			{
				if (webControl.CssClass == "rgCollapse")
				{
					this.Expanded = false;
					webControl.CssClass = "rgExpand";
					return;
				}
				this.Expanded = true;
				webControl.CssClass = "rgCollapse";
				return;
			}
		}

		// Token: 0x17003948 RID: 14664
		// (get) Token: 0x0600B0D9 RID: 45273 RVA: 0x0026486D File Offset: 0x00262A6D
		// (set) Token: 0x0600B0DA RID: 45274 RVA: 0x00264875 File Offset: 0x00262A75
		public bool Expanded { get; set; }

		// Token: 0x0600B0DB RID: 45275 RVA: 0x00264880 File Offset: 0x00262A80
		public override void ExecuteCommand(object source)
		{
			foreach (object obj in base.Item.OwnerTableView.Items)
			{
				GridDataItem gridDataItem = (GridDataItem)obj;
				gridDataItem.Expanded = this.Expanded;
			}
		}
	}
}
