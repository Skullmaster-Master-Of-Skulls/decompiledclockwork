using System;

namespace TechnoPro.Common.Public.Entities.MergeDuplicates.Courses
{
	// Token: 0x0200029A RID: 666
	public class DuplicateCourseServiceProviderCourseAssignment : BusinessBase<int>
	{
		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x06001431 RID: 5169 RVA: 0x00019CEC File Offset: 0x00017EEC
		// (set) Token: 0x06001432 RID: 5170 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int ServiceProviderApplicationCourseId
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

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x06001433 RID: 5171 RVA: 0x00019D04 File Offset: 0x00017F04
		// (set) Token: 0x06001434 RID: 5172 RVA: 0x00019D0C File Offset: 0x00017F0C
		public int ServiceProviderId { get; set; }

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x06001435 RID: 5173 RVA: 0x00019D15 File Offset: 0x00017F15
		// (set) Token: 0x06001436 RID: 5174 RVA: 0x00019D1D File Offset: 0x00017F1D
		public int Lucid { get; set; }

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x06001437 RID: 5175 RVA: 0x00019D26 File Offset: 0x00017F26
		// (set) Token: 0x06001438 RID: 5176 RVA: 0x00019D2E File Offset: 0x00017F2E
		public DuplicateCourseMergeResult MergeResult { get; set; }
	}
}
