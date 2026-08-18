using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.LookupCourses.Management
{
	// Token: 0x020002F2 RID: 754
	public class LookupInstructorCourseAttachmentForManagement
	{
		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x060016CF RID: 5839 RVA: 0x0001C0F7 File Offset: 0x0001A2F7
		// (set) Token: 0x060016D0 RID: 5840 RVA: 0x0001C0FF File Offset: 0x0001A2FF
		public int LuCourseId { get; set; }

		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x060016D1 RID: 5841 RVA: 0x0001C108 File Offset: 0x0001A308
		// (set) Token: 0x060016D2 RID: 5842 RVA: 0x0001C110 File Offset: 0x0001A310
		public string CourseDescription { get; set; }

		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x060016D3 RID: 5843 RVA: 0x0001C119 File Offset: 0x0001A319
		// (set) Token: 0x060016D4 RID: 5844 RVA: 0x0001C121 File Offset: 0x0001A321
		public DateTime StartDate { get; set; }

		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x060016D5 RID: 5845 RVA: 0x0001C12A File Offset: 0x0001A32A
		// (set) Token: 0x060016D6 RID: 5846 RVA: 0x0001C132 File Offset: 0x0001A332
		public DateTime EndDate { get; set; }

		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x060016D7 RID: 5847 RVA: 0x0001C13B File Offset: 0x0001A33B
		// (set) Token: 0x060016D8 RID: 5848 RVA: 0x0001C143 File Offset: 0x0001A343
		public bool IsInstructorExemptFromDataSyncAssignment { get; set; }

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x060016D9 RID: 5849 RVA: 0x0001C14C File Offset: 0x0001A34C
		// (set) Token: 0x060016DA RID: 5850 RVA: 0x0001C154 File Offset: 0x0001A354
		public IList<LookupInstructorCourseStudentAttachmentForManagement> Students { get; set; }
	}
}
