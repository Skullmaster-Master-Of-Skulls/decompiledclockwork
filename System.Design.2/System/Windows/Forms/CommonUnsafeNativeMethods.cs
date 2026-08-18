using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Windows.Forms
{
	// Token: 0x0200028D RID: 653
	[SuppressUnmanagedCodeSecurity]
	internal class CommonUnsafeNativeMethods
	{
		// Token: 0x060018E6 RID: 6374
		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		public static extern IntPtr GetProcAddress(HandleRef hModule, string lpProcName);

		// Token: 0x060018E7 RID: 6375
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern IntPtr GetModuleHandle(string modName);

		// Token: 0x060018E8 RID: 6376
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr LoadLibraryEx(string lpModuleName, IntPtr hFile, uint dwFlags);

		// Token: 0x060018E9 RID: 6377
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr LoadLibrary(string libname);

		// Token: 0x060018EA RID: 6378
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool FreeLibrary(HandleRef hModule);

		// Token: 0x060018EB RID: 6379 RVA: 0x0008BC64 File Offset: 0x00089E64
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

		// Token: 0x060018EC RID: 6380
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		internal static extern DpiAwarenessContext GetThreadDpiAwarenessContext();

		// Token: 0x060018ED RID: 6381
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		internal static extern IntPtr GetWindowDpiAwarenessContext(IntPtr hWnd);

		// Token: 0x060018EE RID: 6382
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		internal static extern CommonUnsafeNativeMethods.DPI_AWARENESS GetAwarenessFromDpiAwarenessContext(IntPtr dpiAwarenessContext);

		// Token: 0x060018EF RID: 6383
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		internal static extern DpiAwarenessContext SetThreadDpiAwarenessContext(DpiAwarenessContext dpiContext);

		// Token: 0x060018F0 RID: 6384
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool AreDpiAwarenessContextsEqual(DpiAwarenessContext dpiContextA, DpiAwarenessContext dpiContextB);

		// Token: 0x060018F1 RID: 6385 RVA: 0x0008BCC7 File Offset: 0x00089EC7
		public static bool TryFindDpiAwarenessContextsEqual(DpiAwarenessContext dpiContextA, DpiAwarenessContext dpiContextB)
		{
			return (dpiContextA == DpiAwarenessContext.DPI_AWARENESS_CONTEXT_UNSPECIFIED && dpiContextB == DpiAwarenessContext.DPI_AWARENESS_CONTEXT_UNSPECIFIED) || (ApiHelper.IsApiAvailable("user32.dll", "AreDpiAwarenessContextsEqual") && CommonUnsafeNativeMethods.AreDpiAwarenessContextsEqual(dpiContextA, dpiContextB));
		}

		// Token: 0x060018F2 RID: 6386 RVA: 0x0008BCEB File Offset: 0x00089EEB
		public static DpiAwarenessContext TryGetThreadDpiAwarenessContext()
		{
			if (ApiHelper.IsApiAvailable("user32.dll", "GetThreadDpiAwarenessContext"))
			{
				return CommonUnsafeNativeMethods.GetThreadDpiAwarenessContext();
			}
			return DpiAwarenessContext.DPI_AWARENESS_CONTEXT_UNSPECIFIED;
		}

		// Token: 0x060018F3 RID: 6387 RVA: 0x0008BD05 File Offset: 0x00089F05
		public static DpiAwarenessContext TrySetThreadDpiAwarenessContext(DpiAwarenessContext dpiCOntext)
		{
			if (ApiHelper.IsApiAvailable("user32.dll", "SetThreadDpiAwarenessContext"))
			{
				return CommonUnsafeNativeMethods.SetThreadDpiAwarenessContext(dpiCOntext);
			}
			return DpiAwarenessContext.DPI_AWARENESS_CONTEXT_UNSPECIFIED;
		}

		// Token: 0x060018F4 RID: 6388 RVA: 0x0008BD20 File Offset: 0x00089F20
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

		// Token: 0x060018F5 RID: 6389 RVA: 0x0008BD7C File Offset: 0x00089F7C
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

		// Token: 0x04001550 RID: 5456
		internal const int LOAD_LIBRARY_SEARCH_SYSTEM32 = 2048;

		// Token: 0x0200051B RID: 1307
		internal enum DPI_AWARENESS
		{
			// Token: 0x0400208B RID: 8331
			DPI_AWARENESS_INVALID = -1,
			// Token: 0x0400208C RID: 8332
			DPI_AWARENESS_UNAWARE,
			// Token: 0x0400208D RID: 8333
			DPI_AWARENESS_SYSTEM_AWARE,
			// Token: 0x0400208E RID: 8334
			DPI_AWARENESS_PER_MONITOR_AWARE
		}
	}
}
