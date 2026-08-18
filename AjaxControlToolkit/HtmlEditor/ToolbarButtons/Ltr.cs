using System;

namespace AjaxControlToolkit.HtmlEditor.ToolbarButtons
{
	// Token: 0x02000118 RID: 280
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.ToolbarButtons.Ltr", "HtmlEditor.ToolbarButtons.Ltr")]
	public class Ltr : EditorToggleButton
	{
		// Token: 0x06000741 RID: 1857 RVA: 0x00013E63 File Offset: 0x00012063
		protected override void OnPreRender(EventArgs e)
		{
			base.RegisterButtonImages("Ed-FormatLtr");
			base.OnPreRender(e);
		}
	}
}
