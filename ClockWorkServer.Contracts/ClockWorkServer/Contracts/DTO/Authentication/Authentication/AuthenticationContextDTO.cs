using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Authentication.Authentication
{
	// Token: 0x020008ED RID: 2285
	[DataContract(Namespace = "http://tpro.ca")]
	public class AuthenticationContextDTO
	{
		// Token: 0x17001073 RID: 4211
		// (get) Token: 0x06002E7A RID: 11898 RVA: 0x00016007 File Offset: 0x00014207
		// (set) Token: 0x06002E7B RID: 11899 RVA: 0x0001600F File Offset: 0x0001420F
		[DataMember]
		public IList<AuthenticationContextItemDTO> ContextItems { get; set; }
	}
}
