using System;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Databases;
using TechnoPro.Common.DAO.UserSettingsPermissions;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions;

namespace TechnoPro.Common.DAO.Impl.UserSettingsPermissions
{
	// Token: 0x02000026 RID: 38
	public class MiscCodeDAO : IMiscCodeDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00006AE8 File Offset: 0x00004CE8
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x00006AF0 File Offset: 0x00004CF0
		public OperationContext OpContext { get; set; }

		// Token: 0x060000E3 RID: 227 RVA: 0x00006AF9 File Offset: 0x00004CF9
		public MiscCodeDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00006B0C File Offset: 0x00004D0C
		public string LoadMiscCodeValue(eMiscCode miscCode)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@misccode", DbType.Int32, (int)miscCode)
			};
			string result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT miscstring FROM misc WHERE misccode=@misccode", parameters))
			{
				bool flag = dataReader == null || !dataReader.Read() || dataReader["miscstring"] is DBNull;
				if (flag)
				{
					result = null;
				}
				else
				{
					result = (string)dataReader["miscstring"];
				}
			}
			return result;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00006BBC File Offset: 0x00004DBC
		[DebuggerStepThrough]
		public Task<string> LoadMiscCodeValueAsync(eMiscCode miscCode)
		{
			MiscCodeDAO.<LoadMiscCodeValueAsync>d__6 <LoadMiscCodeValueAsync>d__ = new MiscCodeDAO.<LoadMiscCodeValueAsync>d__6();
			<LoadMiscCodeValueAsync>d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
			<LoadMiscCodeValueAsync>d__.<>4__this = this;
			<LoadMiscCodeValueAsync>d__.miscCode = miscCode;
			<LoadMiscCodeValueAsync>d__.<>1__state = -1;
			<LoadMiscCodeValueAsync>d__.<>t__builder.Start<MiscCodeDAO.<LoadMiscCodeValueAsync>d__6>(ref <LoadMiscCodeValueAsync>d__);
			return <LoadMiscCodeValueAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00006C08 File Offset: 0x00004E08
		public void SaveMiscCodeValue(eMiscCode miscCode, string newValue)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@misccode", DbType.Int32, (int)miscCode),
				databaseLayer.GetParameter("@miscstring", DbType.String, newValue ?? "")
			};
			databaseLayer.ExecuteNonQuery("IF EXISTS(SELECT misccode FROM misc WHERE misccode=@misccode)\r\n    UPDATE misc SET miscstring=@miscstring WHERE misccode=@misccode\r\nELSE \r\n    INSERT INTO misc(misccode,miscstring) VALUES (@misccode,@miscstring)", parameters);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00006C74 File Offset: 0x00004E74
		[DebuggerStepThrough]
		public Task SaveMiscCodeValueAsync(eMiscCode miscCode, string newValue)
		{
			MiscCodeDAO.<SaveMiscCodeValueAsync>d__8 <SaveMiscCodeValueAsync>d__ = new MiscCodeDAO.<SaveMiscCodeValueAsync>d__8();
			<SaveMiscCodeValueAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SaveMiscCodeValueAsync>d__.<>4__this = this;
			<SaveMiscCodeValueAsync>d__.miscCode = miscCode;
			<SaveMiscCodeValueAsync>d__.newValue = newValue;
			<SaveMiscCodeValueAsync>d__.<>1__state = -1;
			<SaveMiscCodeValueAsync>d__.<>t__builder.Start<MiscCodeDAO.<SaveMiscCodeValueAsync>d__8>(ref <SaveMiscCodeValueAsync>d__);
			return <SaveMiscCodeValueAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00006CC8 File Offset: 0x00004EC8
		public void DeleteMiscCodeValue(eMiscCode miscCode)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@misccode", DbType.Int32, (int)miscCode)
			};
			databaseLayer.ExecuteNonQuery("DELETE FROM misc WHERE misccode=@misccode", parameters);
		}
	}
}
