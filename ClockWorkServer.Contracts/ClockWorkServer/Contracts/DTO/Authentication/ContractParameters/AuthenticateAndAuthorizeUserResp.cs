using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Authentication.ContractParameters
{
	// Token: 0x020008E9 RID: 2281
	[DataContract(Namespace = "http://tpro.ca")]
	public class AuthenticateAndAuthorizeUserResp
	{
		// Token: 0x1700106C RID: 4204
		// (get) Token: 0x06002E68 RID: 11880 RVA: 0x00015F6E File Offset: 0x0001416E
		// (set) Token: 0x06002E69 RID: 11881 RVA: 0x00015F76 File Offset: 0x00014176
		[DataMember]
		public AuthenticationAndAuthorizationResultDTO AuthenticationAndAuthorizationResult { get; set; }
	}
}
