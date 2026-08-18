using System;

namespace AjaxControlToolkit.HtmlEditor.ToolbarButtons
{
	// Token: 0x020000F7 RID: 247
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.ToolbarButtons.Bold", "HtmlEditor.ToolbarButtons.Bold")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	public class Bold : EditorToggleButton
	{
		// Token: 0x060006EC RID: 1772 RVA: 0x000134E8 File Offset: 0x000116E8
		protected override void OnPreRender(EventArgs e)
		{
			base.RegisterButtonImages("Ed-FormatBold");
			base.OnPreRender(e);
		}
	}
}
