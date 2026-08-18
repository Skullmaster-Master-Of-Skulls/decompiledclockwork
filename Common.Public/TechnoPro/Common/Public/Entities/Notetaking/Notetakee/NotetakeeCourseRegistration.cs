using System;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.Notetaking.Notetakee.Info;
using TechnoPro.Common.Public.Entities.Notetaking.Notetakee.Status;

namespace TechnoPro.Common.Public.Entities.Notetaking.Notetakee
{
	// Token: 0x02000284 RID: 644
	public class NotetakeeCourseRegistration
	{
		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x0600138C RID: 5004 RVA: 0x000197FA File Offset: 0x000179FA
		// (set) Token: 0x0600138D RID: 5005 RVA: 0x00019802 File Offset: 0x00017A02
		public LookupCourseBase CourseBase { get; set; }

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x0600138E RID: 5006 RVA: 0x0001980B File Offset: 0x00017A0B
		// (set) Token: 0x0600138F RID: 5007 RVA: 0x00019813 File Offset: 0x00017A13
		public NotetakeeCourseRegistrationStudentCourseInfo CourseInfo { get; set; }

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x06001390 RID: 5008 RVA: 0x0001981C File Offset: 0x00017A1C
		// (set) Token: 0x06001391 RID: 5009 RVA: 0x00019824 File Offset: 0x00017A24
		public NotetakeeCourseRegistrationStudentCourseStatus CourseStatus { get; set; }
	}
}
