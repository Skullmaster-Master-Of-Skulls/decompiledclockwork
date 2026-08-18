using System;

namespace TechnoPro.Common.Public.Entities.StudentFiles
{
	// Token: 0x0200018C RID: 396
	public class StudentFilesLookupStatus
	{
		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x060009E6 RID: 2534 RVA: 0x0001312D File Offset: 0x0001132D
		// (set) Token: 0x060009E7 RID: 2535 RVA: 0x00013135 File Offset: 0x00011335
		public string Title { get; set; }

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x060009E8 RID: 2536 RVA: 0x0001313E File Offset: 0x0001133E
		// (set) Token: 0x060009E9 RID: 2537 RVA: 0x00013146 File Offset: 0x00011346
		public eStudentFileStatusType StatusType { get; set; }
	}
}
