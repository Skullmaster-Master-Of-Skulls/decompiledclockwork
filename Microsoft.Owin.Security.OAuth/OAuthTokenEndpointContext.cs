using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.Owin.Security.OAuth.Messages;
using Microsoft.Owin.Security.Provider;

namespace Microsoft.Owin.Security.OAuth
{
	// Token: 0x0200002C RID: 44
	public class OAuthTokenEndpointContext : EndpointContext<OAuthAuthorizationServerOptions>
	{
		// Token: 0x0600012D RID: 301 RVA: 0x00007354 File Offset: 0x00005554
		public OAuthTokenEndpointContext(IOwinContext context, OAuthAuthorizationServerOptions options, AuthenticationTicket ticket, TokenEndpointRequest tokenEndpointRequest) : base(context, options)
		{
			if (ticket == null)
			{
				throw new ArgumentNullException("ticket");
			}
			this.Identity = ticket.Identity;
			this.Properties = ticket.Properties;
			this.TokenEndpointRequest = tokenEndpointRequest;
			this.AdditionalResponseParameters = new Dictionary<string, object>(StringComparer.Ordinal);
			this.TokenIssued = (this.Identity != null);
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600012E RID: 302 RVA: 0x000073B9 File Offset: 0x000055B9
		// (set) Token: 0x0600012F RID: 303 RVA: 0x000073C1 File Offset: 0x000055C1
		public ClaimsIdentity Identity { get; private set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000130 RID: 304 RVA: 0x000073CA File Offset: 0x000055CA
		// (set) Token: 0x06000131 RID: 305 RVA: 0x000073D2 File Offset: 0x000055D2
		public AuthenticationProperties Properties { get; private set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000132 RID: 306 RVA: 0x000073DB File Offset: 0x000055DB
		// (set) Token: 0x06000133 RID: 307 RVA: 0x000073E3 File Offset: 0x000055E3
		public TokenEndpointRequest TokenEndpointRequest { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000134 RID: 308 RVA: 0x000073EC File Offset: 0x000055EC
		// (set) Token: 0x06000135 RID: 309 RVA: 0x000073F4 File Offset: 0x000055F4
		public bool TokenIssued { get; private set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000136 RID: 310 RVA: 0x000073FD File Offset: 0x000055FD
		// (set) Token: 0x06000137 RID: 311 RVA: 0x00007405 File Offset: 0x00005605
		public IDictionary<string, object> AdditionalResponseParameters { get; private set; }

		// Token: 0x06000138 RID: 312 RVA: 0x0000740E File Offset: 0x0000560E
		public void Issue(ClaimsIdentity identity, AuthenticationProperties properties)
		{
			this.Identity = identity;
			this.Properties = properties;
			this.TokenIssued = true;
		}
	}
}
