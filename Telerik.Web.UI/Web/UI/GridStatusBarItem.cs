using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001151 RID: 4433
	[Obsolete]
	public class GridStatusBarItem : GridItem
	{
		// Token: 0x0600B496 RID: 46230 RVA: 0x0027C4F5 File Offset: 0x0027A6F5
		public GridStatusBarItem(GridTableView ownerTableView, int itemIndex, int dataSetIndex) : base(ownerTableView, itemIndex, dataSetIndex, GridItemType.StatusBar)
		{
		}

		// Token: 0x17003A51 RID: 14929
		// (get) Token: 0x0600B497 RID: 46231 RVA: 0x0027C502 File Offset: 0x0027A702
		public TableCell TemplateCell
		{
			get
			{
				return this._templateCell;
			}
		}

		// Token: 0x0600B498 RID: 46232 RVA: 0x0027C50A File Offset: 0x0027A70A
		public override void PrepareItemStyle()
		{
			if (this.TemplateCell == null)
			{
				return;
			}
			this.TemplateCell.ColumnSpan = base.CalcColSpan(base.OwnerTableView.RenderColumns, this.calculatedColumnIndex, -1);
			base.PrepareItemStyle();
		}

		// Token: 0x0600B499 RID: 46233 RVA: 0x0027C540 File Offset: 0x0027A740
		public override void Initialize(GridColumn[] columns)
		{
			TableCell tableCell = this.CreateCellObject();
			this.Cells.Add(tableCell);
			tableCell.ColumnSpan = base.CalcColSpan(columns, this.calculatedColumnIndex, -1);
			tableCell.VerticalAlign = VerticalAlign.Top;
			if (base.OwnerTableView.Dir == GridTableTextDirection.LTR)
			{
				tableCell.HorizontalAlign = HorizontalAlign.Left;
			}
			else
			{
				tableCell.HorizontalAlign = HorizontalAlign.Right;
			}
			this._templateCell = tableCell;
		}

		// Token: 0x0600B49A RID: 46234 RVA: 0x0027C5A0 File Offset: 0x0027A7A0
		public override void SetupItem(bool dataBind, object dataItem, GridColumn[] columns, ControlCollection rows)
		{
			rows.Add(this);
			this.Initialize(columns);
			Label label = new Label();
			label.ID = "StatusLabel";
			label.Text = HttpUtility.HtmlEncode(base.OwnerTableView.OwnerGrid.StatusBarSettings.ReadyText);
			this.TemplateCell.Controls.Add(label);
			GridItemEventArgs e = new GridItemEventArgs(this, new GridItemCreated());
			base.OwnerTableView.OwnerGrid.CallOnItemCreated(e);
		}

		// Token: 0x0600B49B RID: 46235 RVA: 0x0027C61B File Offset: 0x0027A81B
		private void RebindLink_Click(object sender, EventArgs e)
		{
			base.OwnerTableView.OwnerGrid.Rebind();
		}

		// Token: 0x0600B49C RID: 46236 RVA: 0x0027C630 File Offset: 0x0027A830
		protected override void Render(HtmlTextWriter writer)
		{
			Label label = (Label)this.FindControl("StatusLabel");
			if (label != null)
			{
				label.Text = HttpUtility.HtmlEncode(base.OwnerTableView.OwnerGrid.StatusBarSettings.ReadyText);
			}
			base.Render(writer);
		}

		// Token: 0x04002F99 RID: 12185
		private TableCell _templateCell;

		// Token: 0x04002F9A RID: 12186
		private int calculatedColumnIndex;
	}
}
