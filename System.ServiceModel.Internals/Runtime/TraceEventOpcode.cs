using System;

namespace System.Runtime
{
	// Token: 0x02000031 RID: 49
	internal enum TraceEventOpcode
	{
		// Token: 0x040000BD RID: 189
		Info,
		// Token: 0x040000BE RID: 190
		Start,
		// Token: 0x040000BF RID: 191
		Stop,
		// Token: 0x040000C0 RID: 192
		Reply = 6,
		// Token: 0x040000C1 RID: 193
		Resume,
		// Token: 0x040000C2 RID: 194
		Suspend,
		// Token: 0x040000C3 RID: 195
		Send,
		// Token: 0x040000C4 RID: 196
		Receive = 240
	}
}
