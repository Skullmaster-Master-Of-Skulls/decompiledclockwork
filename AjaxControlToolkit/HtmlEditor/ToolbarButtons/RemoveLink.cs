using System;

namespace AjaxControlToolkit.HtmlEditor.ToolbarButtons
{
	// Token: 0x02000121 RID: 289
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.ToolbarButtons.RemoveLink", "HtmlEditor.ToolbarButtons.RemoveLink")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	public class RemoveLink : MethodButton
	{
		// Token: 0x06000753 RID: 1875 RVA: 0x00013F66 File Offset: 0x00012166
		protected override void OnPreRender(EventArgs e)
		{
			base.RegisterButtonImages("Ed-Unlink");
			base.OnPreRender(e);
		}
	}
}
