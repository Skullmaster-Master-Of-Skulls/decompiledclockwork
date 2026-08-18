using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace AjaxControlToolkit.HtmlEditor.Popups
{
	// Token: 0x020000E4 RID: 228
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.Popups.LinkProperties", "HtmlEditor.Popups.LinkProperties")]
	internal class LinkProperties : OkCancelAttachedTemplatePopup
	{
		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000680 RID: 1664 RVA: 0x000122EC File Offset: 0x000104EC
		// (set) Token: 0x06000681 RID: 1665 RVA: 0x000122F4 File Offset: 0x000104F4
		[Category("Behavior")]
		[DefaultValue("_self")]
		public string DefaultTarget
		{
			get
			{
				return this._defaultTarget;
			}
			set
			{
				this._defaultTarget = value;
			}
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x00012300 File Offset: 0x00010500
		protected override void CreateChildControls()
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("span");
			HtmlGenericControl htmlGenericControl2 = new HtmlGenericControl("span");
			Table table = new Table();
			table.Attributes.Add("border", "0");
			table.Attributes.Add("cellspacing", "0");
			table.Attributes.Add("cellpadding", "2");
			TableRow tableRow = new TableRow();
			table.Rows.Add(tableRow);
			TableCell tableCell = new TableCell();
			tableRow.Cells.Add(tableCell);
			tableCell.HorizontalAlign = HorizontalAlign.Left;
			tableCell.Controls.Add(htmlGenericControl);
			htmlGenericControl.Controls.Add(new LiteralControl(base.GetField("URL")));
			tableCell.Controls.Add(new LiteralControl(":"));
			tableCell = new TableCell();
			tableRow.Cells.Add(tableCell);
			tableCell.HorizontalAlign = HorizontalAlign.Left;
			this._url.Style["width"] = "200px";
			this._url.MaxLength = 255;
			tableCell.Controls.Add(this._url);
			tableRow = new TableRow();
			table.Rows.Add(tableRow);
			tableCell = new TableCell();
			tableRow.Cells.Add(tableCell);
			tableCell.HorizontalAlign = HorizontalAlign.Left;
			tableCell.Controls.Add(htmlGenericControl2);
			htmlGenericControl2.Controls.Add(new LiteralControl(base.GetField("Target")));
			tableCell.Controls.Add(new LiteralControl(":"));
			tableCell = new TableCell();
			tableRow.Cells.Add(tableCell);
			tableCell.HorizontalAlign = HorizontalAlign.Left;
			this._target.Style["width"] = "105px";
			this._target.Items.Add(new ListItem(base.GetField("Target", "New"), "_blank"));
			this._target.Items.Add(new ListItem(base.GetField("Target", "Current"), "_self"));
			this._target.Items.Add(new ListItem(base.GetField("Target", "Parent"), "_parent"));
			this._target.Items.Add(new ListItem(base.GetField("Target", "Top"), "_top"));
			tableCell.Controls.Add(this._target);
			base.Content.Add(table);
			base.RegisteredFields.Add(new RegisteredField("url", this._url));
			base.RegisteredFields.Add(new RegisteredField("target", this._target));
			base.CreateChildControls();
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x000125D8 File Offset: 0x000107D8
		protected override void OnPreRender(EventArgs e)
		{
			this._url.Attributes.Add("id", this._url.ClientID);
			this._target.Attributes.Add("id", this._target.ClientID);
			base.OnPreRender(e);
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x0001262C File Offset: 0x0001082C
		protected override void DescribeComponent(ScriptComponentDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			descriptor.AddProperty("defaultTarget", this.DefaultTarget);
		}

		// Token: 0x040002F4 RID: 756
		private TextBox _url = new TextBox();

		// Token: 0x040002F5 RID: 757
		private HtmlSelect _target = new HtmlSelect();

		// Token: 0x040002F6 RID: 758
		private string _defaultTarget = "_self";
	}
}
