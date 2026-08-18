using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Authentication.ContractParameters
{
	// Token: 0x020008EA RID: 2282
	[DataContract(Namespace = "http://tpro.ca")]
	public class LdapLoginResp
	{
		// Token: 0x1700106D RID: 4205
		// (get) Token: 0x06002E6B RID: 11883 RVA: 0x00015F7F File Offset: 0x0001417F
		// (set) Token: 0x06002E6C RID: 11884 RVA: 0x00015F87 File Offset: 0x00014187
		[DataMember]
		public LdapAuthenticationResultDTO LoginResult { get; set; }
	}
}
