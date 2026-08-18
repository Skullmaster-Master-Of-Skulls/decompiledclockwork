using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tasks
{
	// Token: 0x020001FD RID: 509
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteTaskGroupReq : BaseMessageReq
	{
		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000B98 RID: 2968 RVA: 0x0000551D File Offset: 0x0000371D
		// (set) Token: 0x06000B99 RID: 2969 RVA: 0x00005525 File Offset: 0x00003725
		[DataMember]
		public int TaskGroupId { get; set; }
	}
}
