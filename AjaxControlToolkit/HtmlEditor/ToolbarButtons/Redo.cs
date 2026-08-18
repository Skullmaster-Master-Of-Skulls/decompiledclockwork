using System;

namespace AjaxControlToolkit.HtmlEditor.ToolbarButtons
{
	// Token: 0x0200011F RID: 287
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.ToolbarButtons.Redo", "HtmlEditor.ToolbarButtons.Redo")]
	public class Redo : MethodButton
	{
		// Token: 0x0600074F RID: 1871 RVA: 0x00013F2E File Offset: 0x0001212E
		protected override void OnPreRender(EventArgs e)
		{
			base.RegisterButtonImages("Ed-Redo");
			base.OnPreRender(e);
		}
	}
}
