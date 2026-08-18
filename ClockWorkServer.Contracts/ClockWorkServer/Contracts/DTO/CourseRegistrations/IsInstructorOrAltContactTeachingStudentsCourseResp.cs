using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations
{
	// Token: 0x0200082E RID: 2094
	[DataContract(Namespace = "http://tpro.ca")]
	public class IsInstructorOrAltContactTeachingStudentsCourseResp
	{
		// Token: 0x17000EEF RID: 3823
		// (get) Token: 0x06002AAB RID: 10923 RVA: 0x000143FE File Offset: 0x000125FE
		// (set) Token: 0x06002AAC RID: 10924 RVA: 0x00014406 File Offset: 0x00012606
		[DataMember]
		public bool IsTeachingStudentsCourse { get; set; }
	}
}
