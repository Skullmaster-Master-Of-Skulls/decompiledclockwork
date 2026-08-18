using System;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Owin.Properties;
using Microsoft.Owin.Security;

namespace System.Web.Http.Owin
{
	// Token: 0x02000019 RID: 25
	public class PassiveAuthenticationMessageHandler : DelegatingHandler
	{
		// Token: 0x060000B5 RID: 181 RVA: 0x0000511C File Offset: 0x0000331C
		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			PassiveAuthenticationMessageHandler.SetCurrentPrincipalToAnonymous(request);
			HttpResponseMessage response = await base.SendAsync(request, cancellationToken);
			PassiveAuthenticationMessageHandler.SuppressDefaultAuthenticationChallenges(request);
			return response;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00005174 File Offset: 0x00003374
		private static void SetCurrentPrincipalToAnonymous(HttpRequestMessage request)
		{
			HttpRequestContext requestContext = request.GetRequestContext();
			if (requestContext == null)
			{
				throw new ArgumentException(OwinResources.Request_RequestContextMustNotBeNull, "request");
			}
			requestContext.Principal = PassiveAuthenticationMessageHandler._anonymousPrincipal.Value;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000051AC File Offset: 0x000033AC
		private static void SuppressDefaultAuthenticationChallenges(HttpRequestMessage request)
		{
			IAuthenticationManager authenticationManager = request.GetAuthenticationManager();
			if (authenticationManager == null)
			{
				throw new InvalidOperationException(OwinResources.IAuthenticationManagerNotAvailable);
			}
			AuthenticationResponseChallenge authenticationResponseChallenge = authenticationManager.AuthenticationResponseChallenge;
			string[] array = new string[1];
			string[] authenticationTypes = array;
			if (authenticationResponseChallenge == null)
			{
				authenticationManager.AuthenticationResponseChallenge = new AuthenticationResponseChallenge(authenticationTypes, new AuthenticationProperties());
				return;
			}
			if (authenticationResponseChallenge.AuthenticationTypes == null || authenticationResponseChallenge.AuthenticationTypes.Length == 0)
			{
				authenticationManager.AuthenticationResponseChallenge = new AuthenticationResponseChallenge(authenticationTypes, authenticationResponseChallenge.Properties);
			}
		}

		// Token: 0x0400002F RID: 47
		private static readonly Lazy<IPrincipal> _anonymousPrincipal = new Lazy<IPrincipal>(() => new ClaimsPrincipal(new ClaimsIdentity()), true);
	}
}
