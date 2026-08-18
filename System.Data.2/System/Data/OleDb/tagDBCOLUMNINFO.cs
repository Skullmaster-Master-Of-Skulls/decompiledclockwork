using System;
using System.Runtime.InteropServices;

namespace System.Data.OleDb
{
	// Token: 0x0200026F RID: 623
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal sealed class tagDBCOLUMNINFO
	{
		// Token: 0x06002671 RID: 9841 RVA: 0x00104934 File Offset: 0x00103D34
		internal tagDBCOLUMNINFO()
		{
		}

		// Token: 0x040017F7 RID: 6135
		[MarshalAs(UnmanagedType.LPWStr)]
		internal string pwszName;

		// Token: 0x040017F8 RID: 6136
		internal IntPtr pTypeInfo = (IntPtr)0;

		// Token: 0x040017F9 RID: 6137
		internal IntPtr iOrdinal = (IntPtr)0;

		// Token: 0x040017FA RID: 6138
		internal int dwFlags;

		// Token: 0x040017FB RID: 6139
		internal IntPtr ulColumnSize = (IntPtr)0;

		// Token: 0x040017FC RID: 6140
		internal short wType;

		// Token: 0x040017FD RID: 6141
		internal byte bPrecision;

		// Token: 0x040017FE RID: 6142
		internal byte bScale;

		// Token: 0x040017FF RID: 6143
		internal tagDBIDX columnid;
	}
}
