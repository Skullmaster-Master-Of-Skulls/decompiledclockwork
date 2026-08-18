using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.Http.Filters
{
	// Token: 0x020000F2 RID: 242
	public interface IExceptionFilter : IFilter
	{
		// Token: 0x06000607 RID: 1543
		Task ExecuteExceptionFilterAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken);
	}
}
