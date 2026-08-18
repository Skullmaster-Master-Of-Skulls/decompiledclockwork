using System;
using System.Data.SqlClient;
using TechnoPro.Common.Core.UnivDataAccess;
using TechnoPro.Common.ICore.UnivDataAccess;
using TechnoPro.Common.Reports.ICore.Common;
using TechnoPro.Common.Reports.Mappers.Database;
using TechnoPro.Common.Reports.Mappers.OperationContexts;
using TechnoPro.Common.Reports.Public;
using TechnoPro.Common.Reports.Public.Entities.Database;
using TechnoPro.Common.Reports.Public.Entities.OperationContexts;

namespace TechnoPro.Common.Reports.Core.Common
{
	// Token: 0x02000003 RID: 3
	public class DatabaseReportManager : IDatabaseReportManager, IOperationContextRO, IBaseOperationContextRO<OperationContextRO>
	{
		// Token: 0x06000005 RID: 5 RVA: 0x00002174 File Offset: 0x00000374
		public DatabaseReportManager(OperationContextRO opContext)
		{
			this.OpContext = opContext;
			this._dataAccessManager = new UnivDataAccessManager((opContext != null) ? opContext.ToDomainObject() : null);
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000006 RID: 6 RVA: 0x0000219D File Offset: 0x0000039D
		// (set) Token: 0x06000007 RID: 7 RVA: 0x000021A5 File Offset: 0x000003A5
		public OperationContextRO OpContext { get; set; }

		// Token: 0x06000008 RID: 8 RVA: 0x000021B0 File Offset: 0x000003B0
		public bool DoesTableExist(string tableName)
		{
			return this._dataAccessManager.DoesTableExist(tableName);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021D0 File Offset: 0x000003D0
		public bool DoesColumnExist(string tableName, string colName)
		{
			return this._dataAccessManager.DoesColumnExist(tableName, colName);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021F0 File Offset: 0x000003F0
		public string GetSQLCommandParametersFilledIn(QueryRequestRO Query)
		{
			return this._dataAccessManager.GetSQLCommandParametersFilledIn(Query.ToDomainObject());
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002214 File Offset: 0x00000414
		public QueryResultRO FillReturnIdentity(QueryRequestRO Query, string autoIncrementColName, string tableName)
		{
			return this._dataAccessManager.FillReturnIdentity(Query.ToDomainObject(), autoIncrementColName, tableName).ToReportObject();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002240 File Offset: 0x00000440
		public QueryResultRO Fill(QueryRequestRO Query)
		{
			return this._dataAccessManager.Fill(Query.ToDomainObject()).ToReportObject();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002268 File Offset: 0x00000468
		public QueryResultRO ExecuteScalar(QueryRequestRO Query)
		{
			return this._dataAccessManager.ExecuteScalar(Query.ToDomainObject()).ToReportObject();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002290 File Offset: 0x00000490
		public QueryResultRO ExecuteNonQuery(QueryRequestRO Query)
		{
			return this._dataAccessManager.ExecuteNonQuery(Query.ToDomainObject()).ToReportObject();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000022B8 File Offset: 0x000004B8
		public SqlDataReader ExecuteReader(QueryRequestRO Query)
		{
			return this._dataAccessManager.ExecuteReader(Query.ToDomainObject());
		}

		// Token: 0x04000002 RID: 2
		private readonly IUnivDataAccessManager _dataAccessManager;
	}
}
