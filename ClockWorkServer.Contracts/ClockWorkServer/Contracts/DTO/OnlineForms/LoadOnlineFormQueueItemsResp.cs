using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.OnlineForms
{
	// Token: 0x020003FE RID: 1022
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadOnlineFormQueueItemsResp
	{
		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x06001657 RID: 5719 RVA: 0x0000A66B File Offset: 0x0000886B
		// (set) Token: 0x06001658 RID: 5720 RVA: 0x0000A673 File Offset: 0x00008873
		[DataMember]
		public IList<OnlineFormQueueItemDTO> Items { get; set; }
	}
}
