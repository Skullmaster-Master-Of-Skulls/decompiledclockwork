using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;

namespace System.Web.Http.Owin.ExceptionHandling
{
	// Token: 0x0200000C RID: 12
	internal class EmptyExceptionLogger : IExceptionLogger
	{
		// Token: 0x0600005F RID: 95 RVA: 0x00002FE3 File Offset: 0x000011E3
		public Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
		{
			return TaskHelpers.Completed();
		}
	}
}
