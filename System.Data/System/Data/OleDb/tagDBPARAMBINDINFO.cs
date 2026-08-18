using System;
using System.Runtime.InteropServices;

namespace System.Data.OleDb
{
	// Token: 0x02000241 RID: 577
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct tagDBPARAMBINDINFO
	{
		// Token: 0x040014B0 RID: 5296
		internal IntPtr pwszDataSourceType;

		// Token: 0x040014B1 RID: 5297
		internal IntPtr pwszName;

		// Token: 0x040014B2 RID: 5298
		internal IntPtr ulParamSize;

		// Token: 0x040014B3 RID: 5299
		internal int dwFlags;

		// Token: 0x040014B4 RID: 5300
		internal byte bPrecision;

		// Token: 0x040014B5 RID: 5301
		internal byte bScale;
	}
}
