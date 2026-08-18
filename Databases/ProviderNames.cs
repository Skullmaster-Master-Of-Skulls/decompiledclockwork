using System;

namespace Databases
{
	// Token: 0x02000006 RID: 6
	public sealed class ProviderNames
	{
		// Token: 0x04000007 RID: 7
		public static readonly string SqlClient = "System.Data.SqlClient";

		// Token: 0x04000008 RID: 8
		public static readonly string OracleClient = "System.Data.OracleClient";

		// Token: 0x04000009 RID: 9
		public static readonly string OracleClient2 = "Oracle.DataAccess.Client";

		// Token: 0x0400000A RID: 10
		public static readonly string OleDb = "System.Data.OleDb";

		// Token: 0x0400000B RID: 11
		public static readonly string Odbc = "System.Data.Odbc";

		// Token: 0x0400000C RID: 12
		public static readonly string[] Providers = new string[]
		{
			ProviderNames.SqlClient,
			ProviderNames.OracleClient,
			ProviderNames.OleDb,
			ProviderNames.Odbc
		};
	}
}
