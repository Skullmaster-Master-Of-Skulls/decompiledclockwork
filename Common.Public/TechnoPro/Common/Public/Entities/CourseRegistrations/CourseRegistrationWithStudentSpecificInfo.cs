using System;

namespace TechnoPro.Common.Public.Entities.CourseRegistrations
{
	// Token: 0x02000438 RID: 1080
	public class CourseRegistrationWithStudentSpecificInfo : CourseRegistration
	{
		// Token: 0x17000D79 RID: 3449
		// (get) Token: 0x060020B0 RID: 8368 RVA: 0x00024D29 File Offset: 0x00022F29
		// (set) Token: 0x060020B1 RID: 8369 RVA: 0x00024D31 File Offset: 0x00022F31
		public CourseStudentSpecific StudentSpecificInfo { get; set; }
	}
}
