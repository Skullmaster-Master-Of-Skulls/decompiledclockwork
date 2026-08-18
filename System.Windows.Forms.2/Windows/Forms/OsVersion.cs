using System;

namespace System.Windows.Forms
{
	// Token: 0x02000315 RID: 789
	internal static class OsVersion
	{
		// Token: 0x06003226 RID: 12838 RVA: 0x000E1AB8 File Offset: 0x000DFCB8
		private static NativeMethods.NtDll.RTL_OSVERSIONINFOEX InitVersion()
		{
			IntSecurity.UnmanagedCode.Assert();
			NativeMethods.NtDll.RTL_OSVERSIONINFOEX result;
			NativeMethods.NtDll.RtlGetVersion(out result);
			return result;
		}

		// Token: 0x17000BC2 RID: 3010
		// (get) Token: 0x06003227 RID: 12839 RVA: 0x000E1AD8 File Offset: 0x000DFCD8
		public static bool IsWindows11_OrGreater
		{
			get
			{
				return OsVersion.s_versionInfo.dwMajorVersion >= 10U && OsVersion.s_versionInfo.dwBuildNumber >= 22000U;
			}
		}

		// Token: 0x04001E6C RID: 7788
		private static NativeMethods.NtDll.RTL_OSVERSIONINFOEX s_versionInfo = OsVersion.InitVersion();
	}
}
