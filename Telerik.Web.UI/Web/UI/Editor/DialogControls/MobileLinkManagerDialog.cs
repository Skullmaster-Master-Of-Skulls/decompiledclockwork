using System;
using System.ComponentModel;
using Telerik.Web.UI.Dialogs;

namespace Telerik.Web.UI.Editor.DialogControls
{
	// Token: 0x02000283 RID: 643
	[RequiredScript(typeof(jQuery))]
	[ToolboxItem(false)]
	[ClientScriptResource("Telerik.Web.UI.Dialogs.MobileLinkManager", "Telerik.Web.UI.Common.Core.js")]
	public class MobileLinkManagerDialog : MobileDialogBase, IClientParameterConsumer
	{
		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x060016FD RID: 5885 RVA: 0x0004DB8E File Offset: 0x0004BD8E
		public override string DialogName
		{
			get
			{
				return "MobileLinkManager";
			}
		}
	}
}
