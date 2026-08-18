using System;
using System.Runtime.InteropServices;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000251 RID: 593
	[StructLayout(LayoutKind.Sequential)]
	internal class GENERIC_MAPPING
	{
		// Token: 0x0400192D RID: 6445
		internal uint genericRead;

		// Token: 0x0400192E RID: 6446
		internal uint genericWrite;

		// Token: 0x0400192F RID: 6447
		internal uint genericExecute;

		// Token: 0x04001930 RID: 6448
		internal uint genericAll;
	}
}
