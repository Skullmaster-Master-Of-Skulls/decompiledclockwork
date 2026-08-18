using System;

namespace AjaxControlToolkit.HtmlEditor.ToolbarButtons
{
	// Token: 0x02000127 RID: 295
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.ToolbarButtons.SuperScript", "HtmlEditor.ToolbarButtons.SuperScript")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	public class SuperScript : EditorToggleButton
	{
		// Token: 0x06000762 RID: 1890 RVA: 0x00014032 File Offset: 0x00012232
		protected override void OnPreRender(EventArgs e)
		{
			base.RegisterButtonImages("Ed-FormatSup");
			base.OnPreRender(e);
		}
	}
}
