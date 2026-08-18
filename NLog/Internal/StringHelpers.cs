using System;

namespace NLog.Internal
{
	// Token: 0x020000B3 RID: 179
	public static class StringHelpers
	{
		// Token: 0x06000569 RID: 1385 RVA: 0x0000C373 File Offset: 0x0000A573
		internal static bool IsNullOrWhiteSpace(string value)
		{
			return string.IsNullOrWhiteSpace(value);
		}
	}
}
