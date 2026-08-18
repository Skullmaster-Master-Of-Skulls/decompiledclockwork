using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.Http.ExceptionHandling
{
	// Token: 0x02000042 RID: 66
	public static class ExceptionLoggerExtensions
	{
		// Token: 0x06000182 RID: 386 RVA: 0x0000781C File Offset: 0x00005A1C
		public static Task LogAsync(this IExceptionLogger logger, ExceptionContext context, CancellationToken cancellationToken)
		{
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			ExceptionLoggerContext context2 = new ExceptionLoggerContext(context);
			return logger.LogAsync(context2, cancellationToken);
		}
	}
}
