using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MiscTableSettings
{
	// Token: 0x02000458 RID: 1112
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadLdapConnectionInfoResp
	{
		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x060017CE RID: 6094 RVA: 0x0000AFF3 File Offset: 0x000091F3
		// (set) Token: 0x060017CF RID: 6095 RVA: 0x0000AFFB File Offset: 0x000091FB
		[DataMember]
		public LdapConnectionInfoDTO Info { get; set; }
	}
}
