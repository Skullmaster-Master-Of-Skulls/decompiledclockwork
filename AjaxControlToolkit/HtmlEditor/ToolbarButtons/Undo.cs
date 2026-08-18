using System;

namespace AjaxControlToolkit.HtmlEditor.ToolbarButtons
{
	// Token: 0x02000129 RID: 297
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.ToolbarButtons.Undo", "HtmlEditor.ToolbarButtons.Undo")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	public class Undo : MethodButton
	{
		// Token: 0x06000766 RID: 1894 RVA: 0x0001406A File Offset: 0x0001226A
		protected override void OnPreRender(EventArgs e)
		{
			base.RegisterButtonImages("Ed-Undo");
			base.OnPreRender(e);
		}
	}
}
