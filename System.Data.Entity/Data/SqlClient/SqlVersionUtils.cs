using System;
using System.Data.Entity;
using System.Globalization;

namespace System.Data.SqlClient
{
	// Token: 0x0200002B RID: 43
	internal static class SqlVersionUtils
	{
		// Token: 0x060003DE RID: 990 RVA: 0x0000EE08 File Offset: 0x0000D008
		internal static SqlVersion GetSqlVersion(SqlConnection connection)
		{
			int num = int.Parse(connection.ServerVersion.Substring(0, 2), CultureInfo.InvariantCulture);
			if (num >= 10)
			{
				return SqlVersion.Sql10;
			}
			if (num == 9)
			{
				return SqlVersion.Sql9;
			}
			return SqlVersion.Sql8;
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0000EE3F File Offset: 0x0000D03F
		internal static string GetVersionHint(SqlVersion version)
		{
			if (version == SqlVersion.Sql8)
			{
				return "2000";
			}
			if (version == SqlVersion.Sql9)
			{
				return "2005";
			}
			if (version != SqlVersion.Sql10)
			{
				throw EntityUtil.Argument(Strings.UnableToDetermineStoreVersion);
			}
			return "2008";
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0000EE70 File Offset: 0x0000D070
		internal static SqlVersion GetSqlVersion(string versionHint)
		{
			if (!string.IsNullOrEmpty(versionHint))
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
			}
			throw EntityUtil.Argument(Strings.UnableToDetermineStoreVersion);
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000EEC1 File Offset: 0x0000D0C1
		internal static bool IsPreKatmai(SqlVersion sqlVersion)
		{
			return sqlVersion == SqlVersion.Sql8 || sqlVersion == SqlVersion.Sql9;
		}
	}
}
