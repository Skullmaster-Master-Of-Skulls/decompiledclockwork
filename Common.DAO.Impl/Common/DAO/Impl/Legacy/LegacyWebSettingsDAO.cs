using System;
using System.Data;
using System.Data.Common;
using Databases;
using TechnoPro.Common.DAO.Legacy;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.DAO.Impl.Legacy
{
	// Token: 0x020000A9 RID: 169
	public class LegacyWebSettingsDAO : ILegacyWebSettingsDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060004B1 RID: 1201 RVA: 0x0002B950 File Offset: 0x00029B50
		public LegacyWebSettingsDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x0002B962 File Offset: 0x00029B62
		// (set) Token: 0x060004B3 RID: 1203 RVA: 0x0002B96A File Offset: 0x00029B6A
		public OperationContext OpContext { get; set; }

		// Token: 0x060004B4 RID: 1204 RVA: 0x0002B974 File Offset: 0x00029B74
		public string GetWebSettingValue(int webSetting, string instanceName)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@iname", DbType.String, instanceName),
				databaseLayer.GetParameter("@code", DbType.Int32, webSetting)
			};
			object obj = databaseLayer.ExecuteScalar("SELECT settingstringvalue FROM websettings2 WHERE instancename=@iname AND settingcode=@code", parameters);
			byte[] array = obj as byte[];
			return (array == null) ? "" : databaseLayer.Encryption.Decrypt(array);
		}
	}
}
