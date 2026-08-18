using System;
using System.Runtime.InteropServices;

namespace System.Data.OleDb
{
	// Token: 0x02000271 RID: 625
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal sealed class tagDBPROPINFO
	{
		// Token: 0x06002673 RID: 9843 RVA: 0x00104980 File Offset: 0x00103D80
		internal tagDBPROPINFO()
		{
		}

		// Token: 0x04001803 RID: 6147
		[MarshalAs(UnmanagedType.LPWStr)]
		internal string pwszDescription;

		// Token: 0x04001804 RID: 6148
		internal int dwPropertyID;

		// Token: 0x04001805 RID: 6149
		internal int dwFlags;

		// Token: 0x04001806 RID: 6150
		internal short vtType;

		// Token: 0x04001807 RID: 6151
		[MarshalAs(UnmanagedType.Struct)]
		internal object vValue;
	}
}
