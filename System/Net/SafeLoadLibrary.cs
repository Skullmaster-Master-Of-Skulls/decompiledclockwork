using System;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.Net
{
	// Token: 0x0200051A RID: 1306
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeLoadLibrary : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06002846 RID: 10310 RVA: 0x000A5E4F File Offset: 0x000A4E4F
		private SafeLoadLibrary() : base(true)
		{
		}

		// Token: 0x06002847 RID: 10311 RVA: 0x000A5E58 File Offset: 0x000A4E58
		private SafeLoadLibrary(bool ownsHandle) : base(ownsHandle)
		{
		}

		// Token: 0x06002848 RID: 10312 RVA: 0x000A5E64 File Offset: 0x000A4E64
		public static SafeLoadLibrary LoadLibraryEx(string library)
		{
			SafeLoadLibrary safeLoadLibrary = ComNetOS.IsWin9x ? UnsafeNclNativeMethods.SafeNetHandles.LoadLibraryExA(library, null, 0U) : UnsafeNclNativeMethods.SafeNetHandles.LoadLibraryExW(library, null, 0U);
			if (safeLoadLibrary.IsInvalid)
			{
				safeLoadLibrary.SetHandleAsInvalid();
			}
			return safeLoadLibrary;
		}

		// Token: 0x06002849 RID: 10313 RVA: 0x000A5E9C File Offset: 0x000A4E9C
		protected override bool ReleaseHandle()
		{
			return UnsafeNclNativeMethods.SafeNetHandles.FreeLibrary(this.handle);
		}

		// Token: 0x0400277C RID: 10108
		private const string KERNEL32 = "kernel32.dll";

		// Token: 0x0400277D RID: 10109
		public static readonly SafeLoadLibrary Zero = new SafeLoadLibrary(false);
	}
}
