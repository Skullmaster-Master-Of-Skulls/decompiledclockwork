using System;
using TechnoPro.Common.Public.Entities.Authentication.Authentication.AuthenticationParameter;

namespace TechnoPro.Common.Public.Entities.Authentication.Authentication
{
	// Token: 0x020004A2 RID: 1186
	public class PortalGuardAuthenticationContext
	{
		// Token: 0x17000EC2 RID: 3778
		// (get) Token: 0x060023BD RID: 9149 RVA: 0x0002725C File Offset: 0x0002545C
		// (set) Token: 0x060023BE RID: 9150 RVA: 0x00027264 File Offset: 0x00025464
		public TokenIssuerAuthParameter TokenIssuer { get; set; }

		// Token: 0x17000EC3 RID: 3779
		// (get) Token: 0x060023BF RID: 9151 RVA: 0x0002726D File Offset: 0x0002546D
		// (set) Token: 0x060023C0 RID: 9152 RVA: 0x00027275 File Offset: 0x00025475
		public string SamlAssertionConsumerServiceUrl { get; set; }

		// Token: 0x17000EC4 RID: 3780
		// (get) Token: 0x060023C1 RID: 9153 RVA: 0x0002727E File Offset: 0x0002547E
		// (set) Token: 0x060023C2 RID: 9154 RVA: 0x00027286 File Offset: 0x00025486
		public string SamlRequestIssuer { get; set; }

		// Token: 0x17000EC5 RID: 3781
		// (get) Token: 0x060023C3 RID: 9155 RVA: 0x0002728F File Offset: 0x0002548F
		// (set) Token: 0x060023C4 RID: 9156 RVA: 0x00027297 File Offset: 0x00025497
		public string IdpUrl { get; set; }
	}
}
