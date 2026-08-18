using System;
using System.Threading.Tasks;
using Microsoft.Owin.Logging;
using Microsoft.Owin.Security.Infrastructure;

namespace Microsoft.Owin.Security.OAuth
{
	// Token: 0x0200000E RID: 14
	internal class OAuthBearerAuthenticationHandler : AuthenticationHandler<OAuthBearerAuthenticationOptions>
	{
		// Token: 0x06000074 RID: 116 RVA: 0x0000610D File Offset: 0x0000430D
		public OAuthBearerAuthenticationHandler(ILogger logger, string challenge)
		{
			this._logger = logger;
			this._challenge = challenge;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000065A0 File Offset: 0x000047A0
		protected override async Task<AuthenticationTicket> AuthenticateCoreAsync()
		{
			AuthenticationTicket result;
			try
			{
				string requestToken = null;
				string authorization = base.Request.Headers.Get("Authorization");
				if (!string.IsNullOrEmpty(authorization) && authorization.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
				{
					requestToken = authorization.Substring("Bearer ".Length).Trim();
				}
				OAuthRequestTokenContext requestTokenContext = new OAuthRequestTokenContext(base.Context, requestToken);
				await base.Options.Provider.RequestToken(requestTokenContext);
				if (string.IsNullOrEmpty(requestTokenContext.Token))
				{
					result = null;
				}
				else
				{
					AuthenticationTokenReceiveContext tokenReceiveContext = new AuthenticationTokenReceiveContext(base.Context, base.Options.AccessTokenFormat, requestTokenContext.Token);
					await base.Options.AccessTokenProvider.ReceiveAsync(tokenReceiveContext);
					if (tokenReceiveContext.Ticket == null)
					{
						tokenReceiveContext.DeserializeTicket(tokenReceiveContext.Token);
					}
					AuthenticationTicket ticket = tokenReceiveContext.Ticket;
					if (ticket == null)
					{
						this._logger.WriteWarning("invalid bearer token received", new string[0]);
						result = null;
					}
					else
					{
						DateTimeOffset currentUtc = base.Options.SystemClock.UtcNow;
						if (ticket.Properties.ExpiresUtc != null && ticket.Properties.ExpiresUtc.Value < currentUtc)
						{
							this._logger.WriteWarning("expired bearer token received", new string[0]);
							result = null;
						}
						else
						{
							OAuthValidateIdentityContext context = new OAuthValidateIdentityContext(base.Context, base.Options, ticket);
							if (ticket != null && ticket.Identity != null && ticket.Identity.IsAuthenticated)
							{
								context.Validated();
							}
							if (base.Options.Provider != null)
							{
								await base.Options.Provider.ValidateIdentity(context);
							}
							if (!context.IsValidated)
							{
								result = null;
							}
							else
							{
								result = context.Ticket;
							}
						}
					}
				}
			}
			catch (Exception error)
			{
				this._logger.WriteError("Authentication failed", error);
				result = null;
			}
			return result;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000065E8 File Offset: 0x000047E8
		protected override Task ApplyResponseChallengeAsync()
		{
			if (base.Response.StatusCode != 401)
			{
				return Task.FromResult<object>(null);
			}
			AuthenticationResponseChallenge authenticationResponseChallenge = base.Helper.LookupChallenge(base.Options.AuthenticationType, base.Options.AuthenticationMode);
			if (authenticationResponseChallenge != null)
			{
				OAuthChallengeContext context = new OAuthChallengeContext(base.Context, this._challenge);
				base.Options.Provider.ApplyChallenge(context);
			}
			return Task.FromResult<object>(null);
		}

		// Token: 0x0400002E RID: 46
		private readonly ILogger _logger;

		// Token: 0x0400002F RID: 47
		private readonly string _challenge;
	}
}
