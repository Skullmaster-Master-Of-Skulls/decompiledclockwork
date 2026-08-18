using System;
using System.Runtime.InteropServices;

namespace Oracle.DataAccess.Client
{
	// Token: 0x0200010C RID: 268
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal class OpoAQNtfnDataRefCtx
	{
		// Token: 0x040008A2 RID: 2210
		internal string queueName;

		// Token: 0x040008A3 RID: 2211
		internal string consumerName;
	}
}
