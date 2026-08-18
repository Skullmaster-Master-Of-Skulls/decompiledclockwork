using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.SpreadsheetFilterMenu
{
	// Token: 0x020008AF RID: 2223
	internal interface IFilterMenuRenderer
	{
		// Token: 0x17001B03 RID: 6915
		// (get) Token: 0x06005281 RID: 21121
		IFilterMenuView View { get; }

		// Token: 0x17001B04 RID: 6916
		// (get) Token: 0x06005282 RID: 21122
		// (set) Token: 0x06005283 RID: 21123
		WebControl SortMenu { get; set; }

		// Token: 0x17001B05 RID: 6917
		// (get) Token: 0x06005284 RID: 21124
		// (set) Token: 0x06005285 RID: 21125
		Panel ButtonsPanel { get; set; }

		// Token: 0x17001B06 RID: 6918
		// (get) Token: 0x06005286 RID: 21126
		// (set) Token: 0x06005287 RID: 21127
		WebControl FilterByConditionPanel { get; set; }

		// Token: 0x17001B07 RID: 6919
		// (get) Token: 0x06005288 RID: 21128
		// (set) Token: 0x06005289 RID: 21129
		WebControl FilterByValuePanel { get; set; }

		// Token: 0x0600528A RID: 21130
		void CreateLayout(Control container);

		// Token: 0x0600528B RID: 21131
		void CreateControls();
	}
}
