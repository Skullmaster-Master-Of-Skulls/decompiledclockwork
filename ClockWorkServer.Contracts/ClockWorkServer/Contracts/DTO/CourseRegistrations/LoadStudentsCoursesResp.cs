using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations
{
	// Token: 0x0200081E RID: 2078
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadStudentsCoursesResp
	{
		// Token: 0x17000ED0 RID: 3792
		// (get) Token: 0x06002A5F RID: 10847 RVA: 0x000141E9 File Offset: 0x000123E9
		// (set) Token: 0x06002A60 RID: 10848 RVA: 0x000141F1 File Offset: 0x000123F1
		[DataMember]
		public List<CourseRegistrationDTO> CourseRegistrations { get; set; }
	}
}
