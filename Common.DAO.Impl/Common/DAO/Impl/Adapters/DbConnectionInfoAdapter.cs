using System;
using Databases;
using Databases.Exceptions;
using TechnoPro.Common.Public.Entities.Database;

namespace TechnoPro.Common.DAO.Impl.Adapters
{
	// Token: 0x0200017D RID: 381
	public static class DbConnectionInfoAdapter
	{
		// Token: 0x06000B62 RID: 2914 RVA: 0x00078F00 File Offset: 0x00077100
		public static DatabaseLayer GetDatabaseLayer(this DbConnectionInfo dbConn, eDatabaseConnectionStringName dbRole)
		{
			bool flag = dbConn == null || string.IsNullOrEmpty(dbConn.DbEncryptionPassword) || string.IsNullOrEmpty(dbConn.ConnectionString);
			if (flag)
			{
				throw new DbNotSupportedException(string.Format("GetDatabaseLayer:: Database role '{0}' is not supported or is not configure in your system", dbRole));
			}
			return new DatabaseLayer(dbRole, ProviderNames.SqlClient, dbConn.ConnectionString, dbConn.DbEncryptionPassword);
		}
	}
}
