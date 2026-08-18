using System;

namespace AjaxControlToolkit.HtmlEditor.ToolbarButtons
{
	// Token: 0x02000115 RID: 277
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.ToolbarButtons.JustifyFull", "HtmlEditor.ToolbarButtons.JustifyFull")]
	public class JustifyFull : EditorToggleButton
	{
		// Token: 0x0600073B RID: 1851 RVA: 0x00013E0F File Offset: 0x0001200F
		protected override void OnPreRender(EventArgs e)
		{
			base.RegisterButtonImages("Ed-AlignJustify");
			base.OnPreRender(e);
		}
	}
}
