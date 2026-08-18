using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x020002A3 RID: 675
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateProviderReq : BaseMessageReq
	{
		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06000FC5 RID: 4037 RVA: 0x00007634 File Offset: 0x00005834
		// (set) Token: 0x06000FC6 RID: 4038 RVA: 0x0000763C File Offset: 0x0000583C
		[DataMember]
		public SPProviderDTO Provider { get; set; }
	}
}
