using System;
using Microsoft.Owin.Logging;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.Infrastructure;
using Owin;

namespace Microsoft.Owin.Security.OAuth
{
	// Token: 0x02000010 RID: 16
	public class OAuthBearerAuthenticationMiddleware : AuthenticationMiddleware<OAuthBearerAuthenticationOptions>
	{
		// Token: 0x06000078 RID: 120 RVA: 0x000066A8 File Offset: 0x000048A8
		public OAuthBearerAuthenticationMiddleware(OwinMiddleware next, IAppBuilder app, OAuthBearerAuthenticationOptions options) : base(next, options)
		{
			this._logger = app.CreateLogger<OAuthBearerAuthenticationMiddleware>();
			if (!string.IsNullOrWhiteSpace(base.Options.Challenge))
			{
				this._challenge = base.Options.Challenge;
			}
			else if (string.IsNullOrWhiteSpace(base.Options.Realm))
			{
				this._challenge = "Bearer";
			}
			else
			{
				this._challenge = "Bearer realm=\"" + base.Options.Realm + "\"";
			}
			if (base.Options.Provider == null)
			{
				base.Options.Provider = new OAuthBearerAuthenticationProvider();
			}
			if (base.Options.AccessTokenFormat == null)
			{
				IDataProtector protector = app.CreateDataProtector(new string[]
				{
					typeof(OAuthBearerAuthenticationMiddleware).Namespace,
					"Access_Token",
					"v1"
				});
				base.Options.AccessTokenFormat = new TicketDataFormat(protector);
			}
			if (base.Options.AccessTokenProvider == null)
			{
				base.Options.AccessTokenProvider = new AuthenticationTokenProvider();
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000067B6 File Offset: 0x000049B6
		protected override AuthenticationHandler<OAuthBearerAuthenticationOptions> CreateHandler()
		{
			return new OAuthBearerAuthenticationHandler(this._logger, this._challenge);
		}

		// Token: 0x04000030 RID: 48
		private readonly ILogger _logger;

		// Token: 0x04000031 RID: 49
		private readonly string _challenge;
	}
}
