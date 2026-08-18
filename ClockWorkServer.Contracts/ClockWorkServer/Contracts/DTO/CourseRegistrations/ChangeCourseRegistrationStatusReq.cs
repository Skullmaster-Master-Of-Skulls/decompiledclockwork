using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations
{
	// Token: 0x02000820 RID: 2080
	[DataContract(Namespace = "http://tpro.ca")]
	public class ChangeCourseRegistrationStatusReq : BaseMessageReq
	{
		// Token: 0x17000ED5 RID: 3797
		// (get) Token: 0x06002A6B RID: 10859 RVA: 0x0001423E File Offset: 0x0001243E
		// (set) Token: 0x06002A6C RID: 10860 RVA: 0x00014246 File Offset: 0x00012446
		[DataMember]
		public int CoursesId { get; set; }

		// Token: 0x17000ED6 RID: 3798
		// (get) Token: 0x06002A6D RID: 10861 RVA: 0x0001424F File Offset: 0x0001244F
		// (set) Token: 0x06002A6E RID: 10862 RVA: 0x00014257 File Offset: 0x00012457
		[DataMember]
		public eRegistrationStatusDTO NewRegistrationStatus { get; set; }
	}
}
