using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.Owin.Security.OAuth.Messages;
using Microsoft.Owin.Security.Provider;

namespace Microsoft.Owin.Security.OAuth
{
	// Token: 0x02000020 RID: 32
	public class OAuthAuthorizationEndpointResponseContext : EndpointContext<OAuthAuthorizationServerOptions>
	{
		// Token: 0x060000B2 RID: 178 RVA: 0x00006BA0 File Offset: 0x00004DA0
		public OAuthAuthorizationEndpointResponseContext(IOwinContext context, OAuthAuthorizationServerOptions options, AuthenticationTicket ticket, AuthorizeEndpointRequest authorizeEndpointRequest, string accessToken, string authorizationCode) : base(context, options)
		{
			if (ticket == null)
			{
				throw new ArgumentNullException("ticket");
			}
			this.Identity = ticket.Identity;
			this.Properties = ticket.Properties;
			this.AuthorizeEndpointRequest = authorizeEndpointRequest;
			this.AdditionalResponseParameters = new Dictionary<string, object>(StringComparer.Ordinal);
			this.AccessToken = accessToken;
			this.AuthorizationCode = authorizationCode;
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00006C03 File Offset: 0x00004E03
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x00006C0B File Offset: 0x00004E0B
		public ClaimsIdentity Identity { get; private set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00006C14 File Offset: 0x00004E14
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x00006C1C File Offset: 0x00004E1C
		public AuthenticationProperties Properties { get; private set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00006C25 File Offset: 0x00004E25
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x00006C2D File Offset: 0x00004E2D
		public AuthorizeEndpointRequest AuthorizeEndpointRequest { get; private set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00006C36 File Offset: 0x00004E36
		// (set) Token: 0x060000BA RID: 186 RVA: 0x00006C3E File Offset: 0x00004E3E
		public IDictionary<string, object> AdditionalResponseParameters { get; private set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00006C47 File Offset: 0x00004E47
		// (set) Token: 0x060000BC RID: 188 RVA: 0x00006C4F File Offset: 0x00004E4F
		public string AccessToken { get; private set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00006C58 File Offset: 0x00004E58
		// (set) Token: 0x060000BE RID: 190 RVA: 0x00006C60 File Offset: 0x00004E60
		public string AuthorizationCode { get; private set; }
	}
}
