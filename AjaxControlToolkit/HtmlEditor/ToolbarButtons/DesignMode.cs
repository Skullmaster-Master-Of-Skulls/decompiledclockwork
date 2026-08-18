using System;

namespace AjaxControlToolkit.HtmlEditor.ToolbarButtons
{
	// Token: 0x020000FF RID: 255
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.ToolbarButtons.DesignMode", "HtmlEditor.ToolbarButtons.DesignMode")]
	public class DesignMode : ModeButton
	{
		// Token: 0x06000702 RID: 1794 RVA: 0x000136B1 File Offset: 0x000118B1
		protected override void OnPreRender(EventArgs e)
		{
			base.RegisterButtonImages("Ed-Design");
			base.ActiveMode = ActiveModeType.Design;
			base.OnPreRender(e);
		}
	}
}
