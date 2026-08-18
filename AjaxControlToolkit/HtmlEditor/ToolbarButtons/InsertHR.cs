using System;

namespace AjaxControlToolkit.HtmlEditor.ToolbarButtons
{
	// Token: 0x02000110 RID: 272
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.ToolbarButtons.InsertHR", "HtmlEditor.ToolbarButtons.InsertHR")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	public class InsertHR : MethodButton
	{
		// Token: 0x06000731 RID: 1841 RVA: 0x00013D7C File Offset: 0x00011F7C
		protected override void OnPreRender(EventArgs e)
		{
			base.RegisterButtonImages("Ed-Rule");
			base.OnPreRender(e);
		}
	}
}
