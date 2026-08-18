using System;

namespace System.Net
{
	// Token: 0x020004DB RID: 1243
	internal enum FtpLoginState : byte
	{
		// Token: 0x04002640 RID: 9792
		NotLoggedIn,
		// Token: 0x04002641 RID: 9793
		LoggedIn,
		// Token: 0x04002642 RID: 9794
		LoggedInButNeedsRelogin,
		// Token: 0x04002643 RID: 9795
		ReloginFailed
	}
}
