using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests
{
	// Token: 0x02000244 RID: 580
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetCoursesAllowedBySelfRegCustomLogicRulesToViewLoaForResp
	{
		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000D26 RID: 3366 RVA: 0x000060F3 File Offset: 0x000042F3
		// (set) Token: 0x06000D27 RID: 3367 RVA: 0x000060FB File Offset: 0x000042FB
		[DataMember]
		public AllowedStudentCourseRegistrationsForCustomEmailLogicDTO AllowedCourseRegistrations { get; set; }
	}
}
