using System;

namespace TechnoPro.Common.Public.Entities.MergeDuplicates.Courses
{
	// Token: 0x02000294 RID: 660
	public class DuplicateCourseAltContactAssignment
	{
		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x06001403 RID: 5123 RVA: 0x00019B89 File Offset: 0x00017D89
		// (set) Token: 0x06001404 RID: 5124 RVA: 0x00019B91 File Offset: 0x00017D91
		public int Lucid { get; set; }

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x06001405 RID: 5125 RVA: 0x00019B9A File Offset: 0x00017D9A
		// (set) Token: 0x06001406 RID: 5126 RVA: 0x00019BA2 File Offset: 0x00017DA2
		public int AlternateContactId { get; set; }

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x06001407 RID: 5127 RVA: 0x00019BAB File Offset: 0x00017DAB
		// (set) Token: 0x06001408 RID: 5128 RVA: 0x00019BB3 File Offset: 0x00017DB3
		public DuplicateCourseMergeResult MergeResult { get; set; }
	}
}
