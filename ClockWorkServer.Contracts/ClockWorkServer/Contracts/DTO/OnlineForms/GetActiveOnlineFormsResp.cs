using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.OnlineForms
{
	// Token: 0x020003F1 RID: 1009
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetActiveOnlineFormsResp
	{
		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x0600162C RID: 5676 RVA: 0x0000A56C File Offset: 0x0000876C
		// (set) Token: 0x0600162D RID: 5677 RVA: 0x0000A574 File Offset: 0x00008774
		[DataMember]
		public List<OnlineFormDTO> OnlineForms { get; set; }
	}
}
