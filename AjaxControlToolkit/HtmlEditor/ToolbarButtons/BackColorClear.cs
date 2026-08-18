using System;

namespace AjaxControlToolkit.HtmlEditor.ToolbarButtons
{
	// Token: 0x020000F0 RID: 240
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.ToolbarButtons.BackColorClear", "HtmlEditor.ToolbarButtons.BackColorClear")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	public class BackColorClear : MethodButton
	{
		// Token: 0x060006D7 RID: 1751 RVA: 0x000132BB File Offset: 0x000114BB
		protected override void OnPreRender(EventArgs e)
		{
			base.RegisterButtonImages("Ed-ColorBgClear");
			base.OnPreRender(e);
		}
	}
}
