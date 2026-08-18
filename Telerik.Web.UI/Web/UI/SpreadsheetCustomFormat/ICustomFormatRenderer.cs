using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.SpreadsheetCustomFormat
{
	// Token: 0x020008A0 RID: 2208
	internal interface ICustomFormatRenderer
	{
		// Token: 0x17001AE4 RID: 6884
		// (get) Token: 0x0600521E RID: 21022
		ICustomFormatView View { get; }

		// Token: 0x17001AE5 RID: 6885
		// (get) Token: 0x0600521F RID: 21023
		// (set) Token: 0x06005220 RID: 21024
		Panel ButtonsPanel { get; set; }

		// Token: 0x17001AE6 RID: 6886
		// (get) Token: 0x06005221 RID: 21025
		// (set) Token: 0x06005222 RID: 21026
		RadTabStrip FormatsTabStrip { get; set; }

		// Token: 0x17001AE7 RID: 6887
		// (get) Token: 0x06005223 RID: 21027
		// (set) Token: 0x06005224 RID: 21028
		RadMultiPage FormatsMultiPage { get; set; }

		// Token: 0x06005225 RID: 21029
		void CreateLayout(Control container);

		// Token: 0x06005226 RID: 21030
		void CreateControls();
	}
}
