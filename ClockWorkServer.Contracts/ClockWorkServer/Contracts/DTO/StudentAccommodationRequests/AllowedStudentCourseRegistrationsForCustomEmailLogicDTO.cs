using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests
{
	// Token: 0x0200023C RID: 572
	public class AllowedStudentCourseRegistrationsForCustomEmailLogicDTO
	{
		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000CEE RID: 3310 RVA: 0x00005F4A File Offset: 0x0000414A
		// (set) Token: 0x06000CEF RID: 3311 RVA: 0x00005F52 File Offset: 0x00004152
		public PersonBaseDTO Student { get; set; }

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000CF0 RID: 3312 RVA: 0x00005F5B File Offset: 0x0000415B
		// (set) Token: 0x06000CF1 RID: 3313 RVA: 0x00005F63 File Offset: 0x00004163
		public int AuthorizedUserPersonId { get; set; }

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000CF2 RID: 3314 RVA: 0x00005F6C File Offset: 0x0000416C
		// (set) Token: 0x06000CF3 RID: 3315 RVA: 0x00005F74 File Offset: 0x00004174
		public IList<CourseRegistrationDTO> CourseRegistrations { get; set; }
	}
}
