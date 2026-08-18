using System;
using System.Security;

namespace System.Net
{
	// Token: 0x02000204 RID: 516
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeFreeContextBufferChannelBinding_SECURITY : SafeFreeContextBufferChannelBinding
	{
		// Token: 0x06001366 RID: 4966 RVA: 0x0006616C File Offset: 0x0006436C
		protected override bool ReleaseHandle()
		{
			return UnsafeNclNativeMethods.SafeNetHandles_SECURITY.FreeContextBuffer(this.handle) == 0;
		}
	}
}
