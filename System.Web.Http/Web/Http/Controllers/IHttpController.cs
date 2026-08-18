using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.Http.Controllers
{
	// Token: 0x0200012D RID: 301
	public interface IHttpController
	{
		// Token: 0x06000774 RID: 1908
		Task<HttpResponseMessage> ExecuteAsync(HttpControllerContext controllerContext, CancellationToken cancellationToken);
	}
}
