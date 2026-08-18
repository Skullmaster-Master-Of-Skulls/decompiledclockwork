using System;

namespace System.Net.Sockets
{
	// Token: 0x020005C4 RID: 1476
	[Flags]
	public enum SocketFlags
	{
		// Token: 0x04002BE6 RID: 11238
		None = 0,
		// Token: 0x04002BE7 RID: 11239
		OutOfBand = 1,
		// Token: 0x04002BE8 RID: 11240
		Peek = 2,
		// Token: 0x04002BE9 RID: 11241
		DontRoute = 4,
		// Token: 0x04002BEA RID: 11242
		MaxIOVectorLength = 16,
		// Token: 0x04002BEB RID: 11243
		Truncated = 256,
		// Token: 0x04002BEC RID: 11244
		ControlDataTruncated = 512,
		// Token: 0x04002BED RID: 11245
		Broadcast = 1024,
		// Token: 0x04002BEE RID: 11246
		Multicast = 2048,
		// Token: 0x04002BEF RID: 11247
		Partial = 32768
	}
}
