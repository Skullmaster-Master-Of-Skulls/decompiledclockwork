using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.OnlineForms
{
	// Token: 0x020003F7 RID: 1015
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateNewOnlineFormResp
	{
		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x0600163C RID: 5692 RVA: 0x0000A5C1 File Offset: 0x000087C1
		// (set) Token: 0x0600163D RID: 5693 RVA: 0x0000A5C9 File Offset: 0x000087C9
		[DataMember]
		public int OnlineFormId { get; set; }
	}
}
