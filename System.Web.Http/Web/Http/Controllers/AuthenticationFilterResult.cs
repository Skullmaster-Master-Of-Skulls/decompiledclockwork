using System;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace System.Web.Http.Controllers
{
	// Token: 0x0200002A RID: 42
	internal class AuthenticationFilterResult : IHttpActionResult
	{
		// Token: 0x06000105 RID: 261 RVA: 0x00006107 File Offset: 0x00004307
		public AuthenticationFilterResult(HttpActionContext context, ApiController controller, IAuthenticationFilter[] filters, IHttpActionResult innerResult)
		{
			this._context = context;
			this._controller = controller;
			this._filters = filters;
			this._innerResult = innerResult;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00006470 File Offset: 0x00004670
		public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			IHttpActionResult result = this._innerResult;
			IPrincipal originalPrincipal = this._controller.User;
			HttpAuthenticationContext authenticationContext = new HttpAuthenticationContext(this._context, originalPrincipal);
			for (int i = 0; i < this._filters.Length; i++)
			{
				IAuthenticationFilter filter = this._filters[i];
				await filter.AuthenticateAsync(authenticationContext, cancellationToken);
				IHttpActionResult error = authenticationContext.ErrorResult;
				if (error != null)
				{
					result = error;
					break;
				}
			}
			IPrincipal newPrincipal = authenticationContext.Principal;
			if (newPrincipal != originalPrincipal)
			{
				this._controller.User = newPrincipal;
			}
			HttpAuthenticationChallengeContext challengeContext = new HttpAuthenticationChallengeContext(this._context, result);
			for (int j = 0; j < this._filters.Length; j++)
			{
				IAuthenticationFilter filter2 = this._filters[j];
				await filter2.ChallengeAsync(challengeContext, cancellationToken);
			}
			result = challengeContext.Result;
			return await result.ExecuteAsync(cancellationToken);
		}

		// Token: 0x04000055 RID: 85
		private readonly HttpActionContext _context;

		// Token: 0x04000056 RID: 86
		private readonly ApiController _controller;

		// Token: 0x04000057 RID: 87
		private readonly IAuthenticationFilter[] _filters;

		// Token: 0x04000058 RID: 88
		private readonly IHttpActionResult _innerResult;
	}
}
