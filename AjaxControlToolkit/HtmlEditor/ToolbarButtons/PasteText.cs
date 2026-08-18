using System;

namespace AjaxControlToolkit.HtmlEditor.ToolbarButtons
{
	// Token: 0x0200011C RID: 284
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.ToolbarButtons.PasteText", "HtmlEditor.ToolbarButtons.PasteText")]
	public class PasteText : MethodButton
	{
		// Token: 0x06000749 RID: 1865 RVA: 0x00013ED3 File Offset: 0x000120D3
		protected override void OnPreRender(EventArgs e)
		{
			base.RegisterButtonImages("Ed-PasteText");
			base.OnPreRender(e);
		}
	}
}
