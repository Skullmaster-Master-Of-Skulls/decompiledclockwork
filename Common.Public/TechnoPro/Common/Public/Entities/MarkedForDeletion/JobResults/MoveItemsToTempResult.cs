using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.MarkedForDeletion.JobResults
{
	// Token: 0x020002BA RID: 698
	public class MoveItemsToTempResult
	{
		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x060014FA RID: 5370 RVA: 0x0001A45F File Offset: 0x0001865F
		// (set) Token: 0x060014FB RID: 5371 RVA: 0x0001A467 File Offset: 0x00018667
		public IList<MoveItemToTempResult> Items { get; set; }

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x060014FC RID: 5372 RVA: 0x0001A470 File Offset: 0x00018670
		// (set) Token: 0x060014FD RID: 5373 RVA: 0x0001A478 File Offset: 0x00018678
		public bool WasSuccessful { get; set; }

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x060014FE RID: 5374 RVA: 0x0001A481 File Offset: 0x00018681
		// (set) Token: 0x060014FF RID: 5375 RVA: 0x0001A489 File Offset: 0x00018689
		public string ErrorMessage { get; set; }
	}
}
