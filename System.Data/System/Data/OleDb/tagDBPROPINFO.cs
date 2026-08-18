using System;
using System.Runtime.InteropServices;

namespace System.Data.OleDb
{
	// Token: 0x0200024C RID: 588
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal sealed class tagDBPROPINFO
	{
		// Token: 0x06002062 RID: 8290 RVA: 0x002800D8 File Offset: 0x0027F4D8
		internal tagDBPROPINFO()
		{
		}

		// Token: 0x040014F1 RID: 5361
		[MarshalAs(UnmanagedType.LPWStr)]
		internal string pwszDescription;

		// Token: 0x040014F2 RID: 5362
		internal int dwPropertyID;

		// Token: 0x040014F3 RID: 5363
		internal int dwFlags;

		// Token: 0x040014F4 RID: 5364
		internal short vtType;

		// Token: 0x040014F5 RID: 5365
		[MarshalAs(UnmanagedType.Struct)]
		internal object vValue;
	}
}
