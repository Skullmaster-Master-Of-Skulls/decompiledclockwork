using System;

namespace AjaxControlToolkit.HtmlEditor.ToolbarButtons
{
	// Token: 0x0200011A RID: 282
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.ToolbarButtons.Paragraph", "HtmlEditor.ToolbarButtons.Paragraph")]
	public class Paragraph : EditorToggleButton
	{
		// Token: 0x06000745 RID: 1861 RVA: 0x00013E9B File Offset: 0x0001209B
		protected override void OnPreRender(EventArgs e)
		{
			base.RegisterButtonImages("Ed-FormatParagraph");
			base.OnPreRender(e);
		}
	}
}
