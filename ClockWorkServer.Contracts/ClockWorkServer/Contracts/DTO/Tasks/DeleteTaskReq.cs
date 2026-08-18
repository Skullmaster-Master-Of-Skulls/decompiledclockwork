using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tasks
{
	// Token: 0x020001F8 RID: 504
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteTaskReq : BaseMessageReq
	{
		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000B87 RID: 2951 RVA: 0x000054B7 File Offset: 0x000036B7
		// (set) Token: 0x06000B88 RID: 2952 RVA: 0x000054BF File Offset: 0x000036BF
		[DataMember]
		public int TaskId { get; set; }
	}
}
