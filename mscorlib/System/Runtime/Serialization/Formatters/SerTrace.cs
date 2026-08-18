using System;
using System.Diagnostics;

namespace System.Runtime.Serialization.Formatters
{
	// Token: 0x020007BA RID: 1978
	internal static class SerTrace
	{
		// Token: 0x0600467E RID: 18046 RVA: 0x000F0845 File Offset: 0x000EF845
		[Conditional("_LOGGING")]
		internal static void InfoLog(params object[] messages)
		{
		}

		// Token: 0x0600467F RID: 18047 RVA: 0x000F0847 File Offset: 0x000EF847
		[Conditional("SER_LOGGING")]
		internal static void Log(params object[] messages)
		{
			if (!(messages[0] is string))
			{
				messages[0] = messages[0].GetType().Name + " ";
				return;
			}
			messages[0] = messages[0] + " ";
		}
	}
}
