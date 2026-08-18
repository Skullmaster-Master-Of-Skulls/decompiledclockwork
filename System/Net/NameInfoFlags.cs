using System;

namespace System.Net
{
	// Token: 0x02000502 RID: 1282
	[Flags]
	internal enum NameInfoFlags
	{
		// Token: 0x0400273A RID: 10042
		NI_NOFQDN = 1,
		// Token: 0x0400273B RID: 10043
		NI_NUMERICHOST = 2,
		// Token: 0x0400273C RID: 10044
		NI_NAMEREQD = 4,
		// Token: 0x0400273D RID: 10045
		NI_NUMERICSERV = 8,
		// Token: 0x0400273E RID: 10046
		NI_DGRAM = 16
	}
}
