using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.OnlineForms
{
	// Token: 0x02000409 RID: 1033
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadOnlineFormQueueItemFormDataItemsReq : BaseMessageReq
	{
		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x0600167E RID: 5758 RVA: 0x0000A759 File Offset: 0x00008959
		// (set) Token: 0x0600167F RID: 5759 RVA: 0x0000A761 File Offset: 0x00008961
		[DataMember]
		public int PeopleOnlineFormId { get; set; }
	}
}
