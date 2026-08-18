using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.LookupCourses.Management
{
	// Token: 0x020002F4 RID: 756
	public class LookupInstructorForManagement
	{
		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x060016E5 RID: 5861 RVA: 0x0001C1A1 File Offset: 0x0001A3A1
		// (set) Token: 0x060016E6 RID: 5862 RVA: 0x0001C1A9 File Offset: 0x0001A3A9
		public LookupInstructor Instructor { get; set; }

		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x060016E7 RID: 5863 RVA: 0x0001C1B2 File Offset: 0x0001A3B2
		// (set) Token: 0x060016E8 RID: 5864 RVA: 0x0001C1BA File Offset: 0x0001A3BA
		public IList<LookupInstructorCourseAttachmentForManagement> AttachedCourses { get; set; }
	}
}
