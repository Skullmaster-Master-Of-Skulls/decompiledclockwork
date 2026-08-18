using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tasks
{
	// Token: 0x020001E9 RID: 489
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTaskNotesByTaskIdReq : BaseMessageReq
	{
		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000B34 RID: 2868 RVA: 0x00005275 File Offset: 0x00003475
		// (set) Token: 0x06000B35 RID: 2869 RVA: 0x0000527D File Offset: 0x0000347D
		[DataMember]
		public int TaskId { get; set; }
	}
}
