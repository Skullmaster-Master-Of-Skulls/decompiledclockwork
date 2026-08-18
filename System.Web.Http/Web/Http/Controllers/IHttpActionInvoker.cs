using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.Http.Controllers
{
	// Token: 0x020000E1 RID: 225
	public interface IHttpActionInvoker
	{
		// Token: 0x06000583 RID: 1411
		Task<HttpResponseMessage> InvokeActionAsync(HttpActionContext actionContext, CancellationToken cancellationToken);
	}
}
