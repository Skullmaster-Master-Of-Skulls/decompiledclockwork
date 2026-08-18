using System;

namespace AjaxControlToolkit.HtmlEditor.ToolbarButtons
{
	// Token: 0x02000119 RID: 281
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.ToolbarButtons.OrderedList", "HtmlEditor.ToolbarButtons.OrderedList")]
	public class OrderedList : MethodButton
	{
		// Token: 0x06000743 RID: 1859 RVA: 0x00013E7F File Offset: 0x0001207F
		protected override void OnPreRender(EventArgs e)
		{
			base.RegisterButtonImages("Ed-ListNum");
			base.OnPreRender(e);
		}
	}
}
