using System;
using TechnoPro.Common.Public.Entities.Authentication.Authorization;

namespace TechnoPro.Common.Public.Entities.Authentication
{
	// Token: 0x02000489 RID: 1161
	public class AuthenticationAndAuthorizationResult
	{
		// Token: 0x17000E6E RID: 3694
		// (get) Token: 0x060022FA RID: 8954 RVA: 0x00026B0C File Offset: 0x00024D0C
		// (set) Token: 0x060022FB RID: 8955 RVA: 0x00026B14 File Offset: 0x00024D14
		public ClockWorkUser ClockWorkUser { get; set; }

		// Token: 0x17000E6F RID: 3695
		// (get) Token: 0x060022FC RID: 8956 RVA: 0x00026B1D File Offset: 0x00024D1D
		// (set) Token: 0x060022FD RID: 8957 RVA: 0x00026B25 File Offset: 0x00024D25
		public bool PassedAuthentication { get; set; }
	}
}
