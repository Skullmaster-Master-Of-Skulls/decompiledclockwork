using System;

namespace AjaxControlToolkit.HtmlEditor.ToolbarButtons
{
	// Token: 0x02000105 RID: 261
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.ToolbarButtons.FixedBackColor", "HtmlEditor.ToolbarButtons.FixedBackColor")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	public class FixedBackColor : FixedColorButton
	{
		// Token: 0x0600071A RID: 1818 RVA: 0x00013BCB File Offset: 0x00011DCB
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			base.MethodButton = new MethodButton();
			base.MethodButton.CssClass = string.Empty;
			base.DefaultColor = "#FFFF00";
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x00013BFA File Offset: 0x00011DFA
		protected override void OnPreRender(EventArgs e)
		{
			base.MethodButton.InternalRegisterButtonImages("Ed-BackColor");
			base.OnPreRender(e);
		}
	}
}
