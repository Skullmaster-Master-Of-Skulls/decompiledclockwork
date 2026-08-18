using System;
using System.Collections.Generic;
using System.Data;
using Databases;
using TechnoPro.Common.DAO.ClockWorkAudit;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.DAO.Impl.ClockWorkAudit
{
	// Token: 0x02000116 RID: 278
	public class ClockWorkAuditDAO : IClockWorkAuditDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060007F1 RID: 2033 RVA: 0x0005219C File Offset: 0x0005039C
		public ClockWorkAuditDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060007F2 RID: 2034 RVA: 0x000521AE File Offset: 0x000503AE
		// (set) Token: 0x060007F3 RID: 2035 RVA: 0x000521B6 File Offset: 0x000503B6
		public OperationContext OpContext { get; set; }

		// Token: 0x060007F4 RID: 2036 RVA: 0x000521C0 File Offset: 0x000503C0
		private string[] GetCurrentUserPrivileges(DatabaseLayer db)
		{
			List<string> list = new List<string>();
			using (IDataReader dataReader = db.ExecuteQueryReader("DECLARE @cuser varchar(max)\r\nSET @cuser=(SELECT TOP 1 CURRENT_USER)\r\n\r\nEXEC sp_helpuser @cuser"))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					return null;
				}
				while (dataReader.Read())
				{
					string item = dataReader["RoleName"].ToString().Trim();
					bool flag2 = !list.Contains(item);
					if (flag2)
					{
						list.Add(item);
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x00052258 File Offset: 0x00050458
		public string[] GetClockWorkDbUserPriviliges()
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			return this.GetCurrentUserPrivileges(databaseLayer);
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x0005228C File Offset: 0x0005048C
		public string[] GetFilesDbUserPriviliges()
		{
			DatabaseLayer clockWorkFiles = DatabaseLayerFactory.ClockWorkFiles;
			return this.GetCurrentUserPrivileges(clockWorkFiles);
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x000522AC File Offset: 0x000504AC
		public string[] GetDbPatchSqlUserPrivileges()
		{
			DatabaseLayer patchDatabaseLayer = DatabaseLayerFactory.GetPatchDatabaseLayer(eClockWorkServerInstanceName.ClockWorkServer.GetServerVirtualDirByInstanceName(), eDatabaseConnectionStringName.ClockWork);
			return this.GetCurrentUserPrivileges(patchDatabaseLayer);
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x000522D4 File Offset: 0x000504D4
		public bool AreFileDbAndRegularDbTheSameDatabase()
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DatabaseLayer clockWorkFiles = DatabaseLayerFactory.ClockWorkFiles;
			return databaseLayer.ConnectionString.Equals(clockWorkFiles.ConnectionString);
		}
	}
}
