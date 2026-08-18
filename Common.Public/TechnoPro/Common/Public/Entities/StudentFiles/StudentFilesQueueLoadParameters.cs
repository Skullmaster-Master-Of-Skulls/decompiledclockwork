using System;

namespace TechnoPro.Common.Public.Entities.StudentFiles
{
	// Token: 0x0200018F RID: 399
	public class StudentFilesQueueLoadParameters
	{
		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000A02 RID: 2562 RVA: 0x00013233 File Offset: 0x00011433
		// (set) Token: 0x06000A03 RID: 2563 RVA: 0x0001323B File Offset: 0x0001143B
		public DateTime StartDate { get; set; }

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000A04 RID: 2564 RVA: 0x00013244 File Offset: 0x00011444
		// (set) Token: 0x06000A05 RID: 2565 RVA: 0x0001324C File Offset: 0x0001144C
		public bool ExcludeItemsWithClosedStatuses { get; set; }
	}
}
