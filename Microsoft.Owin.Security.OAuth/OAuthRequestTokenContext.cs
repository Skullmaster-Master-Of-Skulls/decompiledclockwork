using System;
using Microsoft.Owin.Security.Provider;

namespace Microsoft.Owin.Security.OAuth
{
	// Token: 0x02000029 RID: 41
	public class OAuthRequestTokenContext : BaseContext
	{
		// Token: 0x0600011C RID: 284 RVA: 0x000071A5 File Offset: 0x000053A5
		public OAuthRequestTokenContext(IOwinContext context, string token) : base(context)
		{
			this.Token = token;
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600011D RID: 285 RVA: 0x000071B5 File Offset: 0x000053B5
		// (set) Token: 0x0600011E RID: 286 RVA: 0x000071BD File Offset: 0x000053BD
		public string Token { get; set; }
	}
}
