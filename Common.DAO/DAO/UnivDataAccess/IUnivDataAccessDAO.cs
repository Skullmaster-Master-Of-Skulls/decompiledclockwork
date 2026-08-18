using System;
using System.Data.SqlClient;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.UnivDataAccess;

namespace TechnoPro.Common.DAO.UnivDataAccess
{
	// Token: 0x0200001E RID: 30
	public interface IUnivDataAccessDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000062 RID: 98
		bool DoesTableExist(string tableName);

		// Token: 0x06000063 RID: 99
		bool DoesColumnExist(string tableName, string colName);

		// Token: 0x06000064 RID: 100
		string GetSQLCommandParametersFilledIn(QueryRequest Query);

		// Token: 0x06000065 RID: 101
		QueryResult FillReturnIdentity(QueryRequest Query, string autoIncrementColName, string tableName);

		// Token: 0x06000066 RID: 102
		QueryResult Fill(QueryRequest Query);

		// Token: 0x06000067 RID: 103
		QueryResult ExecuteScalar(QueryRequest Query);

		// Token: 0x06000068 RID: 104
		QueryResult ExecuteNonQuery(QueryRequest Query);

		// Token: 0x06000069 RID: 105
		SqlDataReader ExecuteReader(QueryRequest Query);
	}
}
