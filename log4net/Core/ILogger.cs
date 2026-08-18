using System;
using log4net.Repository;

namespace log4net.Core
{
	// Token: 0x0200005F RID: 95
	public interface ILogger
	{
		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000312 RID: 786
		string Name { get; }

		// Token: 0x06000313 RID: 787
		void Log(Type callerStackBoundaryDeclaringType, Level level, object message, Exception exception);

		// Token: 0x06000314 RID: 788
		void Log(LoggingEvent logEvent);

		// Token: 0x06000315 RID: 789
		bool IsEnabledFor(Level level);

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000316 RID: 790
		ILoggerRepository Repository { get; }
	}
}
