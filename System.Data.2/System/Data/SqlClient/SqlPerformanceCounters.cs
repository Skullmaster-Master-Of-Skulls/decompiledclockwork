using System;
using System.Data.ProviderBase;
using System.Diagnostics;
using System.Security.Permissions;

namespace System.Data.SqlClient
{
	// Token: 0x020001BB RID: 443
	internal sealed class SqlPerformanceCounters : DbConnectionPoolCounters
	{
		// Token: 0x06001ADD RID: 6877 RVA: 0x000BDBF0 File Offset: 0x000BCFF0
		[PerformanceCounterPermission(SecurityAction.Assert, PermissionAccess = PerformanceCounterPermissionAccess.Write, MachineName = ".", CategoryName = ".NET Data Provider for SqlServer")]
		private SqlPerformanceCounters() : base(".NET Data Provider for SqlServer", "Counters for System.Data.SqlClient")
		{
		}

		// Token: 0x04000F93 RID: 3987
		private const string CategoryName = ".NET Data Provider for SqlServer";

		// Token: 0x04000F94 RID: 3988
		private const string CategoryHelp = "Counters for System.Data.SqlClient";

		// Token: 0x04000F95 RID: 3989
		public static readonly SqlPerformanceCounters SingletonInstance = new SqlPerformanceCounters();
	}
}
