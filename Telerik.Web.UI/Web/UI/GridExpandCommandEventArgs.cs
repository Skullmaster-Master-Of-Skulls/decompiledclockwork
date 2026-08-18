using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020010DD RID: 4317
	public class GridExpandCommandEventArgs : GridCommandEventArgs
	{
		// Token: 0x0600B0D2 RID: 45266 RVA: 0x002641A1 File Offset: 0x002623A1
		public GridExpandCommandEventArgs(GridItem item, object commandSource, object argument) : base(item, commandSource, "ExpandCollapse", argument)
		{
		}

		// Token: 0x0600B0D3 RID: 45267 RVA: 0x002641B4 File Offset: 0x002623B4
		public override void ExecuteCommand(object source)
		{
			base.Item.Expanded = !base.Item.Expanded;
			if (base.Item.OwnerTableView.HierarchyLoadMode == GridChildLoadMode.Conditional && base.Item.Expanded && !base.Item.ConditionalExpanded)
			{
				base.Item.ConditionalExpanded = base.Item.Expanded;
			}
			if (base.Item.OwnerTableView.EnableGroupsExpandAll)
			{
				this.AdjustGroupsExpandAllHeaderButton(base.Item as GridGroupHeaderItem);
			}
			if (base.Item.OwnerTableView.EnableHierarchyExpandAll)
			{
				this.AdjustHierarchyExpandAllHeaderButton();
			}
		}

		// Token: 0x0600B0D4 RID: 45268 RVA: 0x00264264 File Offset: 0x00262464
		private void AdjustHierarchyExpandAllHeaderButton()
		{
			bool expanded = base.Item.Expanded;
			bool flag = true;
			foreach (object obj in base.Item.OwnerTableView.Items)
			{
				GridItem gridItem = (GridItem)obj;
				if (gridItem.Expanded != expanded)
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				GridHeaderItem gridHeaderItem = base.Item.OwnerTableView.GetItems(new GridItemType[]
				{
					GridItemType.Header
				})[0] as GridHeaderItem;
				IEnumerable<GridColumn> enumerable = from col in base.Item.OwnerTableView.RenderColumns
				where col is GridExpandColumn
				select col;
				WebControl webControl = null;
				foreach (GridColumn gridColumn in enumerable)
				{
					if (gridHeaderItem.MultiHeaderCells != null)
					{
						for (int i = 0; i < gridHeaderItem.MultiHeaderCells.Count; i++)
						{
							GridColumn gridColumn2 = gridHeaderItem.MultiHeaderCells[i] as GridColumn;
							if (gridColumn2 != null && gridColumn2.UniqueName == gridColumn.UniqueName)
							{
								webControl = (gridHeaderItem.Cells[i].Controls[0] as WebControl);
								break;
							}
						}
					}
					if (webControl == null)
					{
						webControl = (gridHeaderItem[gridColumn.UniqueName].Controls[0] as WebControl);
					}
					if ((base.Item.OwnerTableView.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile || base.Item.OwnerTableView.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight) && !(webControl is ImageButton))
					{
						ElasticButton elasticButton = webControl as ElasticButton;
						elasticButton.CssClass = "t-button rgActionButton ";
						ElasticButton elasticButton2 = elasticButton;
						elasticButton2.CssClass += (base.Item.Expanded ? "rgCollapse" : "rgExpand");
						elasticButton.FirstSpanClass = "t-font-icon rgIcon " + (base.Item.Expanded ? "rgCollapseIcon" : "rgExpandIcon");
					}
					else if (base.Item.Expanded)
					{
						webControl.CssClass = "rgCollapse";
					}
					else
					{
						webControl.CssClass = "rgExpand";
					}
				}
			}
		}

		// Token: 0x0600B0D5 RID: 45269 RVA: 0x00264524 File Offset: 0x00262724
		private void AdjustGroupsExpandAllHeaderButton(GridGroupHeaderItem item)
		{
			if (item != null)
			{
				IEnumerable<GridItem> source = from it in item.OwnerTableView.GetItems(new GridItemType[]
				{
					GridItemType.GroupHeader
				})
				where it.GroupLevel == item.GroupLevel
				select it;
				int num = (from it in source
				where it.Expanded
				select it).Count<GridItem>();
				if (source.Count<GridItem>() == num || num == 0)
				{
					GridItem gridItem = item.OwnerTableView.GetItems(new GridItemType[]
					{
						GridItemType.Header
					})[0];
					WebControl webControl = gridItem.Cells[item.GroupLevel].Controls[0] as WebControl;
					if (item.OwnerTableView.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile || item.OwnerTableView.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight)
					{
						ElasticButton elasticButton = webControl as ElasticButton;
						elasticButton.CssClass = "t-button rgActionButton ";
						ElasticButton elasticButton2 = elasticButton;
						elasticButton2.CssClass += (item.Expanded ? "rgCollapse" : "rgExpand");
						elasticButton.FirstSpanClass = "t-font-icon rgIcon " + (item.Expanded ? "rgCollapseIcon" : "rgExpandIcon");
					}
					else if (item.Expanded)
					{
						webControl.CssClass = "rgCollapse";
					}
					else
					{
						webControl.CssClass = "rgExpand";
					}
					WebControl webControl2 = webControl;
					for (int i = item.GroupLevel + 1; i < item.OwnerTableView.GroupByExpressions.Count; i++)
					{
						WebControl webControl3 = gridItem.Cells[i].Controls[0] as WebControl;
						if (webControl2 != null)
						{
							if (webControl2.CssClass.Contains("rgCollapse") && webControl2.Visible)
							{
								webControl3.Visible = true;
							}
							else
							{
								webControl3.Visible = false;
							}
						}
						webControl2 = webControl3;
					}
				}
			}
		}
	}
}
