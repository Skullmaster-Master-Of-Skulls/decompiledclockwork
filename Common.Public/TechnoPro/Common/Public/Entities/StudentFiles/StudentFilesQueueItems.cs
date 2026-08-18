using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.StudentFiles
{
	// Token: 0x0200018E RID: 398
	public class StudentFilesQueueItems
	{
		// Token: 0x170003BF RID: 959
		// (get) Token: 0x060009FD RID: 2557 RVA: 0x00013211 File Offset: 0x00011411
		// (set) Token: 0x060009FE RID: 2558 RVA: 0x00013219 File Offset: 0x00011419
		public IList<StudentFilesQueueStudentItem> StudentItems { get; set; }

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x060009FF RID: 2559 RVA: 0x00013222 File Offset: 0x00011422
		// (set) Token: 0x06000A00 RID: 2560 RVA: 0x0001322A File Offset: 0x0001142A
		public IList<StudentFilesLookupStatus> LookupStatuses { get; set; }
	}
}
