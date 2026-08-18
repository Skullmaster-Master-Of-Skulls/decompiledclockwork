using System;
using Databases;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.OperationContexts;

namespace TechnoPro.Common.Core.ServiceProvidersOriginal
{
	// Token: 0x02000011 RID: 17
	public class ServiceProviderOriginalApplicationDAO : IServiceProviderOriginalApplicationDAO, IBaseOperationContext<ServiceProvidersOperationContext>
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002ED4 File Offset: 0x000010D4
		// (set) Token: 0x06000055 RID: 85 RVA: 0x00002EDC File Offset: 0x000010DC
		public DatabaseLayer DatabaseManager { get; private set; }

		// Token: 0x06000056 RID: 86 RVA: 0x00002EE5 File Offset: 0x000010E5
		public ServiceProviderOriginalApplicationDAO(ServiceProvidersOperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			ServiceProvidersOperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002F16 File Offset: 0x00001116
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00002F1E File Offset: 0x0000111E
		public ServiceProvidersOperationContext OpContext { get; set; }
	}
}
