using System;
using TechnoPro.Common.Public.Entities.CourseRegistrations;

namespace TechnoPro.Common.Public.Entities.StudentAccommodationRequests
{
	// Token: 0x02000199 RID: 409
	public class CourseRegistrationWithAccommodationRequest
	{
		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06000A76 RID: 2678 RVA: 0x00013862 File Offset: 0x00011A62
		// (set) Token: 0x06000A77 RID: 2679 RVA: 0x0001386A File Offset: 0x00011A6A
		public CourseRegistrationWithAccommodations CourseRegistrationWithAccommodations { get; set; }

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000A78 RID: 2680 RVA: 0x00013873 File Offset: 0x00011A73
		// (set) Token: 0x06000A79 RID: 2681 RVA: 0x0001387B File Offset: 0x00011A7B
		public StudentCourseAccommodationRequest AccommodationRequest { get; set; }
	}
}
