using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.Authentication.Authentication;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Authentication.Authentication
{
	// Token: 0x020008EE RID: 2286
	[DataContract(Namespace = "http://tpro.ca")]
	public class AuthenticationContextItemDTO
	{
		// Token: 0x17001074 RID: 4212
		// (get) Token: 0x06002E7D RID: 11901 RVA: 0x00016018 File Offset: 0x00014218
		// (set) Token: 0x06002E7E RID: 11902 RVA: 0x00016020 File Offset: 0x00014220
		[DataMember]
		public eAuthenticationContextItemType ContextItemType { get; set; }

		// Token: 0x17001075 RID: 4213
		// (get) Token: 0x06002E7F RID: 11903 RVA: 0x00016029 File Offset: 0x00014229
		// (set) Token: 0x06002E80 RID: 11904 RVA: 0x00016031 File Offset: 0x00014231
		[DataMember]
		public bool IsDisabled { get; set; }

		// Token: 0x17001076 RID: 4214
		// (get) Token: 0x06002E81 RID: 11905 RVA: 0x0001603A File Offset: 0x0001423A
		// (set) Token: 0x06002E82 RID: 11906 RVA: 0x00016042 File Offset: 0x00014242
		[DataMember]
		public int OrderId { get; set; }

		// Token: 0x17001077 RID: 4215
		// (get) Token: 0x06002E83 RID: 11907 RVA: 0x0001604B File Offset: 0x0001424B
		// (set) Token: 0x06002E84 RID: 11908 RVA: 0x00016053 File Offset: 0x00014253
		[DataMember]
		public IDictionary<string, string> Args { get; set; }
	}
}
