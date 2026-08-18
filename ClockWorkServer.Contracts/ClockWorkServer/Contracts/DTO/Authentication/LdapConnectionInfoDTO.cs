using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Authentication
{
	// Token: 0x020008E2 RID: 2274
	[DataContract(Namespace = "http://tpro.ca")]
	public class LdapConnectionInfoDTO
	{
		// Token: 0x1700104E RID: 4174
		// (get) Token: 0x06002E25 RID: 11813 RVA: 0x00015D70 File Offset: 0x00013F70
		// (set) Token: 0x06002E26 RID: 11814 RVA: 0x00015D78 File Offset: 0x00013F78
		[DataMember]
		public string ServerName { get; set; }

		// Token: 0x1700104F RID: 4175
		// (get) Token: 0x06002E27 RID: 11815 RVA: 0x00015D81 File Offset: 0x00013F81
		// (set) Token: 0x06002E28 RID: 11816 RVA: 0x00015D89 File Offset: 0x00013F89
		[DataMember]
		public int Port { get; set; }

		// Token: 0x17001050 RID: 4176
		// (get) Token: 0x06002E29 RID: 11817 RVA: 0x00015D92 File Offset: 0x00013F92
		// (set) Token: 0x06002E2A RID: 11818 RVA: 0x00015D9A File Offset: 0x00013F9A
		[DataMember]
		public string LookupAttribute { get; set; }

		// Token: 0x17001051 RID: 4177
		// (get) Token: 0x06002E2B RID: 11819 RVA: 0x00015DA3 File Offset: 0x00013FA3
		// (set) Token: 0x06002E2C RID: 11820 RVA: 0x00015DAB File Offset: 0x00013FAB
		[DataMember]
		public string AuthType { get; set; }

		// Token: 0x17001052 RID: 4178
		// (get) Token: 0x06002E2D RID: 11821 RVA: 0x00015DB4 File Offset: 0x00013FB4
		// (set) Token: 0x06002E2E RID: 11822 RVA: 0x00015DBC File Offset: 0x00013FBC
		[DataMember]
		public bool SSL { get; set; }

		// Token: 0x17001053 RID: 4179
		// (get) Token: 0x06002E2F RID: 11823 RVA: 0x00015DC5 File Offset: 0x00013FC5
		// (set) Token: 0x06002E30 RID: 11824 RVA: 0x00015DCD File Offset: 0x00013FCD
		[DataMember]
		public bool TLS { get; set; }

		// Token: 0x17001054 RID: 4180
		// (get) Token: 0x06002E31 RID: 11825 RVA: 0x00015DD6 File Offset: 0x00013FD6
		// (set) Token: 0x06002E32 RID: 11826 RVA: 0x00015DDE File Offset: 0x00013FDE
		[DataMember]
		public bool DontVerifyServerCertificate { get; set; }

		// Token: 0x17001055 RID: 4181
		// (get) Token: 0x06002E33 RID: 11827 RVA: 0x00015DE7 File Offset: 0x00013FE7
		// (set) Token: 0x06002E34 RID: 11828 RVA: 0x00015DEF File Offset: 0x00013FEF
		[DataMember]
		public string Domain { get; set; }

		// Token: 0x17001056 RID: 4182
		// (get) Token: 0x06002E35 RID: 11829 RVA: 0x00015DF8 File Offset: 0x00013FF8
		// (set) Token: 0x06002E36 RID: 11830 RVA: 0x00015E00 File Offset: 0x00014000
		[DataMember]
		public int ProtocolVersion { get; set; }

		// Token: 0x17001057 RID: 4183
		// (get) Token: 0x06002E37 RID: 11831 RVA: 0x00015E09 File Offset: 0x00014009
		// (set) Token: 0x06002E38 RID: 11832 RVA: 0x00015E11 File Offset: 0x00014011
		[DataMember]
		public bool IsDoubleBinding { get; set; }

		// Token: 0x17001058 RID: 4184
		// (get) Token: 0x06002E39 RID: 11833 RVA: 0x00015E1A File Offset: 0x0001401A
		// (set) Token: 0x06002E3A RID: 11834 RVA: 0x00015E22 File Offset: 0x00014022
		[DataMember]
		public bool IsActiveDirectory { get; set; }

		// Token: 0x17001059 RID: 4185
		// (get) Token: 0x06002E3B RID: 11835 RVA: 0x00015E2B File Offset: 0x0001402B
		// (set) Token: 0x06002E3C RID: 11836 RVA: 0x00015E33 File Offset: 0x00014033
		[DataMember]
		public bool UseLookupAttributeForActiveDirectory { get; set; }

		// Token: 0x1700105A RID: 4186
		// (get) Token: 0x06002E3D RID: 11837 RVA: 0x00015E3C File Offset: 0x0001403C
		// (set) Token: 0x06002E3E RID: 11838 RVA: 0x00015E44 File Offset: 0x00014044
		[DataMember]
		public string[] ReturnAttributes { get; set; }

		// Token: 0x1700105B RID: 4187
		// (get) Token: 0x06002E3F RID: 11839 RVA: 0x00015E4D File Offset: 0x0001404D
		// (set) Token: 0x06002E40 RID: 11840 RVA: 0x00015E55 File Offset: 0x00014055
		[DataMember]
		public string PreUsername { get; set; }

		// Token: 0x1700105C RID: 4188
		// (get) Token: 0x06002E41 RID: 11841 RVA: 0x00015E5E File Offset: 0x0001405E
		// (set) Token: 0x06002E42 RID: 11842 RVA: 0x00015E66 File Offset: 0x00014066
		[DataMember]
		public string PrePassword { get; set; }

		// Token: 0x1700105D RID: 4189
		// (get) Token: 0x06002E43 RID: 11843 RVA: 0x00015E6F File Offset: 0x0001406F
		// (set) Token: 0x06002E44 RID: 11844 RVA: 0x00015E77 File Offset: 0x00014077
		[DataMember]
		public string PreDomain { get; set; }

		// Token: 0x1700105E RID: 4190
		// (get) Token: 0x06002E45 RID: 11845 RVA: 0x00015E80 File Offset: 0x00014080
		// (set) Token: 0x06002E46 RID: 11846 RVA: 0x00015E88 File Offset: 0x00014088
		[DataMember]
		public string PreLookupAttribute { get; set; }
	}
}
