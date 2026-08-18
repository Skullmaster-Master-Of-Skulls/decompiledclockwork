using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.ServiceProvider
{
	// Token: 0x020004D6 RID: 1238
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateServiceProviderResp
	{
		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x06001A0D RID: 6669 RVA: 0x0000C075 File Offset: 0x0000A275
		// (set) Token: 0x06001A0E RID: 6670 RVA: 0x0000C07D File Offset: 0x0000A27D
		[DataMember]
		public int ServiceProviderId { get; set; }
	}
}
