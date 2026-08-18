using System;
using System.Security;
using System.Security.Authentication.ExtendedProtection;

namespace System.Net
{
	// Token: 0x0200052B RID: 1323
	[SuppressUnmanagedCodeSecurity]
	internal class SafeLocalFreeChannelBinding : ChannelBinding
	{
		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x06002893 RID: 10387 RVA: 0x000A7CD0 File Offset: 0x000A6CD0
		public override int Size
		{
			get
			{
				return this.size;
			}
		}

		// Token: 0x06002894 RID: 10388 RVA: 0x000A7CD8 File Offset: 0x000A6CD8
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

		// Token: 0x06002895 RID: 10389 RVA: 0x000A7D0F File Offset: 0x000A6D0F
		protected override bool ReleaseHandle()
		{
			return UnsafeNclNativeMethods.SafeNetHandles.LocalFree(this.handle) == IntPtr.Zero;
		}

		// Token: 0x04002796 RID: 10134
		private const int LMEM_FIXED = 0;

		// Token: 0x04002797 RID: 10135
		private int size;
	}
}
