using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.Http.Results
{
	// Token: 0x0200005E RID: 94
	public class NotFoundResult : IHttpActionResult
	{
		// Token: 0x060002A7 RID: 679 RVA: 0x000094FA File Offset: 0x000076FA
		public NotFoundResult(HttpRequestMessage request) : this(new StatusCodeResult.DirectDependencyProvider(request))
		{
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00009508 File Offset: 0x00007708
		public NotFoundResult(ApiController controller) : this(new StatusCodeResult.ApiControllerDependencyProvider(controller))
		{
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00009516 File Offset: 0x00007716
		private NotFoundResult(StatusCodeResult.IDependencyProvider dependencies)
		{
			this._dependencies = dependencies;
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060002AA RID: 682 RVA: 0x00009525 File Offset: 0x00007725
		public HttpRequestMessage Request
		{
			get
			{
				return this._dependencies.Request;
			}
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00009532 File Offset: 0x00007732
		public virtual Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			return Task.FromResult<HttpResponseMessage>(StatusCodeResult.Execute(HttpStatusCode.NotFound, this._dependencies.Request));
		}

		// Token: 0x040000BF RID: 191
		private readonly StatusCodeResult.IDependencyProvider _dependencies;
	}
}
