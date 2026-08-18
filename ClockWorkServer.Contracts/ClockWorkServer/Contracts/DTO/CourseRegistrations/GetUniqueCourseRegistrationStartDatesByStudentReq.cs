using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations
{
	// Token: 0x02000824 RID: 2084
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetUniqueCourseRegistrationStartDatesByStudentReq : BaseMessageReq
	{
		// Token: 0x17000EDC RID: 3804
		// (get) Token: 0x06002A7D RID: 10877 RVA: 0x000142B5 File Offset: 0x000124B5
		// (set) Token: 0x06002A7E RID: 10878 RVA: 0x000142BD File Offset: 0x000124BD
		[DataMember]
		public int StudentPid { get; set; }
	}
}
