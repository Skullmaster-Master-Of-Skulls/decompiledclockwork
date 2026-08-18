using System;

namespace AjaxControlToolkit.HtmlEditor.ToolbarButtons
{
	// Token: 0x0200010F RID: 271
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.ToolbarButtons.IncreaseIndent", "HtmlEditor.ToolbarButtons.IncreaseIndent")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	public class IncreaseIndent : MethodButton
	{
		// Token: 0x0600072F RID: 1839 RVA: 0x00013D60 File Offset: 0x00011F60
		protected override void OnPreRender(EventArgs e)
		{
			base.RegisterButtonImages("Ed-IndentMore");
			base.OnPreRender(e);
		}
	}
}
