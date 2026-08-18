using System;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200114A RID: 4426
	public class GridMultiRowItem : GridItem
	{
		// Token: 0x0600B447 RID: 46151 RVA: 0x00277205 File Offset: 0x00275405
		public GridMultiRowItem(GridTableView ownerTableView) : base(ownerTableView, -1, -1, GridItemType.Unknown)
		{
		}

		// Token: 0x0600B448 RID: 46152 RVA: 0x00277214 File Offset: 0x00275414
		public override void PrepareItemStyle()
		{
			foreach (object obj in this.Controls)
			{
				GridItem gridItem = (GridItem)obj;
				gridItem.PrepareItemStyle();
			}
		}

		// Token: 0x0600B449 RID: 46153 RVA: 0x0027726C File Offset: 0x0027546C
		public override void PrepareItemVisibility()
		{
			bool flag = false;
			foreach (object obj in this.Controls)
			{
				GridItem gridItem = (GridItem)obj;
				gridItem.PrepareItemVisibility();
				if (gridItem.Visible)
				{
					flag = true;
				}
			}
			if (!flag)
			{
				this.Visible = false;
			}
		}

		// Token: 0x0600B44A RID: 46154 RVA: 0x002772DC File Offset: 0x002754DC
		protected override void Render(HtmlTextWriter writer)
		{
			bool flag = false;
			foreach (object obj in this.Controls)
			{
				Control control = (Control)obj;
				if (control.Visible)
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				base.Render(writer);
				return;
			}
			this.Visible = false;
		}

		// Token: 0x0600B44B RID: 46155 RVA: 0x00277350 File Offset: 0x00275550
		protected override ControlCollection CreateControlCollection()
		{
			this._innerRows = new ControlCollection(this);
			return this._innerRows;
		}

		// Token: 0x04002F82 RID: 12162
		private ControlCollection _innerRows;
	}
}
