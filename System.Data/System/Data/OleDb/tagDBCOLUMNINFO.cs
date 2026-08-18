using System;
using System.Runtime.InteropServices;

namespace System.Data.OleDb
{
	// Token: 0x0200024A RID: 586
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal sealed class tagDBCOLUMNINFO
	{
		// Token: 0x06002060 RID: 8288 RVA: 0x00280078 File Offset: 0x0027F478
		internal tagDBCOLUMNINFO()
		{
		}

		// Token: 0x040014E5 RID: 5349
		[MarshalAs(UnmanagedType.LPWStr)]
		internal string pwszName;

		// Token: 0x040014E6 RID: 5350
		internal IntPtr pTypeInfo = (IntPtr)0;

		// Token: 0x040014E7 RID: 5351
		internal IntPtr iOrdinal = (IntPtr)0;

		// Token: 0x040014E8 RID: 5352
		internal int dwFlags;

		// Token: 0x040014E9 RID: 5353
		internal IntPtr ulColumnSize = (IntPtr)0;

		// Token: 0x040014EA RID: 5354
		internal short wType;

		// Token: 0x040014EB RID: 5355
		internal byte bPrecision;

		// Token: 0x040014EC RID: 5356
		internal byte bScale;

		// Token: 0x040014ED RID: 5357
		internal tagDBIDX columnid;
	}
}
