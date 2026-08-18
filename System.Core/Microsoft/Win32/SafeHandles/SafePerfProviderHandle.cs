using System;
using System.Security;
using System.Threading;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x02000021 RID: 33
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal sealed class SafePerfProviderHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000100 RID: 256 RVA: 0x00003914 File Offset: 0x00001B14
		private SafePerfProviderHandle() : base(true)
		{
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00003920 File Offset: 0x00001B20
		protected override bool ReleaseHandle()
		{
			IntPtr handle = this.handle;
			if (Interlocked.Exchange(ref this.handle, IntPtr.Zero) != IntPtr.Zero)
			{
				uint num = UnsafeNativeMethods.PerfStopProvider(handle);
			}
			return true;
		}
	}
}
