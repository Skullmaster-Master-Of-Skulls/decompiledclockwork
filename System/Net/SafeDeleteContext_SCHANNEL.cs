using System;
using System.Security;

namespace System.Net
{
	// Token: 0x02000526 RID: 1318
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeDeleteContext_SCHANNEL : SafeDeleteContext
	{
		// Token: 0x06002873 RID: 10355 RVA: 0x000A77BC File Offset: 0x000A67BC
		internal SafeDeleteContext_SCHANNEL()
		{
		}

		// Token: 0x06002874 RID: 10356 RVA: 0x000A77C4 File Offset: 0x000A67C4
		protected override bool ReleaseHandle()
		{
			if (this._EffectiveCredential != null)
			{
				this._EffectiveCredential.DangerousRelease();
			}
			return UnsafeNclNativeMethods.SafeNetHandles_SCHANNEL.DeleteSecurityContext(ref this._handle) == 0;
		}

		// Token: 0x0400278F RID: 10127
		private const string SCHANNEL = "schannel.Dll";
	}
}
