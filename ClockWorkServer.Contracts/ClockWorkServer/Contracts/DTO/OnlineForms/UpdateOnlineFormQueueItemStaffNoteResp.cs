using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.OnlineForms
{
	// Token: 0x02000402 RID: 1026
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateOnlineFormQueueItemStaffNoteResp
	{
		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06001669 RID: 5737 RVA: 0x0000A6E2 File Offset: 0x000088E2
		// (set) Token: 0x0600166A RID: 5738 RVA: 0x0000A6EA File Offset: 0x000088EA
		[DataMember]
		public OnlineFormQueueItemDTO RefreshedItem { get; set; }
	}
}
