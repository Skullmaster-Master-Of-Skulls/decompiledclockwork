using System;
using System.Runtime.InteropServices;

namespace System.Data.OleDb
{
	// Token: 0x0200026E RID: 622
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal sealed class tagDBPARAMS
	{
		// Token: 0x06002670 RID: 9840 RVA: 0x00104920 File Offset: 0x00103D20
		internal tagDBPARAMS()
		{
		}

		// Token: 0x040017F4 RID: 6132
		internal IntPtr pData;

		// Token: 0x040017F5 RID: 6133
		internal int cParamSets;

		// Token: 0x040017F6 RID: 6134
		internal IntPtr hAccessor;
	}
}
