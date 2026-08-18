using System;
using System.Runtime.InteropServices;

namespace System.Data.OleDb
{
	// Token: 0x0200024B RID: 587
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal sealed class tagDBPROPINFOSET
	{
		// Token: 0x06002061 RID: 8289 RVA: 0x002800B8 File Offset: 0x0027F4B8
		internal tagDBPROPINFOSET()
		{
		}

		// Token: 0x040014EE RID: 5358
		internal IntPtr rgPropertyInfos;

		// Token: 0x040014EF RID: 5359
		internal int cPropertyInfos;

		// Token: 0x040014F0 RID: 5360
		internal Guid guidPropertySet;
	}
}
