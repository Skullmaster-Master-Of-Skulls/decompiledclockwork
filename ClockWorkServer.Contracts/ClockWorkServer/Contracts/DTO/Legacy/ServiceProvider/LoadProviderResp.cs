using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProviderOriginal;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.ServiceProvider
{
	// Token: 0x020004D8 RID: 1240
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadProviderResp
	{
		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x06001A13 RID: 6675 RVA: 0x0000C097 File Offset: 0x0000A297
		// (set) Token: 0x06001A14 RID: 6676 RVA: 0x0000C09F File Offset: 0x0000A29F
		[DataMember]
		public ServiceProviderDTO Provider { get; set; }
	}
}
