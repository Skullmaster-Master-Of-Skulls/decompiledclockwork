using System;
using System.Data;
using System.Data.Common;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.DAO
{
	// Token: 0x02000010 RID: 16
	public interface IQueryingDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000022 RID: 34
		DataTable ExecuteQuery(string query);

		// Token: 0x06000023 RID: 35
		DataTable ExecuteQuery(string query, DbParameter[] parameters);

		// Token: 0x06000024 RID: 36
		int ExecuteNonQuery(string query);

		// Token: 0x06000025 RID: 37
		int ExecuteNonQuery(string query, DbParameter[] parameters);
	}
}
