using System;

namespace System.Web.Http.Tracing
{
	// Token: 0x020000A4 RID: 164
	internal static class TraceKindHelper
	{
		// Token: 0x060003DB RID: 987 RVA: 0x0000C081 File Offset: 0x0000A281
		public static bool IsDefined(TraceKind traceKind)
		{
			return traceKind == TraceKind.Trace || traceKind == TraceKind.Begin || traceKind == TraceKind.End;
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0000C090 File Offset: 0x0000A290
		public static void Validate(TraceKind value, string parameterValue)
		{
			if (!TraceKindHelper.IsDefined(value))
			{
				throw Error.InvalidEnumArgument(parameterValue, (int)value, typeof(TraceKind));
			}
		}
	}
}
