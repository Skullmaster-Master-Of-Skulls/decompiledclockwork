using System;
using System.Diagnostics;

namespace System.Data.Entity.SqlServer.Utilities
{
	// Token: 0x0200000A RID: 10
	internal class DebugCheck
	{
		// Token: 0x0600006F RID: 111 RVA: 0x00003931 File Offset: 0x00001B31
		[Conditional("DEBUG")]
		public static void NotNull<T>(T value) where T : class
		{
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003933 File Offset: 0x00001B33
		[Conditional("DEBUG")]
		public static void NotNull<T>(T? value) where T : struct
		{
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003935 File Offset: 0x00001B35
		[Conditional("DEBUG")]
		public static void NotEmpty(string value)
		{
		}
	}
}
