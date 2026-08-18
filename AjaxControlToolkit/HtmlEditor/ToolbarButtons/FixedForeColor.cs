using System;

namespace AjaxControlToolkit.HtmlEditor.ToolbarButtons
{
	// Token: 0x02000106 RID: 262
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.ToolbarButtons.FixedForeColor", "HtmlEditor.ToolbarButtons.FixedForeColor")]
	public class FixedForeColor : FixedColorButton
	{
		// Token: 0x0600071D RID: 1821 RVA: 0x00013C1B File Offset: 0x00011E1B
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			base.MethodButton = new MethodButton();
			base.MethodButton.CssClass = string.Empty;
			base.DefaultColor = "#FF0000";
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x00013C4A File Offset: 0x00011E4A
		protected override void OnPreRender(EventArgs e)
		{
			base.MethodButton.InternalRegisterButtonImages("Ed-ForeColor");
			base.OnPreRender(e);
		}
	}
}
