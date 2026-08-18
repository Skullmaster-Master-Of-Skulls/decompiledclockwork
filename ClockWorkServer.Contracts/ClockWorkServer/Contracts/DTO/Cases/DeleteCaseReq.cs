using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Cases
{
	// Token: 0x020008A2 RID: 2210
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteCaseReq : BaseMessageReq
	{
		// Token: 0x17000FC6 RID: 4038
		// (get) Token: 0x06002CCE RID: 11470 RVA: 0x00015364 File Offset: 0x00013564
		// (set) Token: 0x06002CCF RID: 11471 RVA: 0x0001536C File Offset: 0x0001356C
		[DataMember]
		public int InfoPcId { get; set; }
	}
}
