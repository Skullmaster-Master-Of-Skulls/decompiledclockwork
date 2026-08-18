using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.Http.ExceptionHandling
{
	// Token: 0x02000046 RID: 70
	internal class EmptyExceptionHandler : IExceptionHandler
	{
		// Token: 0x06000195 RID: 405 RVA: 0x00007A43 File Offset: 0x00005C43
		public Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
		{
			return TaskHelpers.Completed();
		}
	}
}
