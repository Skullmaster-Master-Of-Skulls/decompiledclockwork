using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace AjaxControlToolkit.HtmlEditor.Popups
{
	// Token: 0x020000E8 RID: 232
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.Popups.PopupBGIButton", "HtmlEditor.Popups.PopupBGIButton")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	internal class PopupBGIButton : PopupBoxButton
	{
		// Token: 0x06000698 RID: 1688 RVA: 0x00012818 File Offset: 0x00010A18
		public PopupBGIButton() : base(HtmlTextWriterTag.Div)
		{
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0001282D File Offset: 0x00010A2D
		public PopupBGIButton(HtmlTextWriterTag tag) : base(tag)
		{
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x0600069A RID: 1690 RVA: 0x00012841 File Offset: 0x00010A41
		// (set) Token: 0x0600069B RID: 1691 RVA: 0x00012849 File Offset: 0x00010A49
		[DefaultValue("")]
		[Category("Appearance")]
		public string Text
		{
			get
			{
				return this._text;
			}
			set
			{
				this._text = value;
			}
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x00012854 File Offset: 0x00010A54
		protected override void CreateChildControls()
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("span");
			Table table = new Table();
			table.Attributes.Add("border", "0");
			table.Attributes.Add("cellspacing", "0");
			table.Attributes.Add("cellpadding", "0");
			TableRow tableRow = new TableRow();
			table.Rows.Add(tableRow);
			TableCell tableCell = new TableCell();
			tableRow.Cells.Add(tableCell);
			tableCell.VerticalAlign = VerticalAlign.Middle;
			tableCell.HorizontalAlign = HorizontalAlign.Center;
			tableCell.CssClass = "ajax__htmleditor_popup_bgibutton";
			LiteralControl child = new LiteralControl(this.Text);
			htmlGenericControl.Controls.Add(child);
			tableCell.Controls.Add(htmlGenericControl);
			base.Content.Add(table);
			base.CreateChildControls();
		}

		// Token: 0x040002FD RID: 765
		private string _text = string.Empty;
	}
}
