using System;
using System.Security;

namespace System.Net
{
	// Token: 0x02000525 RID: 1317
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeDeleteContext_SECUR32 : SafeDeleteContext
	{
		// Token: 0x06002871 RID: 10353 RVA: 0x000A7791 File Offset: 0x000A6791
		internal SafeDeleteContext_SECUR32()
		{
		}

		// Token: 0x06002872 RID: 10354 RVA: 0x000A7799 File Offset: 0x000A6799
		protected override bool ReleaseHandle()
		{
			if (this._EffectiveCredential != null)
			{
				this._EffectiveCredential.DangerousRelease();
			}
			return UnsafeNclNativeMethods.SafeNetHandles_SECUR32.DeleteSecurityContext(ref this._handle) == 0;
		}

		// Token: 0x0400278E RID: 10126
		private const string SECUR32 = "secur32.Dll";
	}
}
