using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Telerik.Web.UI
{
	// Token: 0x0200038E RID: 910
	internal class GridMobileColumnView : GridMobileView, IScriptControl
	{
		// Token: 0x06001F69 RID: 8041 RVA: 0x00063425 File Offset: 0x00061625
		public GridMobileColumnView(GridTableView tableView) : base(tableView)
		{
			this.CssClass = "rgMobileColumnForm";
		}

		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x06001F6A RID: 8042 RVA: 0x00063444 File Offset: 0x00061644
		public override GridMobileViewType Type
		{
			get
			{
				return GridMobileViewType.Column;
			}
		}

		// Token: 0x06001F6B RID: 8043 RVA: 0x00063448 File Offset: 0x00061648
		protected override void CreateContent(HtmlGenericControl container)
		{
			if (base.TableView.AllowSorting)
			{
				container.Controls.Add(base.CreateTitle(string.Empty));
				container.Controls.Add(base.CreateLink(base.Localization.HeaderContextMenuSortAsc, string.Format("{0} rgButtonSortAsc", this.SortCssClass)));
				container.Controls.Add(base.CreateLink(base.Localization.HeaderContextMenuSortDesc, string.Format("{0} rgButtonSortDesc", this.SortCssClass)));
				container.Controls.Add(base.CreateLink(base.Localization.HeaderContextMenuSortClear, string.Format("{0} rgButtonSortClear", this.SortCssClass)));
			}
			if (base.TableView.OwnerGrid.GroupingEnabled)
			{
				container.Controls.Add(base.CreateButton(string.Empty, "rgButtonGroup"));
			}
			if (base.TableView.OwnerGrid.ClientSettings.Resizing.AllowColumnResize)
			{
				bool allowResizeToFit = base.TableView.OwnerGrid.ClientSettings.Resizing.AllowResizeToFit;
			}
			if (base.TableView.OwnerGrid.ClientSettings.Scrolling.FrozenColumnsCount > 0)
			{
				container.Controls.Add(base.CreateButton("Freeze", "rgFreeze"));
			}
			bool enableHeaderContextAggregatesMenu = base.TableView.EnableHeaderContextAggregatesMenu;
			if (base.TableView.AllowFilteringByColumn && base.TableView.EnableHeaderContextFilterMenu)
			{
				container.Controls.Add(base.CreateLink(base.Localization.HeaderContextMenuFilterItemText, "rgFilter"));
			}
			if (base.TableView.OwnerGrid.ClientSettings.AllowColumnsReorder || base.TableView.OwnerGrid.ClientSettings.AllowColumnHide)
			{
				container.Controls.Add(base.CreateLink(base.Localization.HeaderContextMenuColumns, "rgColumns"));
			}
		}

		// Token: 0x06001F6C RID: 8044 RVA: 0x00063631 File Offset: 0x00061831
		protected override void DescribeProperties(ScriptControlDescriptor descriptor)
		{
			base.DescribeProperties(descriptor);
			descriptor.AddProperty("_groupText", base.Localization.HeaderContextMenuGroupBy);
			descriptor.AddProperty("_ungroupText", base.Localization.HeaderContextMenuUnGroupBy);
		}

		// Token: 0x04000807 RID: 2055
		private readonly string SortCssClass = "rgButtonSort";
	}
}
