using System;
using System.ComponentModel;

namespace AjaxControlToolkit.HtmlEditor.ToolbarButtons
{
	// Token: 0x02000108 RID: 264
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.ToolbarButtons.FontSize", "HtmlEditor.ToolbarButtons.FontSize")]
	public class FontSize : DesignModeSelectButton
	{
		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000721 RID: 1825 RVA: 0x00013C73 File Offset: 0x00011E73
		[Category("Appearance")]
		[DefaultValue("70px")]
		public override string SelectWidth
		{
			get
			{
				return "70px";
			}
		}
	}
}
