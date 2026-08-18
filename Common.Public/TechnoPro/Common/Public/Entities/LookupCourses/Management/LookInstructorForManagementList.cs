using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.LookupCourses.Management
{
	// Token: 0x020002F1 RID: 753
	public class LookInstructorForManagementList
	{
		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x060016C6 RID: 5830 RVA: 0x0001C0B3 File Offset: 0x0001A2B3
		// (set) Token: 0x060016C7 RID: 5831 RVA: 0x0001C0BB File Offset: 0x0001A2BB
		public IList<LookupInstructorForManagement> Instructors { get; set; }

		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x060016C8 RID: 5832 RVA: 0x0001C0C4 File Offset: 0x0001A2C4
		// (set) Token: 0x060016C9 RID: 5833 RVA: 0x0001C0CC File Offset: 0x0001A2CC
		public int StartIndex { get; set; }

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x060016CA RID: 5834 RVA: 0x0001C0D5 File Offset: 0x0001A2D5
		// (set) Token: 0x060016CB RID: 5835 RVA: 0x0001C0DD File Offset: 0x0001A2DD
		public int Count { get; set; }

		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x060016CC RID: 5836 RVA: 0x0001C0E6 File Offset: 0x0001A2E6
		// (set) Token: 0x060016CD RID: 5837 RVA: 0x0001C0EE File Offset: 0x0001A2EE
		public int TotalCount { get; set; }
	}
}
