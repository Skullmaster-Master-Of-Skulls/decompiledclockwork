using System;
using System.Diagnostics;
using NLog.Config;

namespace NLog.Internal
{
	// Token: 0x020000B0 RID: 176
	internal class StackTraceUsageUtils
	{
		// Token: 0x06000564 RID: 1380 RVA: 0x0000C2E0 File Offset: 0x0000A4E0
		internal static StackTraceUsage Max(StackTraceUsage u1, StackTraceUsage u2)
		{
			return (StackTraceUsage)Math.Max((int)u1, (int)u2);
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0000C2E9 File Offset: 0x0000A4E9
		internal static StackTrace GetWriteStackTrace(Type loggerType)
		{
			return new StackTrace();
		}
	}
}
