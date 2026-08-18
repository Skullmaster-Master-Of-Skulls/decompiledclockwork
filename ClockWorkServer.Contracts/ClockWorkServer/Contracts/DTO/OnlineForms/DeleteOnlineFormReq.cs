using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.OnlineForms
{
	// Token: 0x020003F8 RID: 1016
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteOnlineFormReq : BaseMessageReq
	{
		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x0600163F RID: 5695 RVA: 0x0000A5D2 File Offset: 0x000087D2
		// (set) Token: 0x06001640 RID: 5696 RVA: 0x0000A5DA File Offset: 0x000087DA
		[DataMember]
		public int OnlineFormId { get; set; }
	}
}
