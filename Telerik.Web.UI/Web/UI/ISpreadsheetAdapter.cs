using System;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020008C5 RID: 2245
	public interface ISpreadsheetAdapter
	{
		// Token: 0x0600533F RID: 21311
		WebControl CreateToolbar(SpreadsheetToolbar toolbar);

		// Token: 0x17001B46 RID: 6982
		// (get) Token: 0x06005340 RID: 21312
		// (set) Token: 0x06005341 RID: 21313
		ISpreadsheet Owner { get; set; }
	}
}
