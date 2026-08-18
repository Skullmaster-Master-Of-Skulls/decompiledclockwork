using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x0200061B RID: 1563
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAccommodationsByStudentAndCourseOrTemplateReq : BaseMessageReq
	{
		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x06001FC3 RID: 8131 RVA: 0x0000E6C5 File Offset: 0x0000C8C5
		// (set) Token: 0x06001FC4 RID: 8132 RVA: 0x0000E6CD File Offset: 0x0000C8CD
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x06001FC5 RID: 8133 RVA: 0x0000E6D6 File Offset: 0x0000C8D6
		// (set) Token: 0x06001FC6 RID: 8134 RVA: 0x0000E6DE File Offset: 0x0000C8DE
		[DataMember]
		public int CourseId { get; set; }
	}
}
