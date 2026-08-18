using System;

namespace AjaxControlToolkit.HtmlEditor.ToolbarButtons
{
	// Token: 0x020000FC RID: 252
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.ToolbarButtons.Cut", "HtmlEditor.ToolbarButtons.Cut")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	public class Cut : MethodButton
	{
		// Token: 0x060006FA RID: 1786 RVA: 0x0001360C File Offset: 0x0001180C
		protected override void OnPreRender(EventArgs e)
		{
			base.RegisterButtonImages("Ed-Cut");
			base.OnPreRender(e);
		}
	}
}
