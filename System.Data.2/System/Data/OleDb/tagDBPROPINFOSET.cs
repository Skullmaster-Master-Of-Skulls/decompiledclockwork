using System;
using System.Runtime.InteropServices;

namespace System.Data.OleDb
{
	// Token: 0x02000270 RID: 624
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal sealed class tagDBPROPINFOSET
	{
		// Token: 0x06002672 RID: 9842 RVA: 0x0010496C File Offset: 0x00103D6C
		internal tagDBPROPINFOSET()
		{
		}

		// Token: 0x04001800 RID: 6144
		internal IntPtr rgPropertyInfos;

		// Token: 0x04001801 RID: 6145
		internal int cPropertyInfos;

		// Token: 0x04001802 RID: 6146
		internal Guid guidPropertySet;
	}
}
