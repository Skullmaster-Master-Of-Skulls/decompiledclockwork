using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x020002BC RID: 700
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadRequestByIdResp
	{
		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06001006 RID: 4102 RVA: 0x00007788 File Offset: 0x00005988
		// (set) Token: 0x06001007 RID: 4103 RVA: 0x00007790 File Offset: 0x00005990
		[DataMember]
		public SPRequestWithSubItemsDTO RequestWithSubItems { get; set; }
	}
}
