using System;

namespace TechnoPro.Common.Public.Entities.LookupCourses
{
	// Token: 0x020002E5 RID: 741
	public class LookupCourseBaseWithPrimaryInstructor : LookupCourseBase
	{
		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x0600162D RID: 5677 RVA: 0x0001B986 File Offset: 0x00019B86
		// (set) Token: 0x0600162E RID: 5678 RVA: 0x0001B98E File Offset: 0x00019B8E
		public LookupInstructor PrimaryInstructor { get; set; }

		// Token: 0x0600162F RID: 5679 RVA: 0x0001B997 File Offset: 0x00019B97
		public LookupCourseBaseWithPrimaryInstructor()
		{
		}

		// Token: 0x06001630 RID: 5680 RVA: 0x0001B9A1 File Offset: 0x00019BA1
		public LookupCourseBaseWithPrimaryInstructor(LookupCourseBase item) : base(item)
		{
		}
	}
}
