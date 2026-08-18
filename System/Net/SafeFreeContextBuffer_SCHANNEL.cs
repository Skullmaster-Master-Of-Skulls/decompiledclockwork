using System;
using System.Security;

namespace System.Net
{
	// Token: 0x02000515 RID: 1301
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeFreeContextBuffer_SCHANNEL : SafeFreeContextBuffer
	{
		// Token: 0x06002833 RID: 10291 RVA: 0x000A5CA8 File Offset: 0x000A4CA8
		internal SafeFreeContextBuffer_SCHANNEL()
		{
		}

		// Token: 0x06002834 RID: 10292 RVA: 0x000A5CB0 File Offset: 0x000A4CB0
		protected override bool ReleaseHandle()
		{
			return UnsafeNclNativeMethods.SafeNetHandles_SCHANNEL.FreeContextBuffer(this.handle) == 0;
		}

		// Token: 0x04002774 RID: 10100
		private const string SCHANNEL = "schannel.dll";
	}
}
