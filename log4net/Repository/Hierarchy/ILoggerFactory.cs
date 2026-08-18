using System;

namespace log4net.Repository.Hierarchy
{
	// Token: 0x020000C3 RID: 195
	public interface ILoggerFactory
	{
		// Token: 0x060005A2 RID: 1442
		Logger CreateLogger(ILoggerRepository repository, string name);
	}
}
