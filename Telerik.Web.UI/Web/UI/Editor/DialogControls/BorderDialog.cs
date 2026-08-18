using System;
using System.ComponentModel;
using Telerik.Web.UI.Dialogs;

namespace Telerik.Web.UI.Editor.DialogControls
{
	// Token: 0x02000280 RID: 640
	[ClientScriptResource("Telerik.Web.UI.Dialogs.Border", "Telerik.Web.UI.Common.Core.js")]
	[ToolboxItem(false)]
	[RequiredScript(typeof(jQuery))]
	public class BorderDialog : MobileDialogBase, IClientParameterConsumer
	{
		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x060016F7 RID: 5879 RVA: 0x0004DB61 File Offset: 0x0004BD61
		public override string DialogName
		{
			get
			{
				return "Border";
			}
		}
	}
}
