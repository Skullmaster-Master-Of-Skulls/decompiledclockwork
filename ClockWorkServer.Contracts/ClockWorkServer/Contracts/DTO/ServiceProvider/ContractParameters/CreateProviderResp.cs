using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x020002A2 RID: 674
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateProviderResp
	{
		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06000FC2 RID: 4034 RVA: 0x00007623 File Offset: 0x00005823
		// (set) Token: 0x06000FC3 RID: 4035 RVA: 0x0000762B File Offset: 0x0000582B
		[DataMember]
		public int SPProviderId { get; set; }
	}
}
