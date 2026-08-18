using System;

namespace System.Web.Http.Tracing
{
	// Token: 0x020000A3 RID: 163
	internal static class TraceLevelHelper
	{
		// Token: 0x060003D9 RID: 985 RVA: 0x0000C04A File Offset: 0x0000A24A
		public static bool IsDefined(TraceLevel traceLevel)
		{
			return traceLevel == TraceLevel.Off || traceLevel == TraceLevel.Debug || traceLevel == TraceLevel.Info || traceLevel == TraceLevel.Warn || traceLevel == TraceLevel.Error || traceLevel == TraceLevel.Fatal;
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000C065 File Offset: 0x0000A265
		public static void Validate(TraceLevel value, string parameterValue)
		{
			if (!TraceLevelHelper.IsDefined(value))
			{
				throw Error.InvalidEnumArgument(parameterValue, (int)value, typeof(TraceLevel));
			}
		}
	}
}
