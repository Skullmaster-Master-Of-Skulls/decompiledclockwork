using System;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x02000686 RID: 1670
	internal sealed class SafeProcessHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06003C57 RID: 15447 RVA: 0x000CE4A6 File Offset: 0x000CD4A6
		private SafeProcessHandle() : base(true)
		{
		}

		// Token: 0x06003C58 RID: 15448 RVA: 0x000CE4AF File Offset: 0x000CD4AF
		internal SafeProcessHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x06003C59 RID: 15449 RVA: 0x000CE4BF File Offset: 0x000CD4BF
		internal static SafeProcessHandle InvalidHandle
		{
			get
			{
				return new SafeProcessHandle(IntPtr.Zero);
			}
		}

		// Token: 0x06003C5A RID: 15450 RVA: 0x000CE4CB File Offset: 0x000CD4CB
		protected override bool ReleaseHandle()
		{
			return Win32Native.CloseHandle(this.handle);
		}
	}
}
