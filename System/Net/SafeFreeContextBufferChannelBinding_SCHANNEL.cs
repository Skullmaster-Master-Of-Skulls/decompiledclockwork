using System;
using System.Security;

namespace System.Net
{
	// Token: 0x0200052E RID: 1326
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeFreeContextBufferChannelBinding_SCHANNEL : SafeFreeContextBufferChannelBinding
	{
		// Token: 0x060028A1 RID: 10401 RVA: 0x000A7FE4 File Offset: 0x000A6FE4
		protected override bool ReleaseHandle()
		{
			return UnsafeNclNativeMethods.SafeNetHandles_SCHANNEL.FreeContextBuffer(this.handle) == 0;
		}
	}
}
