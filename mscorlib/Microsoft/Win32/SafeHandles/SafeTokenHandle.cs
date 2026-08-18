using System;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x02000688 RID: 1672
	internal sealed class SafeTokenHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06003C5E RID: 15454 RVA: 0x000CE4FE File Offset: 0x000CD4FE
		private SafeTokenHandle() : base(true)
		{
		}

		// Token: 0x06003C5F RID: 15455 RVA: 0x000CE507 File Offset: 0x000CD507
		internal SafeTokenHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x06003C60 RID: 15456 RVA: 0x000CE517 File Offset: 0x000CD517
		internal static SafeTokenHandle InvalidHandle
		{
			get
			{
				return new SafeTokenHandle(IntPtr.Zero);
			}
		}

		// Token: 0x06003C61 RID: 15457 RVA: 0x000CE523 File Offset: 0x000CD523
		protected override bool ReleaseHandle()
		{
			return Win32Native.CloseHandle(this.handle);
		}
	}
}
