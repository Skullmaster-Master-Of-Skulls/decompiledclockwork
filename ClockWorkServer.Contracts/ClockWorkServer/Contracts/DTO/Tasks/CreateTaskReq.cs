using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tasks
{
	// Token: 0x020001F7 RID: 503
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateTaskReq : BaseMessageReq
	{
		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000B84 RID: 2948 RVA: 0x000054A6 File Offset: 0x000036A6
		// (set) Token: 0x06000B85 RID: 2949 RVA: 0x000054AE File Offset: 0x000036AE
		[DataMember]
		public TaskDTO Task { get; set; }
	}
}
