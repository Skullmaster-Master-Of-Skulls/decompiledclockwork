using System;
using System.Runtime.InteropServices;

namespace System.Data.OleDb
{
	// Token: 0x02000244 RID: 580
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct tagDBIDX
	{
		// Token: 0x040014CE RID: 5326
		internal Guid uGuid;

		// Token: 0x040014CF RID: 5327
		internal int eKind;

		// Token: 0x040014D0 RID: 5328
		internal IntPtr ulPropid;
	}
}
