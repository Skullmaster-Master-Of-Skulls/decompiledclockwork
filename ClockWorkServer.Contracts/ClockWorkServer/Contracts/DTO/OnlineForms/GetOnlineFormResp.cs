using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.OnlineForms
{
	// Token: 0x020003F3 RID: 1011
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetOnlineFormResp
	{
		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06001630 RID: 5680 RVA: 0x0000A57D File Offset: 0x0000877D
		// (set) Token: 0x06001631 RID: 5681 RVA: 0x0000A585 File Offset: 0x00008785
		[DataMember]
		public OnlineFormDTO OnlineForm { get; set; }
	}
}
