using System;

namespace AjaxControlToolkit.HtmlEditor.ToolbarButtons
{
	// Token: 0x02000126 RID: 294
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.ToolbarButtons.SubScript", "HtmlEditor.ToolbarButtons.SubScript")]
	public class SubScript : EditorToggleButton
	{
		// Token: 0x06000760 RID: 1888 RVA: 0x00014016 File Offset: 0x00012216
		protected override void OnPreRender(EventArgs e)
		{
			base.RegisterButtonImages("Ed-FormatSub");
			base.OnPreRender(e);
		}
	}
}
