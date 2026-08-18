using System;
using System.ComponentModel;
using Telerik.Web.UI.Dialogs;

namespace Telerik.Web.UI.Editor.DialogControls
{
	// Token: 0x02000281 RID: 641
	[ToolboxItem(false)]
	[ClientScriptResource("Telerik.Web.UI.Dialogs.MobileTableProperties", "Telerik.Web.UI.Common.Core.js")]
	public class MobileTablePropertiesDialog : MobileDialogBase, IClientParameterConsumer
	{
		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x060016F9 RID: 5881 RVA: 0x0004DB70 File Offset: 0x0004BD70
		public override string DialogName
		{
			get
			{
				return "MobileTableProperties";
			}
		}
	}
}
