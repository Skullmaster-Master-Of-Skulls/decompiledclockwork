using System;

namespace AjaxControlToolkit.HtmlEditor.ToolbarButtons
{
	// Token: 0x020000EE RID: 238
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.ToolbarButtons.DesignModeImageButton", "HtmlEditor.ToolbarButtons.DesignModeImageButton")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	public abstract class DesignModeImageButton : ImageButton
	{
		// Token: 0x060006D5 RID: 1749 RVA: 0x0001329F File Offset: 0x0001149F
		protected DesignModeImageButton()
		{
			base.ActiveModes.Add(ActiveModeType.Design);
		}
	}
}
