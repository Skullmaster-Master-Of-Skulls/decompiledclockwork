using System;
using System.Data.ProviderBase;
using System.Diagnostics;
using System.Security.Permissions;

namespace System.Data.SqlClient
{
	// Token: 0x020002D1 RID: 721
	internal sealed class SqlPerformanceCounters : DbConnectionPoolCounters
	{
		// Token: 0x060024F7 RID: 9463 RVA: 0x00299988 File Offset: 0x00298D88
		[PerformanceCounterPermission(SecurityAction.Assert, PermissionAccess = PerformanceCounterPermissionAccess.Write, MachineName = ".", CategoryName = ".NET Data Provider for SqlServer")]
		private SqlPerformanceCounters() : base(".NET Data Provider for SqlServer", "Counters for System.Data.SqlClient")
		{
		}

		// Token: 0x04001792 RID: 6034
		private const string CategoryName = ".NET Data Provider for SqlServer";

		// Token: 0x04001793 RID: 6035
		private const string CategoryHelp = "Counters for System.Data.SqlClient";

		// Token: 0x04001794 RID: 6036
		public static readonly SqlPerformanceCounters SingletonInstance = new SqlPerformanceCounters();
	}
}
