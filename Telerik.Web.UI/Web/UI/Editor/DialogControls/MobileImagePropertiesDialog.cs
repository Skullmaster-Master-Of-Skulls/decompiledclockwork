using System;
using System.ComponentModel;
using Telerik.Web.UI.Dialogs;

namespace Telerik.Web.UI.Editor.DialogControls
{
	// Token: 0x0200027E RID: 638
	[RequiredScript(typeof(jQuery))]
	[ToolboxItem(false)]
	[ClientScriptResource("Telerik.Web.UI.Dialogs.MobileImageProperties", "Telerik.Web.UI.Common.Core.js")]
	public class MobileImagePropertiesDialog : MobileDialogBase, IClientParameterConsumer
	{
		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x060016F3 RID: 5875 RVA: 0x0004DB43 File Offset: 0x0004BD43
		public override string DialogName
		{
			get
			{
				return "MobileImageProperties";
			}
		}
	}
}
