using System;
using Databases;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.OperationContexts;

namespace TechnoPro.Common.Core.ServiceProvidersOriginal
{
	// Token: 0x0200000E RID: 14
	public class ServiceProviderApplicationStatusLookupDAO : IServiceProviderApplicationStatusLookupDAO, IBaseOperationContext<ServiceProvidersOperationContext>
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002CCE File Offset: 0x00000ECE
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00002CD6 File Offset: 0x00000ED6
		public DatabaseLayer DatabaseManager { get; private set; }

		// Token: 0x06000046 RID: 70 RVA: 0x00002CDF File Offset: 0x00000EDF
		public ServiceProviderApplicationStatusLookupDAO(ServiceProvidersOperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			ServiceProvidersOperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002D10 File Offset: 0x00000F10
		// (set) Token: 0x06000048 RID: 72 RVA: 0x00002D18 File Offset: 0x00000F18
		public ServiceProvidersOperationContext OpContext { get; set; }
	}
}
