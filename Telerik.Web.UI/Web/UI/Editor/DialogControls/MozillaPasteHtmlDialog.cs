using System;
using System.ComponentModel;
using Telerik.Web.UI.Dialogs;

namespace Telerik.Web.UI.Editor.DialogControls
{
	// Token: 0x020019E5 RID: 6629
	[ToolboxItem(false)]
	[RequiredScript(typeof(Polling))]
	[ClientScriptResource("Telerik.Web.UI.Widgets.MozillaPasteHtmlDialog", "Telerik.Web.UI.Common.Core.js")]
	public class MozillaPasteHtmlDialog : UserControlBase, IClientParameterConsumer
	{
		// Token: 0x17004D75 RID: 19829
		// (get) Token: 0x0601009B RID: 65691 RVA: 0x0039943F File Offset: 0x0039763F
		public override string DialogName
		{
			get
			{
				return "MozillaPasteHtmlDialog";
			}
		}
	}
}
