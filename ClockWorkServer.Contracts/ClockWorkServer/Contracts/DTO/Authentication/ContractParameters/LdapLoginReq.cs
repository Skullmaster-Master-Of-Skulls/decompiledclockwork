using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Authentication.ContractParameters
{
	// Token: 0x020008EB RID: 2283
	[DataContract(Namespace = "http://tpro.ca")]
	public class LdapLoginReq : BaseMessageReq
	{
		// Token: 0x1700106E RID: 4206
		// (get) Token: 0x06002E6E RID: 11886 RVA: 0x00015F90 File Offset: 0x00014190
		// (set) Token: 0x06002E6F RID: 11887 RVA: 0x00015F98 File Offset: 0x00014198
		[DataMember]
		public LdapConnectionInfoDTO ConnectionInfo { get; set; }

		// Token: 0x1700106F RID: 4207
		// (get) Token: 0x06002E70 RID: 11888 RVA: 0x00015FA1 File Offset: 0x000141A1
		// (set) Token: 0x06002E71 RID: 11889 RVA: 0x00015FA9 File Offset: 0x000141A9
		[DataMember]
		public string UserName { get; set; }

		// Token: 0x17001070 RID: 4208
		// (get) Token: 0x06002E72 RID: 11890 RVA: 0x00015FB2 File Offset: 0x000141B2
		// (set) Token: 0x06002E73 RID: 11891 RVA: 0x00015FBA File Offset: 0x000141BA
		[DataMember]
		public string PassWord { get; set; }
	}
}
