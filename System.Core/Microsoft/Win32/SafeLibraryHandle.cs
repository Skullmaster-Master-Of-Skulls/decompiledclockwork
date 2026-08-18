using System;
using System.Security;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Win32
{
	// Token: 0x02000013 RID: 19
	[SecurityCritical(SecurityCriticalScope.Everything)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	internal sealed class SafeLibraryHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060000C0 RID: 192 RVA: 0x00003308 File Offset: 0x00001508
		internal SafeLibraryHandle() : base(true)
		{
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003311 File Offset: 0x00001511
		protected override bool ReleaseHandle()
		{
			return UnsafeNativeMethods.FreeLibrary(this.handle);
		}
	}
}
