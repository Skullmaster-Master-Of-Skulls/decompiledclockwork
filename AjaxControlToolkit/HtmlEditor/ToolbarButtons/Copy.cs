using System;

namespace AjaxControlToolkit.HtmlEditor.ToolbarButtons
{
	// Token: 0x020000FB RID: 251
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.ToolbarButtons.Copy", "HtmlEditor.ToolbarButtons.Copy")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	public class Copy : MethodButton
	{
		// Token: 0x060006F8 RID: 1784 RVA: 0x000135F0 File Offset: 0x000117F0
		protected override void OnPreRender(EventArgs e)
		{
			base.RegisterButtonImages("Ed-Copy");
			base.OnPreRender(e);
		}
	}
}
