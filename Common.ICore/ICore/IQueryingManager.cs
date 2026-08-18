using System;
using System.Data;
using System.Data.Common;

namespace TechnoPro.Common.ICore
{
	// Token: 0x0200000C RID: 12
	public interface IQueryingManager
	{
		// Token: 0x0600004B RID: 75
		DataTable ExecuteQuery(string query);

		// Token: 0x0600004C RID: 76
		DataTable ExecuteQuery(string query, DbParameter[] parameters);

		// Token: 0x0600004D RID: 77
		int ExecuteNonQuery(string query);

		// Token: 0x0600004E RID: 78
		int ExecuteNonQuery(string query, DbParameter[] parameters);
	}
}
