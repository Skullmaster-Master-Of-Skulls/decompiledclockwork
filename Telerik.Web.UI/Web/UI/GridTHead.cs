using System;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200114C RID: 4428
	public class GridTHead : GridMultiRowItem
	{
		// Token: 0x0600B44F RID: 46159 RVA: 0x00277387 File Offset: 0x00275587
		public GridTHead(GridTableView ownerTableView, bool isStatic) : base(ownerTableView)
		{
			this.ownerTableView = ownerTableView;
			this.isStatic = isStatic;
			if (ownerTableView.IsClone)
			{
				this.isStatic = false;
			}
			this.SetItemType(GridItemType.THead);
		}

		// Token: 0x0600B450 RID: 46160 RVA: 0x002773B4 File Offset: 0x002755B4
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			writer.RenderBeginTag(HtmlTextWriterTag.Thead);
		}

		// Token: 0x0600B451 RID: 46161 RVA: 0x002773BE File Offset: 0x002755BE
		protected override void Render(HtmlTextWriter writer)
		{
			base.Render(writer);
		}

		// Token: 0x0600B452 RID: 46162 RVA: 0x002773C8 File Offset: 0x002755C8
		protected override void RenderContents(HtmlTextWriter writer)
		{
			if (this.isStatic && this.ownerTableView.OwnerGrid.ClientSettings.Scrolling.AllowScroll && this.ownerTableView.OwnerGrid.ClientSettings.Scrolling.UseStaticHeaders)
			{
				foreach (object obj in this.Controls)
				{
					Control control = (Control)obj;
					if (!(control is GridHeaderItem) && !(control is GridFilteringItem) && !(control is GridEditFormInsertItem) && !(control is GridEditableItem) && control.Visible)
					{
						GridPagerItem gridPagerItem = control as GridPagerItem;
						if (gridPagerItem != null && gridPagerItem.IsTopPager && (this.ownerTableView.PagerStyle.Position == GridPagerPosition.Top || this.ownerTableView.PagerStyle.Position == GridPagerPosition.TopAndBottom || this.ownerTableView.OwnerGrid.PagerStyle.Position == GridPagerPosition.Top || this.ownerTableView.OwnerGrid.PagerStyle.Position == GridPagerPosition.TopAndBottom))
						{
							control.Visible = true;
						}
						control.RenderControl(writer);
						control.Visible = false;
					}
				}
				this.isStatic = false;
				return;
			}
			this.isStatic = false;
			base.RenderContents(writer);
		}

		// Token: 0x04002F83 RID: 12163
		private GridTableView ownerTableView;

		// Token: 0x04002F84 RID: 12164
		internal bool isStatic;
	}
}
