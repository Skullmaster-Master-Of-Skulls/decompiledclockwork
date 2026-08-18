using System;

namespace AjaxControlToolkit.HtmlEditor.ToolbarButtons
{
	// Token: 0x02000128 RID: 296
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.ToolbarButtons.Underline", "HtmlEditor.ToolbarButtons.Underline")]
	public class Underline : EditorToggleButton
	{
		// Token: 0x06000764 RID: 1892 RVA: 0x0001404E File Offset: 0x0001224E
		protected override void OnPreRender(EventArgs e)
		{
			base.RegisterButtonImages("Ed-FormatUnderline");
			base.OnPreRender(e);
		}
	}
}
