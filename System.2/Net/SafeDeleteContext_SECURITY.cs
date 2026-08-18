using System;
using System.Security;

namespace System.Net
{
	// Token: 0x020001FE RID: 510
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeDeleteContext_SECURITY : SafeDeleteContext
	{
		// Token: 0x06001343 RID: 4931 RVA: 0x00065C65 File Offset: 0x00063E65
		internal SafeDeleteContext_SECURITY()
		{
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x00065C6D File Offset: 0x00063E6D
		protected override bool ReleaseHandle()
		{
			if (this._EffectiveCredential != null)
			{
				this._EffectiveCredential.DangerousRelease();
			}
			return UnsafeNclNativeMethods.SafeNetHandles_SECURITY.DeleteSecurityContext(ref this._handle) == 0;
		}
	}
}
