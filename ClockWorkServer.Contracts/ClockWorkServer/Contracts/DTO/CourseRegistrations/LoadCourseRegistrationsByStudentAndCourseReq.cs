using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations
{
	// Token: 0x0200082B RID: 2091
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadCourseRegistrationsByStudentAndCourseReq : BaseMessageReq
	{
		// Token: 0x17000EE8 RID: 3816
		// (get) Token: 0x06002A9A RID: 10906 RVA: 0x00014387 File Offset: 0x00012587
		// (set) Token: 0x06002A9B RID: 10907 RVA: 0x0001438F File Offset: 0x0001258F
		[DataMember]
		public int StudentPersonId { get; set; }

		// Token: 0x17000EE9 RID: 3817
		// (get) Token: 0x06002A9C RID: 10908 RVA: 0x00014398 File Offset: 0x00012598
		// (set) Token: 0x06002A9D RID: 10909 RVA: 0x000143A0 File Offset: 0x000125A0
		[DataMember]
		public int LuCourseId { get; set; }
	}
}
