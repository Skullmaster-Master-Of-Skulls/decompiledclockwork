using System;
using System.Data;
using System.Data.Common;
using Databases;
using TechnoPro.Common.DAO.Settings;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.DAO.Impl.Settings
{
	// Token: 0x02000046 RID: 70
	public class MiscTableSettingsDAO : IMiscTableSettingsDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x000115BC File Offset: 0x0000F7BC
		// (set) Token: 0x060001E6 RID: 486 RVA: 0x000115C4 File Offset: 0x0000F7C4
		public DatabaseLayer DatabaseManager { get; private set; }

		// Token: 0x060001E7 RID: 487 RVA: 0x000115CD File Offset: 0x0000F7CD
		public MiscTableSettingsDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x000115FE File Offset: 0x0000F7FE
		// (set) Token: 0x060001E9 RID: 489 RVA: 0x00011606 File Offset: 0x0000F806
		public OperationContext OpContext { get; set; }

		// Token: 0x060001EA RID: 490 RVA: 0x00011610 File Offset: 0x0000F810
		public string LoadMiscSettingValue(int code)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@code", DbType.Int32, code)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader(QueryStorageMiscTable.QS_MISC_SETTING_VALUE, parameters))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return dataReader["miscstring"].ToString();
				}
			}
			return null;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0001169C File Offset: 0x0000F89C
		public void SaveMiscSettingValue(int code, string value)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@code", DbType.String, code),
				this.DatabaseManager.GetParameter("@value", DbType.String, value)
			};
			this.DatabaseManager.ExecuteNonQuery(QueryStorageMiscTable.QI_MISC_SETTING_VALUE, parameters);
		}
	}
}
