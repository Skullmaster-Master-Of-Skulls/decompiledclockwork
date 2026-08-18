using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.Common.DataStructure.Tree;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tasks
{
	// Token: 0x020001EC RID: 492
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTasksAsTreeResp
	{
		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000B43 RID: 2883 RVA: 0x000052DB File Offset: 0x000034DB
		// (set) Token: 0x06000B44 RID: 2884 RVA: 0x000052E3 File Offset: 0x000034E3
		[DataMember]
		public List<TaskDTO> Tasks { get; set; }

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000B45 RID: 2885 RVA: 0x000052EC File Offset: 0x000034EC
		// (set) Token: 0x06000B46 RID: 2886 RVA: 0x000052F4 File Offset: 0x000034F4
		[DataMember]
		public Forest<TaskOrGroupDTO> Tree { get; set; }

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000B47 RID: 2887 RVA: 0x000052FD File Offset: 0x000034FD
		// (set) Token: 0x06000B48 RID: 2888 RVA: 0x00005305 File Offset: 0x00003505
		[DataMember]
		public List<TaskGroupDTO> Groups { get; set; }
	}
}
