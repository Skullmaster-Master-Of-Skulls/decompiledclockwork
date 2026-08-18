using System;
using System.Web.UI.HtmlControls;

namespace Telerik.Web.UI
{
	// Token: 0x0200096A RID: 2410
	internal class TreeListMobileColumnsView : TreeListMobileView
	{
		// Token: 0x06005BC2 RID: 23490 RVA: 0x00117A40 File Offset: 0x00115C40
		public TreeListMobileColumnsView(RadTreeList treelist) : base(treelist)
		{
			base.Title = base.Localization.MobileColumnsViewTitle;
		}

		// Token: 0x17001E3E RID: 7742
		// (get) Token: 0x06005BC3 RID: 23491 RVA: 0x00117A5A File Offset: 0x00115C5A
		public override TreeListMobileViewType Type
		{
			get
			{
				return TreeListMobileViewType.Columns;
			}
		}

		// Token: 0x06005BC4 RID: 23492 RVA: 0x00117A60 File Offset: 0x00115C60
		protected override void CreateContent(HtmlGenericControl container)
		{
			container.Controls.Add(base.CreateTitle(base.Localization.MobileColumnsViewDescription));
			foreach (TreeListColumn treeListColumn in base.TreeList.RenderColumns)
			{
				if (!(treeListColumn is TreeListDragDropColumn) && treeListColumn.Visible)
				{
					string text = treeListColumn.HeaderText;
					if (string.IsNullOrEmpty(text))
					{
						text = treeListColumn.UniqueName;
					}
					if (string.IsNullOrEmpty(text))
					{
						text = "&nbsp;";
					}
					HtmlGenericControl htmlGenericControl;
					if (base.TreeList.ClientSettings.AllowColumnHide)
					{
						htmlGenericControl = base.CreateOption(TreeListMobileViewOptionType.Checkbox, text, "Column", "rtlColumnItem", treeListColumn.Display);
					}
					else
					{
						htmlGenericControl = base.CreateLabel(text, "rtlColumnItem");
					}
					if (base.TreeList.ClientSettings.Reordering.AllowColumnsReorder)
					{
						HtmlGenericControl htmlGenericControl2 = new HtmlGenericControl("span");
						htmlGenericControl2.Attributes.Add("class", "rtlDrag");
						htmlGenericControl.Controls.AddAt(0, htmlGenericControl2);
					}
					container.Controls.Add(htmlGenericControl);
				}
			}
		}
	}
}
