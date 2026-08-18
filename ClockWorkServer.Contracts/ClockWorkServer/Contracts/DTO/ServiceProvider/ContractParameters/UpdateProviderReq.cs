using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x020002A5 RID: 677
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateProviderReq : BaseMessageReq
	{
		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06000FC9 RID: 4041 RVA: 0x00007645 File Offset: 0x00005845
		// (set) Token: 0x06000FCA RID: 4042 RVA: 0x0000764D File Offset: 0x0000584D
		[DataMember]
		public SPProviderDTO Provider { get; set; }
	}
}
