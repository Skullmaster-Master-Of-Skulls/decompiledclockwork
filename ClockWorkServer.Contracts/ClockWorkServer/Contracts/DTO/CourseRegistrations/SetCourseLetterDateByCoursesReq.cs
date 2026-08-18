using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations
{
	// Token: 0x02000826 RID: 2086
	[DataContract(Namespace = "http://tpro.ca")]
	public class SetCourseLetterDateByCoursesReq : BaseMessageReq
	{
		// Token: 0x17000EE0 RID: 3808
		// (get) Token: 0x06002A87 RID: 10887 RVA: 0x000142F9 File Offset: 0x000124F9
		// (set) Token: 0x06002A88 RID: 10888 RVA: 0x00014301 File Offset: 0x00012501
		[DataMember]
		public int CoursesId { get; set; }

		// Token: 0x17000EE1 RID: 3809
		// (get) Token: 0x06002A89 RID: 10889 RVA: 0x0001430A File Offset: 0x0001250A
		// (set) Token: 0x06002A8A RID: 10890 RVA: 0x00014312 File Offset: 0x00012512
		[DataMember]
		public DateTime? Date { get; set; }
	}
}
