using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations
{
	// Token: 0x02000829 RID: 2089
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadCoursesStudentIsAllowedToBookFinalExamsForNowReq : BaseMessageReq
	{
		// Token: 0x17000EE5 RID: 3813
		// (get) Token: 0x06002A93 RID: 10899 RVA: 0x00014351 File Offset: 0x00012551
		// (set) Token: 0x06002A94 RID: 10900 RVA: 0x00014359 File Offset: 0x00012559
		[DataMember]
		public int StudentPersonId { get; set; }
	}
}
