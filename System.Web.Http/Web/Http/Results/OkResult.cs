using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.Http.Results
{
	// Token: 0x0200005C RID: 92
	public class OkResult : IHttpActionResult
	{
		// Token: 0x0600029D RID: 669 RVA: 0x00009452 File Offset: 0x00007652
		public OkResult(HttpRequestMessage request) : this(new StatusCodeResult.DirectDependencyProvider(request))
		{
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00009460 File Offset: 0x00007660
		public OkResult(ApiController controller) : this(new StatusCodeResult.ApiControllerDependencyProvider(controller))
		{
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000946E File Offset: 0x0000766E
		private OkResult(StatusCodeResult.IDependencyProvider dependencies)
		{
			this._dependencies = dependencies;
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x0000947D File Offset: 0x0000767D
		public HttpRequestMessage Request
		{
			get
			{
				return this._dependencies.Request;
			}
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000948A File Offset: 0x0000768A
		public virtual Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			return Task.FromResult<HttpResponseMessage>(StatusCodeResult.Execute(HttpStatusCode.OK, this._dependencies.Request));
		}

		// Token: 0x040000BD RID: 189
		private readonly StatusCodeResult.IDependencyProvider _dependencies;
	}
}
