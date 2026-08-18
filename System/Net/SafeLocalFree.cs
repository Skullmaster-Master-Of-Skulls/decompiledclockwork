using System;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.Net
{
	// Token: 0x02000517 RID: 1303
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeLocalFree : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06002837 RID: 10295 RVA: 0x000A5CD8 File Offset: 0x000A4CD8
		private SafeLocalFree() : base(true)
		{
		}

		// Token: 0x06002838 RID: 10296 RVA: 0x000A5CE1 File Offset: 0x000A4CE1
		private SafeLocalFree(bool ownsHandle) : base(ownsHandle)
		{
		}

		// Token: 0x06002839 RID: 10297 RVA: 0x000A5CEC File Offset: 0x000A4CEC
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

		// Token: 0x0600283A RID: 10298 RVA: 0x000A5D1C File Offset: 0x000A4D1C
		protected override bool ReleaseHandle()
		{
			return UnsafeNclNativeMethods.SafeNetHandles.LocalFree(this.handle) == IntPtr.Zero;
		}

		// Token: 0x04002776 RID: 10102
		private const int LMEM_FIXED = 0;

		// Token: 0x04002777 RID: 10103
		private const int NULL = 0;

		// Token: 0x04002778 RID: 10104
		public static SafeLocalFree Zero = new SafeLocalFree(false);
	}
}
