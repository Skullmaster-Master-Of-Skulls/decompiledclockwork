using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tasks
{
	// Token: 0x020001FC RID: 508
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateNewTaskGroupReq : BaseMessageReq
	{
		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000B95 RID: 2965 RVA: 0x0000550C File Offset: 0x0000370C
		// (set) Token: 0x06000B96 RID: 2966 RVA: 0x00005514 File Offset: 0x00003714
		[DataMember]
		public TaskGroupDTO TaskGroup { get; set; }
	}
}
