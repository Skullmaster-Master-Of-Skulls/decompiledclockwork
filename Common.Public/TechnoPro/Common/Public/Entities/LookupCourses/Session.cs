using System;

namespace TechnoPro.Common.Public.Entities.LookupCourses
{
	// Token: 0x020002F0 RID: 752
	public class Session
	{
		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x060016C0 RID: 5824 RVA: 0x0001C080 File Offset: 0x0001A280
		// (set) Token: 0x060016C1 RID: 5825 RVA: 0x0001C088 File Offset: 0x0001A288
		public AcademicTerm AcademicTerm { get; set; }

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x060016C2 RID: 5826 RVA: 0x0001C091 File Offset: 0x0001A291
		// (set) Token: 0x060016C3 RID: 5827 RVA: 0x0001C099 File Offset: 0x0001A299
		public DateTime StartDate { get; set; }

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x060016C4 RID: 5828 RVA: 0x0001C0A2 File Offset: 0x0001A2A2
		// (set) Token: 0x060016C5 RID: 5829 RVA: 0x0001C0AA File Offset: 0x0001A2AA
		public DateTime EndDate { get; set; }
	}
}
