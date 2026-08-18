using System;
using System.Security;

namespace System.Net
{
	// Token: 0x020001FC RID: 508
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeFreeCredential_SECURITY : SafeFreeCredentials
	{
		// Token: 0x06001338 RID: 4920 RVA: 0x00064D01 File Offset: 0x00062F01
		protected override bool ReleaseHandle()
		{
			return UnsafeNclNativeMethods.SafeNetHandles_SECURITY.FreeCredentialsHandle(ref this._handle) == 0;
		}
	}
}
