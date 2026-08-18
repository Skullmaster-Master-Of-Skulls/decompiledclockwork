using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.CourseRegistrations;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.StudentAccommodationRequests
{
	// Token: 0x02000198 RID: 408
	public class AllowedStudentCourseRegistrationsForCustomEmailLogic
	{
		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000A6F RID: 2671 RVA: 0x0001382F File Offset: 0x00011A2F
		// (set) Token: 0x06000A70 RID: 2672 RVA: 0x00013837 File Offset: 0x00011A37
		public PersonBase Student { get; set; }

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000A71 RID: 2673 RVA: 0x00013840 File Offset: 0x00011A40
		// (set) Token: 0x06000A72 RID: 2674 RVA: 0x00013848 File Offset: 0x00011A48
		public int AuthorizedUserPersonId { get; set; }

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000A73 RID: 2675 RVA: 0x00013851 File Offset: 0x00011A51
		// (set) Token: 0x06000A74 RID: 2676 RVA: 0x00013859 File Offset: 0x00011A59
		public IList<CourseRegistration> CourseRegistrations { get; set; }
	}
}
