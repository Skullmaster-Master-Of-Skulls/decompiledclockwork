using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.OnlineForms
{
	// Token: 0x020003F5 RID: 1013
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateOnlineFormReq : BaseMessageReq
	{
		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x06001636 RID: 5686 RVA: 0x0000A59F File Offset: 0x0000879F
		// (set) Token: 0x06001637 RID: 5687 RVA: 0x0000A5A7 File Offset: 0x000087A7
		[DataMember]
		public OnlineFormDTO OnlineForm { get; set; }
	}
}
