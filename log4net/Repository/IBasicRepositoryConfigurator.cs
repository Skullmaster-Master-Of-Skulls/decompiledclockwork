using System;
using log4net.Appender;

namespace log4net.Repository
{
	// Token: 0x020000CB RID: 203
	public interface IBasicRepositoryConfigurator
	{
		// Token: 0x0600060A RID: 1546
		void Configure(IAppender appender);

		// Token: 0x0600060B RID: 1547
		void Configure(params IAppender[] appenders);
	}
}
