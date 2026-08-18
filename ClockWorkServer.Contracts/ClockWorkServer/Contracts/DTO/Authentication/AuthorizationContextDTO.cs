using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Authentication
{
	// Token: 0x020008DB RID: 2267
	[DataContract(Namespace = "http://tpro.ca")]
	public class AuthorizationContextDTO
	{
		// Token: 0x17001032 RID: 4146
		// (get) Token: 0x06002DE6 RID: 11750 RVA: 0x00015B94 File Offset: 0x00013D94
		// (set) Token: 0x06002DE7 RID: 11751 RVA: 0x00015B9C File Offset: 0x00013D9C
		[DataMember]
		public IList<AuthorizationContextItemDTO> ContextItems { get; set; }
	}
}
