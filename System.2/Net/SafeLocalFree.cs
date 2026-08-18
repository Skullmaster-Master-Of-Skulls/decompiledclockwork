using System;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.Net
{
	// Token: 0x020001F1 RID: 497
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeLocalFree : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06001305 RID: 4869 RVA: 0x000643E0 File Offset: 0x000625E0
		private SafeLocalFree() : base(true)
		{
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x000643E9 File Offset: 0x000625E9
		private SafeLocalFree(bool ownsHandle) : base(ownsHandle)
		{
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x000643F4 File Offset: 0x000625F4
		public static SafeLocalFree LocalAlloc(int cb)
		{
			SafeLocalFree safeLocalFree = UnsafeNclNativeMethods.SafeNetHandles.LocalAlloc(0, (UIntPtr)((ulong)((long)cb)));
			if (safeLocalFree.IsInvalid)
			{
				safeLocalFree.SetHandleAsInvalid();
				throw new OutOfMemoryException();
			}
			return safeLocalFree;
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x00064424 File Offset: 0x00062624
		protected override bool ReleaseHandle()
		{
			return UnsafeNclNativeMethods.SafeNetHandles.LocalFree(this.handle) == IntPtr.Zero;
		}

		// Token: 0x0400153E RID: 5438
		private const int LMEM_FIXED = 0;

		// Token: 0x0400153F RID: 5439
		private const int NULL = 0;

		// Token: 0x04001540 RID: 5440
		public static SafeLocalFree Zero = new SafeLocalFree(false);
	}
}
