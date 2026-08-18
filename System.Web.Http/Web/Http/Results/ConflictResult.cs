using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.Http.Results
{
	// Token: 0x02000056 RID: 86
	public class ConflictResult : IHttpActionResult
	{
		// Token: 0x06000278 RID: 632 RVA: 0x00009094 File Offset: 0x00007294
		public ConflictResult(HttpRequestMessage request) : this(new StatusCodeResult.DirectDependencyProvider(request))
		{
		}

		// Token: 0x06000279 RID: 633 RVA: 0x000090A2 File Offset: 0x000072A2
		public ConflictResult(ApiController controller) : this(new StatusCodeResult.ApiControllerDependencyProvider(controller))
		{
		}

		// Token: 0x0600027A RID: 634 RVA: 0x000090B0 File Offset: 0x000072B0
		private ConflictResult(StatusCodeResult.IDependencyProvider dependencies)
		{
			this._dependencies = dependencies;
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x0600027B RID: 635 RVA: 0x000090BF File Offset: 0x000072BF
		public HttpRequestMessage Request
		{
			get
			{
				return this._dependencies.Request;
			}
		}

		// Token: 0x0600027C RID: 636 RVA: 0x000090CC File Offset: 0x000072CC
		public virtual Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			return Task.FromResult<HttpResponseMessage>(StatusCodeResult.Execute(HttpStatusCode.Conflict, this._dependencies.Request));
		}

		// Token: 0x040000B2 RID: 178
		private readonly StatusCodeResult.IDependencyProvider _dependencies;
	}
}
