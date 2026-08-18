using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.MarkedForDeletion.JobResults
{
	// Token: 0x020002B8 RID: 696
	public class MarkItemsForDeletionResult
	{
		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x060014F2 RID: 5362 RVA: 0x0001A42C File Offset: 0x0001862C
		// (set) Token: 0x060014F3 RID: 5363 RVA: 0x0001A434 File Offset: 0x00018634
		public IList<MarkItemForDeletionResult> Items { get; set; }

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x060014F4 RID: 5364 RVA: 0x0001A43D File Offset: 0x0001863D
		// (set) Token: 0x060014F5 RID: 5365 RVA: 0x0001A445 File Offset: 0x00018645
		public bool WasSuccessful { get; set; }

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x060014F6 RID: 5366 RVA: 0x0001A44E File Offset: 0x0001864E
		// (set) Token: 0x060014F7 RID: 5367 RVA: 0x0001A456 File Offset: 0x00018656
		public string ErrorMessage { get; set; }
	}
}
