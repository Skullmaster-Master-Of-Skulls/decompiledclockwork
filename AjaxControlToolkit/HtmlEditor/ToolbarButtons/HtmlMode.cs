using System;

namespace AjaxControlToolkit.HtmlEditor.ToolbarButtons
{
	// Token: 0x0200010E RID: 270
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.ToolbarButtons.HtmlMode", "HtmlEditor.ToolbarButtons.HtmlMode")]
	public class HtmlMode : ModeButton
	{
		// Token: 0x0600072D RID: 1837 RVA: 0x00013D3D File Offset: 0x00011F3D
		protected override void OnPreRender(EventArgs e)
		{
			base.RegisterButtonImages("Ed-Html");
			base.ActiveMode = ActiveModeType.Html;
			base.OnPreRender(e);
		}
	}
}
