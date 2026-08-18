using System;

namespace TechnoPro.Common.Public.Entities.MergeDuplicates.Courses
{
	// Token: 0x02000295 RID: 661
	public class DuplicateCourseClassTestDefinition : BusinessBase<int>
	{
		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x0600140A RID: 5130 RVA: 0x00019BBC File Offset: 0x00017DBC
		// (set) Token: 0x0600140B RID: 5131 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int ExamId
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x0600140C RID: 5132 RVA: 0x00019BD4 File Offset: 0x00017DD4
		// (set) Token: 0x0600140D RID: 5133 RVA: 0x00019BDC File Offset: 0x00017DDC
		public int Lucid { get; set; }

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x0600140E RID: 5134 RVA: 0x00019BE5 File Offset: 0x00017DE5
		// (set) Token: 0x0600140F RID: 5135 RVA: 0x00019BED File Offset: 0x00017DED
		public DuplicateCourseMergeResult MergeResult { get; set; }
	}
}
