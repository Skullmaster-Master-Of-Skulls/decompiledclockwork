using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tasks
{
	// Token: 0x020001F2 RID: 498
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTasksResp
	{
		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000B67 RID: 2919 RVA: 0x000053DA File Offset: 0x000035DA
		// (set) Token: 0x06000B68 RID: 2920 RVA: 0x000053E2 File Offset: 0x000035E2
		[DataMember]
		public List<TaskDTO> Tasks { get; set; }
	}
}
