using System;
using AjaxControlToolkit.HtmlEditor.Popups;

namespace AjaxControlToolkit.HtmlEditor.ToolbarButtons
{
	// Token: 0x020000FA RID: 250
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.ToolbarButtons.ColorButton", "HtmlEditor.ToolbarButtons.ColorButton")]
	public abstract class ColorButton : DesignModePopupImageButton
	{
		// Token: 0x060006F6 RID: 1782 RVA: 0x000135D4 File Offset: 0x000117D4
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			base.RelatedPopup = new BaseColorsPopup();
		}
	}
}
