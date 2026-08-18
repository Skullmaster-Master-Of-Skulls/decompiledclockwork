using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tasks
{
	// Token: 0x020001F1 RID: 497
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTaskByIdReq : BaseMessageReq
	{
		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000B64 RID: 2916 RVA: 0x000053C9 File Offset: 0x000035C9
		// (set) Token: 0x06000B65 RID: 2917 RVA: 0x000053D1 File Offset: 0x000035D1
		[DataMember]
		public int TaskId { get; set; }
	}
}
