using System;
using Google.Apis.Logging;

namespace Google
{
	// Token: 0x02000002 RID: 2
	public static class ApplicationContext
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static ILogger Logger
		{
			get
			{
				ILogger result;
				if ((result = ApplicationContext.logger) == null)
				{
					result = (ApplicationContext.logger = new NullLogger());
				}
				return result;
			}
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002066 File Offset: 0x00000266
		public static void RegisterLogger(ILogger loggerToRegister)
		{
			if (ApplicationContext.logger != null && !(ApplicationContext.logger is NullLogger))
			{
				throw new InvalidOperationException("A logger was already registered with this context.");
			}
			ApplicationContext.logger = loggerToRegister;
		}

		// Token: 0x04000001 RID: 1
		private static ILogger logger;
	}
}
