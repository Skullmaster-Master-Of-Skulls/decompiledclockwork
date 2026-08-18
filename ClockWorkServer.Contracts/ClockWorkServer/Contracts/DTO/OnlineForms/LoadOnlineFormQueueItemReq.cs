using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.OnlineForms
{
	// Token: 0x0200040B RID: 1035
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadOnlineFormQueueItemReq : BaseMessageReq
	{
		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x06001684 RID: 5764 RVA: 0x0000A77B File Offset: 0x0000897B
		// (set) Token: 0x06001685 RID: 5765 RVA: 0x0000A783 File Offset: 0x00008983
		[DataMember]
		public int PeopleOnlineFormId { get; set; }
	}
}
