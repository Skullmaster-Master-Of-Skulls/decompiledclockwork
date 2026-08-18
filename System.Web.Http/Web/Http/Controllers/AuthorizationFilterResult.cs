using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace System.Web.Http.Controllers
{
	// Token: 0x0200002B RID: 43
	internal class AuthorizationFilterResult : IHttpActionResult
	{
		// Token: 0x06000107 RID: 263 RVA: 0x000064BE File Offset: 0x000046BE
		public AuthorizationFilterResult(HttpActionContext context, IAuthorizationFilter[] filters, IHttpActionResult innerResult)
		{
			this._context = context;
			this._filters = filters;
			this._innerResult = innerResult;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00006568 File Offset: 0x00004768
		public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			Func<Task<HttpResponseMessage>> func = () => this._innerResult.ExecuteAsync(cancellationToken);
			for (int i = this._filters.Length - 1; i >= 0; i--)
			{
				IAuthorizationFilter arg = this._filters[i];
				Func<Func<Task<HttpResponseMessage>>, IAuthorizationFilter, Func<Task<HttpResponseMessage>>> func2 = (Func<Task<HttpResponseMessage>> continuation, IAuthorizationFilter innerFilter) => () => innerFilter.ExecuteAuthorizationFilterAsync(this._context, cancellationToken, continuation);
				func = func2(func, arg);
			}
			return func();
		}

		// Token: 0x04000059 RID: 89
		private readonly HttpActionContext _context;

		// Token: 0x0400005A RID: 90
		private readonly IAuthorizationFilter[] _filters;

		// Token: 0x0400005B RID: 91
		private readonly IHttpActionResult _innerResult;
	}
}
