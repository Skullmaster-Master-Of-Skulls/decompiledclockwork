using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AjaxControlToolkit.HtmlEditor
{
	// Token: 0x020000CF RID: 207
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.DesignPanel", "HtmlEditor.DesignPanel")]
	[RequiredScript(typeof(DesignPanelEventHandler), 3)]
	[RequiredScript(typeof(CommonToolkitScripts), 0)]
	[RequiredScript(typeof(HtmlEditor), 1)]
	[RequiredScript(typeof(ExecCommandEmulation), 2)]
	internal class DesignPanel : ModePanel
	{
		// Token: 0x060005CC RID: 1484 RVA: 0x0000F058 File Offset: 0x0000D258
		public DesignPanel() : base(HtmlTextWriterTag.Iframe)
		{
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x0000F064 File Offset: 0x0000D264
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
