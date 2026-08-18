using System;

namespace AjaxControlToolkit.HtmlEditor.ToolbarButtons
{
	// Token: 0x02000125 RID: 293
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.ToolbarButtons.StrikeThrough", "HtmlEditor.ToolbarButtons.StrikeThrough")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	public class StrikeThrough : EditorToggleButton
	{
		// Token: 0x0600075E RID: 1886 RVA: 0x00013FFA File Offset: 0x000121FA
		protected override void OnPreRender(EventArgs e)
		{
			base.RegisterButtonImages("Ed-FormatStrike");
			base.OnPreRender(e);
		}
	}
}
