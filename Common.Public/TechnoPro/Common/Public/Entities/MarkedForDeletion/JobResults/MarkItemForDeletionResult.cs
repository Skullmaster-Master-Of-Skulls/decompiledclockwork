using System;

namespace TechnoPro.Common.Public.Entities.MarkedForDeletion.JobResults
{
	// Token: 0x020002B7 RID: 695
	public class MarkItemForDeletionResult
	{
		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x060014ED RID: 5357 RVA: 0x0001A40A File Offset: 0x0001860A
		// (set) Token: 0x060014EE RID: 5358 RVA: 0x0001A412 File Offset: 0x00018612
		public string MarkedForDeletionItemId { get; set; }

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x060014EF RID: 5359 RVA: 0x0001A41B File Offset: 0x0001861B
		// (set) Token: 0x060014F0 RID: 5360 RVA: 0x0001A423 File Offset: 0x00018623
		public eMarkItemForDeletionActionType ActionType { get; set; }
	}
}
