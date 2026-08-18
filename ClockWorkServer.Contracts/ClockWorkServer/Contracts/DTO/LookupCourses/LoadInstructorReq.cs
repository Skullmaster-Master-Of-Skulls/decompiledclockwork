using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007EC RID: 2028
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadInstructorReq : BaseMessageReq
	{
		// Token: 0x17000E6D RID: 3693
		// (get) Token: 0x06002961 RID: 10593 RVA: 0x00013A55 File Offset: 0x00011C55
		// (set) Token: 0x06002962 RID: 10594 RVA: 0x00013A5D File Offset: 0x00011C5D
		[DataMember]
		public int InstructorId { get; set; }
	}
}
