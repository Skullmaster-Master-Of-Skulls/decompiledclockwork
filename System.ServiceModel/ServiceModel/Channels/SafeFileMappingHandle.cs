using System;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008C1 RID: 2241
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeFileMappingHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060055AD RID: 21933 RVA: 0x001398EE File Offset: 0x00137AEE
		internal SafeFileMappingHandle() : base(true)
		{
		}

		// Token: 0x060055AE RID: 21934 RVA: 0x001398F7 File Offset: 0x00137AF7
		protected override bool ReleaseHandle()
		{
			return UnsafeNativeMethods.CloseHandle(this.handle) != 0;
		}
	}
}
