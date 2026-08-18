using System;

namespace TechnoPro.Common.Public.Entities.LookupCourses
{
	// Token: 0x020002E6 RID: 742
	public class LookupCourseDateRange
	{
		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x06001631 RID: 5681 RVA: 0x0001B9AC File Offset: 0x00019BAC
		// (set) Token: 0x06001632 RID: 5682 RVA: 0x0001B9B4 File Offset: 0x00019BB4
		public DateTime StartDate { get; set; }

		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x06001633 RID: 5683 RVA: 0x0001B9BD File Offset: 0x00019BBD
		// (set) Token: 0x06001634 RID: 5684 RVA: 0x0001B9C5 File Offset: 0x00019BC5
		public DateTime EndDate { get; set; }

		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x06001635 RID: 5685 RVA: 0x0001B9CE File Offset: 0x00019BCE
		// (set) Token: 0x06001636 RID: 5686 RVA: 0x0001B9D6 File Offset: 0x00019BD6
		public int CourseCount { get; set; }
	}
}
