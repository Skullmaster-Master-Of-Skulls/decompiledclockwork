using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.OnlineForms
{
	// Token: 0x02000404 RID: 1028
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateOnlineFormQueueItemStatusResp
	{
		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06001671 RID: 5745 RVA: 0x0000A715 File Offset: 0x00008915
		// (set) Token: 0x06001672 RID: 5746 RVA: 0x0000A71D File Offset: 0x0000891D
		[DataMember]
		public OnlineFormQueueItemDTO RefreshedItem { get; set; }
	}
}
