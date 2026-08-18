using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tasks
{
	// Token: 0x020001FF RID: 511
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadGroupsResp
	{
		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000B9E RID: 2974 RVA: 0x0000553F File Offset: 0x0000373F
		// (set) Token: 0x06000B9F RID: 2975 RVA: 0x00005547 File Offset: 0x00003747
		[DataMember]
		public List<TaskGroupDTO> TaskGroups { get; set; }
	}
}
