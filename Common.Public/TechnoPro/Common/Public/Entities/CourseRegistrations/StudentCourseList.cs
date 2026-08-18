using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.CourseRegistrations
{
	// Token: 0x0200043A RID: 1082
	public class StudentCourseList
	{
		// Token: 0x17000D81 RID: 3457
		// (get) Token: 0x060020C2 RID: 8386 RVA: 0x00024DBA File Offset: 0x00022FBA
		// (set) Token: 0x060020C3 RID: 8387 RVA: 0x00024DC2 File Offset: 0x00022FC2
		public IList<CourseRegistration> Courses { get; set; }

		// Token: 0x17000D82 RID: 3458
		// (get) Token: 0x060020C4 RID: 8388 RVA: 0x00024DCB File Offset: 0x00022FCB
		// (set) Token: 0x060020C5 RID: 8389 RVA: 0x00024DD3 File Offset: 0x00022FD3
		public bool AtLeastOneCourseRemovedBecauseOfSpecialAccommodationNotAllowedToBookRestriction { get; set; }
	}
}
