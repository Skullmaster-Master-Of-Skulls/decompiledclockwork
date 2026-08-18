using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Authentication.Authentication
{
	// Token: 0x020008F0 RID: 2288
	[DataContract(Namespace = "http://tpro.ca")]
	public class AuthenticationResultParametersDTO
	{
		// Token: 0x1700107C RID: 4220
		// (get) Token: 0x06002E90 RID: 11920 RVA: 0x000160E1 File Offset: 0x000142E1
		// (set) Token: 0x06002E91 RID: 11921 RVA: 0x000160E9 File Offset: 0x000142E9
		[DataMember]
		public ExternalUserInfoDTO ExternalUserInfo { get; set; }

		// Token: 0x1700107D RID: 4221
		// (get) Token: 0x06002E92 RID: 11922 RVA: 0x000160F2 File Offset: 0x000142F2
		// (set) Token: 0x06002E93 RID: 11923 RVA: 0x000160FA File Offset: 0x000142FA
		[DataMember]
		public bool IsSuccess { get; set; }

		// Token: 0x1700107E RID: 4222
		// (get) Token: 0x06002E94 RID: 11924 RVA: 0x00016103 File Offset: 0x00014303
		// (set) Token: 0x06002E95 RID: 11925 RVA: 0x0001610B File Offset: 0x0001430B
		[DataMember]
		public IDictionary<string, string> Args { get; set; }

		// Token: 0x1700107F RID: 4223
		// (get) Token: 0x06002E96 RID: 11926 RVA: 0x00016114 File Offset: 0x00014314
		// (set) Token: 0x06002E97 RID: 11927 RVA: 0x0001611C File Offset: 0x0001431C
		[DataMember]
		public string LoggingMessage { get; set; }
	}
}
