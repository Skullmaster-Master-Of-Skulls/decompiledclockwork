using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tasks
{
	// Token: 0x020001FE RID: 510
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateTaskGroupReq : BaseMessageReq
	{
		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000B9B RID: 2971 RVA: 0x0000552E File Offset: 0x0000372E
		// (set) Token: 0x06000B9C RID: 2972 RVA: 0x00005536 File Offset: 0x00003736
		[DataMember]
		public TaskGroupDTO TaskGroup { get; set; }
	}
}
