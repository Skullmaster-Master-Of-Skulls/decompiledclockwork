using System;
using System.ComponentModel;
using Telerik.Web.UI.Dialogs;

namespace Telerik.Web.UI.Editor.DialogControls
{
	// Token: 0x0200104D RID: 4173
	[ClientScriptResource("Telerik.Web.UI.Widgets.CellProperties", "Telerik.Web.UI.Common.Core.js")]
	[ToolboxItem(false)]
	public class CellProperties : UserControlBase
	{
		// Token: 0x170033BD RID: 13245
		// (get) Token: 0x0600A3ED RID: 41965 RVA: 0x00247014 File Offset: 0x00245214
		public override string DialogName
		{
			get
			{
				return "CellProperties";
			}
		}

		// Token: 0x0600A3EE RID: 41966 RVA: 0x0024701B File Offset: 0x0024521B
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
		}
	}
}
