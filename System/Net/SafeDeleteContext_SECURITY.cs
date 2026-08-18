using System;
using System.Security;

namespace System.Net
{
	// Token: 0x02000524 RID: 1316
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeDeleteContext_SECURITY : SafeDeleteContext
	{
		// Token: 0x0600286F RID: 10351 RVA: 0x000A7766 File Offset: 0x000A6766
		internal SafeDeleteContext_SECURITY()
		{
		}

		// Token: 0x06002870 RID: 10352 RVA: 0x000A776E File Offset: 0x000A676E
		protected override bool ReleaseHandle()
		{
			if (this._EffectiveCredential != null)
			{
				this._EffectiveCredential.DangerousRelease();
			}
			return UnsafeNclNativeMethods.SafeNetHandles_SECURITY.DeleteSecurityContext(ref this._handle) == 0;
		}

		// Token: 0x0400278D RID: 10125
		private const string SECURITY = "security.Dll";
	}
}
