using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Authentication
{
	// Token: 0x020008E1 RID: 2273
	[DataContract(Namespace = "http://tpro.ca")]
	public class LdapAuthenticationResultDTO
	{
		// Token: 0x1700104B RID: 4171
		// (get) Token: 0x06002E1E RID: 11806 RVA: 0x00015D3D File Offset: 0x00013F3D
		// (set) Token: 0x06002E1F RID: 11807 RVA: 0x00015D45 File Offset: 0x00013F45
		[DataMember]
		public bool IsAuthenticated { get; set; }

		// Token: 0x1700104C RID: 4172
		// (get) Token: 0x06002E20 RID: 11808 RVA: 0x00015D4E File Offset: 0x00013F4E
		// (set) Token: 0x06002E21 RID: 11809 RVA: 0x00015D56 File Offset: 0x00013F56
		[DataMember]
		public Dictionary<string, string> ReturnAttributes { get; set; }

		// Token: 0x1700104D RID: 4173
		// (get) Token: 0x06002E22 RID: 11810 RVA: 0x00015D5F File Offset: 0x00013F5F
		// (set) Token: 0x06002E23 RID: 11811 RVA: 0x00015D67 File Offset: 0x00013F67
		[DataMember]
		public string ErrorMessage { get; set; }
	}
}
