using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x0200039E RID: 926
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadPersonsByIdsReq : BaseMessageReq
	{
		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x060014C9 RID: 5321 RVA: 0x00009C4F File Offset: 0x00007E4F
		// (set) Token: 0x060014CA RID: 5322 RVA: 0x00009C57 File Offset: 0x00007E57
		[DataMember]
		public int[] PersonIds { get; set; }
	}
}
