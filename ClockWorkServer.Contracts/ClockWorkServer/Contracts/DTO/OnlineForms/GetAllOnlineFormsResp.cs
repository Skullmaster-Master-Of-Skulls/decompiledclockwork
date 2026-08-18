using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.OnlineForms
{
	// Token: 0x020003EF RID: 1007
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetAllOnlineFormsResp
	{
		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x06001628 RID: 5672 RVA: 0x0000A55B File Offset: 0x0000875B
		// (set) Token: 0x06001629 RID: 5673 RVA: 0x0000A563 File Offset: 0x00008763
		[DataMember]
		public List<OnlineFormDTO> OnlineForms { get; set; }
	}
}
