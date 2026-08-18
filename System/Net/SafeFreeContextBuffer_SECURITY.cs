using System;
using System.Security;

namespace System.Net
{
	// Token: 0x02000514 RID: 1300
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeFreeContextBuffer_SECURITY : SafeFreeContextBuffer
	{
		// Token: 0x06002831 RID: 10289 RVA: 0x000A5C90 File Offset: 0x000A4C90
		internal SafeFreeContextBuffer_SECURITY()
		{
		}

		// Token: 0x06002832 RID: 10290 RVA: 0x000A5C98 File Offset: 0x000A4C98
		protected override bool ReleaseHandle()
		{
			return UnsafeNclNativeMethods.SafeNetHandles_SECURITY.FreeContextBuffer(this.handle) == 0;
		}

		// Token: 0x04002773 RID: 10099
		private const string SECURITY = "security.dll";
	}
}
