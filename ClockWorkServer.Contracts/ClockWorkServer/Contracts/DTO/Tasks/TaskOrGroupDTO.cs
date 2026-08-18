using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tasks
{
	// Token: 0x020001E8 RID: 488
	[DataContract(Namespace = "http://tpro.ca")]
	public class TaskOrGroupDTO
	{
		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000B2F RID: 2863 RVA: 0x00005253 File Offset: 0x00003453
		// (set) Token: 0x06000B30 RID: 2864 RVA: 0x0000525B File Offset: 0x0000345B
		[DataMember]
		public TaskDTO Task { get; set; }

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000B31 RID: 2865 RVA: 0x00005264 File Offset: 0x00003464
		// (set) Token: 0x06000B32 RID: 2866 RVA: 0x0000526C File Offset: 0x0000346C
		[DataMember]
		public TaskGroupDTO Group { get; set; }
	}
}
