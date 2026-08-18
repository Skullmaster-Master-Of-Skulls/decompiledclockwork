using System;

namespace TechnoPro.Common.Public.Entities.Authentication
{
	// Token: 0x0200048A RID: 1162
	public class CASAuthenticationOptions
	{
		// Token: 0x17000E70 RID: 3696
		// (get) Token: 0x060022FF RID: 8959 RVA: 0x00026B2E File Offset: 0x00024D2E
		// (set) Token: 0x06002300 RID: 8960 RVA: 0x00026B36 File Offset: 0x00024D36
		public string CASServiceValidateUrl { get; set; }

		// Token: 0x17000E71 RID: 3697
		// (get) Token: 0x06002301 RID: 8961 RVA: 0x00026B3F File Offset: 0x00024D3F
		// (set) Token: 0x06002302 RID: 8962 RVA: 0x00026B47 File Offset: 0x00024D47
		public string ClockWorkLoginSuccessUrl { get; set; }

		// Token: 0x17000E72 RID: 3698
		// (get) Token: 0x06002303 RID: 8963 RVA: 0x00026B50 File Offset: 0x00024D50
		// (set) Token: 0x06002304 RID: 8964 RVA: 0x00026B58 File Offset: 0x00024D58
		public string CASLoginUrl { get; set; }

		// Token: 0x17000E73 RID: 3699
		// (get) Token: 0x06002305 RID: 8965 RVA: 0x00026B61 File Offset: 0x00024D61
		// (set) Token: 0x06002306 RID: 8966 RVA: 0x00026B69 File Offset: 0x00024D69
		public string CASLogoutUrl { get; set; }
	}
}
