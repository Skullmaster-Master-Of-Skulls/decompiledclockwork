using System;
using System.Runtime.InteropServices;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000104 RID: 260
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal class OpoAQDeqOptionsRefCtx
	{
		// Token: 0x04000869 RID: 2153
		internal string consumerName;

		// Token: 0x0400086A RID: 2154
		internal string correlationId;

		// Token: 0x0400086B RID: 2155
		internal byte[] msgId;
	}
}
