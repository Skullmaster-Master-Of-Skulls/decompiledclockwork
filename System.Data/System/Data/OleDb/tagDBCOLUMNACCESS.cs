using System;
using System.Runtime.InteropServices;

namespace System.Data.OleDb
{
	// Token: 0x02000243 RID: 579
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct tagDBCOLUMNACCESS
	{
		// Token: 0x040014C5 RID: 5317
		internal IntPtr pData;

		// Token: 0x040014C6 RID: 5318
		internal tagDBIDX columnid;

		// Token: 0x040014C7 RID: 5319
		internal IntPtr cbDataLen;

		// Token: 0x040014C8 RID: 5320
		internal int dwStatus;

		// Token: 0x040014C9 RID: 5321
		internal IntPtr cbMaxLen;

		// Token: 0x040014CA RID: 5322
		internal IntPtr dwReserved;

		// Token: 0x040014CB RID: 5323
		internal short wType;

		// Token: 0x040014CC RID: 5324
		internal byte bPrecision;

		// Token: 0x040014CD RID: 5325
		internal byte bScale;
	}
}
