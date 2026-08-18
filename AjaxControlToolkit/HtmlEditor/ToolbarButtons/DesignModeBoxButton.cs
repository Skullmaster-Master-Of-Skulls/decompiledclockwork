using System;

namespace AjaxControlToolkit.HtmlEditor.ToolbarButtons
{
	// Token: 0x02000100 RID: 256
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.ToolbarButtons.DesignModeBoxButton", "HtmlEditor.ToolbarButtons.DesignModeBoxButton")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	public class DesignModeBoxButton : BoxButton
	{
		// Token: 0x06000704 RID: 1796 RVA: 0x000136D4 File Offset: 0x000118D4
		public DesignModeBoxButton()
		{
			base.ActiveModes.Add(ActiveModeType.Design);
		}
	}
}
