using System;

namespace AjaxControlToolkit.HtmlEditor.ToolbarButtons
{
	// Token: 0x02000122 RID: 290
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.ToolbarButtons.RemoveStyles", "HtmlEditor.ToolbarButtons.RemoveStyles")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	public class RemoveStyles : MethodButton
	{
		// Token: 0x06000755 RID: 1877 RVA: 0x00013F82 File Offset: 0x00012182
		protected override void OnPreRender(EventArgs e)
		{
			base.RegisterButtonImages("Ed-Unformat");
			base.OnPreRender(e);
		}
	}
}
