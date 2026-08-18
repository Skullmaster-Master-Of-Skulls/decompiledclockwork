using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000CE8 RID: 3304
	internal class ResultItemsGeneratedEventArgs : EventArgs
	{
		// Token: 0x06007B5A RID: 31578 RVA: 0x001C525A File Offset: 0x001C345A
		internal ResultItemsGeneratedEventArgs(IEnumerable<PivotResultItem> resultItems)
		{
			this.ResultItems = resultItems;
		}

		// Token: 0x17002773 RID: 10099
		// (get) Token: 0x06007B5B RID: 31579 RVA: 0x001C5269 File Offset: 0x001C3469
		// (set) Token: 0x06007B5C RID: 31580 RVA: 0x001C5271 File Offset: 0x001C3471
		public IEnumerable<PivotResultItem> ResultItems { get; private set; }
	}
}
