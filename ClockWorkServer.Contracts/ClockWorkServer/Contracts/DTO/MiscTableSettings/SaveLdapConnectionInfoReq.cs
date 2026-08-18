using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MiscTableSettings
{
	// Token: 0x0200045A RID: 1114
	[DataContract(Namespace = "http://tpro.ca")]
	public class SaveLdapConnectionInfoReq : BaseMessageReq
	{
		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x060017D2 RID: 6098 RVA: 0x0000B004 File Offset: 0x00009204
		// (set) Token: 0x060017D3 RID: 6099 RVA: 0x0000B00C File Offset: 0x0000920C
		[DataMember]
		public LdapConnectionInfoDTO Info { get; set; }
	}
}
