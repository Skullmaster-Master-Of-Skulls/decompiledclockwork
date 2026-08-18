using System;
using System.Runtime.InteropServices;

namespace System.Data.OleDb
{
	// Token: 0x02000242 RID: 578
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal sealed class tagDBBINDING
	{
		// Token: 0x06002058 RID: 8280 RVA: 0x0027FF58 File Offset: 0x0027F358
		internal tagDBBINDING()
		{
		}

		// Token: 0x040014B6 RID: 5302
		internal IntPtr iOrdinal;

		// Token: 0x040014B7 RID: 5303
		internal IntPtr obValue;

		// Token: 0x040014B8 RID: 5304
		internal IntPtr obLength;

		// Token: 0x040014B9 RID: 5305
		internal IntPtr obStatus;

		// Token: 0x040014BA RID: 5306
		internal IntPtr pTypeInfo;

		// Token: 0x040014BB RID: 5307
		internal IntPtr pObject;

		// Token: 0x040014BC RID: 5308
		internal IntPtr pBindExt;

		// Token: 0x040014BD RID: 5309
		internal int dwPart;

		// Token: 0x040014BE RID: 5310
		internal int dwMemOwner;

		// Token: 0x040014BF RID: 5311
		internal int eParamIO;

		// Token: 0x040014C0 RID: 5312
		internal IntPtr cbMaxLen;

		// Token: 0x040014C1 RID: 5313
		internal int dwFlags;

		// Token: 0x040014C2 RID: 5314
		internal short wType;

		// Token: 0x040014C3 RID: 5315
		internal byte bPrecision;

		// Token: 0x040014C4 RID: 5316
		internal byte bScale;
	}
}
