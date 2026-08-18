using System;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.Net
{
	// Token: 0x02000518 RID: 1304
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeGlobalFree : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600283C RID: 10300 RVA: 0x000A5D40 File Offset: 0x000A4D40
		private SafeGlobalFree() : base(true)
		{
		}

		// Token: 0x0600283D RID: 10301 RVA: 0x000A5D49 File Offset: 0x000A4D49
		private SafeGlobalFree(bool ownsHandle) : base(ownsHandle)
		{
		}

		// Token: 0x0600283E RID: 10302 RVA: 0x000A5D52 File Offset: 0x000A4D52
		protected override bool ReleaseHandle()
		{
			return UnsafeNclNativeMethods.SafeNetHandles.GlobalFree(this.handle) == IntPtr.Zero;
		}
	}
}
