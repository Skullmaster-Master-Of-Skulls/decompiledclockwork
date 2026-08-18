using System;

namespace AjaxControlToolkit.HtmlEditor.ToolbarButtons
{
	// Token: 0x02000114 RID: 276
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.ToolbarButtons.JustifyCenter", "HtmlEditor.ToolbarButtons.JustifyCenter")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	public class JustifyCenter : EditorToggleButton
	{
		// Token: 0x06000739 RID: 1849 RVA: 0x00013DF3 File Offset: 0x00011FF3
		protected override void OnPreRender(EventArgs e)
		{
			base.RegisterButtonImages("Ed-AlignCenter");
			base.OnPreRender(e);
		}
	}
}
