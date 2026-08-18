using System;
using System.Runtime.InteropServices;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000129 RID: 297
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct NotiTblVal
	{
		// Token: 0x0400098C RID: 2444
		internal OracleNotificationInfo info;

		// Token: 0x0400098D RID: 2445
		internal int numRows;

		// Token: 0x0400098E RID: 2446
		internal IntPtr pOpsTableChangeDesc;
	}
}
