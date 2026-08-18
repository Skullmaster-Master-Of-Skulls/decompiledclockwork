using System;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.Net
{
	// Token: 0x020001F4 RID: 500
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeLoadLibrary : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06001314 RID: 4884 RVA: 0x00064558 File Offset: 0x00062758
		static SafeLoadLibrary()
		{
			try
			{
				IntPtr moduleHandleW = UnsafeNclNativeMethods.SafeNetHandles.GetModuleHandleW("kernel32.dll");
				if (moduleHandleW != IntPtr.Zero && UnsafeNclNativeMethods.GetProcAddress(moduleHandleW, "AddDllDirectory") != IntPtr.Zero)
				{
					SafeLoadLibrary._flags = 2048U;
				}
			}
			catch
			{
			}
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x000645C4 File Offset: 0x000627C4
		private SafeLoadLibrary() : base(true)
		{
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x000645CD File Offset: 0x000627CD
		private SafeLoadLibrary(bool ownsHandle) : base(ownsHandle)
		{
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x000645D8 File Offset: 0x000627D8
		public static SafeLoadLibrary LoadLibraryEx(string library)
		{
			SafeLoadLibrary safeLoadLibrary = UnsafeNclNativeMethods.SafeNetHandles.LoadLibraryExW(library, null, SafeLoadLibrary._flags);
			if (safeLoadLibrary.IsInvalid)
			{
				safeLoadLibrary.SetHandleAsInvalid();
			}
			return safeLoadLibrary;
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x00064604 File Offset: 0x00062804
		public bool HasFunction(string functionName)
		{
			IntPtr procAddress = UnsafeNclNativeMethods.GetProcAddress(this, functionName);
			return procAddress != IntPtr.Zero;
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x00064624 File Offset: 0x00062824
		protected override bool ReleaseHandle()
		{
			return UnsafeNclNativeMethods.SafeNetHandles.FreeLibrary(this.handle);
		}

		// Token: 0x04001544 RID: 5444
		private const string KERNEL32 = "kernel32.dll";

		// Token: 0x04001545 RID: 5445
		private const string AddDllDirectory = "AddDllDirectory";

		// Token: 0x04001546 RID: 5446
		private const uint LOAD_LIBRARY_SEARCH_SYSTEM32 = 2048U;

		// Token: 0x04001547 RID: 5447
		public static readonly SafeLoadLibrary Zero = new SafeLoadLibrary(false);

		// Token: 0x04001548 RID: 5448
		private static uint _flags = 0U;
	}
}
