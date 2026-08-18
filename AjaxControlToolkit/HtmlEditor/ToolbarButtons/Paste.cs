using System;

namespace AjaxControlToolkit.HtmlEditor.ToolbarButtons
{
	// Token: 0x0200011B RID: 283
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.ToolbarButtons.Paste", "HtmlEditor.ToolbarButtons.Paste")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	public class Paste : MethodButton
	{
		// Token: 0x06000747 RID: 1863 RVA: 0x00013EB7 File Offset: 0x000120B7
		protected override void OnPreRender(EventArgs e)
		{
			base.RegisterButtonImages("Ed-Paste");
			base.OnPreRender(e);
		}
	}
}
