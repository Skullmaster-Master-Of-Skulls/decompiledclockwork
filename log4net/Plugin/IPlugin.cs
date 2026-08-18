using System;
using log4net.Repository;

namespace log4net.Plugin
{
	// Token: 0x020000B9 RID: 185
	public interface IPlugin
	{
		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600053F RID: 1343
		string Name { get; }

		// Token: 0x06000540 RID: 1344
		void Attach(ILoggerRepository repository);

		// Token: 0x06000541 RID: 1345
		void Shutdown();
	}
}
