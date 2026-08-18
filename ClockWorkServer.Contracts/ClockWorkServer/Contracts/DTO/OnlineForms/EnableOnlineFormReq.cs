using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.OnlineForms
{
	// Token: 0x020003FA RID: 1018
	[DataContract(Namespace = "http://tpro.ca")]
	public class EnableOnlineFormReq : BaseMessageReq
	{
		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x06001645 RID: 5701 RVA: 0x0000A5F4 File Offset: 0x000087F4
		// (set) Token: 0x06001646 RID: 5702 RVA: 0x0000A5FC File Offset: 0x000087FC
		[DataMember]
		public int OnlineFormId { get; set; }
	}
}
