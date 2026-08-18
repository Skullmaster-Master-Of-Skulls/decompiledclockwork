using System;

namespace System.Net
{
	// Token: 0x020001B0 RID: 432
	internal enum FtpLoginState : byte
	{
		// Token: 0x040013F7 RID: 5111
		NotLoggedIn,
		// Token: 0x040013F8 RID: 5112
		LoggedIn,
		// Token: 0x040013F9 RID: 5113
		LoggedInButNeedsRelogin,
		// Token: 0x040013FA RID: 5114
		ReloginFailed
	}
}
