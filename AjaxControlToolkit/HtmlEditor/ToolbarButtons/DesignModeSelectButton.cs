using System;

namespace AjaxControlToolkit.HtmlEditor.ToolbarButtons
{
	// Token: 0x02000103 RID: 259
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.ToolbarButtons.DesignModeSelectButton", "HtmlEditor.ToolbarButtons.DesignModeSelectButton")]
	public abstract class DesignModeSelectButton : SelectButton
	{
		// Token: 0x0600070E RID: 1806 RVA: 0x00013929 File Offset: 0x00011B29
		protected DesignModeSelectButton()
		{
			base.ActiveModes.Add(ActiveModeType.Design);
		}
	}
}
