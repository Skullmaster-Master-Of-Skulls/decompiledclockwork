using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication.Authentication;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Authentication.ContractParameters
{
	// Token: 0x020008E8 RID: 2280
	[DataContract(Namespace = "http://tpro.ca")]
	public class AuthenticateAndAuthorizeUserReq : BaseReportMessageReq
	{
		// Token: 0x17001066 RID: 4198
		// (get) Token: 0x06002E5B RID: 11867 RVA: 0x00015F08 File Offset: 0x00014108
		// (set) Token: 0x06002E5C RID: 11868 RVA: 0x00015F10 File Offset: 0x00014110
		[DataMember]
		public AuthenticationContextDTO AuthenticationContext { get; set; }

		// Token: 0x17001067 RID: 4199
		// (get) Token: 0x06002E5D RID: 11869 RVA: 0x00015F19 File Offset: 0x00014119
		// (set) Token: 0x06002E5E RID: 11870 RVA: 0x00015F21 File Offset: 0x00014121
		[DataMember]
		public AuthorizationContextDTO AuthorizationContext { get; set; }

		// Token: 0x17001068 RID: 4200
		// (get) Token: 0x06002E5F RID: 11871 RVA: 0x00015F2A File Offset: 0x0001412A
		// (set) Token: 0x06002E60 RID: 11872 RVA: 0x00015F32 File Offset: 0x00014132
		[DataMember]
		public string UserName { get; set; }

		// Token: 0x17001069 RID: 4201
		// (get) Token: 0x06002E61 RID: 11873 RVA: 0x00015F3B File Offset: 0x0001413B
		// (set) Token: 0x06002E62 RID: 11874 RVA: 0x00015F43 File Offset: 0x00014143
		[DataMember]
		public string Password { get; set; }

		// Token: 0x1700106A RID: 4202
		// (get) Token: 0x06002E63 RID: 11875 RVA: 0x00015F4C File Offset: 0x0001414C
		// (set) Token: 0x06002E64 RID: 11876 RVA: 0x00015F54 File Offset: 0x00014154
		[DataMember]
		public AuthenticationArgsDTO Args { get; set; }

		// Token: 0x1700106B RID: 4203
		// (get) Token: 0x06002E65 RID: 11877 RVA: 0x00015F5D File Offset: 0x0001415D
		// (set) Token: 0x06002E66 RID: 11878 RVA: 0x00015F65 File Offset: 0x00014165
		[DataMember]
		public bool VerboseLogging { get; set; }
	}
}
