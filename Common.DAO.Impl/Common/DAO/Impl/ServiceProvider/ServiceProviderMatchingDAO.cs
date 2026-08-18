using System;
using Databases;
using TechnoPro.Common.DAO.ServiceProvider;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.DAO.Impl.ServiceProvider
{
	// Token: 0x0200005C RID: 92
	public class ServiceProviderMatchingDAO : IServiceProviderMatchingDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000252 RID: 594 RVA: 0x00013B24 File Offset: 0x00011D24
		public ServiceProviderMatchingDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000253 RID: 595 RVA: 0x00013B54 File Offset: 0x00011D54
		// (set) Token: 0x06000254 RID: 596 RVA: 0x00013B5C File Offset: 0x00011D5C
		public OperationContext OpContext { get; set; }

		// Token: 0x040000E4 RID: 228
		public DatabaseLayer DatabaseManager;
	}
}
