using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.Http.ExceptionHandling
{
	// Token: 0x02000038 RID: 56
	public interface IExceptionLogger
	{
		// Token: 0x0600014E RID: 334
		Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken);
	}
}
