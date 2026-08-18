using System;

namespace AjaxControlToolkit.HtmlEditor.ToolbarButtons
{
	// Token: 0x020000FD RID: 253
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.ToolbarButtons.DecreaseIndent", "HtmlEditor.ToolbarButtons.DecreaseIndent")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	public class DecreaseIndent : MethodButton
	{
		// Token: 0x060006FC RID: 1788 RVA: 0x00013628 File Offset: 0x00011828
		protected override void OnPreRender(EventArgs e)
		{
			base.RegisterButtonImages("Ed-IndentLess");
			base.OnPreRender(e);
		}
	}
}
