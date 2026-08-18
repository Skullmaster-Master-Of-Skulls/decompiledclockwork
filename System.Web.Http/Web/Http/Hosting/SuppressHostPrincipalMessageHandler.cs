using System;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Properties;

namespace System.Web.Http.Hosting
{
	// Token: 0x02000063 RID: 99
	public class SuppressHostPrincipalMessageHandler : DelegatingHandler
	{
		// Token: 0x060002D3 RID: 723 RVA: 0x00009934 File Offset: 0x00007B34
		protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			SuppressHostPrincipalMessageHandler.SetCurrentPrincipalToAnonymous(request);
			return base.SendAsync(request, cancellationToken);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00009954 File Offset: 0x00007B54
		private static void SetCurrentPrincipalToAnonymous(HttpRequestMessage request)
		{
			HttpRequestContext requestContext = request.GetRequestContext();
			if (requestContext == null)
			{
				throw new ArgumentException(SRResources.Request_RequestContextMustNotBeNull, "request");
			}
			requestContext.Principal = SuppressHostPrincipalMessageHandler._anonymousPrincipal.Value;
		}

		// Token: 0x040000CC RID: 204
		private static readonly Lazy<IPrincipal> _anonymousPrincipal = new Lazy<IPrincipal>(() => new ClaimsPrincipal(new ClaimsIdentity()), true);
	}
}
