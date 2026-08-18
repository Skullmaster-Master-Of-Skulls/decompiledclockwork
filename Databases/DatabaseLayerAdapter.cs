using System;
using System.Data.Common;
using ClockWorkLogger;
using Databases.Exceptions;

namespace Databases
{
	// Token: 0x02000003 RID: 3
	public static class DatabaseLayerAdapter
	{
		// Token: 0x0600000E RID: 14 RVA: 0x000024C8 File Offset: 0x000006C8
		public static DatabaseLayer ChangeDatabaseLayerCredentials(this DatabaseLayer db, string username, string password)
		{
			bool flag = db == null;
			if (!flag)
			{
				string connectionString = db.ConnectionString;
				DbConnectionStringBuilder connectionStringBuilder = db.ConnectionStringBuilder;
				connectionStringBuilder.ConnectionString = connectionString;
				connectionStringBuilder["User ID"] = username;
				connectionStringBuilder["Password"] = password;
				connectionString = connectionStringBuilder.ConnectionString;
				bool flag2 = !string.IsNullOrEmpty(connectionString);
				if (flag2)
				{
					DatabaseLayer databaseLayer = new DatabaseLayer(db.DatabaseRole, ProviderNames.SqlClient, connectionString, db.EncryptionPassword, db.Encryption);
					try
					{
						bool flag3 = databaseLayer.TestDatabaseConnectivity(null);
						if (flag3)
						{
							return databaseLayer;
						}
					}
					catch (Exception ex)
					{
						CWLogger.Logger.ErrorException(string.Format("DatabaseLayerAdapter::ChangeDatabaseLayerCredentials: csName={0}, {1}", databaseLayer.DatabaseRole, ex.ToString()), ex);
						throw new DbNotSupportedException(string.Format("Database Role '{0}' is not supported in your system or you do not have privileges to connect to it", databaseLayer.DatabaseRole), ex);
					}
				}
				throw new DbNotSupportedException(string.Format("Database Role '{0}' is not supported in your system", db.DatabaseRole));
			}
			return null;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000025E0 File Offset: 0x000007E0
		public static DatabaseLayer ChangePrimaryDatabaseLayer(this DatabaseLayer db, eDatabaseConnectionStringName database, string username = null, string password = null)
		{
			bool flag = db == null;
			if (!flag)
			{
				string connectionString = db.ConnectionString;
				DbConnectionStringBuilder connectionStringBuilder = db.ConnectionStringBuilder;
				connectionStringBuilder.ConnectionString = connectionString;
				object arg = connectionStringBuilder["Database"];
				connectionStringBuilder["Database"] = arg + database.GetAttribute<DatabaseSuffixAttribute>().DatabaseNameSuffix;
				bool flag2 = !string.IsNullOrEmpty(username);
				if (flag2)
				{
					connectionStringBuilder["User ID"] = username;
				}
				bool flag3 = !string.IsNullOrEmpty(password);
				if (flag3)
				{
					connectionStringBuilder["Password"] = password;
				}
				connectionString = connectionStringBuilder.ConnectionString;
				bool flag4 = !string.IsNullOrEmpty(connectionString);
				if (flag4)
				{
					DatabaseLayer databaseLayer = new DatabaseLayer(database, ProviderNames.SqlClient, connectionString, db.EncryptionPassword, db.Encryption);
					try
					{
						bool flag5 = databaseLayer.TestDatabaseConnectivity(null);
						if (flag5)
						{
							return databaseLayer;
						}
					}
					catch (Exception ex)
					{
						CWLogger.Logger.ErrorException(string.Format("DatabaseLayerAdapter::ChangeDatabaseLayerCredentials: csName={0}, {1}", database, ex.ToString()), ex);
						throw new DbNotSupportedException(string.Format("Database Role '{0}' is not supported in your system or you do not have privileges to connect to it", database), ex);
					}
				}
				throw new DbNotSupportedException(string.Format("Database Role '{0}' is not supported in your system", database));
			}
			return null;
		}
	}
}
