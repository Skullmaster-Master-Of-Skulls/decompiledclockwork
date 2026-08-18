using System;
using System.Runtime.InteropServices;

namespace System.Data.OleDb
{
	// Token: 0x0200024D RID: 589
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct tagDBPROPIDSET
	{
		// Token: 0x040014F6 RID: 5366
		internal IntPtr rgPropertyIDs;

		// Token: 0x040014F7 RID: 5367
		internal int cPropertyIDs;

		// Token: 0x040014F8 RID: 5368
		internal Guid guidPropertySet;
	}
}
