using System;
using Databases;
using TechnoPro.Common.DAO.Institution;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.DAO.Impl.Institution
{
	// Token: 0x020000C8 RID: 200
	public class InstitutionDAO : IInstitutionDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000562 RID: 1378 RVA: 0x000342E0 File Offset: 0x000324E0
		// (set) Token: 0x06000563 RID: 1379 RVA: 0x000342E8 File Offset: 0x000324E8
		private DatabaseLayer DatabaseManager { get; set; }

		// Token: 0x06000564 RID: 1380 RVA: 0x000342F1 File Offset: 0x000324F1
		public InstitutionDAO()
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0003431A File Offset: 0x0003251A
		public InstitutionDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000566 RID: 1382 RVA: 0x0003434B File Offset: 0x0003254B
		// (set) Token: 0x06000567 RID: 1383 RVA: 0x00034353 File Offset: 0x00032553
		public OperationContext OpContext { get; set; }

		// Token: 0x06000568 RID: 1384 RVA: 0x0003435C File Offset: 0x0003255C
		public string GetInstitutionUniqueName()
		{
			object obj = this.DatabaseManager.ExecuteScalar("SELECT UniqueName FROM UniqueDatabaseName2()");
			return (obj != null) ? ((string)obj) : string.Empty;
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00034390 File Offset: 0x00032590
		public string GetInstitutionName()
		{
			object obj = this.DatabaseManager.ExecuteScalar("select settingstringvalue from SettingsGroups where groupid = -1 and settingcode = 312");
			return (obj != null) ? ((string)obj) : string.Empty;
		}
	}
}
