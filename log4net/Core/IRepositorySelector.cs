using System;
using System.Reflection;
using log4net.Repository;

namespace log4net.Core
{
	// Token: 0x02000057 RID: 87
	public interface IRepositorySelector
	{
		// Token: 0x060002DE RID: 734
		ILoggerRepository GetRepository(Assembly assembly);

		// Token: 0x060002DF RID: 735
		ILoggerRepository GetRepository(string repositoryName);

		// Token: 0x060002E0 RID: 736
		ILoggerRepository CreateRepository(Assembly assembly, Type repositoryType);

		// Token: 0x060002E1 RID: 737
		ILoggerRepository CreateRepository(string repositoryName, Type repositoryType);

		// Token: 0x060002E2 RID: 738
		bool ExistsRepository(string repositoryName);

		// Token: 0x060002E3 RID: 739
		ILoggerRepository[] GetAllRepositories();

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060002E4 RID: 740
		// (remove) Token: 0x060002E5 RID: 741
		event LoggerRepositoryCreationEventHandler LoggerRepositoryCreatedEvent;
	}
}
