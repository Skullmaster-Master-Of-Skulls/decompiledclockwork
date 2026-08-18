using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.OnlineForms
{
	// Token: 0x02000407 RID: 1031
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteOnlineFormQueueItemReq : BaseMessageReq
	{
		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06001678 RID: 5752 RVA: 0x0000A737 File Offset: 0x00008937
		// (set) Token: 0x06001679 RID: 5753 RVA: 0x0000A73F File Offset: 0x0000893F
		[DataMember]
		public int PeopleOnlineFormId { get; set; }
	}
}
