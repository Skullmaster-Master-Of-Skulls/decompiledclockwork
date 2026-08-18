using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.Authentication.Authorization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Authentication
{
	// Token: 0x020008DC RID: 2268
	[DataContract(Namespace = "http://tpro.ca")]
	public class AuthorizationContextItemDTO
	{
		// Token: 0x17001033 RID: 4147
		// (get) Token: 0x06002DE9 RID: 11753 RVA: 0x00015BA5 File Offset: 0x00013DA5
		// (set) Token: 0x06002DEA RID: 11754 RVA: 0x00015BAD File Offset: 0x00013DAD
		[DataMember]
		public eAuthorizationContextItemType ContextItemType { get; set; }

		// Token: 0x17001034 RID: 4148
		// (get) Token: 0x06002DEB RID: 11755 RVA: 0x00015BB6 File Offset: 0x00013DB6
		// (set) Token: 0x06002DEC RID: 11756 RVA: 0x00015BBE File Offset: 0x00013DBE
		[DataMember]
		public string Title { get; set; }

		// Token: 0x17001035 RID: 4149
		// (get) Token: 0x06002DED RID: 11757 RVA: 0x00015BC7 File Offset: 0x00013DC7
		// (set) Token: 0x06002DEE RID: 11758 RVA: 0x00015BCF File Offset: 0x00013DCF
		[DataMember]
		public bool IsDisabled { get; set; }

		// Token: 0x17001036 RID: 4150
		// (get) Token: 0x06002DEF RID: 11759 RVA: 0x00015BD8 File Offset: 0x00013DD8
		// (set) Token: 0x06002DF0 RID: 11760 RVA: 0x00015BE0 File Offset: 0x00013DE0
		[DataMember]
		public eLookupMethod LookupMethod { get; set; }

		// Token: 0x17001037 RID: 4151
		// (get) Token: 0x06002DF1 RID: 11761 RVA: 0x00015BE9 File Offset: 0x00013DE9
		// (set) Token: 0x06002DF2 RID: 11762 RVA: 0x00015BF1 File Offset: 0x00013DF1
		[DataMember]
		public int LookupMethodCid { get; set; }

		// Token: 0x17001038 RID: 4152
		// (get) Token: 0x06002DF3 RID: 11763 RVA: 0x00015BFA File Offset: 0x00013DFA
		// (set) Token: 0x06002DF4 RID: 11764 RVA: 0x00015C02 File Offset: 0x00013E02
		[DataMember]
		public string UsernamePostfix { get; set; }

		// Token: 0x17001039 RID: 4153
		// (get) Token: 0x06002DF5 RID: 11765 RVA: 0x00015C0B File Offset: 0x00013E0B
		// (set) Token: 0x06002DF6 RID: 11766 RVA: 0x00015C13 File Offset: 0x00013E13
		[DataMember]
		public int OrderId { get; set; }
	}
}
