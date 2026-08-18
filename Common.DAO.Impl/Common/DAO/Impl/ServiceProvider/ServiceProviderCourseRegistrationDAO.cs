using System;
using Databases;
using TechnoPro.Common.DAO.ServiceProvider;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.DAO.Impl.ServiceProvider
{
	// Token: 0x02000059 RID: 89
	public class ServiceProviderCourseRegistrationDAO : IServiceProviderCourseRegistrationDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000237 RID: 567 RVA: 0x000134C4 File Offset: 0x000116C4
		public ServiceProviderCourseRegistrationDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000238 RID: 568 RVA: 0x000134F4 File Offset: 0x000116F4
		// (set) Token: 0x06000239 RID: 569 RVA: 0x000134FC File Offset: 0x000116FC
		public OperationContext OpContext { get; set; }

		// Token: 0x040000DE RID: 222
		public DatabaseLayer DatabaseManager;
	}
}
