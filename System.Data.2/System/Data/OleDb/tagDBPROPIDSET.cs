using System;
using System.Runtime.InteropServices;

namespace System.Data.OleDb
{
	// Token: 0x02000272 RID: 626
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct tagDBPROPIDSET
	{
		// Token: 0x04001808 RID: 6152
		internal IntPtr rgPropertyIDs;

		// Token: 0x04001809 RID: 6153
		internal int cPropertyIDs;

		// Token: 0x0400180A RID: 6154
		internal Guid guidPropertySet;
	}
}
