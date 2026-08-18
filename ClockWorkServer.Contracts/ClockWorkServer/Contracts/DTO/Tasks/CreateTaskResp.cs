using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tasks
{
	// Token: 0x020001F6 RID: 502
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateTaskResp
	{
		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000B81 RID: 2945 RVA: 0x00005495 File Offset: 0x00003695
		// (set) Token: 0x06000B82 RID: 2946 RVA: 0x0000549D File Offset: 0x0000369D
		[DataMember]
		public int TaskId { get; set; }
	}
}
