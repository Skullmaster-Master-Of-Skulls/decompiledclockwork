using System;
using System.Data.SqlClient;
using TechnoPro.Common.Reports.Public;
using TechnoPro.Common.Reports.Public.Entities.Database;
using TechnoPro.Common.Reports.Public.Entities.OperationContexts;

namespace TechnoPro.Common.Reports.ICore.Common
{
	// Token: 0x02000003 RID: 3
	public interface IDatabaseReportManager : IOperationContextRO, IBaseOperationContextRO<OperationContextRO>
	{
		// Token: 0x06000002 RID: 2
		bool DoesTableExist(string tableName);

		// Token: 0x06000003 RID: 3
		bool DoesColumnExist(string tableName, string colName);

		// Token: 0x06000004 RID: 4
		string GetSQLCommandParametersFilledIn(QueryRequestRO Query);

		// Token: 0x06000005 RID: 5
		QueryResultRO FillReturnIdentity(QueryRequestRO Query, string autoIncrementColName, string tableName);

		// Token: 0x06000006 RID: 6
		QueryResultRO Fill(QueryRequestRO Query);

		// Token: 0x06000007 RID: 7
		QueryResultRO ExecuteScalar(QueryRequestRO Query);

		// Token: 0x06000008 RID: 8
		QueryResultRO ExecuteNonQuery(QueryRequestRO Query);

		// Token: 0x06000009 RID: 9
		SqlDataReader ExecuteReader(QueryRequestRO Query);
	}
}
