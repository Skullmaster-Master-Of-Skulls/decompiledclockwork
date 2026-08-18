using System;
using System.Security.Claims;

namespace Microsoft.Owin.Security
{
	// Token: 0x0200001F RID: 31
	public class AuthenticationTicket
	{
		// Token: 0x06000080 RID: 128 RVA: 0x00003E06 File Offset: 0x00002006
		public AuthenticationTicket(ClaimsIdentity identity, AuthenticationProperties properties)
		{
			this.Identity = identity;
			this.Properties = (properties ?? new AuthenticationProperties());
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00003E25 File Offset: 0x00002025
		// (set) Token: 0x06000082 RID: 130 RVA: 0x00003E2D File Offset: 0x0000202D
		public ClaimsIdentity Identity { get; private set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00003E36 File Offset: 0x00002036
		// (set) Token: 0x06000084 RID: 132 RVA: 0x00003E3E File Offset: 0x0000203E
		public AuthenticationProperties Properties { get; private set; }
	}
}
