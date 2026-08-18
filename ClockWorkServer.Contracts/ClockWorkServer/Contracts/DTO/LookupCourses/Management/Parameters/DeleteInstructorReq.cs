using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses.Management.Parameters
{
	// Token: 0x02000818 RID: 2072
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteInstructorReq : BaseMessageReq
	{
		// Token: 0x17000EC2 RID: 3778
		// (get) Token: 0x06002A3D RID: 10813 RVA: 0x000140FB File Offset: 0x000122FB
		// (set) Token: 0x06002A3E RID: 10814 RVA: 0x00014103 File Offset: 0x00012303
		[DataMember]
		public int InstructorId { get; set; }
	}
}
