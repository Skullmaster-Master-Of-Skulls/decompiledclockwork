using System;
using System.Security;

namespace System.Net
{
	// Token: 0x0200052D RID: 1325
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeFreeContextBufferChannelBinding_SECURITY : SafeFreeContextBufferChannelBinding
	{
		// Token: 0x0600289F RID: 10399 RVA: 0x000A7FCC File Offset: 0x000A6FCC
		protected override bool ReleaseHandle()
		{
			return UnsafeNclNativeMethods.SafeNetHandles_SECURITY.FreeContextBuffer(this.handle) == 0;
		}
	}
}
