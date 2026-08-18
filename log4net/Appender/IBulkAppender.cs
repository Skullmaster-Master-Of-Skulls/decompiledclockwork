using System;
using log4net.Core;

namespace log4net.Appender
{
	// Token: 0x02000003 RID: 3
	public interface IBulkAppender : IAppender
	{
		// Token: 0x06000005 RID: 5
		void DoAppend(LoggingEvent[] loggingEvents);
	}
}
