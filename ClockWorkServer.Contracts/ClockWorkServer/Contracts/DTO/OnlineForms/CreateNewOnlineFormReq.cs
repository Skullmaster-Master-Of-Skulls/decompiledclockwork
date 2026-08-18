using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.OnlineForms
{
	// Token: 0x020003F6 RID: 1014
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateNewOnlineFormReq : BaseMessageReq
	{
		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x06001639 RID: 5689 RVA: 0x0000A5B0 File Offset: 0x000087B0
		// (set) Token: 0x0600163A RID: 5690 RVA: 0x0000A5B8 File Offset: 0x000087B8
		[DataMember]
		public OnlineFormDTO OnlineForm { get; set; }
	}
}
