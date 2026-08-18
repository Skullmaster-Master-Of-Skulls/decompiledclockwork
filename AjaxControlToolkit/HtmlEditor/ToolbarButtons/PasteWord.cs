using System;

namespace AjaxControlToolkit.HtmlEditor.ToolbarButtons
{
	// Token: 0x0200011D RID: 285
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.ToolbarButtons.PasteWord", "HtmlEditor.ToolbarButtons.PasteWord")]
	public class PasteWord : MethodButton
	{
		// Token: 0x0600074B RID: 1867 RVA: 0x00013EEF File Offset: 0x000120EF
		protected override void OnPreRender(EventArgs e)
		{
			base.RegisterButtonImages("Ed-PasteWord");
			base.OnPreRender(e);
		}
	}
}
