using System;
using Databases;
using TechnoPro.Common.DAO.ServiceProvider;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.DAO.Impl.ServiceProvider
{
	// Token: 0x0200005A RID: 90
	public class ServiceProviderDAO : IServiceProviderDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600023A RID: 570 RVA: 0x00013505 File Offset: 0x00011705
		public ServiceProviderDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600023B RID: 571 RVA: 0x00013535 File Offset: 0x00011735
		// (set) Token: 0x0600023C RID: 572 RVA: 0x0001353D File Offset: 0x0001173D
		public OperationContext OpContext { get; set; }

		// Token: 0x040000E0 RID: 224
		public DatabaseLayer DatabaseManager;
	}
}
