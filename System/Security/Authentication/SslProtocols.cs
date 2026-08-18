using System;

namespace System.Security.Authentication
{
	// Token: 0x0200058E RID: 1422
	[Flags]
	public enum SslProtocols
	{
		// Token: 0x040029E6 RID: 10726
		None = 0,
		// Token: 0x040029E7 RID: 10727
		Ssl2 = 12,
		// Token: 0x040029E8 RID: 10728
		Ssl3 = 48,
		// Token: 0x040029E9 RID: 10729
		Tls = 192,
		// Token: 0x040029EA RID: 10730
		Default = 240
	}
}
