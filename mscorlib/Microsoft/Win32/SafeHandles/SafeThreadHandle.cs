using System;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x02000687 RID: 1671
	internal sealed class SafeThreadHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06003C5B RID: 15451 RVA: 0x000CE4D8 File Offset: 0x000CD4D8
		private SafeThreadHandle() : base(true)
		{
		}

		// Token: 0x06003C5C RID: 15452 RVA: 0x000CE4E1 File Offset: 0x000CD4E1
		internal SafeThreadHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x06003C5D RID: 15453 RVA: 0x000CE4F1 File Offset: 0x000CD4F1
		protected override bool ReleaseHandle()
		{
			return Win32Native.CloseHandle(this.handle);
		}
	}
}
