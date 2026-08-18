using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProviderOriginal;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.ServiceProvider
{
	// Token: 0x020004D5 RID: 1237
	public class CreateServiceProviderReq : BaseMessageReq
	{
		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x06001A0A RID: 6666 RVA: 0x0000C064 File Offset: 0x0000A264
		// (set) Token: 0x06001A0B RID: 6667 RVA: 0x0000C06C File Offset: 0x0000A26C
		[DataMember]
		public ServiceProviderDTO Provider { get; set; }
	}
}
