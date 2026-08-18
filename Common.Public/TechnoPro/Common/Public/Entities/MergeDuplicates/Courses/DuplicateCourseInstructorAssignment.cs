using System;

namespace TechnoPro.Common.Public.Entities.MergeDuplicates.Courses
{
	// Token: 0x02000296 RID: 662
	public class DuplicateCourseInstructorAssignment
	{
		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x06001411 RID: 5137 RVA: 0x00019BF6 File Offset: 0x00017DF6
		// (set) Token: 0x06001412 RID: 5138 RVA: 0x00019BFE File Offset: 0x00017DFE
		public int Lucid { get; set; }

		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x06001413 RID: 5139 RVA: 0x00019C07 File Offset: 0x00017E07
		// (set) Token: 0x06001414 RID: 5140 RVA: 0x00019C0F File Offset: 0x00017E0F
		public int InstructorId { get; set; }

		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x06001415 RID: 5141 RVA: 0x00019C18 File Offset: 0x00017E18
		// (set) Token: 0x06001416 RID: 5142 RVA: 0x00019C20 File Offset: 0x00017E20
		public DuplicateCourseMergeResult MergeResult { get; set; }
	}
}
