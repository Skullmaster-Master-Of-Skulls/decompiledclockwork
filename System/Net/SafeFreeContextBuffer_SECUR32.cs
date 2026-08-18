using System;
using System.Security;

namespace System.Net
{
	// Token: 0x02000516 RID: 1302
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeFreeContextBuffer_SECUR32 : SafeFreeContextBuffer
	{
		// Token: 0x06002835 RID: 10293 RVA: 0x000A5CC0 File Offset: 0x000A4CC0
		internal SafeFreeContextBuffer_SECUR32()
		{
		}

		// Token: 0x06002836 RID: 10294 RVA: 0x000A5CC8 File Offset: 0x000A4CC8
		protected override bool ReleaseHandle()
		{
			return UnsafeNclNativeMethods.SafeNetHandles_SECUR32.FreeContextBuffer(this.handle) == 0;
		}

		// Token: 0x04002775 RID: 10101
		private const string SECUR32 = "secur32.dll";
	}
}
