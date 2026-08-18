using System;
using System.Security;

namespace System.Net
{
	// Token: 0x02000522 RID: 1314
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeFreeCredential_SCHANNEL : SafeFreeCredentials
	{
		// Token: 0x06002861 RID: 10337 RVA: 0x000A6335 File Offset: 0x000A5335
		protected override bool ReleaseHandle()
		{
			return UnsafeNclNativeMethods.SafeNetHandles_SCHANNEL.FreeCredentialsHandle(ref this._handle) == 0;
		}

		// Token: 0x04002788 RID: 10120
		private const string SCHANNEL = "schannel.Dll";
	}
}
