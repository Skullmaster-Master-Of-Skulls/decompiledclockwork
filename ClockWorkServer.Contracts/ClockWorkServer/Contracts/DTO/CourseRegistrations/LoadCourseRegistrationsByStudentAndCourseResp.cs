using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations
{
	// Token: 0x0200082C RID: 2092
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadCourseRegistrationsByStudentAndCourseResp
	{
		// Token: 0x17000EEA RID: 3818
		// (get) Token: 0x06002A9F RID: 10911 RVA: 0x000143A9 File Offset: 0x000125A9
		// (set) Token: 0x06002AA0 RID: 10912 RVA: 0x000143B1 File Offset: 0x000125B1
		[DataMember]
		public CourseRegistrationDTO CourseRegistration { get; set; }
	}
}
