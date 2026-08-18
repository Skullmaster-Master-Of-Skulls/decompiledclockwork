using System;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x02000684 RID: 1668
	internal sealed class SafeLsaPolicyHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06003C4F RID: 15439 RVA: 0x000CE439 File Offset: 0x000CD439
		private SafeLsaPolicyHandle() : base(true)
		{
		}

		// Token: 0x06003C50 RID: 15440 RVA: 0x000CE442 File Offset: 0x000CD442
		internal SafeLsaPolicyHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x06003C51 RID: 15441 RVA: 0x000CE452 File Offset: 0x000CD452
		internal static SafeLsaPolicyHandle InvalidHandle
		{
			get
			{
				return new SafeLsaPolicyHandle(IntPtr.Zero);
			}
		}

		// Token: 0x06003C52 RID: 15442 RVA: 0x000CE45E File Offset: 0x000CD45E
		protected override bool ReleaseHandle()
		{
			return Win32Native.LsaClose(this.handle) == 0;
		}
	}
}
