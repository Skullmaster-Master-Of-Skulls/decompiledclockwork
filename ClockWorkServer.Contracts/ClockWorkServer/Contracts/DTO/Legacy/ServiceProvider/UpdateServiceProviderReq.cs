using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProviderOriginal;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.ServiceProvider
{
	// Token: 0x020004D3 RID: 1235
	public class UpdateServiceProviderReq : BaseMessageReq
	{
		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x06001A06 RID: 6662 RVA: 0x0000C053 File Offset: 0x0000A253
		// (set) Token: 0x06001A07 RID: 6663 RVA: 0x0000C05B File Offset: 0x0000A25B
		[DataMember]
		public ServiceProviderDTO Provider { get; set; }
	}
}
