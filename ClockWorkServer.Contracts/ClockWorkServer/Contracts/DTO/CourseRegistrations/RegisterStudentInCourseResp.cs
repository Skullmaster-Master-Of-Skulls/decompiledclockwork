using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations
{
	// Token: 0x02000821 RID: 2081
	[DataContract(Namespace = "http://tpro.ca")]
	public class RegisterStudentInCourseResp
	{
		// Token: 0x17000ED7 RID: 3799
		// (get) Token: 0x06002A70 RID: 10864 RVA: 0x00014260 File Offset: 0x00012460
		// (set) Token: 0x06002A71 RID: 10865 RVA: 0x00014268 File Offset: 0x00012468
		[DataMember]
		public CourseRegistrationDTO CourseRegistration { get; set; }
	}
}
