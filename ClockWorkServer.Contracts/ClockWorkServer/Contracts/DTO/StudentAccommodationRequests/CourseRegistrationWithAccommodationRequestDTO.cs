using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests
{
	// Token: 0x0200023D RID: 573
	[DataContract(Namespace = "http://tpro.ca")]
	public class CourseRegistrationWithAccommodationRequestDTO
	{
		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000CF5 RID: 3317 RVA: 0x00005F7D File Offset: 0x0000417D
		// (set) Token: 0x06000CF6 RID: 3318 RVA: 0x00005F85 File Offset: 0x00004185
		[DataMember]
		public CourseRegistrationWithAccommodationsDTO CourseRegistrationWithAccommodations { get; set; }

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000CF7 RID: 3319 RVA: 0x00005F8E File Offset: 0x0000418E
		// (set) Token: 0x06000CF8 RID: 3320 RVA: 0x00005F96 File Offset: 0x00004196
		[DataMember]
		public StudentCourseAccommodationRequestDTO AccommodationRequest { get; set; }
	}
}
