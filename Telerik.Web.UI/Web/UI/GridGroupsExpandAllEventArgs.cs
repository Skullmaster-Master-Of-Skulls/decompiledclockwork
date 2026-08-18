using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020010E0 RID: 4320
	public class GridGroupsExpandAllEventArgs : GridCommandEventArgs
	{
		// Token: 0x0600B0E1 RID: 45281 RVA: 0x00264C18 File Offset: 0x00262E18
		public GridGroupsExpandAllEventArgs(GridItem item, object commandSource, object argument) : base(item, commandSource, "GroupsExpandAll", argument)
		{
			this.GroupLevel = Convert.ToInt32(base.CommandArgument);
			GridColumn[] source = (from col in item.OwnerTableView.RenderColumns
			where col is GridGroupSplitterColumn
			select col).ToArray<GridColumn>();
			GridHeaderItem gridHeaderItem = item as GridHeaderItem;
			WebControl webControl = commandSource as WebControl;
			if (webControl.CssClass.Contains("rgCollapse"))
			{
				this.Expanded = false;
				webControl.CssClass = "rgExpand";
				if (item.OwnerTableView.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile || item.OwnerTableView.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight)
				{
					webControl.CssClass = "t-button rgActionButton rgExpand";
					(webControl as ElasticButton).FirstSpanClass = "t-font-icon rgIcon rgExpandIcon";
				}
				for (int i = this.GroupLevel + 1; i < source.Count<GridColumn>(); i++)
				{
					gridHeaderItem.Cells[i].Controls[0].Visible = false;
				}
				return;
			}
			this.Expanded = true;
			webControl.CssClass = "rgCollapse";
			if (item.OwnerTableView.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile || item.OwnerTableView.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight)
			{
				webControl.CssClass = "t-button rgActionButton rgCollapse";
				(webControl as ElasticButton).FirstSpanClass = "t-font-icon rgIcon rgCollapseIcon";
			}
			WebControl webControl2 = webControl;
			for (int j = this.GroupLevel + 1; j < source.Count<GridColumn>(); j++)
			{
				if (webControl2.CssClass.Contains("rgCollapse") && webControl2.Visible)
				{
					gridHeaderItem.Cells[j].Controls[0].Visible = true;
				}
				webControl2 = (gridHeaderItem.Cells[j].Controls[0] as WebControl);
			}
		}

		// Token: 0x17003949 RID: 14665
		// (get) Token: 0x0600B0E2 RID: 45282 RVA: 0x00264DEE File Offset: 0x00262FEE
		// (set) Token: 0x0600B0E3 RID: 45283 RVA: 0x00264DF6 File Offset: 0x00262FF6
		public GridGroupSplitterColumn GroupSplitterColumn { get; set; }

		// Token: 0x1700394A RID: 14666
		// (get) Token: 0x0600B0E4 RID: 45284 RVA: 0x00264DFF File Offset: 0x00262FFF
		// (set) Token: 0x0600B0E5 RID: 45285 RVA: 0x00264E07 File Offset: 0x00263007
		public int GroupLevel { get; set; }

		// Token: 0x1700394B RID: 14667
		// (get) Token: 0x0600B0E6 RID: 45286 RVA: 0x00264E10 File Offset: 0x00263010
		// (set) Token: 0x0600B0E7 RID: 45287 RVA: 0x00264E18 File Offset: 0x00263018
		public bool Expanded { get; set; }

		// Token: 0x0600B0E8 RID: 45288 RVA: 0x00264E34 File Offset: 0x00263034
		public override void ExecuteCommand(object source)
		{
			IEnumerable<GridItem> enumerable = from item in base.Item.OwnerTableView.GetItems(new GridItemType[]
			{
				GridItemType.GroupHeader
			})
			where item.GroupLevel == this.GroupLevel
			select item;
			foreach (GridItem gridItem in enumerable)
			{
				GridGroupHeaderItem gridGroupHeaderItem = (GridGroupHeaderItem)gridItem;
				gridGroupHeaderItem.Expanded = this.Expanded;
			}
		}
	}
}
