using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.OnlineForms
{
	// Token: 0x02000406 RID: 1030
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllowedOnlineFormsResp
	{
		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06001675 RID: 5749 RVA: 0x0000A726 File Offset: 0x00008926
		// (set) Token: 0x06001676 RID: 5750 RVA: 0x0000A72E File Offset: 0x0000892E
		[DataMember]
		public IList<OnlineFormDTO> AllowedOnlineForms { get; set; }
	}
}
