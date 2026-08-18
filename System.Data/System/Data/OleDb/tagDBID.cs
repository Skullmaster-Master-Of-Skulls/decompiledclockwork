using System;
using System.Runtime.InteropServices;

namespace System.Data.OleDb
{
	// Token: 0x02000245 RID: 581
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal sealed class tagDBID
	{
		// Token: 0x040014D1 RID: 5329
		internal Guid uGuid;

		// Token: 0x040014D2 RID: 5330
		internal int eKind;

		// Token: 0x040014D3 RID: 5331
		internal IntPtr ulPropid;
	}
}
