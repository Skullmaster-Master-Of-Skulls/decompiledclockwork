using System;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.Net
{
	// Token: 0x020001F2 RID: 498
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeGlobalFree : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600130A RID: 4874 RVA: 0x00064448 File Offset: 0x00062648
		private SafeGlobalFree() : base(true)
		{
		}

		// Token: 0x0600130B RID: 4875 RVA: 0x00064451 File Offset: 0x00062651
		private SafeGlobalFree(bool ownsHandle) : base(ownsHandle)
		{
		}

		// Token: 0x0600130C RID: 4876 RVA: 0x0006445A File Offset: 0x0006265A
		protected override bool ReleaseHandle()
		{
			return UnsafeNclNativeMethods.SafeNetHandles.GlobalFree(this.handle) == IntPtr.Zero;
		}
	}
}
