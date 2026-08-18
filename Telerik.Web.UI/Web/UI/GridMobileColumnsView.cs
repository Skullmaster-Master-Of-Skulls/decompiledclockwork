using System;
using System.Web.UI.HtmlControls;

namespace Telerik.Web.UI
{
	// Token: 0x0200038D RID: 909
	internal class GridMobileColumnsView : GridMobileView
	{
		// Token: 0x06001F66 RID: 8038 RVA: 0x000632C4 File Offset: 0x000614C4
		public GridMobileColumnsView(GridTableView tableView) : base(tableView)
		{
			base.Title = base.Localization.MobileColumnsViewTitle;
		}

		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x06001F67 RID: 8039 RVA: 0x000632DE File Offset: 0x000614DE
		public override GridMobileViewType Type
		{
			get
			{
				return GridMobileViewType.Columns;
			}
		}

		// Token: 0x06001F68 RID: 8040 RVA: 0x000632E4 File Offset: 0x000614E4
		protected override void CreateContent(HtmlGenericControl container)
		{
			container.Controls.Add(base.CreateTitle(base.Localization.MobileColumnsViewDescription));
			foreach (GridColumn gridColumn in base.TableView.RenderColumns)
			{
				if (!(gridColumn is GridGroupSplitterColumn) && !(gridColumn is GridExpandColumn) && !(gridColumn is GridRowIndicatorColumn) && !(gridColumn is GridDragDropColumn) && gridColumn.Visible)
				{
					string text = gridColumn.HeaderText;
					if (string.IsNullOrEmpty(text))
					{
						text = gridColumn.UniqueName;
					}
					if (string.IsNullOrEmpty(text))
					{
						text = "&nbsp;";
					}
					HtmlGenericControl htmlGenericControl;
					if (base.TableView.OwnerGrid.ClientSettings.AllowColumnHide)
					{
						htmlGenericControl = base.CreateOption(GridMobileViewOptionType.Checkbox, text, "Column", "rgColumnItem", gridColumn.Display);
					}
					else
					{
						htmlGenericControl = base.CreateLabel(text, "rgColumnItem");
					}
					if (base.TableView.OwnerGrid.ClientSettings.AllowColumnsReorder)
					{
						HtmlGenericControl htmlGenericControl2 = new HtmlGenericControl("span");
						htmlGenericControl2.Attributes.Add("class", "rgDrag");
						htmlGenericControl.Controls.AddAt(0, htmlGenericControl2);
					}
					container.Controls.Add(htmlGenericControl);
				}
			}
		}
	}
}
