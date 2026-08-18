using System;
using System.Runtime.InteropServices;

namespace System.Data.OleDb
{
	// Token: 0x02000266 RID: 614
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct tagDBPARAMBINDINFO
	{
		// Token: 0x040017C2 RID: 6082
		internal IntPtr pwszDataSourceType;

		// Token: 0x040017C3 RID: 6083
		internal IntPtr pwszName;

		// Token: 0x040017C4 RID: 6084
		internal IntPtr ulParamSize;

		// Token: 0x040017C5 RID: 6085
		internal int dwFlags;

		// Token: 0x040017C6 RID: 6086
		internal byte bPrecision;

		// Token: 0x040017C7 RID: 6087
		internal byte bScale;
	}
}
