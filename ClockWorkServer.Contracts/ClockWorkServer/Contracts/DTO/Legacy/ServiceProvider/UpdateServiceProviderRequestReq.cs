using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.ServiceProvider
{
	// Token: 0x020004CF RID: 1231
	public class UpdateServiceProviderRequestReq : BaseMessageReq
	{
		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x060019FC RID: 6652 RVA: 0x0000C020 File Offset: 0x0000A220
		// (set) Token: 0x060019FD RID: 6653 RVA: 0x0000C028 File Offset: 0x0000A228
		[DataMember]
		public LegacyServiceProviderRequestDetailDTO RequestDetail { get; set; }
	}
}
