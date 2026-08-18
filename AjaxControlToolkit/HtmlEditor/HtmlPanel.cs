using System;
using System.Web.UI;

namespace AjaxControlToolkit.HtmlEditor
{
	// Token: 0x020000DD RID: 221
	[ClientCssResource("HtmlEditor.HtmlPanel")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.HtmlPanel", "HtmlEditor.HtmlPanel")]
	internal class HtmlPanel : ModePanel
	{
		// Token: 0x06000656 RID: 1622 RVA: 0x00010D12 File Offset: 0x0000EF12
		public HtmlPanel() : base(HtmlTextWriterTag.Textarea)
		{
		}
	}
}
