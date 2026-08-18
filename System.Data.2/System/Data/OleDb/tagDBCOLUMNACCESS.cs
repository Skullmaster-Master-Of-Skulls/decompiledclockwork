using System;
using System.Runtime.InteropServices;

namespace System.Data.OleDb
{
	// Token: 0x02000268 RID: 616
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct tagDBCOLUMNACCESS
	{
		// Token: 0x040017D7 RID: 6103
		internal IntPtr pData;

		// Token: 0x040017D8 RID: 6104
		internal tagDBIDX columnid;

		// Token: 0x040017D9 RID: 6105
		internal IntPtr cbDataLen;

		// Token: 0x040017DA RID: 6106
		internal int dwStatus;

		// Token: 0x040017DB RID: 6107
		internal IntPtr cbMaxLen;

		// Token: 0x040017DC RID: 6108
		internal IntPtr dwReserved;

		// Token: 0x040017DD RID: 6109
		internal short wType;

		// Token: 0x040017DE RID: 6110
		internal byte bPrecision;

		// Token: 0x040017DF RID: 6111
		internal byte bScale;
	}
}
