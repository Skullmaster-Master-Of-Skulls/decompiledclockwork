using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Web.Http.Owin.Properties;
using Microsoft.Owin.Security;

namespace System.Web.Http
{
	// Token: 0x02000011 RID: 17
	public class HostAuthenticationFilter : IAuthenticationFilter, IFilter
	{
		// Token: 0x06000079 RID: 121 RVA: 0x00003192 File Offset: 0x00001392
		public HostAuthenticationFilter(string authenticationType)
		{
			if (authenticationType == null)
			{
				throw new ArgumentNullException("authenticationType");
			}
			this._authenticationType = authenticationType;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600007A RID: 122 RVA: 0x000031AF File Offset: 0x000013AF
		public string AuthenticationType
		{
			get
			{
				return this._authenticationType;
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000332C File Offset: 0x0000152C
		public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			HttpRequestMessage request = context.Request;
			if (request == null)
			{
				throw new InvalidOperationException(OwinResources.HttpAuthenticationContext_RequestMustNotBeNull);
			}
			IAuthenticationManager authenticationManager = HostAuthenticationFilter.GetAuthenticationManagerOrThrow(request);
			cancellationToken.ThrowIfCancellationRequested();
			AuthenticateResult result = await authenticationManager.AuthenticateAsync(this._authenticationType);
			if (result != null)
			{
				IIdentity identity = result.Identity;
				if (identity != null)
				{
					context.Principal = new ClaimsPrincipal(identity);
				}
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003384 File Offset: 0x00001584
		public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			HttpRequestMessage request = context.Request;
			if (request == null)
			{
				throw new InvalidOperationException(OwinResources.HttpAuthenticationChallengeContext_RequestMustNotBeNull);
			}
			IAuthenticationManager authenticationManagerOrThrow = HostAuthenticationFilter.GetAuthenticationManagerOrThrow(request);
			authenticationManagerOrThrow.AuthenticationResponseChallenge = HostAuthenticationFilter.AddChallengeAuthenticationType(authenticationManagerOrThrow.AuthenticationResponseChallenge, this._authenticationType);
			return TaskHelpers.Completed();
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600007D RID: 125 RVA: 0x000033D7 File Offset: 0x000015D7
		public bool AllowMultiple
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000033DC File Offset: 0x000015DC
		private static AuthenticationResponseChallenge AddChallengeAuthenticationType(AuthenticationResponseChallenge challenge, string authenticationType)
		{
			List<string> list = new List<string>();
			AuthenticationProperties properties;
			if (challenge != null)
			{
				string[] authenticationTypes = challenge.AuthenticationTypes;
				if (authenticationTypes != null)
				{
					list.AddRange(authenticationTypes);
				}
				properties = challenge.Properties;
			}
			else
			{
				properties = new AuthenticationProperties();
			}
			list.Add(authenticationType);
			return new AuthenticationResponseChallenge(list.ToArray(), properties);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003428 File Offset: 0x00001628
		private static IAuthenticationManager GetAuthenticationManagerOrThrow(HttpRequestMessage request)
		{
			IAuthenticationManager authenticationManager = request.GetAuthenticationManager();
			if (authenticationManager == null)
			{
				throw new InvalidOperationException(OwinResources.IAuthenticationManagerNotAvailable);
			}
			return authenticationManager;
		}

		// Token: 0x04000014 RID: 20
		private readonly string _authenticationType;
	}
}
