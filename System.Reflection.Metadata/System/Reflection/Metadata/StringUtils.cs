using System;

namespace System.Reflection.Metadata
{
	// Token: 0x0200002E RID: 46
	internal static class StringUtils
	{
		// Token: 0x06000262 RID: 610 RVA: 0x000072FE File Offset: 0x000054FE
		internal static int IgnoreCaseMask(bool ignoreCase)
		{
			if (!ignoreCase)
			{
				return 255;
			}
			return 32;
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000730B File Offset: 0x0000550B
		internal static bool IsEqualAscii(int a, int b, int ignoreCaseMask)
		{
			return a == b || ((a | 32) == (b | 32) && (a | ignoreCaseMask) - 97 <= 25);
		}
	}
}
