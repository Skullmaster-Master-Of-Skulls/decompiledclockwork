using System;
using System.Collections.Generic;
using System.Web.Http.Controllers;

namespace System.Web.Http.ExceptionHandling
{
	// Token: 0x02000043 RID: 67
	public static class ExceptionServices
	{
		// Token: 0x06000183 RID: 387 RVA: 0x00007854 File Offset: 0x00005A54
		public static IExceptionLogger GetLogger(HttpConfiguration configuration)
		{
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			ServicesContainer services = configuration.Services;
			return ExceptionServices.GetLogger(services);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000787C File Offset: 0x00005A7C
		public static IExceptionLogger GetLogger(ServicesContainer services)
		{
			if (services == null)
			{
				throw new ArgumentNullException("services");
			}
			Lazy<IExceptionLogger> exceptionServicesLogger = services.ExceptionServicesLogger;
			return exceptionServicesLogger.Value;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x000078A4 File Offset: 0x00005AA4
		internal static IExceptionLogger CreateLogger(ServicesContainer services)
		{
			IEnumerable<IExceptionLogger> exceptionLoggers = services.GetExceptionLoggers();
			return new CompositeExceptionLogger(exceptionLoggers);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x000078C0 File Offset: 0x00005AC0
		public static IExceptionHandler GetHandler(HttpConfiguration configuration)
		{
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			ServicesContainer services = configuration.Services;
			return ExceptionServices.GetHandler(services);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x000078E8 File Offset: 0x00005AE8
		public static IExceptionHandler GetHandler(ServicesContainer services)
		{
			if (services == null)
			{
				throw new ArgumentNullException("services");
			}
			Lazy<IExceptionHandler> exceptionServicesHandler = services.ExceptionServicesHandler;
			return exceptionServicesHandler.Value;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00007910 File Offset: 0x00005B10
		internal static IExceptionHandler CreateHandler(ServicesContainer services)
		{
			IExceptionHandler innerHandler = services.GetExceptionHandler() ?? new EmptyExceptionHandler();
			return new LastChanceExceptionHandler(innerHandler);
		}
	}
}
