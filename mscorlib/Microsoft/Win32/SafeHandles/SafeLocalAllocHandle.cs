using System;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x02000681 RID: 1665
	internal sealed class SafeLocalAllocHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06003C43 RID: 15427 RVA: 0x000CE390 File Offset: 0x000CD390
		private SafeLocalAllocHandle() : base(true)
		{
		}

		// Token: 0x06003C44 RID: 15428 RVA: 0x000CE399 File Offset: 0x000CD399
		internal SafeLocalAllocHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x06003C45 RID: 15429 RVA: 0x000CE3A9 File Offset: 0x000CD3A9
		internal static SafeLocalAllocHandle InvalidHandle
		{
			get
			{
				return new SafeLocalAllocHandle(IntPtr.Zero);
			}
		}

		// Token: 0x06003C46 RID: 15430 RVA: 0x000CE3B5 File Offset: 0x000CD3B5
		protected override bool ReleaseHandle()
		{
			return Win32Native.LocalFree(this.handle) == IntPtr.Zero;
		}
	}
}
