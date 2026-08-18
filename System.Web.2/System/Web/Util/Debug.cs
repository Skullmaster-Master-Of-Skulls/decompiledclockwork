using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Web.Util
{
	// Token: 0x020001F3 RID: 499
	internal static class Debug
	{
		// Token: 0x060018C4 RID: 6340 RVA: 0x00006164 File Offset: 0x00004364
		[Conditional("DBG")]
		internal static void Trace(string tagName, string message)
		{
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x00006164 File Offset: 0x00004364
		[Conditional("DBG")]
		internal static void Trace(string tagName, string message, bool includePrefix)
		{
		}

		// Token: 0x060018C6 RID: 6342 RVA: 0x00006164 File Offset: 0x00004364
		[Conditional("DBG")]
		internal static void Trace(string tagName, string message, Exception e)
		{
		}

		// Token: 0x060018C7 RID: 6343 RVA: 0x00006164 File Offset: 0x00004364
		[Conditional("DBG")]
		internal static void Trace(string tagName, Exception e)
		{
		}

		// Token: 0x060018C8 RID: 6344 RVA: 0x00006164 File Offset: 0x00004364
		[Conditional("DBG")]
		internal static void Trace(string tagName, string message, Exception e, bool includePrefix)
		{
		}

		// Token: 0x060018C9 RID: 6345 RVA: 0x00006164 File Offset: 0x00004364
		[Conditional("DBG")]
		public static void TraceException(string tagName, Exception e)
		{
		}

		// Token: 0x060018CA RID: 6346 RVA: 0x00006164 File Offset: 0x00004364
		[Conditional("DBG")]
		internal static void Assert(bool assertion, string message)
		{
		}

		// Token: 0x060018CB RID: 6347 RVA: 0x00006164 File Offset: 0x00004364
		[Conditional("DBG")]
		internal static void Assert(bool assertion)
		{
		}

		// Token: 0x060018CC RID: 6348 RVA: 0x00006164 File Offset: 0x00004364
		[Conditional("DBG")]
		internal static void Fail(string message)
		{
		}

		// Token: 0x060018CD RID: 6349 RVA: 0x00007722 File Offset: 0x00005922
		internal static bool IsTagEnabled(string tagName)
		{
			return false;
		}

		// Token: 0x060018CE RID: 6350 RVA: 0x00007722 File Offset: 0x00005922
		internal static bool IsTagPresent(string tagName)
		{
			return false;
		}

		// Token: 0x060018CF RID: 6351 RVA: 0x0004CC6F File Offset: 0x0004AE6F
		internal static bool IsDebuggerPresent()
		{
			return Debug.NativeMethods.IsDebuggerPresent() || Debugger.IsAttached;
		}

		// Token: 0x060018D0 RID: 6352 RVA: 0x00006164 File Offset: 0x00004364
		[Conditional("DBG")]
		internal static void Break()
		{
		}

		// Token: 0x060018D1 RID: 6353 RVA: 0x00006164 File Offset: 0x00004364
		[Conditional("DBG")]
		internal static void AlwaysValidate(string tagName)
		{
		}

		// Token: 0x060018D2 RID: 6354 RVA: 0x00006164 File Offset: 0x00004364
		[Conditional("DBG")]
		internal static void CheckValid(bool assertion, string message)
		{
		}

		// Token: 0x060018D3 RID: 6355 RVA: 0x00006164 File Offset: 0x00004364
		[Conditional("DBG")]
		internal static void Validate(object obj)
		{
		}

		// Token: 0x060018D4 RID: 6356 RVA: 0x00006164 File Offset: 0x00004364
		[Conditional("DBG")]
		internal static void ValidateArrayBounds<T>(T[] array, int offset, int count)
		{
		}

		// Token: 0x060018D5 RID: 6357 RVA: 0x00006164 File Offset: 0x00004364
		[Conditional("DBG")]
		internal static void Validate(string tagName, object obj)
		{
		}

		// Token: 0x060018D6 RID: 6358 RVA: 0x00006164 File Offset: 0x00004364
		[Conditional("DBG")]
		internal static void Dump(string tagName, object obj)
		{
		}

		// Token: 0x060018D7 RID: 6359 RVA: 0x00028752 File Offset: 0x00026952
		internal static string FormatUtcDate(DateTime utcTime)
		{
			return string.Empty;
		}

		// Token: 0x060018D8 RID: 6360 RVA: 0x00028752 File Offset: 0x00026952
		internal static string FormatLocalDate(DateTime localTime)
		{
			return string.Empty;
		}

		// Token: 0x0400178E RID: 6030
		internal const string TAG_INTERNAL = "Internal";

		// Token: 0x0400178F RID: 6031
		internal const string TAG_EXTERNAL = "External";

		// Token: 0x04001790 RID: 6032
		internal const string TAG_ALL = "*";

		// Token: 0x04001791 RID: 6033
		internal const string DATE_FORMAT = "yyyy/MM/dd HH:mm:ss.ffff";

		// Token: 0x04001792 RID: 6034
		internal const string TIME_FORMAT = "HH:mm:ss:ffff";

		// Token: 0x02000947 RID: 2375
		[SuppressUnmanagedCodeSecurity]
		private static class NativeMethods
		{
			// Token: 0x06006986 RID: 27014
			[DllImport("kernel32.dll")]
			internal static extern bool IsDebuggerPresent();
		}
	}
}
