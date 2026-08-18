using System;

namespace AjaxControlToolkit.HtmlEditor.ToolbarButtons
{
	// Token: 0x0200011E RID: 286
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.ToolbarButtons.PreviewMode", "HtmlEditor.ToolbarButtons.PreviewMode")]
	public class PreviewMode : ModeButton
	{
		// Token: 0x0600074D RID: 1869 RVA: 0x00013F0B File Offset: 0x0001210B
		protected override void OnPreRender(EventArgs e)
		{
			base.RegisterButtonImages("Ed-Preview");
			base.ActiveMode = ActiveModeType.Preview;
			base.OnPreRender(e);
		}
	}
}
