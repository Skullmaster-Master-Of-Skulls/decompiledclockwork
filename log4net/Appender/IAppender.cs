using System;
using log4net.Core;

namespace log4net.Appender
{
	// Token: 0x02000002 RID: 2
	public interface IAppender
	{
		// Token: 0x06000001 RID: 1
		void Close();

		// Token: 0x06000002 RID: 2
		void DoAppend(LoggingEvent loggingEvent);

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3
		// (set) Token: 0x06000004 RID: 4
		string Name { get; set; }
	}
}
