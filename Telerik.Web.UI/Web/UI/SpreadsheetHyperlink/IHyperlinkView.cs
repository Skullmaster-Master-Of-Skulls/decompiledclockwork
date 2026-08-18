using System;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.SpreadsheetHyperlink
{
	// Token: 0x020008BF RID: 2239
	internal interface IHyperlinkView
	{
		// Token: 0x17001B33 RID: 6963
		// (get) Token: 0x06005314 RID: 21268
		HyperlinkTemplate Owner { get; }

		// Token: 0x17001B34 RID: 6964
		// (get) Token: 0x06005315 RID: 21269
		SpreadsheetStrings Localization { get; }

		// Token: 0x17001B35 RID: 6965
		// (get) Token: 0x06005316 RID: 21270
		// (set) Token: 0x06005317 RID: 21271
		WebControl SaveButton { get; set; }

		// Token: 0x17001B36 RID: 6966
		// (get) Token: 0x06005318 RID: 21272
		// (set) Token: 0x06005319 RID: 21273
		WebControl CancelButton { get; set; }

		// Token: 0x17001B37 RID: 6967
		// (get) Token: 0x0600531A RID: 21274
		// (set) Token: 0x0600531B RID: 21275
		WebControl RemoveButton { get; set; }

		// Token: 0x17001B38 RID: 6968
		// (get) Token: 0x0600531C RID: 21276
		// (set) Token: 0x0600531D RID: 21277
		WebControl UrlTextBox { get; set; }

		// Token: 0x0600531E RID: 21278
		void CreateControls();
	}
}
