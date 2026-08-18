using System;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.CodeDom.Compiler
{
	// Token: 0x0200067B RID: 1659
	internal static class FileIntegrity
	{
		// Token: 0x17000E99 RID: 3737
		// (get) Token: 0x06003D35 RID: 15669 RVA: 0x000FBF7D File Offset: 0x000FA17D
		public static bool IsEnabled
		{
			get
			{
				return FileIntegrity.s_lazyIsEnabled.Value;
			}
		}

		// Token: 0x06003D36 RID: 15670 RVA: 0x000FBF8C File Offset: 0x000FA18C
		public static void MarkAsTrusted(SafeFileHandle safeFileHandle)
		{
			int errorCode = UnsafeNativeMethods.WldpSetDynamicCodeTrust(safeFileHandle);
			Marshal.ThrowExceptionForHR(errorCode, new IntPtr(-1));
		}

		// Token: 0x06003D37 RID: 15671 RVA: 0x000FBFAC File Offset: 0x000FA1AC
		public static bool IsTrusted(SafeFileHandle safeFileHandle)
		{
			int num = UnsafeNativeMethods.WldpQueryDynamicCodeTrust(safeFileHandle, IntPtr.Zero, 0U);
			if (num == -805305819)
			{
				return false;
			}
			Marshal.ThrowExceptionForHR(num, new IntPtr(-1));
			return true;
		}

		// Token: 0x04002C9F RID: 11423
		private static readonly Lazy<bool> s_lazyIsEnabled = new Lazy<bool>(delegate()
		{
			Version version = Environment.OSVersion.Version;
			if (version.Major < 6 || (version.Major == 6 && version.Minor < 2))
			{
				return false;
			}
			bool result;
			using (SafeLibraryHandle safeLibraryHandle = SafeLibraryHandle.LoadLibraryEx("wldp.dll", IntPtr.Zero, 2048))
			{
				if (safeLibraryHandle.IsInvalid)
				{
					result = false;
				}
				else
				{
					IntPtr moduleHandle = UnsafeNativeMethods.GetModuleHandle("wldp.dll");
					if (!(moduleHandle != IntPtr.Zero) || !(IntPtr.Zero != UnsafeNativeMethods.GetProcAddress(moduleHandle, "WldpIsDynamicCodePolicyEnabled")) || !(IntPtr.Zero != UnsafeNativeMethods.GetProcAddress(moduleHandle, "WldpSetDynamicCodeTrust")) || !(IntPtr.Zero != UnsafeNativeMethods.GetProcAddress(moduleHandle, "WldpQueryDynamicCodeTrust")))
					{
						result = false;
					}
					else
					{
						int num = 0;
						int errorCode = UnsafeNativeMethods.WldpIsDynamicCodePolicyEnabled(out num);
						Marshal.ThrowExceptionForHR(errorCode, new IntPtr(-1));
						result = (num != 0);
					}
				}
			}
			return result;
		});
	}
}
