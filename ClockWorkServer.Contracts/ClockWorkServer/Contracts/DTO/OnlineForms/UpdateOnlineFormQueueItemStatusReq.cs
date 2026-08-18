using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.OnlineForms
{
	// Token: 0x02000403 RID: 1027
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateOnlineFormQueueItemStatusReq : BaseMessageReq
	{
		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x0600166C RID: 5740 RVA: 0x0000A6F3 File Offset: 0x000088F3
		// (set) Token: 0x0600166D RID: 5741 RVA: 0x0000A6FB File Offset: 0x000088FB
		[DataMember]
		public int PeopleOnlineFormId { get; set; }

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x0600166E RID: 5742 RVA: 0x0000A704 File Offset: 0x00008904
		// (set) Token: 0x0600166F RID: 5743 RVA: 0x0000A70C File Offset: 0x0000890C
		[DataMember]
		public int? NewPeopleOnlineFormStatusId { get; set; }
	}
}
