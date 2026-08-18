using System;
using System.Security;

namespace System.Net
{
	// Token: 0x020001F0 RID: 496
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeFreeContextBuffer_SECURITY : SafeFreeContextBuffer
	{
		// Token: 0x06001303 RID: 4867 RVA: 0x000643C8 File Offset: 0x000625C8
		internal SafeFreeContextBuffer_SECURITY()
		{
		}

		// Token: 0x06001304 RID: 4868 RVA: 0x000643D0 File Offset: 0x000625D0
		protected override bool ReleaseHandle()
		{
			return UnsafeNclNativeMethods.SafeNetHandles_SECURITY.FreeContextBuffer(this.handle) == 0;
		}
	}
}
