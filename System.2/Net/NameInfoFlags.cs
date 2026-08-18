using System;

namespace System.Net
{
	// Token: 0x020001D9 RID: 473
	[Flags]
	internal enum NameInfoFlags
	{
		// Token: 0x040014F9 RID: 5369
		NI_NOFQDN = 1,
		// Token: 0x040014FA RID: 5370
		NI_NUMERICHOST = 2,
		// Token: 0x040014FB RID: 5371
		NI_NAMEREQD = 4,
		// Token: 0x040014FC RID: 5372
		NI_NUMERICSERV = 8,
		// Token: 0x040014FD RID: 5373
		NI_DGRAM = 16
	}
}
