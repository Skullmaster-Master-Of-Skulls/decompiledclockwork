using System;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.Public.Entities.AppointmentsCalendar
{
	// Token: 0x0200055B RID: 1371
	public class AppointmentTimetableItem
	{
		// Token: 0x17001282 RID: 4738
		// (get) Token: 0x06002C26 RID: 11302 RVA: 0x0003141E File Offset: 0x0002F61E
		// (set) Token: 0x06002C27 RID: 11303 RVA: 0x00031426 File Offset: 0x0002F626
		public string CourseDescription { get; set; }

		// Token: 0x17001283 RID: 4739
		// (get) Token: 0x06002C28 RID: 11304 RVA: 0x0003142F File Offset: 0x0002F62F
		// (set) Token: 0x06002C29 RID: 11305 RVA: 0x00031437 File Offset: 0x0002F637
		public int LuCourseId { get; set; }

		// Token: 0x17001284 RID: 4740
		// (get) Token: 0x06002C2A RID: 11306 RVA: 0x00031440 File Offset: 0x0002F640
		// (set) Token: 0x06002C2B RID: 11307 RVA: 0x00031448 File Offset: 0x0002F648
		public LookupTimetableItem TimetableItem { get; set; }
	}
}
