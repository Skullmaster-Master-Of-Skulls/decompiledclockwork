using System;

namespace TechnoPro.Common.Public.Entities.MergeDuplicates.Courses
{
	// Token: 0x0200029C RID: 668
	public class DuplicateCourseServiceProviderRequestStudent : BusinessBase<int>
	{
		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x06001441 RID: 5185 RVA: 0x00019D74 File Offset: 0x00017F74
		// (set) Token: 0x06001442 RID: 5186 RVA: 0x0000E258 File Offset: 0x0000C458
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

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x06001443 RID: 5187 RVA: 0x00019D8C File Offset: 0x00017F8C
		// (set) Token: 0x06001444 RID: 5188 RVA: 0x00019D94 File Offset: 0x00017F94
		public int Lucid { get; set; }

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x06001445 RID: 5189 RVA: 0x00019D9D File Offset: 0x00017F9D
		// (set) Token: 0x06001446 RID: 5190 RVA: 0x00019DA5 File Offset: 0x00017FA5
		public DuplicateCourseMergeResult MergeResult { get; set; }
	}
}
