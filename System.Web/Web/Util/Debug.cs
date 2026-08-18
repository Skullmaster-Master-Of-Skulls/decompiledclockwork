using System;
using System.Diagnostics;

namespace System.Web.Util
{
	// Token: 0x0200075C RID: 1884
	internal static class Debug
	{
		// Token: 0x06005BB7 RID: 23479 RVA: 0x001706A3 File Offset: 0x0016F6A3
		[Conditional("DBG")]
		internal static void Trace(string tagName, string message)
		{
		}

		// Token: 0x06005BB8 RID: 23480 RVA: 0x001706A5 File Offset: 0x0016F6A5
		[Conditional("DBG")]
		internal static void Trace(string tagName, string message, bool includePrefix)
		{
		}

		// Token: 0x06005BB9 RID: 23481 RVA: 0x001706A7 File Offset: 0x0016F6A7
		[Conditional("DBG")]
		internal static void Trace(string tagName, string message, Exception e)
		{
		}

		// Token: 0x06005BBA RID: 23482 RVA: 0x001706A9 File Offset: 0x0016F6A9
		[Conditional("DBG")]
		internal static void Trace(string tagName, Exception e)
		{
		}

		// Token: 0x06005BBB RID: 23483 RVA: 0x001706AB File Offset: 0x0016F6AB
		[Conditional("DBG")]
		internal static void Trace(string tagName, string message, Exception e, bool includePrefix)
		{
		}

		// Token: 0x06005BBC RID: 23484 RVA: 0x001706AD File Offset: 0x0016F6AD
		[Conditional("DBG")]
		public static void TraceException(string tagName, Exception e)
		{
		}

		// Token: 0x06005BBD RID: 23485 RVA: 0x001706AF File Offset: 0x0016F6AF
		[Conditional("DBG")]
		internal static void Assert(bool assertion, string message)
		{
		}

		// Token: 0x06005BBE RID: 23486 RVA: 0x001706B1 File Offset: 0x0016F6B1
		[Conditional("DBG")]
		internal static void Assert(bool assertion)
		{
		}

		// Token: 0x06005BBF RID: 23487 RVA: 0x001706B3 File Offset: 0x0016F6B3
		[Conditional("DBG")]
		internal static void Fail(string message)
		{
		}

		// Token: 0x06005BC0 RID: 23488 RVA: 0x001706B5 File Offset: 0x0016F6B5
		internal static bool IsTagEnabled(string tagName)
		{
			return false;
		}

		// Token: 0x06005BC1 RID: 23489 RVA: 0x001706B8 File Offset: 0x0016F6B8
		internal static bool IsTagPresent(string tagName)
		{
			return false;
		}

		// Token: 0x06005BC2 RID: 23490 RVA: 0x001706BB File Offset: 0x0016F6BB
		[Conditional("DBG")]
		internal static void Break()
		{
		}

		// Token: 0x06005BC3 RID: 23491 RVA: 0x001706BD File Offset: 0x0016F6BD
		[Conditional("DBG")]
		internal static void AlwaysValidate(string tagName)
		{
		}

		// Token: 0x06005BC4 RID: 23492 RVA: 0x001706BF File Offset: 0x0016F6BF
		[Conditional("DBG")]
		internal static void CheckValid(bool assertion, string message)
		{
		}

		// Token: 0x06005BC5 RID: 23493 RVA: 0x001706C1 File Offset: 0x0016F6C1
		[Conditional("DBG")]
		internal static void Validate(object obj)
		{
		}

		// Token: 0x06005BC6 RID: 23494 RVA: 0x001706C3 File Offset: 0x0016F6C3
		[Conditional("DBG")]
		internal static void Validate(string tagName, object obj)
		{
		}

		// Token: 0x06005BC7 RID: 23495 RVA: 0x001706C5 File Offset: 0x0016F6C5
		[Conditional("DBG")]
		internal static void Dump(string tagName, object obj)
		{
		}

		// Token: 0x06005BC8 RID: 23496 RVA: 0x001706C7 File Offset: 0x0016F6C7
		internal static string FormatUtcDate(DateTime utcTime)
		{
			return string.Empty;
		}

		// Token: 0x06005BC9 RID: 23497 RVA: 0x001706CE File Offset: 0x0016F6CE
		internal static string FormatLocalDate(DateTime localTime)
		{
			return string.Empty;
		}

		// Token: 0x0400311B RID: 12571
		internal const string TAG_INTERNAL = "Internal";

		// Token: 0x0400311C RID: 12572
		internal const string TAG_EXTERNAL = "External";

		// Token: 0x0400311D RID: 12573
		internal const string TAG_ALL = "*";

		// Token: 0x0400311E RID: 12574
		internal const string DATE_FORMAT = "yyyy/MM/dd HH:mm:ss.ffff";

		// Token: 0x0400311F RID: 12575
		internal const string TIME_FORMAT = "HH:mm:ss:ffff";
	}
}
