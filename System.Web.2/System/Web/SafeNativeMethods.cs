using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Web
{
	// Token: 0x020000F1 RID: 241
	[ComVisible(false)]
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeNativeMethods
	{
		// Token: 0x06000E79 RID: 3705 RVA: 0x000030B5 File Offset: 0x000012B5
		private SafeNativeMethods()
		{
		}

		// Token: 0x06000E7A RID: 3706
		[DllImport("kernel32.dll")]
		internal static extern int GetCurrentProcessId();

		// Token: 0x06000E7B RID: 3707
		[DllImport("kernel32.dll")]
		internal static extern int GetCurrentThreadId();

		// Token: 0x06000E7C RID: 3708
		[DllImport("kernel32.dll")]
		internal static extern bool QueryPerformanceCounter([In] [Out] ref long lpPerformanceCount);

		// Token: 0x06000E7D RID: 3709
		[DllImport("kernel32.dll")]
		internal static extern bool QueryPerformanceFrequency([In] [Out] ref long lpFrequency);
	}
}
