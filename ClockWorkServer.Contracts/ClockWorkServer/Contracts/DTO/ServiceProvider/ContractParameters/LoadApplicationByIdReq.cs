using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x0200027F RID: 639
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadApplicationByIdReq : BaseMessageReq
	{
		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06000F4D RID: 3917 RVA: 0x0000736A File Offset: 0x0000556A
		// (set) Token: 0x06000F4E RID: 3918 RVA: 0x00007372 File Offset: 0x00005572
		[DataMember]
		public int SPApplicationId { get; set; }
	}
}
