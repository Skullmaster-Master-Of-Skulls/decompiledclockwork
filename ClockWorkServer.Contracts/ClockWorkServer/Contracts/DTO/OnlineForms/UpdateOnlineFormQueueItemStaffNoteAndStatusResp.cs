using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.OnlineForms
{
	// Token: 0x02000400 RID: 1024
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateOnlineFormQueueItemStaffNoteAndStatusResp
	{
		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x06001661 RID: 5729 RVA: 0x0000A6AF File Offset: 0x000088AF
		// (set) Token: 0x06001662 RID: 5730 RVA: 0x0000A6B7 File Offset: 0x000088B7
		[DataMember]
		public OnlineFormQueueItemDTO RefreshedItem { get; set; }
	}
}
