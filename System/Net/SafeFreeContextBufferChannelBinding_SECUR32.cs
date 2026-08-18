using System;
using System.Security;

namespace System.Net
{
	// Token: 0x0200052F RID: 1327
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeFreeContextBufferChannelBinding_SECUR32 : SafeFreeContextBufferChannelBinding
	{
		// Token: 0x060028A3 RID: 10403 RVA: 0x000A7FFC File Offset: 0x000A6FFC
		protected override bool ReleaseHandle()
		{
			return UnsafeNclNativeMethods.SafeNetHandles_SECUR32.FreeContextBuffer(this.handle) == 0;
		}
	}
}
