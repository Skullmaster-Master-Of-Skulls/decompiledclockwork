using System;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x02000685 RID: 1669
	internal sealed class SafeLsaReturnBufferHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06003C53 RID: 15443 RVA: 0x000CE46E File Offset: 0x000CD46E
		private SafeLsaReturnBufferHandle() : base(true)
		{
		}

		// Token: 0x06003C54 RID: 15444 RVA: 0x000CE477 File Offset: 0x000CD477
		internal SafeLsaReturnBufferHandle(IntPtr handle) : base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x06003C55 RID: 15445 RVA: 0x000CE487 File Offset: 0x000CD487
		internal static SafeLsaReturnBufferHandle InvalidHandle
		{
			get
			{
				return new SafeLsaReturnBufferHandle(IntPtr.Zero);
			}
		}

		// Token: 0x06003C56 RID: 15446 RVA: 0x000CE493 File Offset: 0x000CD493
		protected override bool ReleaseHandle()
		{
			return Win32Native.LsaFreeReturnBuffer(this.handle) >= 0;
		}
	}
}
