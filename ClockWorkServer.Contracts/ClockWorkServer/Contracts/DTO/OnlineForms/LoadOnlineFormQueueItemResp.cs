using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.OnlineForms
{
	// Token: 0x0200040C RID: 1036
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadOnlineFormQueueItemResp
	{
		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06001687 RID: 5767 RVA: 0x0000A78C File Offset: 0x0000898C
		// (set) Token: 0x06001688 RID: 5768 RVA: 0x0000A794 File Offset: 0x00008994
		[DataMember]
		public OnlineFormQueueItemDTO Item { get; set; }
	}
}
