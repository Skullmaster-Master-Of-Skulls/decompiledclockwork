using System;
using System.Security.Claims;

namespace Microsoft.Owin.Security.Provider
{
	// Token: 0x02000033 RID: 51
	public abstract class ReturnEndpointContext : EndpointContext
	{
		// Token: 0x060000D3 RID: 211 RVA: 0x000047AE File Offset: 0x000029AE
		protected ReturnEndpointContext(IOwinContext context, AuthenticationTicket ticket) : base(context)
		{
			if (ticket != null)
			{
				this.Identity = ticket.Identity;
				this.Properties = ticket.Properties;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x000047D2 File Offset: 0x000029D2
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x000047DA File Offset: 0x000029DA
		public ClaimsIdentity Identity { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x000047E3 File Offset: 0x000029E3
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x000047EB File Offset: 0x000029EB
		public AuthenticationProperties Properties { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x000047F4 File Offset: 0x000029F4
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x000047FC File Offset: 0x000029FC
		public string SignInAsAuthenticationType { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00004805 File Offset: 0x00002A05
		// (set) Token: 0x060000DB RID: 219 RVA: 0x0000480D File Offset: 0x00002A0D
		public string RedirectUri { get; set; }
	}
}
