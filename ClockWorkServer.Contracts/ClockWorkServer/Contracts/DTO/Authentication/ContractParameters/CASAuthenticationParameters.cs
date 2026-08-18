using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Authentication.ContractParameters
{
	// Token: 0x020008E3 RID: 2275
	public class CASAuthenticationParameters
	{
		// Token: 0x02000CA8 RID: 3240
		[DataContract(Namespace = "http://tpro.ca")]
		public class AuthenticateCASResp
		{
			// Token: 0x170018C0 RID: 6336
			// (get) Token: 0x0600438E RID: 17294 RVA: 0x00024823 File Offset: 0x00022A23
			// (set) Token: 0x0600438F RID: 17295 RVA: 0x0002482B File Offset: 0x00022A2B
			[DataMember]
			public CASAuthenticationResultDTO AuthenticationResult { get; set; }
		}

		// Token: 0x02000CA9 RID: 3241
		[DataContract(Namespace = "http://tpro.ca")]
		public class AuthenticateCASReq : BaseMessageReq
		{
			// Token: 0x170018C1 RID: 6337
			// (get) Token: 0x06004391 RID: 17297 RVA: 0x00024834 File Offset: 0x00022A34
			// (set) Token: 0x06004392 RID: 17298 RVA: 0x0002483C File Offset: 0x00022A3C
			[DataMember]
			public string Ticket { get; set; }
		}

		// Token: 0x02000CAA RID: 3242
		[DataContract(Namespace = "http://tpro.ca")]
		public class AuthenticateCASWithOverrideOptionsResp
		{
			// Token: 0x170018C2 RID: 6338
			// (get) Token: 0x06004394 RID: 17300 RVA: 0x00024845 File Offset: 0x00022A45
			// (set) Token: 0x06004395 RID: 17301 RVA: 0x0002484D File Offset: 0x00022A4D
			[DataMember]
			public CASAuthenticationResultDTO AuthenticationResult { get; set; }
		}

		// Token: 0x02000CAB RID: 3243
		[DataContract(Namespace = "http://tpro.ca")]
		public class AuthenticateCASWithOverrideOptionsReq : BaseMessageReq
		{
			// Token: 0x170018C3 RID: 6339
			// (get) Token: 0x06004397 RID: 17303 RVA: 0x00024856 File Offset: 0x00022A56
			// (set) Token: 0x06004398 RID: 17304 RVA: 0x0002485E File Offset: 0x00022A5E
			[DataMember]
			public string Ticket { get; set; }

			// Token: 0x170018C4 RID: 6340
			// (get) Token: 0x06004399 RID: 17305 RVA: 0x00024867 File Offset: 0x00022A67
			// (set) Token: 0x0600439A RID: 17306 RVA: 0x0002486F File Offset: 0x00022A6F
			[DataMember]
			public CASAuthenticationOptionsDTO OverrideOptions { get; set; }
		}
	}
}
