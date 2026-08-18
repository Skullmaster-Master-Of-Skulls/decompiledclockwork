using System;

namespace AjaxControlToolkit.HtmlEditor.ToolbarButtons
{
	// Token: 0x02000113 RID: 275
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.ToolbarButtons.Italic", "HtmlEditor.ToolbarButtons.Italic")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	public class Italic : EditorToggleButton
	{
		// Token: 0x06000737 RID: 1847 RVA: 0x00013DD7 File Offset: 0x00011FD7
		protected override void OnPreRender(EventArgs e)
		{
			base.RegisterButtonImages("Ed-FormatItalic");
			base.OnPreRender(e);
		}
	}
}
