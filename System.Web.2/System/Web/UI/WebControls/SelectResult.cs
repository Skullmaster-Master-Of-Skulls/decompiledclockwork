using System;
using System.Collections;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004C1 RID: 1217
	public sealed class SelectResult
	{
		// Token: 0x06003CA6 RID: 15526 RVA: 0x000C47F3 File Offset: 0x000C29F3
		public SelectResult(int totalRowCount, IEnumerable results)
		{
			if (totalRowCount < 0)
			{
				throw new ArgumentOutOfRangeException("totalRowCount");
			}
			this.TotalRowCount = totalRowCount;
			this.Results = results;
		}

		// Token: 0x170011B9 RID: 4537
		// (get) Token: 0x06003CA7 RID: 15527 RVA: 0x000C4818 File Offset: 0x000C2A18
		// (set) Token: 0x06003CA8 RID: 15528 RVA: 0x000C4820 File Offset: 0x000C2A20
		public int TotalRowCount { get; private set; }

		// Token: 0x170011BA RID: 4538
		// (get) Token: 0x06003CA9 RID: 15529 RVA: 0x000C4829 File Offset: 0x000C2A29
		// (set) Token: 0x06003CAA RID: 15530 RVA: 0x000C4831 File Offset: 0x000C2A31
		public IEnumerable Results { get; private set; }
	}
}
