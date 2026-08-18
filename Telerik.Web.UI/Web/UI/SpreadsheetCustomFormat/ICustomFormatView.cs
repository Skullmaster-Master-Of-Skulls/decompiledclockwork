using System;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.SpreadsheetCustomFormat
{
	// Token: 0x020008A5 RID: 2213
	internal interface ICustomFormatView
	{
		// Token: 0x17001AEE RID: 6894
		// (get) Token: 0x0600523E RID: 21054
		CustomFormatTemplate Owner { get; }

		// Token: 0x17001AEF RID: 6895
		// (get) Token: 0x0600523F RID: 21055
		SpreadsheetStrings Localization { get; }

		// Token: 0x17001AF0 RID: 6896
		// (get) Token: 0x06005240 RID: 21056
		// (set) Token: 0x06005241 RID: 21057
		WebControl SaveButton { get; set; }

		// Token: 0x17001AF1 RID: 6897
		// (get) Token: 0x06005242 RID: 21058
		// (set) Token: 0x06005243 RID: 21059
		WebControl CancelButton { get; set; }

		// Token: 0x17001AF2 RID: 6898
		// (get) Token: 0x06005244 RID: 21060
		// (set) Token: 0x06005245 RID: 21061
		WebControl NumberFormatsListBox { get; set; }

		// Token: 0x17001AF3 RID: 6899
		// (get) Token: 0x06005246 RID: 21062
		// (set) Token: 0x06005247 RID: 21063
		WebControl CurrencyFormatsListBox { get; set; }

		// Token: 0x17001AF4 RID: 6900
		// (get) Token: 0x06005248 RID: 21064
		// (set) Token: 0x06005249 RID: 21065
		WebControl DateTimeFormatsListBox { get; set; }

		// Token: 0x0600524A RID: 21066
		void CreateControls();
	}
}
