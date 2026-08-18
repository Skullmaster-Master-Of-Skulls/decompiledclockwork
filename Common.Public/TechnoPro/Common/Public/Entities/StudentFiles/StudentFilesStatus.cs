using System;

namespace TechnoPro.Common.Public.Entities.StudentFiles
{
	// Token: 0x02000191 RID: 401
	public class StudentFilesStatus
	{
		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000A1E RID: 2590 RVA: 0x00013310 File Offset: 0x00011510
		// (set) Token: 0x06000A1F RID: 2591 RVA: 0x00013318 File Offset: 0x00011518
		public string Title { get; set; }

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000A20 RID: 2592 RVA: 0x00013321 File Offset: 0x00011521
		// (set) Token: 0x06000A21 RID: 2593 RVA: 0x00013329 File Offset: 0x00011529
		public eStudentFileStatusType StatusType { get; set; }
	}
}
