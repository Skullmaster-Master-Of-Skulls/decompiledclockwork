using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication.Authentication;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Authentication.ContractParameters
{
	// Token: 0x020008E6 RID: 2278
	[DataContract(Namespace = "http://tpro.ca")]
	public class LookupAuthenticatedUserInClockWorkReq : BaseReportMessageReq
	{
		// Token: 0x17001062 RID: 4194
		// (get) Token: 0x06002E51 RID: 11857 RVA: 0x00015EC4 File Offset: 0x000140C4
		// (set) Token: 0x06002E52 RID: 11858 RVA: 0x00015ECC File Offset: 0x000140CC
		[DataMember]
		public AuthorizationContextDTO Context { get; set; }

		// Token: 0x17001063 RID: 4195
		// (get) Token: 0x06002E53 RID: 11859 RVA: 0x00015ED5 File Offset: 0x000140D5
		// (set) Token: 0x06002E54 RID: 11860 RVA: 0x00015EDD File Offset: 0x000140DD
		[DataMember]
		public ExternalUserInfoDTO ExternalUserInfo { get; set; }

		// Token: 0x17001064 RID: 4196
		// (get) Token: 0x06002E55 RID: 11861 RVA: 0x00015EE6 File Offset: 0x000140E6
		// (set) Token: 0x06002E56 RID: 11862 RVA: 0x00015EEE File Offset: 0x000140EE
		[DataMember]
		public bool VerboseLogging { get; set; }
	}
}
