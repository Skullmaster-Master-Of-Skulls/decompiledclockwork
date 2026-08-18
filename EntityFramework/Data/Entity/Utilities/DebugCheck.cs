using System;
using System.Diagnostics;

namespace System.Data.Entity.Utilities
{
	// Token: 0x02000006 RID: 6
	internal class DebugCheck
	{
		// Token: 0x06000053 RID: 83 RVA: 0x000031F9 File Offset: 0x000013F9
		[Conditional("DEBUG")]
		public static void NotNull<T>(T value) where T : class
		{
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000031FB File Offset: 0x000013FB
		[Conditional("DEBUG")]
		public static void NotNull<T>(T? value) where T : struct
		{
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000031FD File Offset: 0x000013FD
		[Conditional("DEBUG")]
		public static void NotEmpty(string value)
		{
		}
	}
}
