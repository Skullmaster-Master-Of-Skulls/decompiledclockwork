using System;

namespace TechnoPro.Common.Public.Entities.MergeDuplicates.Courses
{
	// Token: 0x0200029B RID: 667
	public class DuplicateCourseServiceProviderRequestProvider : BusinessBase<int>
	{
		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x0600143A RID: 5178 RVA: 0x00019D38 File Offset: 0x00017F38
		// (set) Token: 0x0600143B RID: 5179 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int ServiceProviderRequestId
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

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x0600143C RID: 5180 RVA: 0x00019D50 File Offset: 0x00017F50
		// (set) Token: 0x0600143D RID: 5181 RVA: 0x00019D58 File Offset: 0x00017F58
		public int Lucid { get; set; }

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x0600143E RID: 5182 RVA: 0x00019D61 File Offset: 0x00017F61
		// (set) Token: 0x0600143F RID: 5183 RVA: 0x00019D69 File Offset: 0x00017F69
		public DuplicateCourseMergeResult MergeResult { get; set; }
	}
}
