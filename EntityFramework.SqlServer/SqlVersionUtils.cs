using System;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.SqlServer.Resources;
using System.Globalization;

namespace System.Data.Entity.SqlServer
{
	// Token: 0x02000029 RID: 41
	internal static class SqlVersionUtils
	{
		// Token: 0x06000251 RID: 593 RVA: 0x0000B038 File Offset: 0x00009238
		internal static SqlVersion GetSqlVersion(DbConnection connection)
		{
			int num = int.Parse(DbInterception.Dispatch.Connection.GetServerVersion(connection, new DbInterceptionContext()).Substring(0, 2), CultureInfo.InvariantCulture);
			if (num >= 11)
			{
				return SqlVersion.Sql11;
			}
			if (num == 10)
			{
				return SqlVersion.Sql10;
			}
			if (num == 9)
			{
				return SqlVersion.Sql9;
			}
			return SqlVersion.Sql8;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000B088 File Offset: 0x00009288
		internal static ServerType GetServerType(DbConnection connection)
		{
			ServerType result;
			using (DbCommand dbCommand = connection.CreateCommand())
			{
				dbCommand.CommandText = "select cast(serverproperty('EngineEdition') as int)";
				using (DbDataReader dbDataReader = DbInterception.Dispatch.Command.Reader(dbCommand, new DbCommandInterceptionContext()))
				{
					dbDataReader.Read();
					result = ((dbDataReader.GetInt32(0) == 5) ? ServerType.Cloud : ServerType.OnPremises);
				}
			}
			return result;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000B108 File Offset: 0x00009308
		internal static string GetVersionHint(SqlVersion version, ServerType serverType)
		{
			if (serverType == ServerType.Cloud)
			{
				return "2012.Azure";
			}
			if (version <= SqlVersion.Sql9)
			{
				if (version == SqlVersion.Sql8)
				{
					return "2000";
				}
				if (version == SqlVersion.Sql9)
				{
					return "2005";
				}
			}
			else
			{
				if (version == SqlVersion.Sql10)
				{
					return "2008";
				}
				if (version == SqlVersion.Sql11)
				{
					return "2012";
				}
			}
			throw new ArgumentException(Strings.UnableToDetermineStoreVersion);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000B160 File Offset: 0x00009360
		internal static SqlVersion GetSqlVersion(string versionHint)
		{
			if (!string.IsNullOrEmpty(versionHint) && versionHint != null)
			{
				if (versionHint == "2000")
				{
					return SqlVersion.Sql8;
				}
				if (versionHint == "2005")
				{
					return SqlVersion.Sql9;
				}
				if (versionHint == "2008")
				{
					return SqlVersion.Sql10;
				}
				if (versionHint == "2012")
				{
					return SqlVersion.Sql11;
				}
				if (versionHint == "2012.Azure")
				{
					return SqlVersion.Sql11;
				}
			}
			throw new ArgumentException(Strings.UnableToDetermineStoreVersion);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000B1D6 File Offset: 0x000093D6
		internal static bool IsPreKatmai(SqlVersion sqlVersion)
		{
			return sqlVersion == SqlVersion.Sql8 || sqlVersion == SqlVersion.Sql9;
		}
	}
}
