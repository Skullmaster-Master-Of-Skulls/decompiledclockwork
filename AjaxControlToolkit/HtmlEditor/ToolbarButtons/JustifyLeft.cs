using System;

namespace AjaxControlToolkit.HtmlEditor.ToolbarButtons
{
	// Token: 0x02000116 RID: 278
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.ToolbarButtons.JustifyLeft", "HtmlEditor.ToolbarButtons.JustifyLeft")]
	public class JustifyLeft : EditorToggleButton
	{
		// Token: 0x0600073D RID: 1853 RVA: 0x00013E2B File Offset: 0x0001202B
		protected override void OnPreRender(EventArgs e)
		{
			base.RegisterButtonImages("Ed-AlignLeft");
			base.OnPreRender(e);
		}
	}
}
