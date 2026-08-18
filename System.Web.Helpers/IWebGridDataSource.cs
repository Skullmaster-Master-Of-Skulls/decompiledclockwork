using System;
using System.Collections.Generic;

namespace System.Web.Helpers
{
	// Token: 0x0200001A RID: 26
	internal interface IWebGridDataSource
	{
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600012C RID: 300
		int TotalRowCount { get; }

		// Token: 0x0600012D RID: 301
		IList<WebGridRow> GetRows(SortInfo sortInfo, int pageIndex);
	}
}
