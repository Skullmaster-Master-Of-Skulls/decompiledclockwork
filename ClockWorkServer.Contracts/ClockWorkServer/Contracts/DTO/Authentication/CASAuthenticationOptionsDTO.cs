using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Authentication
{
	// Token: 0x020008DD RID: 2269
	[DataContract(Namespace = "http://tpro.ca")]
	public class CASAuthenticationOptionsDTO
	{
		// Token: 0x1700103A RID: 4154
		// (get) Token: 0x06002DF8 RID: 11768 RVA: 0x00015C1C File Offset: 0x00013E1C
		// (set) Token: 0x06002DF9 RID: 11769 RVA: 0x00015C24 File Offset: 0x00013E24
		[DataMember]
		public string CASServiceValidateUrl { get; set; }

		// Token: 0x1700103B RID: 4155
		// (get) Token: 0x06002DFA RID: 11770 RVA: 0x00015C2D File Offset: 0x00013E2D
		// (set) Token: 0x06002DFB RID: 11771 RVA: 0x00015C35 File Offset: 0x00013E35
		[DataMember]
		public string ClockWorkLoginSuccessUrl { get; set; }

		// Token: 0x1700103C RID: 4156
		// (get) Token: 0x06002DFC RID: 11772 RVA: 0x00015C3E File Offset: 0x00013E3E
		// (set) Token: 0x06002DFD RID: 11773 RVA: 0x00015C46 File Offset: 0x00013E46
		[DataMember]
		public string CASLoginUrl { get; set; }

		// Token: 0x1700103D RID: 4157
		// (get) Token: 0x06002DFE RID: 11774 RVA: 0x00015C4F File Offset: 0x00013E4F
		// (set) Token: 0x06002DFF RID: 11775 RVA: 0x00015C57 File Offset: 0x00013E57
		[DataMember]
		public string CASLogoutUrl { get; set; }
	}
}
