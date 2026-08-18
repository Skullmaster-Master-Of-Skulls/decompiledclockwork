using System;
using System.Security;
using System.Security.Authentication.ExtendedProtection;

namespace System.Net
{
	// Token: 0x02000202 RID: 514
	[SuppressUnmanagedCodeSecurity]
	internal class SafeLocalFreeChannelBinding : ChannelBinding
	{
		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x0600135C RID: 4956 RVA: 0x0006600C File Offset: 0x0006420C
		public override int Size
		{
			get
			{
				return this.size;
			}
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x00066014 File Offset: 0x00064214
		public static SafeLocalFreeChannelBinding LocalAlloc(int cb)
		{
			SafeLocalFreeChannelBinding safeLocalFreeChannelBinding = UnsafeNclNativeMethods.SafeNetHandles.LocalAllocChannelBinding(0, (UIntPtr)((ulong)((long)cb)));
			if (safeLocalFreeChannelBinding.IsInvalid)
			{
				safeLocalFreeChannelBinding.SetHandleAsInvalid();
				throw new OutOfMemoryException();
			}
			safeLocalFreeChannelBinding.size = cb;
			return safeLocalFreeChannelBinding;
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x0006604B File Offset: 0x0006424B
		protected override bool ReleaseHandle()
		{
			return UnsafeNclNativeMethods.SafeNetHandles.LocalFree(this.handle) == IntPtr.Zero;
		}

		// Token: 0x0400155E RID: 5470
		private const int LMEM_FIXED = 0;

		// Token: 0x0400155F RID: 5471
		private int size;
	}
}
