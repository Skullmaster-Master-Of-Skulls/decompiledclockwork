using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007E0 RID: 2016
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadInstructorsByCourseReq : BaseMessageReq
	{
		// Token: 0x17000E5E RID: 3678
		// (get) Token: 0x06002937 RID: 10551 RVA: 0x00013956 File Offset: 0x00011B56
		// (set) Token: 0x06002938 RID: 10552 RVA: 0x0001395E File Offset: 0x00011B5E
		[DataMember]
		public int LuCourseId { get; set; }
	}
}
