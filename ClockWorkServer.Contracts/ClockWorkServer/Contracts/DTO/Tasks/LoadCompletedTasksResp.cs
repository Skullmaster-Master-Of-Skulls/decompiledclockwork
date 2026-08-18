using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tasks
{
	// Token: 0x020001F4 RID: 500
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadCompletedTasksResp
	{
		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000B73 RID: 2931 RVA: 0x0000542F File Offset: 0x0000362F
		// (set) Token: 0x06000B74 RID: 2932 RVA: 0x00005437 File Offset: 0x00003637
		[DataMember]
		public List<TaskDTO> Tasks { get; set; }
	}
}
