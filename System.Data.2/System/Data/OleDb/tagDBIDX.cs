using System;
using System.Runtime.InteropServices;

namespace System.Data.OleDb
{
	// Token: 0x02000269 RID: 617
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct tagDBIDX
	{
		// Token: 0x040017E0 RID: 6112
		internal Guid uGuid;

		// Token: 0x040017E1 RID: 6113
		internal int eKind;

		// Token: 0x040017E2 RID: 6114
		internal IntPtr ulPropid;
	}
}
