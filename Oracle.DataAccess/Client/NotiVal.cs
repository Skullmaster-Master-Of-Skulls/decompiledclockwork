using System;
using System.Runtime.InteropServices;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000128 RID: 296
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct NotiVal
	{
		// Token: 0x04000987 RID: 2439
		internal OracleNotificationType type;

		// Token: 0x04000988 RID: 2440
		internal OracleNotificationSource source;

		// Token: 0x04000989 RID: 2441
		internal OracleNotificationInfo info;

		// Token: 0x0400098A RID: 2442
		internal int numTables;

		// Token: 0x0400098B RID: 2443
		internal int numQueries;
	}
}
