using System;
using System.Data.SqlClient;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.UnivDataAccess;

namespace TechnoPro.Common.ICore.UnivDataAccess
{
	// Token: 0x0200001E RID: 30
	public interface IUnivDataAccessManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060000BC RID: 188
		bool DoesTableExist(string tableName);

		// Token: 0x060000BD RID: 189
		bool DoesColumnExist(string tableName, string colName);

		// Token: 0x060000BE RID: 190
		string GetSQLCommandParametersFilledIn(QueryRequest Query);

		// Token: 0x060000BF RID: 191
		QueryResult FillReturnIdentity(QueryRequest Query, string autoIncrementColName, string tableName);

		// Token: 0x060000C0 RID: 192
		QueryResult Fill(QueryRequest Query);

		// Token: 0x060000C1 RID: 193
		QueryResult ExecuteScalar(QueryRequest Query);

		// Token: 0x060000C2 RID: 194
		QueryResult ExecuteNonQuery(QueryRequest Query);

		// Token: 0x060000C3 RID: 195
		SqlDataReader ExecuteReader(QueryRequest Query);
	}
}
