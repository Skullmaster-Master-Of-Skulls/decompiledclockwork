using System;
using System.Runtime.InteropServices;

namespace System.Data.OleDb
{
	// Token: 0x0200026B RID: 619
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal sealed class tagDBLITERALINFO
	{
		// Token: 0x0600266B RID: 9835 RVA: 0x00104890 File Offset: 0x00103C90
		internal tagDBLITERALINFO()
		{
		}

		// Token: 0x040017E6 RID: 6118
		[MarshalAs(UnmanagedType.LPWStr)]
		internal string pwszLiteralValue;

		// Token: 0x040017E7 RID: 6119
		[MarshalAs(UnmanagedType.LPWStr)]
		internal string pwszInvalidChars;

		// Token: 0x040017E8 RID: 6120
		[MarshalAs(UnmanagedType.LPWStr)]
		internal string pwszInvalidStartingChars;

		// Token: 0x040017E9 RID: 6121
		internal int it;

		// Token: 0x040017EA RID: 6122
		internal int fSupported;

		// Token: 0x040017EB RID: 6123
		internal int cchMaxLen;
	}
}
