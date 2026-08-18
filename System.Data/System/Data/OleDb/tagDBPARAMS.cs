using System;
using System.Runtime.InteropServices;

namespace System.Data.OleDb
{
	// Token: 0x02000249 RID: 585
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal sealed class tagDBPARAMS
	{
		// Token: 0x0600205F RID: 8287 RVA: 0x00280058 File Offset: 0x0027F458
		internal tagDBPARAMS()
		{
		}

		// Token: 0x040014E2 RID: 5346
		internal IntPtr pData;

		// Token: 0x040014E3 RID: 5347
		internal int cParamSets;

		// Token: 0x040014E4 RID: 5348
		internal IntPtr hAccessor;
	}
}
