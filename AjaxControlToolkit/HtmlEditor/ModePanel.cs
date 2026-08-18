using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AjaxControlToolkit.HtmlEditor
{
	// Token: 0x020000CE RID: 206
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.Editor", "HtmlEditor.ModePanel")]
	public abstract class ModePanel : ScriptControlBase
	{
		// Token: 0x060005C8 RID: 1480 RVA: 0x0000EFA5 File Offset: 0x0000D1A5
		protected ModePanel(HtmlTextWriterTag tag) : base(false, tag)
		{
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x0000EFB0 File Offset: 0x0000D1B0
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			base.Style.Add(HtmlTextWriterStyle.Height, Unit.Percentage(100.0).ToString());
			base.Style.Add(HtmlTextWriterStyle.Width, Unit.Percentage(100.0).ToString());
			base.Style.Add(HtmlTextWriterStyle.Display, "none");
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0000F028 File Offset: 0x0000D228
		internal void setEditPanel(EditPanel editPanel)
		{
			this._editPanel = editPanel;
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x0000F031 File Offset: 0x0000D231
		protected override void DescribeComponent(ScriptComponentDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			if (this._editPanel != null)
			{
				descriptor.AddComponentProperty("editPanel", this._editPanel.ClientID);
			}
		}

		// Token: 0x040002D4 RID: 724
		private EditPanel _editPanel;
	}
}
