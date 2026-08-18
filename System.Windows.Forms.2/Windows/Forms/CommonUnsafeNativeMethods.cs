using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Windows.Forms
{
	// Token: 0x02000115 RID: 277
	[SuppressUnmanagedCodeSecurity]
	internal class CommonUnsafeNativeMethods
	{
		// Token: 0x0600076C RID: 1900
		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		public static extern IntPtr GetProcAddress(HandleRef hModule, string lpProcName);

		// Token: 0x0600076D RID: 1901
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern IntPtr GetModuleHandle(string modName);

		// Token: 0x0600076E RID: 1902
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr LoadLibraryEx(string lpModuleName, IntPtr hFile, uint dwFlags);

		// Token: 0x0600076F RID: 1903
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr LoadLibrary(string libname);

		// Token: 0x06000770 RID: 1904
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool FreeLibrary(HandleRef hModule);

		// Token: 0x06000771 RID: 1905 RVA: 0x000158C4 File Offset: 0x00013AC4
		public static IntPtr LoadLibraryFromSystemPathIfAvailable(string libraryName)
		{
			IntPtr result = IntPtr.Zero;
			IntPtr moduleHandle = CommonUnsafeNativeMethods.GetModuleHandle("kernel32.dll");
			if (moduleHandle != IntPtr.Zero)
			{
				if (CommonUnsafeNativeMethods.GetProcAddress(new HandleRef(null, moduleHandle), "AddDllDirectory") != IntPtr.Zero)
				{
					result = CommonUnsafeNativeMethods.LoadLibraryEx(libraryName, IntPtr.Zero, 2048U);
				}
				else
				{
					result = CommonUnsafeNativeMethods.LoadLibrary(libraryName);
				}
			}
			return result;
		}

		// Token: 0x06000772 RID: 1906
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		internal static extern DpiAwarenessContext GetThreadDpiAwarenessContext();

		// Token: 0x06000773 RID: 1907
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		internal static extern IntPtr GetWindowDpiAwarenessContext(IntPtr hWnd);

		// Token: 0x06000774 RID: 1908
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		internal static extern CommonUnsafeNativeMethods.DPI_AWARENESS GetAwarenessFromDpiAwarenessContext(IntPtr dpiAwarenessContext);

		// Token: 0x06000775 RID: 1909
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		internal static extern DpiAwarenessContext SetThreadDpiAwarenessContext(DpiAwarenessContext dpiContext);

		// Token: 0x06000776 RID: 1910
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool AreDpiAwarenessContextsEqual(DpiAwarenessContext dpiContextA, DpiAwarenessContext dpiContextB);

		// Token: 0x06000777 RID: 1911 RVA: 0x00015927 File Offset: 0x00013B27
		public static bool TryFindDpiAwarenessContextsEqual(DpiAwarenessContext dpiContextA, DpiAwarenessContext dpiContextB)
		{
			return (dpiContextA == DpiAwarenessContext.DPI_AWARENESS_CONTEXT_UNSPECIFIED && dpiContextB == DpiAwarenessContext.DPI_AWARENESS_CONTEXT_UNSPECIFIED) || (ApiHelper.IsApiAvailable("user32.dll", "AreDpiAwarenessContextsEqual") && CommonUnsafeNativeMethods.AreDpiAwarenessContextsEqual(dpiContextA, dpiContextB));
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x0001594B File Offset: 0x00013B4B
		public static DpiAwarenessContext TryGetThreadDpiAwarenessContext()
		{
			if (ApiHelper.IsApiAvailable("user32.dll", "GetThreadDpiAwarenessContext"))
			{
				return CommonUnsafeNativeMethods.GetThreadDpiAwarenessContext();
			}
			return DpiAwarenessContext.DPI_AWARENESS_CONTEXT_UNSPECIFIED;
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x00015965 File Offset: 0x00013B65
		public static DpiAwarenessContext TrySetThreadDpiAwarenessContext(DpiAwarenessContext dpiCOntext)
		{
			if (ApiHelper.IsApiAvailable("user32.dll", "SetThreadDpiAwarenessContext"))
			{
				return CommonUnsafeNativeMethods.SetThreadDpiAwarenessContext(dpiCOntext);
			}
			return DpiAwarenessContext.DPI_AWARENESS_CONTEXT_UNSPECIFIED;
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x00015980 File Offset: 0x00013B80
		internal static DpiAwarenessContext TryGetDpiAwarenessContextForWindow(IntPtr hWnd)
		{
			DpiAwarenessContext result = DpiAwarenessContext.DPI_AWARENESS_CONTEXT_UNSPECIFIED;
			try
			{
				if (ApiHelper.IsApiAvailable("user32.dll", "GetWindowDpiAwarenessContext") && ApiHelper.IsApiAvailable("user32.dll", "GetAwarenessFromDpiAwarenessContext"))
				{
					IntPtr windowDpiAwarenessContext = CommonUnsafeNativeMethods.GetWindowDpiAwarenessContext(hWnd);
					CommonUnsafeNativeMethods.DPI_AWARENESS awarenessFromDpiAwarenessContext = CommonUnsafeNativeMethods.GetAwarenessFromDpiAwarenessContext(windowDpiAwarenessContext);
					result = CommonUnsafeNativeMethods.ConvertToDpiAwarenessContext(awarenessFromDpiAwarenessContext);
				}
			}
			catch
			{
			}
			return result;
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x000159DC File Offset: 0x00013BDC
		private static DpiAwarenessContext ConvertToDpiAwarenessContext(CommonUnsafeNativeMethods.DPI_AWARENESS dpiAwareness)
		{
			switch (dpiAwareness)
			{
			case CommonUnsafeNativeMethods.DPI_AWARENESS.DPI_AWARENESS_UNAWARE:
				return DpiAwarenessContext.DPI_AWARENESS_CONTEXT_UNAWARE;
			case CommonUnsafeNativeMethods.DPI_AWARENESS.DPI_AWARENESS_SYSTEM_AWARE:
				return DpiAwarenessContext.DPI_AWARENESS_CONTEXT_SYSTEM_AWARE;
			case CommonUnsafeNativeMethods.DPI_AWARENESS.DPI_AWARENESS_PER_MONITOR_AWARE:
				return DpiAwarenessContext.DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2;
			default:
				return DpiAwarenessContext.DPI_AWARENESS_CONTEXT_SYSTEM_AWARE;
			}
		}

		// Token: 0x04000509 RID: 1289
		internal const int LOAD_LIBRARY_SEARCH_SYSTEM32 = 2048;

		// Token: 0x020005FE RID: 1534
		internal enum DPI_AWARENESS
		{
			// Token: 0x040038A3 RID: 14499
			DPI_AWARENESS_INVALID = -1,
			// Token: 0x040038A4 RID: 14500
			DPI_AWARENESS_UNAWARE,
			// Token: 0x040038A5 RID: 14501
			DPI_AWARENESS_SYSTEM_AWARE,
			// Token: 0x040038A6 RID: 14502
			DPI_AWARENESS_PER_MONITOR_AWARE
		}
	}
}
