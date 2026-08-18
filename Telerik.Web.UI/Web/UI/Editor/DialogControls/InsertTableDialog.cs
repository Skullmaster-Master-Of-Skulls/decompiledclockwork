using System;
using System.ComponentModel;
using Telerik.Web.UI.Dialogs;

namespace Telerik.Web.UI.Editor.DialogControls
{
	// Token: 0x02000282 RID: 642
	[ClientScriptResource("Telerik.Web.UI.Dialogs.InsertTable", "Telerik.Web.UI.Common.Core.js")]
	[ToolboxItem(false)]
	public class InsertTableDialog : MobileDialogBase, IClientParameterConsumer
	{
		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x060016FB RID: 5883 RVA: 0x0004DB7F File Offset: 0x0004BD7F
		public override string DialogName
		{
			get
			{
				return "InsertTable";
			}
		}
	}
}
