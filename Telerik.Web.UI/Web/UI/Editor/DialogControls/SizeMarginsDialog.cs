using System;
using System.ComponentModel;
using Telerik.Web.UI.Dialogs;

namespace Telerik.Web.UI.Editor.DialogControls
{
	// Token: 0x0200027F RID: 639
	[ToolboxItem(false)]
	[ClientScriptResource("Telerik.Web.UI.Dialogs.SizeMargins", "Telerik.Web.UI.Common.Core.js")]
	public class SizeMarginsDialog : MobileDialogBase, IClientParameterConsumer
	{
		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x060016F5 RID: 5877 RVA: 0x0004DB52 File Offset: 0x0004BD52
		public override string DialogName
		{
			get
			{
				return "SizeMargins";
			}
		}
	}
}
