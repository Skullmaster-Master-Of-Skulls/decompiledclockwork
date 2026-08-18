using System;
using System.Collections.Generic;
using System.Globalization;
using Telerik.Web.Spreadsheet;

namespace Telerik.Web.UI
{
	// Token: 0x020008C4 RID: 2244
	public interface ISpreadsheet
	{
		// Token: 0x17001B40 RID: 6976
		// (get) Token: 0x06005338 RID: 21304
		List<Worksheet> Sheets { get; }

		// Token: 0x17001B41 RID: 6977
		// (get) Token: 0x06005339 RID: 21305
		RenderMode ResolvedRenderMode { get; }

		// Token: 0x17001B42 RID: 6978
		// (get) Token: 0x0600533A RID: 21306
		SpreadsheetStrings Localization { get; }

		// Token: 0x17001B43 RID: 6979
		// (get) Token: 0x0600533B RID: 21307
		// (set) Token: 0x0600533C RID: 21308
		CultureInfo Culture { get; set; }

		// Token: 0x17001B44 RID: 6980
		// (get) Token: 0x0600533D RID: 21309
		string ResolvedSkin { get; }

		// Token: 0x17001B45 RID: 6981
		// (get) Token: 0x0600533E RID: 21310
		bool EnableEmbeddedSkins { get; }
	}
}
