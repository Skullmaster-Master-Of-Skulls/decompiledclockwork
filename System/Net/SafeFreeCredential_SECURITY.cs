using System;
using System.Security;

namespace System.Net
{
	// Token: 0x02000520 RID: 1312
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeFreeCredential_SECURITY : SafeFreeCredentials
	{
		// Token: 0x0600285D RID: 10333 RVA: 0x000A6305 File Offset: 0x000A5305
		protected override bool ReleaseHandle()
		{
			return UnsafeNclNativeMethods.SafeNetHandles_SECURITY.FreeCredentialsHandle(ref this._handle) == 0;
		}

		// Token: 0x04002786 RID: 10118
		private const string SECURITY = "security.Dll";
	}
}
