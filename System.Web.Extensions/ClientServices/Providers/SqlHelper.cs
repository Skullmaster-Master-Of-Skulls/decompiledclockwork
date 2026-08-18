using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Web.Resources;
using System.Windows.Forms;

namespace System.Web.ClientServices.Providers
{
	// Token: 0x02000117 RID: 279
	internal static class SqlHelper
	{
		// Token: 0x06000EA6 RID: 3750 RVA: 0x00034CB8 File Offset: 0x00032EB8
		internal static string GetDefaultConnectionString()
		{
			return "|FILES|";
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x00034CBF File Offset: 0x00032EBF
		internal static int IsSpecialConnectionString(string connectionString)
		{
			if (string.IsNullOrEmpty(connectionString))
			{
				return 1;
			}
			if (string.Compare(connectionString, "|FILES|", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return 1;
			}
			return 3;
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x00034CDC File Offset: 0x00032EDC
		internal static DbConnection GetConnection(string username, string connectionString, string sqlProvider)
		{
			if (connectionString.Contains("|SQL/CE|") || (sqlProvider != null && sqlProvider.Contains(".SqlServerCe")))
			{
				try
				{
					return SqlHelper.GetSqlCeConnection(username, connectionString);
				}
				catch (TypeLoadException innerException)
				{
					throw new ArgumentException(AtlasWeb.SqlHelper_SqlEverywhereNotInstalled, innerException);
				}
			}
			DbConnection dbConnection = new SqlConnection(connectionString);
			dbConnection.Open();
			return dbConnection;
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x00034D40 File Offset: 0x00032F40
		internal static void AddParameter(DbConnection conn, DbCommand cmd, string paramName, object paramValue)
		{
			if (!(conn is SqlConnection))
			{
				SqlHelper.AddSqlCeParameter(cmd, paramName, paramValue);
				return;
			}
			cmd.Parameters.Add(new SqlParameter(paramName, paramValue));
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x00034D68 File Offset: 0x00032F68
		internal static string GetCookieFromDB(string name, string username, string connectionString, string sqlProvider)
		{
			if (connectionString == "|FILES|")
			{
				return ClientDataManager.GetCookie(username, name, false);
			}
			if (connectionString == "|Isolated_Storage|")
			{
				return ClientDataManager.GetCookie(username, name, true);
			}
			string result;
			using (DbConnection connection = SqlHelper.GetConnection(username, connectionString, sqlProvider))
			{
				DbCommand dbCommand = connection.CreateCommand();
				dbCommand.CommandText = "SELECT PropertyValue FROM UserProperties WHERE PropertyName = @PropName";
				SqlHelper.AddParameter(connection, dbCommand, "@PropName", "CookieName_" + name);
				result = (dbCommand.ExecuteScalar() as string);
			}
			return result;
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x00034DFC File Offset: 0x00032FFC
		internal static string StoreCookieInDB(string cookieName, string cookieValue, string username, string connectionString, string sqlProvider)
		{
			if (connectionString == "|FILES|")
			{
				return ClientDataManager.StoreCookie(username, cookieName, cookieValue, false);
			}
			if (connectionString == "|Isolated_Storage|")
			{
				return ClientDataManager.StoreCookie(username, cookieName, cookieValue, true);
			}
			string text = Guid.NewGuid().ToString("N");
			string result;
			using (DbConnection connection = SqlHelper.GetConnection(username, connectionString, sqlProvider))
			{
				DbTransaction dbTransaction = null;
				try
				{
					dbTransaction = connection.BeginTransaction();
					DbCommand dbCommand = connection.CreateCommand();
					dbCommand.CommandText = "DELETE FROM UserProperties WHERE PropertyName LIKE N'CookieName_%' AND PropertyValue LIKE @PropValue";
					dbCommand.Transaction = dbTransaction;
					SqlHelper.AddParameter(connection, dbCommand, "@PropValue", cookieName + "=%");
					dbCommand.ExecuteNonQuery();
					if (!string.IsNullOrEmpty(cookieValue))
					{
						dbCommand = connection.CreateCommand();
						dbCommand.Transaction = dbTransaction;
						dbCommand.CommandText = "INSERT INTO UserProperties (PropertyName, PropertyValue) VALUES (@PropName, @PropValue)";
						SqlHelper.AddParameter(connection, dbCommand, "@PropName", "CookieName_" + text);
						SqlHelper.AddParameter(connection, dbCommand, "@PropValue", cookieName + "=" + cookieValue);
						dbCommand.ExecuteNonQuery();
						result = text;
					}
					else
					{
						result = cookieName;
					}
				}
				catch
				{
					if (dbTransaction != null)
					{
						dbTransaction.Rollback();
						dbTransaction = null;
					}
					throw;
				}
				finally
				{
					if (dbTransaction != null)
					{
						dbTransaction.Commit();
					}
				}
			}
			return result;
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x00034F4C File Offset: 0x0003314C
		internal static void DeleteAllCookies(string username, string connectionString, string sqlProvider)
		{
			if (connectionString == "|FILES|" || connectionString == "|Isolated_Storage|")
			{
				ClientDataManager.DeleteAllCookies(username, connectionString == "|Isolated_Storage|");
				return;
			}
			using (DbConnection connection = SqlHelper.GetConnection(username, connectionString, sqlProvider))
			{
				DbTransaction dbTransaction = null;
				try
				{
					dbTransaction = connection.BeginTransaction();
					DbCommand dbCommand = connection.CreateCommand();
					dbCommand.CommandText = "DELETE FROM UserProperties WHERE PropertyName LIKE N'CookieName_%'";
					dbCommand.Transaction = dbTransaction;
					dbCommand.ExecuteNonQuery();
				}
				catch
				{
					if (dbTransaction != null)
					{
						dbTransaction.Rollback();
						dbTransaction = null;
					}
					throw;
				}
				finally
				{
					if (dbTransaction != null)
					{
						dbTransaction.Commit();
					}
				}
			}
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x00035004 File Offset: 0x00033204
		private static DbConnection GetSqlCeConnection(string username, string connectionString)
		{
			DbConnection dbConnection = SqlHelper.CreateDBIfRequired(username, connectionString);
			if (dbConnection == null)
			{
				dbConnection = SqlHelper.CreateNewSqlCeConnection(connectionString, true);
			}
			return dbConnection;
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x00035028 File Offset: 0x00033228
		private static DbConnection CreateDBIfRequired(string username, string connectionString)
		{
			if (!connectionString.Contains("|SQL/CE|"))
			{
				return null;
			}
			try
			{
				DbConnection dbConnection = SqlHelper.CreateNewSqlCeConnection(connectionString, false);
				if (string.Compare(dbConnection.Database.Trim(), "|SQL/CE|", StringComparison.OrdinalIgnoreCase) != 0)
				{
					dbConnection.Open();
					return dbConnection;
				}
				dbConnection.Dispose();
			}
			catch (TypeLoadException innerException)
			{
				throw new ArgumentException(AtlasWeb.SqlHelper_SqlEverywhereNotInstalled, innerException);
			}
			string fullDBFileName = SqlHelper.GetFullDBFileName(username, "_DB.spf");
			bool flag = !File.Exists(fullDBFileName);
			connectionString = connectionString.Replace("|SQL/CE|", fullDBFileName);
			if (flag)
			{
				using (IDisposable disposable = (IDisposable)Activator.CreateInstance(SqlHelper.GetSqlCeType("SqlCeEngine"), new object[]
				{
					connectionString
				}))
				{
					disposable.GetType().InvokeMember("CreateDatabase", BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod, null, disposable, null, CultureInfo.InvariantCulture);
				}
				DbConnection dbConnection2;
				DbConnection dbConnection = dbConnection2 = SqlHelper.CreateNewSqlCeConnection(connectionString, true);
				try
				{
					DbCommand dbCommand = dbConnection.CreateCommand();
					if (username == null)
					{
						dbCommand.CommandText = "CREATE TABLE ApplicationProperties (PropertyName nvarchar(256), PropertyValue nvarchar(256))";
						dbCommand.ExecuteNonQuery();
					}
					else
					{
						dbCommand.CommandText = "CREATE TABLE UserProperties (PropertyName nvarchar(256), PropertyValue nvarchar(256))";
						dbCommand.ExecuteNonQuery();
						dbCommand = dbConnection.CreateCommand();
						dbCommand.CommandText = "CREATE TABLE Roles (UserName nvarchar(256), RoleName nvarchar(256))";
						dbCommand.ExecuteNonQuery();
						dbCommand = dbConnection.CreateCommand();
						dbCommand.CommandText = "CREATE TABLE Settings (PropertyName nvarchar(256), PropertyStoredAs nvarchar(1), PropertyValue nvarchar(2048))";
						dbCommand.ExecuteNonQuery();
					}
				}
				finally
				{
					if (dbConnection2 != null)
					{
						((IDisposable)dbConnection2).Dispose();
					}
				}
			}
			return SqlHelper.CreateNewSqlCeConnection(connectionString, true);
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x000351BC File Offset: 0x000333BC
		private static Type GetSqlCeType(string typeName)
		{
			Type type = Type.GetType("System.Data.SqlServerCe." + typeName + ", System.Data.SqlServerCe", false, true);
			if (type != null)
			{
				return type;
			}
			type = Type.GetType("System.Data.SqlServerCe." + typeName + ", System.Data.SqlServerCe, Version=3.5.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91", false, true);
			if (type != null)
			{
				return type;
			}
			type = Type.GetType("System.Data.SqlServerCe." + typeName + ", System.Data.SqlServerCe, Version=3.0.3600.0, Culture=neutral, PublicKeyToken=3be235df1c8d2ad3", false, true);
			if (type != null)
			{
				return type;
			}
			return Type.GetType("System.Data.SqlServerCe." + typeName + ", System.Data.SqlServerCe, Version=3.5.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91", true, true);
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x0003524C File Offset: 0x0003344C
		private static DbConnection CreateNewSqlCeConnection(string connectionString, bool openConn)
		{
			if (SqlHelper._SqlCeConnectionType == null)
			{
				SqlHelper._SqlCeConnectionType = SqlHelper.GetSqlCeType("SqlCeConnection");
			}
			DbConnection dbConnection = (DbConnection)Activator.CreateInstance(SqlHelper._SqlCeConnectionType, new object[]
			{
				connectionString
			});
			if (openConn)
			{
				dbConnection.Open();
			}
			return dbConnection;
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x0003529C File Offset: 0x0003349C
		private static void AddSqlCeParameter(DbCommand cmd, string paramName, object paramValue)
		{
			if (SqlHelper._SqlCeParamType == null)
			{
				SqlHelper._SqlCeParamType = SqlHelper.GetSqlCeType("SqlCeParameter");
			}
			cmd.Parameters.Add((DbParameter)Activator.CreateInstance(SqlHelper._SqlCeParamType, new object[]
			{
				paramName,
				paramValue
			}));
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x000352EE File Offset: 0x000334EE
		internal static string GetFullDBFileName(string username, string extension)
		{
			return Path.Combine(Application.UserAppDataPath, SqlHelper.GetPartialDBFileName(username, extension));
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x00035304 File Offset: 0x00033504
		internal static string GetPartialDBFileName(string username, string extension)
		{
			if (string.IsNullOrEmpty(username))
			{
				return "Application" + extension;
			}
			char[] array = username.ToCharArray();
			for (int i = 0; i < array.Length; i++)
			{
				if (!char.IsLetterOrDigit(array[i]))
				{
					array[i] = '_';
				}
			}
			return "User_" + new string(array) + extension;
		}

		// Token: 0x04000420 RID: 1056
		private const string _SQL_CE_Tag = "|SQL/CE|";

		// Token: 0x04000421 RID: 1057
		private const string _SQL_FILES_Tag = "|FILES|";

		// Token: 0x04000422 RID: 1058
		private const string _SQL_CE_CONN_STRING = "Data Source = |SQL/CE|";

		// Token: 0x04000423 RID: 1059
		private const string _Isolated_Storage_Tag = "|Isolated_Storage|";

		// Token: 0x04000424 RID: 1060
		private static Type _SqlCeConnectionType;

		// Token: 0x04000425 RID: 1061
		private static Type _SqlCeParamType;
	}
}
