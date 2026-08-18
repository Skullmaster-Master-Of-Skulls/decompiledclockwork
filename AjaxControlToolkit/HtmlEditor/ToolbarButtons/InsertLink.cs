using System;
using AjaxControlToolkit.HtmlEditor.Popups;

namespace AjaxControlToolkit.HtmlEditor.ToolbarButtons
{
	// Token: 0x02000112 RID: 274
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.ToolbarButtons.InsertLink", "HtmlEditor.ToolbarButtons.InsertLink")]
	public class InsertLink : OkCancelPopupButton
	{
		// Token: 0x06000734 RID: 1844 RVA: 0x00013DA0 File Offset: 0x00011FA0
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			base.RelatedPopup = new LinkProperties();
			base.AutoClose = false;
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x00013DBB File Offset: 0x00011FBB
		protected override void OnPreRender(EventArgs e)
		{
			base.RegisterButtonImages("Ed-Link");
			base.OnPreRender(e);
		}
	}
}
