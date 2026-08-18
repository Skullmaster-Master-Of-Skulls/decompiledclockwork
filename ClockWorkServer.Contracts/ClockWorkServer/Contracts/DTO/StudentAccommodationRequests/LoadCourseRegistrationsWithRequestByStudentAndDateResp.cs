using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests
{
	// Token: 0x0200024B RID: 587
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadCourseRegistrationsWithRequestByStudentAndDateResp
	{
		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000D47 RID: 3399 RVA: 0x000061D0 File Offset: 0x000043D0
		// (set) Token: 0x06000D48 RID: 3400 RVA: 0x000061D8 File Offset: 0x000043D8
		[DataMember]
		public IList<CourseRegistrationWithAccommodationRequestDTO> CoursesWithRequests { get; set; }
	}
}
