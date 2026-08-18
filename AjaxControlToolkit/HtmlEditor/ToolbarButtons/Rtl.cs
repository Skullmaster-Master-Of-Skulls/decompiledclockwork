using System;

namespace AjaxControlToolkit.HtmlEditor.ToolbarButtons
{
	// Token: 0x02000123 RID: 291
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.ToolbarButtons.Rtl", "HtmlEditor.ToolbarButtons.Rtl")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	public class Rtl : EditorToggleButton
	{
		// Token: 0x06000757 RID: 1879 RVA: 0x00013F9E File Offset: 0x0001219E
		protected override void OnPreRender(EventArgs e)
		{
			base.RegisterButtonImages("Ed-FormatRtl");
			base.OnPreRender(e);
		}
	}
}
