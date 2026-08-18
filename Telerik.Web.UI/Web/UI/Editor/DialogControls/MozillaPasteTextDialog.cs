using System;
using System.ComponentModel;
using Telerik.Web.UI.Dialogs;

namespace Telerik.Web.UI.Editor.DialogControls
{
	// Token: 0x020019E4 RID: 6628
	[ClientScriptResource("Telerik.Web.UI.Widgets.MozillaPasteTextDialog", "Telerik.Web.UI.Common.Core.js")]
	[ToolboxItem(false)]
	public class MozillaPasteTextDialog : UserControlBase, IClientParameterConsumer
	{
		// Token: 0x17004D74 RID: 19828
		// (get) Token: 0x06010099 RID: 65689 RVA: 0x00399430 File Offset: 0x00397630
		public override string DialogName
		{
			get
			{
				return "MozillaPasteTextDialog";
			}
		}
	}
}
