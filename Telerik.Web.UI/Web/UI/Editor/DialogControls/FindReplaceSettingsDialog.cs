using System;
using System.ComponentModel;
using Telerik.Web.UI.Dialogs;

namespace Telerik.Web.UI.Editor.DialogControls
{
	// Token: 0x0200027D RID: 637
	[ToolboxItem(false)]
	[ClientScriptResource("Telerik.Web.UI.Dialogs.FindReplaceSettingsDialog", "Telerik.Web.UI.Common.Core.js")]
	[RequiredScript(typeof(jQuery))]
	public class FindReplaceSettingsDialog : MobileDialogBase, IClientParameterConsumer
	{
		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x060016F1 RID: 5873 RVA: 0x0004DB34 File Offset: 0x0004BD34
		public override string DialogName
		{
			get
			{
				return "FindReplaceSettings";
			}
		}
	}
}
