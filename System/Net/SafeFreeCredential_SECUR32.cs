using System;
using System.Security;

namespace System.Net
{
	// Token: 0x02000521 RID: 1313
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeFreeCredential_SECUR32 : SafeFreeCredentials
	{
		// Token: 0x0600285F RID: 10335 RVA: 0x000A631D File Offset: 0x000A531D
		protected override bool ReleaseHandle()
		{
			return UnsafeNclNativeMethods.SafeNetHandles_SECUR32.FreeCredentialsHandle(ref this._handle) == 0;
		}

		// Token: 0x04002787 RID: 10119
		private const string SECUR32 = "secur32.Dll";
	}
}
