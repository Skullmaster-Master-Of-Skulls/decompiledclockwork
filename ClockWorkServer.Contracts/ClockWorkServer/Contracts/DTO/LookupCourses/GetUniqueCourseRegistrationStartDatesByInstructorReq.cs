using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007F6 RID: 2038
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetUniqueCourseRegistrationStartDatesByInstructorReq : BaseMessageReq
	{
		// Token: 0x17000E7D RID: 3709
		// (get) Token: 0x0600298B RID: 10635 RVA: 0x00013B65 File Offset: 0x00011D65
		// (set) Token: 0x0600298C RID: 10636 RVA: 0x00013B6D File Offset: 0x00011D6D
		[DataMember]
		public int InstructorId { get; set; }
	}
}
