using System;
using System.Runtime.InteropServices;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000105 RID: 261
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct OpoAQDeqOptionsValCtx
	{
		// Token: 0x0400086C RID: 2156
		internal int isDirty;

		// Token: 0x0400086D RID: 2157
		internal int deqMode;

		// Token: 0x0400086E RID: 2158
		internal int msgIdSize;

		// Token: 0x0400086F RID: 2159
		internal int deliveryMode;

		// Token: 0x04000870 RID: 2160
		internal int navigation;

		// Token: 0x04000871 RID: 2161
		internal int visibility;

		// Token: 0x04000872 RID: 2162
		internal int wait;
	}
}
