using System;

namespace AjaxControlToolkit.HtmlEditor.ToolbarButtons
{
	// Token: 0x0200010A RID: 266
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.ToolbarButtons.ForeColorClear", "HtmlEditor.ToolbarButtons.ForeColorClear")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	public class ForeColorClear : MethodButton
	{
		// Token: 0x06000725 RID: 1829 RVA: 0x00013C9E File Offset: 0x00011E9E
		protected override void OnPreRender(EventArgs e)
		{
			base.RegisterButtonImages("Ed-ColorFgClear");
			base.OnPreRender(e);
		}
	}
}
