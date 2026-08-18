using System;

namespace AjaxControlToolkit.HtmlEditor.ToolbarButtons
{
	// Token: 0x02000120 RID: 288
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.ToolbarButtons.RemoveAlignment", "HtmlEditor.ToolbarButtons.RemoveAlignment")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	public class RemoveAlignment : EditorToggleButton
	{
		// Token: 0x06000751 RID: 1873 RVA: 0x00013F4A File Offset: 0x0001214A
		protected override void OnPreRender(EventArgs e)
		{
			base.RegisterButtonImages("Ed-AlignRemove");
			base.OnPreRender(e);
		}
	}
}
