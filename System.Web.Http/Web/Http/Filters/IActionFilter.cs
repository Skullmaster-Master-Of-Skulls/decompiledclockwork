using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;

namespace System.Web.Http.Filters
{
	// Token: 0x020000F0 RID: 240
	public interface IActionFilter : IFilter
	{
		// Token: 0x060005FE RID: 1534
		Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation);
	}
}
