using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.ServiceProvider
{
	// Token: 0x020004D7 RID: 1239
	public class LoadProviderReq : BaseMessageReq
	{
		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x06001A10 RID: 6672 RVA: 0x0000C086 File Offset: 0x0000A286
		// (set) Token: 0x06001A11 RID: 6673 RVA: 0x0000C08E File Offset: 0x0000A28E
		[DataMember]
		public int ServiceProviderId { get; set; }
	}
}
