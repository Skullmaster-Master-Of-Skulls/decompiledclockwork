using System;

namespace AjaxControlToolkit.HtmlEditor.ToolbarButtons
{
	// Token: 0x02000117 RID: 279
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.ToolbarButtons.JustifyRight", "HtmlEditor.ToolbarButtons.JustifyRight")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	public class JustifyRight : EditorToggleButton
	{
		// Token: 0x0600073F RID: 1855 RVA: 0x00013E47 File Offset: 0x00012047
		protected override void OnPreRender(EventArgs e)
		{
			base.RegisterButtonImages("Ed-AlignRight");
			base.OnPreRender(e);
		}
	}
}
