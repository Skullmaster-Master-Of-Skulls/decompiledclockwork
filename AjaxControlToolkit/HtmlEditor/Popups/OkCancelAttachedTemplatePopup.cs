using System;
using System.ComponentModel;
using System.Web.UI.WebControls;

namespace AjaxControlToolkit.HtmlEditor.Popups
{
	// Token: 0x020000E3 RID: 227
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.Popups.OkCancelAttachedTemplatePopup", "HtmlEditor.Popups.OkCancelAttachedTemplatePopup")]
	[ToolboxItem(false)]
	[RequiredScript(typeof(CommonToolkitScripts))]
	public class OkCancelAttachedTemplatePopup : AttachedTemplatePopup
	{
		// Token: 0x0600067E RID: 1662 RVA: 0x00012164 File Offset: 0x00010364
		protected override void CreateChildControls()
		{
			PopupBGIButton popupBGIButton = new PopupBGIButton();
			popupBGIButton.Text = base.GetButton("OK");
			popupBGIButton.Name = "OK";
			PopupBGIButton popupBGIButton2 = popupBGIButton;
			popupBGIButton2.CssClass += " ajax__htmleditor_popup_confirmbutton ";
			PopupBGIButton popupBGIButton3 = new PopupBGIButton();
			popupBGIButton3.Text = base.GetButton("Cancel");
			popupBGIButton3.Name = "Cancel";
			PopupBGIButton popupBGIButton4 = popupBGIButton3;
			popupBGIButton4.CssClass += " ajax__htmleditor_popup_confirmbutton";
			Table table = new Table();
			table.Attributes.Add("border", "0");
			table.Attributes.Add("cellspacing", "0");
			table.Attributes.Add("cellpadding", "0");
			table.Style["width"] = "100%";
			TableRow tableRow = new TableRow();
			table.Rows.Add(tableRow);
			TableCell tableCell = new TableCell();
			tableRow.Cells.Add(tableCell);
			tableCell.HorizontalAlign = HorizontalAlign.Right;
			tableCell.Controls.Add(popupBGIButton);
			tableCell.Controls.Add(popupBGIButton3);
			base.Content.Add(table);
			base.RegisteredHandlers.Add(new RegisteredField("OK", popupBGIButton));
			base.RegisteredHandlers.Add(new RegisteredField("Cancel", popupBGIButton3));
			base.CreateChildControls();
		}
	}
}
