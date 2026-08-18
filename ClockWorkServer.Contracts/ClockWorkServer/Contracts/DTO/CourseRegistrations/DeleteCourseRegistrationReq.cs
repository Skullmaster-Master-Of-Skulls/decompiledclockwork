using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations
{
	// Token: 0x0200081D RID: 2077
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteCourseRegistrationReq : BaseMessageReq
	{
		// Token: 0x17000ECF RID: 3791
		// (get) Token: 0x06002A5C RID: 10844 RVA: 0x000141D8 File Offset: 0x000123D8
		// (set) Token: 0x06002A5D RID: 10845 RVA: 0x000141E0 File Offset: 0x000123E0
		[DataMember]
		public int CoursesId { get; set; }
	}
}
