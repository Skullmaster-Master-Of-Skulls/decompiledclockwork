using System;
using System.Runtime.InteropServices;

namespace System.Data.OleDb
{
	// Token: 0x02000246 RID: 582
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal sealed class tagDBLITERALINFO
	{
		// Token: 0x0600205A RID: 8282 RVA: 0x0027FF98 File Offset: 0x0027F398
		internal tagDBLITERALINFO()
		{
		}

		// Token: 0x040014D4 RID: 5332
		[MarshalAs(UnmanagedType.LPWStr)]
		internal string pwszLiteralValue;

		// Token: 0x040014D5 RID: 5333
		[MarshalAs(UnmanagedType.LPWStr)]
		internal string pwszInvalidChars;

		// Token: 0x040014D6 RID: 5334
		[MarshalAs(UnmanagedType.LPWStr)]
		internal string pwszInvalidStartingChars;

		// Token: 0x040014D7 RID: 5335
		internal int it;

		// Token: 0x040014D8 RID: 5336
		internal int fSupported;

		// Token: 0x040014D9 RID: 5337
		internal int cchMaxLen;
	}
}
