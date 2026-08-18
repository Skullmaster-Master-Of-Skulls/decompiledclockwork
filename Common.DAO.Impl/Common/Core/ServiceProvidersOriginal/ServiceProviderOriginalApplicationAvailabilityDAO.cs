using System;
using Databases;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.OperationContexts;

namespace TechnoPro.Common.Core.ServiceProvidersOriginal
{
	// Token: 0x0200000F RID: 15
	public class ServiceProviderOriginalApplicationAvailabilityDAO : IServiceProviderOriginalApplicationAvailabilityDAO, IBaseOperationContext<ServiceProvidersOperationContext>
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002D21 File Offset: 0x00000F21
		// (set) Token: 0x0600004A RID: 74 RVA: 0x00002D29 File Offset: 0x00000F29
		public DatabaseLayer DatabaseManager { get; private set; }

		// Token: 0x0600004B RID: 75 RVA: 0x00002D32 File Offset: 0x00000F32
		public ServiceProviderOriginalApplicationAvailabilityDAO(ServiceProvidersOperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			ServiceProvidersOperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002D63 File Offset: 0x00000F63
		// (set) Token: 0x0600004D RID: 77 RVA: 0x00002D6B File Offset: 0x00000F6B
		public ServiceProvidersOperationContext OpContext { get; set; }
	}
}
