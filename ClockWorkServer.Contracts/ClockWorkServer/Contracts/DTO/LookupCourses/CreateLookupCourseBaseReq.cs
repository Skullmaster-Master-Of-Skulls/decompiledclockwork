using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007A5 RID: 1957
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateLookupCourseBaseReq : BaseMessageReq
	{
		// Token: 0x17000E07 RID: 3591
		// (get) Token: 0x06002848 RID: 10312 RVA: 0x00013218 File Offset: 0x00011418
		// (set) Token: 0x06002849 RID: 10313 RVA: 0x00013220 File Offset: 0x00011420
		[DataMember]
		public LookupCourseBaseDTO CourseBase { get; set; }
	}
}
