using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000686 RID: 1670
	[SupportsEventValidation]
	internal class WizardDefaultInnerTable : Table
	{
		// Token: 0x06005200 RID: 20992 RVA: 0x0014B96E File Offset: 0x0014A96E
		internal WizardDefaultInnerTable()
		{
			base.PreventAutoID();
			this.CellPadding = 0;
			this.CellSpacing = 0;
		}
	}
}
