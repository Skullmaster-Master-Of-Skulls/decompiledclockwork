using System;
using System.Diagnostics;

namespace System.Configuration
{
	// Token: 0x02000050 RID: 80
	internal static class Debug
	{
		// Token: 0x0600033B RID: 827 RVA: 0x00005E74 File Offset: 0x00004074
		[Conditional("DBG")]
		internal static void Trace(string tagName, string message)
		{
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00005E74 File Offset: 0x00004074
		[Conditional("DBG")]
		internal static void Trace(string tagName, string message, bool includePrefix)
		{
		}

		// Token: 0x0600033D RID: 829 RVA: 0x00005E74 File Offset: 0x00004074
		[Conditional("DBG")]
		internal static void Trace(string tagName, string message, Exception e)
		{
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00005E74 File Offset: 0x00004074
		[Conditional("DBG")]
		internal static void Trace(string tagName, Exception e)
		{
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00005E74 File Offset: 0x00004074
		[Conditional("DBG")]
		internal static void Trace(string tagName, string message, Exception e, bool includePrefix)
		{
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00005E74 File Offset: 0x00004074
		[Conditional("DBG")]
		internal static void Assert(bool assertion, string message)
		{
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00005E74 File Offset: 0x00004074
		[Conditional("DBG")]
		internal static void Assert(bool assertion)
		{
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00005E74 File Offset: 0x00004074
		[Conditional("DBG")]
		internal static void Fail(string message)
		{
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00008751 File Offset: 0x00006951
		internal static bool IsTagEnabled(string tagName)
		{
			return false;
		}

		// Token: 0x06000344 RID: 836 RVA: 0x00008751 File Offset: 0x00006951
		internal static bool IsTagPresent(string tagName)
		{
			return false;
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00005E74 File Offset: 0x00004074
		[Conditional("DBG")]
		internal static void Break()
		{
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00005E74 File Offset: 0x00004074
		[Conditional("DBG")]
		internal static void AlwaysValidate(string tagName)
		{
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00005E74 File Offset: 0x00004074
		[Conditional("DBG")]
		internal static void CheckValid(bool assertion, string message)
		{
		}

		// Token: 0x06000348 RID: 840 RVA: 0x00005E74 File Offset: 0x00004074
		[Conditional("DBG")]
		internal static void Validate(object obj)
		{
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00005E74 File Offset: 0x00004074
		[Conditional("DBG")]
		internal static void Validate(string tagName, object obj)
		{
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00005E74 File Offset: 0x00004074
		[Conditional("DBG")]
		internal static void Dump(string tagName, object obj)
		{
		}

		// Token: 0x0400024C RID: 588
		internal const string TAG_INTERNAL = "Internal";

		// Token: 0x0400024D RID: 589
		internal const string TAG_EXTERNAL = "External";

		// Token: 0x0400024E RID: 590
		internal const string TAG_ALL = "*";

		// Token: 0x0400024F RID: 591
		internal const string DATE_FORMAT = "yyyy/MM/dd HH:mm:ss.ffff";

		// Token: 0x04000250 RID: 592
		internal const string TIME_FORMAT = "HH:mm:ss:ffff";
	}
}
