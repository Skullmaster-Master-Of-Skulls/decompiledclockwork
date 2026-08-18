using System;
using System.Security;
using System.Security.Permissions;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x02000020 RID: 32
	[SecurityCritical(SecurityCriticalScope.Everything)]
	[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class SafePipeHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060000FD RID: 253 RVA: 0x000038EE File Offset: 0x00001AEE
		private SafePipeHandle() : base(true)
		{
		}

		// Token: 0x060000FE RID: 254 RVA: 0x000038F7 File Offset: 0x00001AF7
		public SafePipeHandle(IntPtr preexistingHandle, bool ownsHandle) : base(ownsHandle)
		{
			base.SetHandle(preexistingHandle);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00003907 File Offset: 0x00001B07
		protected override bool ReleaseHandle()
		{
			return UnsafeNativeMethods.CloseHandle(this.handle);
		}
	}
}
