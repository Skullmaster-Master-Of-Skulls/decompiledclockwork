using System;

namespace TechnoPro.Common.Public.Entities.MergeDuplicates.Courses
{
	// Token: 0x02000299 RID: 665
	public class DuplicateCourseRegistrationInfo : BusinessBase<int>
	{
		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x06001428 RID: 5160 RVA: 0x00019CA0 File Offset: 0x00017EA0
		// (set) Token: 0x06001429 RID: 5161 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int CoursesId
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

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x0600142A RID: 5162 RVA: 0x00019CB8 File Offset: 0x00017EB8
		// (set) Token: 0x0600142B RID: 5163 RVA: 0x00019CC0 File Offset: 0x00017EC0
		public DateTime? DateLetterIssued { get; set; }

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x0600142C RID: 5164 RVA: 0x00019CC9 File Offset: 0x00017EC9
		// (set) Token: 0x0600142D RID: 5165 RVA: 0x00019CD1 File Offset: 0x00017ED1
		public int Lucid { get; set; }

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x0600142E RID: 5166 RVA: 0x00019CDA File Offset: 0x00017EDA
		// (set) Token: 0x0600142F RID: 5167 RVA: 0x00019CE2 File Offset: 0x00017EE2
		public DuplicateCourseMergeResult MergeResult { get; set; }
	}
}
