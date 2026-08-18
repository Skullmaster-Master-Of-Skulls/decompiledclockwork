using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.OnlineForms
{
	// Token: 0x020003F9 RID: 1017
	[DataContract(Namespace = "http://tpro.ca")]
	public class DisableOnlineFormReq : BaseMessageReq
	{
		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x06001642 RID: 5698 RVA: 0x0000A5E3 File Offset: 0x000087E3
		// (set) Token: 0x06001643 RID: 5699 RVA: 0x0000A5EB File Offset: 0x000087EB
		[DataMember]
		public int OnlineFormId { get; set; }
	}
}
