using System;

namespace AjaxControlToolkit.HtmlEditor.ToolbarButtons
{
	// Token: 0x02000109 RID: 265
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.ToolbarButtons.ForeColor", "HtmlEditor.ToolbarButtons.ForeColor")]
	public class ForeColor : ColorButton
	{
		// Token: 0x06000723 RID: 1827 RVA: 0x00013C82 File Offset: 0x00011E82
		protected override void OnPreRender(EventArgs e)
		{
			base.RegisterButtonImages("Ed-ColorFg");
			base.OnPreRender(e);
		}
	}
}
