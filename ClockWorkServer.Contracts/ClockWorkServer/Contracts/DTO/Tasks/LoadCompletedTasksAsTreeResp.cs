using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.Common.DataStructure.Tree;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tasks
{
	// Token: 0x020001EE RID: 494
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadCompletedTasksAsTreeResp
	{
		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000B55 RID: 2901 RVA: 0x00005363 File Offset: 0x00003563
		// (set) Token: 0x06000B56 RID: 2902 RVA: 0x0000536B File Offset: 0x0000356B
		[DataMember]
		public List<TaskDTO> Tasks { get; set; }

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000B57 RID: 2903 RVA: 0x00005374 File Offset: 0x00003574
		// (set) Token: 0x06000B58 RID: 2904 RVA: 0x0000537C File Offset: 0x0000357C
		[DataMember]
		public Forest<TaskOrGroupDTO> Tree { get; set; }

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000B59 RID: 2905 RVA: 0x00005385 File Offset: 0x00003585
		// (set) Token: 0x06000B5A RID: 2906 RVA: 0x0000538D File Offset: 0x0000358D
		[DataMember]
		public List<TaskGroupDTO> Groups { get; set; }
	}
}
