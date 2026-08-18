using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.Http.Results
{
	// Token: 0x0200005D RID: 93
	public class InternalServerErrorResult : IHttpActionResult
	{
		// Token: 0x060002A2 RID: 674 RVA: 0x000094A6 File Offset: 0x000076A6
		public InternalServerErrorResult(HttpRequestMessage request) : this(new StatusCodeResult.DirectDependencyProvider(request))
		{
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x000094B4 File Offset: 0x000076B4
		public InternalServerErrorResult(ApiController controller) : this(new StatusCodeResult.ApiControllerDependencyProvider(controller))
		{
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x000094C2 File Offset: 0x000076C2
		private InternalServerErrorResult(StatusCodeResult.IDependencyProvider dependencies)
		{
			this._dependencies = dependencies;
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x000094D1 File Offset: 0x000076D1
		public HttpRequestMessage Request
		{
			get
			{
				return this._dependencies.Request;
			}
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x000094DE File Offset: 0x000076DE
		public virtual Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			return Task.FromResult<HttpResponseMessage>(StatusCodeResult.Execute(HttpStatusCode.InternalServerError, this._dependencies.Request));
		}

		// Token: 0x040000BE RID: 190
		private readonly StatusCodeResult.IDependencyProvider _dependencies;
	}
}
