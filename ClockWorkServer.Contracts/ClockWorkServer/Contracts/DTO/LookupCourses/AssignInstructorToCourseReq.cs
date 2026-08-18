using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007E4 RID: 2020
	[DataContract(Namespace = "http://tpro.ca")]
	public class AssignInstructorToCourseReq : BaseMessageReq
	{
		// Token: 0x17000E62 RID: 3682
		// (get) Token: 0x06002943 RID: 10563 RVA: 0x0001399A File Offset: 0x00011B9A
		// (set) Token: 0x06002944 RID: 10564 RVA: 0x000139A2 File Offset: 0x00011BA2
		[DataMember]
		public int InstructorId { get; set; }

		// Token: 0x17000E63 RID: 3683
		// (get) Token: 0x06002945 RID: 10565 RVA: 0x000139AB File Offset: 0x00011BAB
		// (set) Token: 0x06002946 RID: 10566 RVA: 0x000139B3 File Offset: 0x00011BB3
		[DataMember]
		public int LuCourseId { get; set; }

		// Token: 0x17000E64 RID: 3684
		// (get) Token: 0x06002947 RID: 10567 RVA: 0x000139BC File Offset: 0x00011BBC
		// (set) Token: 0x06002948 RID: 10568 RVA: 0x000139C4 File Offset: 0x00011BC4
		[DataMember]
		public bool? IsAssignmentExemptFromDataSync { get; set; }
	}
}
