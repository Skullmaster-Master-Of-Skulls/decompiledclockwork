using System;
using System.Diagnostics;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020007D5 RID: 2005
	internal static class BinaryUtil
	{
		// Token: 0x06004729 RID: 18217 RVA: 0x000F3AA5 File Offset: 0x000F2AA5
		[Conditional("_LOGGING")]
		public static void NVTraceI(string name, string value)
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x0600472A RID: 18218 RVA: 0x000F3AB2 File Offset: 0x000F2AB2
		[Conditional("_LOGGING")]
		public static void NVTraceI(string name, object value)
		{
			BCLDebug.CheckEnabled("BINARY");
		}
	}
}
