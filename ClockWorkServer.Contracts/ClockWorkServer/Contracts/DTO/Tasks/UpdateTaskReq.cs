using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tasks
{
	// Token: 0x020001F9 RID: 505
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateTaskReq : BaseMessageReq
	{
		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000B8A RID: 2954 RVA: 0x000054C8 File Offset: 0x000036C8
		// (set) Token: 0x06000B8B RID: 2955 RVA: 0x000054D0 File Offset: 0x000036D0
		[DataMember]
		public TaskDTO Task { get; set; }
	}
}
