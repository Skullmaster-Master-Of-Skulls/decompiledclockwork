using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.Owin.Security.OAuth.Messages;
using Microsoft.Owin.Security.Provider;

namespace Microsoft.Owin.Security.OAuth
{
	// Token: 0x0200001B RID: 27
	public class OAuthTokenEndpointResponseContext : EndpointContext<OAuthAuthorizationServerOptions>
	{
		// Token: 0x06000087 RID: 135 RVA: 0x00006848 File Offset: 0x00004A48
		public OAuthTokenEndpointResponseContext(IOwinContext context, OAuthAuthorizationServerOptions options, AuthenticationTicket ticket, TokenEndpointRequest tokenEndpointRequest, string accessToken, IDictionary<string, object> additionalResponseParameters) : base(context, options)
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
			this.AccessToken = accessToken;
			this.AdditionalResponseParameters = additionalResponseParameters;
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000088 RID: 136 RVA: 0x000068BD File Offset: 0x00004ABD
		// (set) Token: 0x06000089 RID: 137 RVA: 0x000068C5 File Offset: 0x00004AC5
		public ClaimsIdentity Identity { get; private set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600008A RID: 138 RVA: 0x000068CE File Offset: 0x00004ACE
		// (set) Token: 0x0600008B RID: 139 RVA: 0x000068D6 File Offset: 0x00004AD6
		public AuthenticationProperties Properties { get; private set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600008C RID: 140 RVA: 0x000068DF File Offset: 0x00004ADF
		// (set) Token: 0x0600008D RID: 141 RVA: 0x000068E7 File Offset: 0x00004AE7
		public string AccessToken { get; private set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600008E RID: 142 RVA: 0x000068F0 File Offset: 0x00004AF0
		// (set) Token: 0x0600008F RID: 143 RVA: 0x000068F8 File Offset: 0x00004AF8
		public TokenEndpointRequest TokenEndpointRequest { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00006901 File Offset: 0x00004B01
		// (set) Token: 0x06000091 RID: 145 RVA: 0x00006909 File Offset: 0x00004B09
		public bool TokenIssued { get; private set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00006912 File Offset: 0x00004B12
		// (set) Token: 0x06000093 RID: 147 RVA: 0x0000691A File Offset: 0x00004B1A
		public IDictionary<string, object> AdditionalResponseParameters { get; private set; }

		// Token: 0x06000094 RID: 148 RVA: 0x00006923 File Offset: 0x00004B23
		public void Issue(ClaimsIdentity identity, AuthenticationProperties properties)
		{
			this.Identity = identity;
			this.Properties = properties;
			this.TokenIssued = true;
		}
	}
}
