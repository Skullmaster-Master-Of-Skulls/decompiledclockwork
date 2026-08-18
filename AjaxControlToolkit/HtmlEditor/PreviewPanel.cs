using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AjaxControlToolkit.HtmlEditor
{
	// Token: 0x020000E9 RID: 233
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.PreviewPanel", "HtmlEditor.PreviewPanel")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	[RequiredScript(typeof(HtmlEditor))]
	internal class PreviewPanel : ModePanel
	{
		// Token: 0x0600069D RID: 1693 RVA: 0x00012928 File Offset: 0x00010B28
		public PreviewPanel() : base(HtmlTextWriterTag.Iframe)
		{
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x00012934 File Offset: 0x00010B34
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			base.Attributes.Add("name", this.ClientID);
			base.Attributes.Add("marginheight", "0");
			base.Attributes.Add("marginwidth", "0");
			base.Attributes.Add("frameborder", "0");
			if (EditPanel.IE(this.Page))
			{
				base.Attributes.Add("src", "javascript:false;");
			}
			base.Style.Add(HtmlTextWriterStyle.BorderWidth, Unit.Pixel(0).ToString());
		}
	}
}
