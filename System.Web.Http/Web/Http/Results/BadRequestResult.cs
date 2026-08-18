using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.Http.Results
{
	// Token: 0x0200004A RID: 74
	public class BadRequestResult : IHttpActionResult
	{
		// Token: 0x0600022C RID: 556 RVA: 0x00008788 File Offset: 0x00006988
		public BadRequestResult(HttpRequestMessage request) : this(new StatusCodeResult.DirectDependencyProvider(request))
		{
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00008796 File Offset: 0x00006996
		public BadRequestResult(ApiController controller) : this(new StatusCodeResult.ApiControllerDependencyProvider(controller))
		{
		}

		// Token: 0x0600022E RID: 558 RVA: 0x000087A4 File Offset: 0x000069A4
		private BadRequestResult(StatusCodeResult.IDependencyProvider dependencies)
		{
			this._dependencies = dependencies;
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600022F RID: 559 RVA: 0x000087B3 File Offset: 0x000069B3
		public HttpRequestMessage Request
		{
			get
			{
				return this._dependencies.Request;
			}
		}

		// Token: 0x06000230 RID: 560 RVA: 0x000087C0 File Offset: 0x000069C0
		public virtual Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			return Task.FromResult<HttpResponseMessage>(StatusCodeResult.Execute(HttpStatusCode.BadRequest, this._dependencies.Request));
		}

		// Token: 0x04000098 RID: 152
		private readonly StatusCodeResult.IDependencyProvider _dependencies;
	}
}
