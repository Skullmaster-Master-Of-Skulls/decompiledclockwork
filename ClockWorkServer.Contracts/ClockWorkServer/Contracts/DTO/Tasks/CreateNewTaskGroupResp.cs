using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tasks
{
	// Token: 0x020001FB RID: 507
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateNewTaskGroupResp
	{
		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000B92 RID: 2962 RVA: 0x000054FB File Offset: 0x000036FB
		// (set) Token: 0x06000B93 RID: 2963 RVA: 0x00005503 File Offset: 0x00003703
		[DataMember]
		public int TaskGroupId { get; set; }
	}
}
