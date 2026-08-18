using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.Http.ExceptionHandling
{
	// Token: 0x0200003D RID: 61
	public interface IExceptionHandler
	{
		// Token: 0x0600015F RID: 351
		Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken);
	}
}
