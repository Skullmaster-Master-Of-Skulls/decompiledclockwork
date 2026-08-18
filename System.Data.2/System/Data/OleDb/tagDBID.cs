using System;
using System.Runtime.InteropServices;

namespace System.Data.OleDb
{
	// Token: 0x0200026A RID: 618
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal sealed class tagDBID
	{
		// Token: 0x040017E3 RID: 6115
		internal Guid uGuid;

		// Token: 0x040017E4 RID: 6116
		internal int eKind;

		// Token: 0x040017E5 RID: 6117
		internal IntPtr ulPropid;
	}
}
