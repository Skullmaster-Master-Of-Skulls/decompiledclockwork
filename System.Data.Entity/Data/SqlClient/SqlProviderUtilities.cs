using System;
using System.Data.Common;
using System.Data.Entity;

namespace System.Data.SqlClient
{
	// Token: 0x02000023 RID: 35
	internal class SqlProviderUtilities
	{
		// Token: 0x06000233 RID: 563 RVA: 0x00007740 File Offset: 0x00005940
		internal static SqlConnection GetRequiredSqlConnection(DbConnection connection)
		{
			SqlConnection sqlConnection = connection as SqlConnection;
			if (sqlConnection == null)
			{
				throw EntityUtil.Argument(Strings.Mapping_Provider_WrongConnectionType(typeof(SqlConnection)));
			}
			return sqlConnection;
		}
	}
}
