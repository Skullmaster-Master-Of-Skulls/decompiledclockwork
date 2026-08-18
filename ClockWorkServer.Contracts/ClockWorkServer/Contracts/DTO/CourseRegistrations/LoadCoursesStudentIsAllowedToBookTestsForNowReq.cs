using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations
{
	// Token: 0x02000827 RID: 2087
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadCoursesStudentIsAllowedToBookTestsForNowReq : BaseMessageReq
	{
		// Token: 0x17000EE2 RID: 3810
		// (get) Token: 0x06002A8C RID: 10892 RVA: 0x0001431B File Offset: 0x0001251B
		// (set) Token: 0x06002A8D RID: 10893 RVA: 0x00014323 File Offset: 0x00012523
		[DataMember]
		public int StudentPersonId { get; set; }
	}
}
