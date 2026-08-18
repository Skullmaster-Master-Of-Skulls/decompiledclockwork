using System;

namespace MailBee
{
	// Token: 0x02000018 RID: 24
	[Flags]
	public enum AuthenticationOptions
	{
		// Token: 0x04000075 RID: 117
		None = 0,
		// Token: 0x04000076 RID: 118
		TryUnsupportedMethods = 1,
		// Token: 0x04000077 RID: 119
		UseSingleMethodOnly = 2,
		// Token: 0x04000078 RID: 120
		PreferSimpleMethods = 4,
		// Token: 0x04000079 RID: 121
		DisableSimpleMethodAfterSecure = 8,
		// Token: 0x0400007A RID: 122
		PreferSspiOverNegotiateStream = 16,
		// Token: 0x0400007B RID: 123
		UseLocalDomainAsDefault = 32,
		// Token: 0x0400007C RID: 124
		BypassLoginProcedure = 64
	}
}
