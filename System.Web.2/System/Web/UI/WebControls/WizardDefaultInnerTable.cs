using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000517 RID: 1303
	[SupportsEventValidation]
	internal class WizardDefaultInnerTable : Table
	{
		// Token: 0x0600422E RID: 16942 RVA: 0x000D85C9 File Offset: 0x000D67C9
		internal WizardDefaultInnerTable()
		{
			base.PreventAutoID();
			this.CellPadding = 0;
			this.CellSpacing = 0;
		}
	}
}
