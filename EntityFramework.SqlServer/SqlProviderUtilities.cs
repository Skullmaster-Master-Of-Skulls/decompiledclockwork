using System;
using System.Data.Common;
using System.Data.Entity.SqlServer.Resources;
using System.Data.SqlClient;

namespace System.Data.Entity.SqlServer
{
	// Token: 0x02000044 RID: 68
	internal class SqlProviderUtilities
	{
		// Token: 0x06000492 RID: 1170 RVA: 0x000172DC File Offset: 0x000154DC
		internal static SqlConnection GetRequiredSqlConnection(DbConnection connection)
		{
			SqlConnection sqlConnection = connection as SqlConnection;
			if (sqlConnection == null)
			{
				throw new ArgumentException(Strings.Mapping_Provider_WrongConnectionType(typeof(SqlConnection)));
			}
			return sqlConnection;
		}
	}
}
